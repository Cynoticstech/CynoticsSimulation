using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Simulations.UI
{
    public class UI_Manager : MonoBehaviour
    {
        //holds output objects for specific simulations
        [Serializable]
        struct ResultHolder
        {
            public GameObject ResultObj;
            public GameObject[] ListContentHolder;
            public TMP_Text PercentageTxt;
            public SimulationSetupManager.SimulationTypes SimulationType;
        }
        [SerializeField] ResultHolder[] AvailableResults;
        [SerializeField] GameObject listAnswerPref, fillAnswerPref;

        SimulationSetupManager setupMang;
        int correctAnswers = 0;

        public string[] finalInpShow;
        //public static List<string> userInputTexts = new List<string>();

        private void Start()
        {
            setupMang = FindAnyObjectByType<SimulationSetupManager>();
        }

        //use this to generate Output
        public void GenerateOutput(SimulationSetupManager.SimulationTypes _type, SimulationFlowSCO _flow)
        {
            switch (_type)
            {
                case SimulationSetupManager.SimulationTypes.MitosisMeiosis:
                    GenerateMytosisResult(_type, _flow);
                    break;
                case SimulationSetupManager.SimulationTypes.AmoebaHydra:
                    GenerateAmoebaResult(_type, _flow);
                    break;
                case SimulationSetupManager.SimulationTypes.Hibiscus:
                    GenerateHibiscusResult(_type, _flow);
                    break;
                case SimulationSetupManager.SimulationTypes.ReproductiveSystem:
                    GenerateReproResult(_type, _flow);
                    break;
                case SimulationSetupManager.SimulationTypes.CockroachEarthworm:
                    GenerateCockroachResult(_type, _flow);
                    break;
                case SimulationSetupManager.SimulationTypes.FishPigeon:
                    GenerateFishResult(_type, _flow);
                    break;
                case SimulationSetupManager.SimulationTypes.BioFertilizer:
                    GenerateBioFertResult(_type, _flow);
                    break;
                case SimulationSetupManager.SimulationTypes.AceticAcid:
                    GenerateAceticResult(_type, _flow);
                    break;
                case SimulationSetupManager.SimulationTypes.Respiration:
                    GenerateRespirationResult(_type, _flow);
                    break;
                default:
                    break;
            }
        }

        #region Output Generation Methods
        private void GenerateMytosisResult(SimulationSetupManager.SimulationTypes _type, SimulationFlowSCO _flow)
        {
            var _active = GetActiveResult(_type);
            var _tempResults = new string[_flow.ValidAnswers.Length];
            var _tempInputs = setupMang.GetAnswers();
            int _totalAnswers = _tempResults.Length;
            finalInpShow = setupMang.GetAnswers();
            for (int i = 0; i < _tempResults.Length; i++)
            {
                _tempResults[i] = _flow.ValidAnswers[i];
                _tempResults[i] = _tempResults[i].Replace(" ", "");
                _tempResults[i] = _tempResults[i].ToLower();//convert to lower case for comparison
                _tempInputs[i] = _tempInputs[i].ToLower();
                _tempInputs[i] = _tempInputs[i].Replace(" ", "");
            }
            int _currentStage = 1;
            for (int i = 0; i < 7; i++, _currentStage++)
            {
                foreach (var _img in _flow.AnswerSprites[i].AnswerSprite)
                {
                    var _tempSprite = Instantiate(_flow.AnswerPrefab, _active.ListContentHolder[0].transform).transform;
                    _tempSprite.GetComponentInChildren<Image>().sprite = _img;
                    _tempSprite.transform.GetChild(0).localScale = _flow.AnswerSprites[i].SpriteScale;
                    _tempSprite.transform.GetChild(0).localEulerAngles = _flow.AnswerSprites[i].SpriteEulerAngles;
                }

                //generate sprite before (if needs to be shown)
                var _tempTxt = Instantiate(listAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>();
                if (_tempResults[i] == _tempInputs[i])//if correct answer
                {
                    print(_tempTxt);
                    _tempTxt.text = "Stage " + _currentStage + ": <color=green>" + finalInpShow[i] + "</color>";
                    correctAnswers++;
                }
                else
                {
                    //if incorrect
                    _tempTxt.text = "Stage " + _currentStage + ": <color=red>" + finalInpShow[i] + ", is incorrect" + "</color>";
                    //add correct answer below it
                    Instantiate(listAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>().
                        text = "Stage " + _currentStage + ": <color=green>" + _flow.ValidAnswers[i] + "</color>";
                }
            }
            _currentStage = 1;
            for (int i = 7; i < 17; i++, _currentStage++)
            {
                foreach (var _img in _flow.AnswerSprites[i].AnswerSprite)
                {
                    var _tempSprite = Instantiate(_flow.AnswerPrefab, _active.ListContentHolder[1].transform).transform;
                    _tempSprite.GetComponentInChildren<Image>().sprite = _img;
                    _tempSprite.transform.GetChild(0).localScale = _flow.AnswerSprites[i].SpriteScale;
                    _tempSprite.transform.GetChild(0).localEulerAngles = _flow.AnswerSprites[i].SpriteEulerAngles;
                }

                //sprite gen end
                var _tempTxt = Instantiate(listAnswerPref, _active.ListContentHolder[1].transform).GetComponent<TMP_Text>();
                if (_tempResults[i] == _tempInputs[i])//if correct answer
                {
                    print(_tempTxt);
                    _tempTxt.text = "Stage " + _currentStage + ": <color=green>" + finalInpShow[i] + "</color>";
                    correctAnswers++;

                }
                else
                {
                    //if incorrect
                    _tempTxt.text = "Stage " + _currentStage + ": <color=red>" + finalInpShow[i] + ", is incorrect " + "</color>";
                    //add correct answer below it
                    Instantiate(listAnswerPref, _active.ListContentHolder[1].transform).GetComponent<TMP_Text>().
                        text = "Stage " + _currentStage + ": <color=green>" + _flow.ValidAnswers[i] + "</color>";
                }
            }
            //z is used as fillup index
            for (int i = 17, z = 0; i < 21; i += 2, z += 2)//skipping to gaps at a time
            {
                bool _incorrect = false;//to check which value is incorrect
                var _tempTxt = Instantiate(fillAnswerPref, _active.ListContentHolder[2].transform).GetComponent<TMP_Text>();
                _tempTxt.text = "";
                for (int j = 0; j < 2; j++)//2 blanks in one question
                {
                    if (_tempResults[i + j] == _tempInputs[i + j])//if correct answer
                    {
                        _tempTxt.text += _flow.FillupsQuestions[z + j] + " <color=green>" + _flow.ValidAnswers[i + j] + "</color> " + ((j == 1) ? " cells." : "");
                        correctAnswers++;
                    }
                    else
                    {
                        //if incorrect
                        _tempTxt.text += _flow.FillupsQuestions[z + j] + " <color=red>" + finalInpShow[i + j] + ", is incorrect" + "</color>" + ((j == 1) ? " cells." : "");
                        _incorrect = true;
                    }
                }//first loop end
                if (_incorrect)
                {
                    var _tempTxt2 = Instantiate(fillAnswerPref, _active.ListContentHolder[2].transform).GetComponent<TMP_Text>();
                    _tempTxt2.text = "";
                    for (int j = 0; j < 2; j++)//2 blanks in one question
                    {
                        _tempTxt2.text += _flow.FillupsQuestions[z + j] + " <color=green>" + _flow.ValidAnswers[i + j] + "</color> " + ((j == 1) ? " cells." : "");
                    }
                }
            }
            _active.PercentageTxt.text = "You answered " + ((float)correctAnswers / _totalAnswers * 100).ToString() + "% answers correctly!";
            print(_totalAnswers + " " + correctAnswers);
            _active.ResultObj.SetActive(true);
            StartCoroutine(RefreshUI(_active));
        }
        private void GenerateAmoebaResult(SimulationSetupManager.SimulationTypes _type, SimulationFlowSCO _flow)
        {
            var _active = GetActiveResult(_type);
            var _tempResults = new string[_flow.ValidAnswers.Length];
            var _tempInputs = setupMang.GetAnswers();
            int _totalAnswers = _tempResults.Length;
            finalInpShow = setupMang.GetAnswers();
            //get all inputs from user
            for (int i = 0; i < _tempResults.Length; i++)
            {
                _tempResults[i] = _flow.ValidAnswers[i];
                _tempResults[i] = _tempResults[i].Replace(" ", "");
                _tempResults[i] = _tempResults[i].ToLower();//convert to lower case for comparison
                _tempInputs[i] = _tempInputs[i].ToLower();
                _tempInputs[i] = _tempInputs[i].Replace(" ", "");
            }
            //genrates output
            int _currentStage = 1;//stage (differs for each step ie reset on step change)shown to user ie <stage 1: a> etc
            for (int i = 0; i < 5; i++, _currentStage++)
            {
                foreach (var _img in _flow.AnswerSprites[i].AnswerSprite)
                {
                    var _tempSprite = Instantiate(_flow.AnswerPrefab, _active.ListContentHolder[0].transform).transform;
                    _tempSprite.GetComponentInChildren<Image>().sprite = _img;
                    _tempSprite.transform.GetChild(0).localScale = _flow.AnswerSprites[i].SpriteScale;
                    _tempSprite.transform.GetChild(0).localEulerAngles = _flow.AnswerSprites[i].SpriteEulerAngles;
                }

                //generate sprite before (if needs to be shown)
                var _tempTxt = Instantiate(listAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>();
                if (_tempResults[i] == _tempInputs[i])//if correct answer
                {
                    print(_tempTxt);
                    _tempTxt.text = "Stage " + _currentStage + ": <color=green>" + finalInpShow[i] + "</color>";
                    correctAnswers++;
                }
                else
                {
                    //if incorrect
                    _tempTxt.text = "Stage " + _currentStage + ": <color=red>" + finalInpShow[i] + ", is incorrect" + "</color>";
                    //add correct answer below it
                    Instantiate(listAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>().
                        text = "Stage " + _currentStage + ": <color=green>" + _flow.ValidAnswers[i] + "</color>";
                }
            }
            //Amoeba Fillups
            //z is fillup index
            for (int i = 5, z = 0; i < 9; i += 1, z += 2)//skipping one gap at a time
            {
                bool _incorrect = false;//to check which value is incorrect
                var _tempTxt = Instantiate(fillAnswerPref, _active.ListContentHolder[1].transform).GetComponent<TMP_Text>();
                _tempTxt.text = "";
                for (int j = 0; j < 1; j++)//1 blanks in one question
                {
                    if (_tempResults[i + j] == _tempInputs[i + j])//if correct answer
                    {
                        _tempTxt.text += _flow.FillupsQuestions[z] + " <color=green>" + finalInpShow[i + j] + "</color> " + _flow.FillupsQuestions[z + j + 1];
                        correctAnswers++;
                    }
                    else
                    {
                        //if incorrect
                        _tempTxt.text += _flow.FillupsQuestions[z] + " <color=red>" + finalInpShow[i + j] + ",is incorrect" + "</color> " + _flow.FillupsQuestions[z + j + 1];
                        _incorrect = true;
                    }
                }//Second loop end
                if (_incorrect)
                {
                    var _tempTxt2 = Instantiate(fillAnswerPref, _active.ListContentHolder[1].transform).GetComponent<TMP_Text>();
                    _tempTxt2.text = "";
                    for (int j = 0; j < 1; j++)//1 blanks in one question
                    {
                        _tempTxt2.text += _flow.FillupsQuestions[z] + " <color=green>" + _flow.ValidAnswers[i + j] + "</color> " + _flow.FillupsQuestions[z + j + 1];
                    }
                }
            }
            //fillups end

            //generate hydra
            var _tempLable = Instantiate(_flow.LabledAnswers[0], _active.ListContentHolder[3].transform).GetComponent<LabledInputHolder>();
            bool _wrong = false;
            for (int i = 9, z = 0; i < 21; i++, z++)
            {
                if (_tempResults[i] == _tempInputs[i])//if correct answer
                {
                    _tempLable.Lables[z].text = "<color=green>" + finalInpShow[i] + "</color>";
                }
                else
                {
                    _tempLable.Lables[z].text = "<color=red>" + finalInpShow[i] + "</color>";
                    _wrong = true;
                }
            }
            if (_wrong)
            {
                _tempLable = Instantiate(_flow.LabledAnswers[0], _active.ListContentHolder[3].transform).GetComponent<LabledInputHolder>();
                for (int i = 9, z = 0; i < 21; i++, z++)
                {
                    _tempLable.Lables[z].text = "<color=green>" + _flow.ValidAnswers[i] + "</color>";
                }
            }

            for (int i = 21, z = 8; i < 23; i++)//skipping 1 gaps at a time
            {
                bool _incorrect = false;//to check which value is incorrect
                var _tempTxt = Instantiate(fillAnswerPref, _active.ListContentHolder[3].transform).GetComponent<TMP_Text>();
                _tempTxt.text = "";
                for (int j = 0; j < 1; j++)//1 blanks in one question except 24th 
                {
                    if (_tempResults[i + j] == _tempInputs[i + j])//if correct answer
                    {
                        _tempTxt.text += _flow.FillupsQuestions[z + j] + " <color=green>" + finalInpShow[i + j] + "</color> " + _flow.FillupsQuestions[z + 1 + j];
                        correctAnswers++;
                    }
                    else
                    {
                        //if incorrect
                        _tempTxt.text += _flow.FillupsQuestions[z + j] + " <color=red>" + finalInpShow[i + j] + ", is incorrect" + "</color>" + _flow.FillupsQuestions[z + 1 + j];
                        _incorrect = true;
                    }
                }//Second loop end
                if (_incorrect)
                {
                    var _tempTxt2 = Instantiate(fillAnswerPref, _active.ListContentHolder[3].transform).GetComponent<TMP_Text>();
                    _tempTxt2.text = "";
                    for (int j = 0; j < 1; j++)//2 blanks in one question
                    {
                        _tempTxt2.text += _flow.FillupsQuestions[z + j] + " <color=green>" + _flow.ValidAnswers[i + j] + "</color> " + _flow.FillupsQuestions[z + 1 + j];
                    }
                }
                z += 2;
            }
            //last 2 
            {
                for (int i = 23, z = 12; i < 25; i += 2)//skipping 2 gaps at a time
                {
                    bool _incorrect = false;//to check which value is incorrect
                    var _tempTxt = Instantiate(fillAnswerPref, _active.ListContentHolder[3].transform).GetComponent<TMP_Text>();
                    _tempTxt.text = "";
                    for (int j = 0; j < 2; j++)//1 blanks in one question except 24th 
                    {
                        if (_tempResults[i + j] == _tempInputs[i + j])//if correct answer
                        {
                            _tempTxt.text += _flow.FillupsQuestions[z + j] + " <color=green>" + finalInpShow[i + j] + "</color> ";
                            correctAnswers++;
                        }
                        else
                        {
                            //if incorrect
                            _tempTxt.text += _flow.FillupsQuestions[z + j] + " <color=red>" + finalInpShow[i + j] + ", is incorrect" + "</color>";
                            _incorrect = true;
                        }
                    }//Second loop end
                    if (_incorrect)
                    {
                        var _tempTxt2 = Instantiate(fillAnswerPref, _active.ListContentHolder[3].transform).GetComponent<TMP_Text>();
                        _tempTxt2.text = "";
                        for (int j = 0; j < 2; j++)//2 blanks in one question
                        {
                            _tempTxt2.text += _flow.FillupsQuestions[z + j] + " <color=green>" + _flow.ValidAnswers[i + j] + "</color> ";
                        }
                    }
                    z += 2;
                }
            }
            //last result
            for (int i = 25, z = 14; i < 26; i++)//skipping 1 gaps at a time
            {
                bool _incorrect = false;//to check which value is incorrect
                var _tempTxt = Instantiate(fillAnswerPref, _active.ListContentHolder[3].transform).GetComponent<TMP_Text>();
                _tempTxt.text = "";
                for (int j = 0; j < 1; j++)//1 blanks in one question except 24th 
                {
                    if (_tempResults[i + j] == _tempInputs[i + j])//if correct answer
                    {
                        _tempTxt.text += _flow.FillupsQuestions[z + j] + " <color=green>" + finalInpShow[i + j] + "</color> " + _flow.FillupsQuestions[z + 1 + j];
                        correctAnswers++;
                    }
                    else
                    {
                        //if incorrect
                        _tempTxt.text += _flow.FillupsQuestions[z + j] + " <color=red>" + finalInpShow[i + j] + ", is incorrect" + "</color>" + _flow.FillupsQuestions[z + 1 + j];
                        _incorrect = true;
                    }
                }//Second loop end
                if (_incorrect)
                {
                    var _tempTxt2 = Instantiate(fillAnswerPref, _active.ListContentHolder[3].transform).GetComponent<TMP_Text>();
                    _tempTxt2.text = "";
                    for (int j = 0; j < 1; j++)//2 blanks in one question
                    {
                        _tempTxt2.text += _flow.FillupsQuestions[z + j] + " <color=green>" + _flow.ValidAnswers[i + j] + "</color> " + _flow.FillupsQuestions[z + 1 + j];
                    }
                }
                z += 2;
            }

            //result calculations
            _active.PercentageTxt.text = "You answered " + ((float)correctAnswers / _totalAnswers * 100).ToString() + "% answers correctly!";
            print(_totalAnswers + " " + correctAnswers);
            _active.ResultObj.SetActive(true);
            StartCoroutine(RefreshUI(_active));
        }
        private void GenerateHibiscusResult(SimulationSetupManager.SimulationTypes _type, SimulationFlowSCO _flow)
        {
            var _active = GetActiveResult(_type);
            var _tempResults = new string[_flow.ValidAnswers.Length];
            var _tempInputs = setupMang.GetAnswers();
            int _totalAnswers = _tempResults.Length;
            finalInpShow = setupMang.GetAnswers();
            //get all inputs from user
            for (int i = 0; i < _tempResults.Length; i++)
            {
                //finalInpShow = _tempInputs[i]
                _tempResults[i] = _flow.ValidAnswers[i];
                _tempResults[i] = _tempResults[i].Replace(" ", "");
                _tempResults[i] = _tempResults[i].ToLower();//convert to lower case for comparison
                _tempInputs[i] = _tempInputs[i].ToLower();
                _tempInputs[i] = _tempInputs[i].Replace(" ", "");
            }
            //genrates output
            {
                var _tempPart = Instantiate(_flow.LabledAnswers[0], _active.ListContentHolder[0].transform);
                int _answerIndex = 0;
                bool _incorrect = false;
                foreach (var v in _tempPart.GetComponent<LabledInputHolder>().Lables)
                {
                    v.interactable = false;
                    v.image.enabled = false;
                    if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                    {
                        //correct answer
                        v.text = "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                        correctAnswers++;
                    }
                    else
                    {
                        //incorrect
                        _incorrect = true;
                        v.text = "<color=red>" + finalInpShow[_answerIndex] + "</color>";
                    }
                    _answerIndex++;
                }
                if (_incorrect)
                {
                    _answerIndex = 0;// refill values from starting phase;
                    _tempPart = Instantiate(_flow.LabledAnswers[0], _active.ListContentHolder[0].transform);
                    foreach (var v in _tempPart.GetComponent<LabledInputHolder>().Lables)
                    {
                        v.interactable = false;
                        v.image.enabled = false;
                        v.text = "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                        _answerIndex++;
                    }
                }
            }//lablled part end

            //------------------------------------------------
            {
                var _tempPart = Instantiate(_flow.LabledAnswers[1], _active.ListContentHolder[0].transform);
                int _answerIndex = 12;
                bool _incorrect = false;
                foreach (var v in _tempPart.GetComponent<LabledInputHolder>().Lables)
                {
                    v.interactable = false;
                    v.image.enabled = false;
                    if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                    {
                        //correct answer
                        v.text = "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                        correctAnswers++;
                    }
                    else
                    {
                        //incorrect
                        _incorrect = true;
                        v.text = "<color=red>" + finalInpShow[_answerIndex] + "</color>";
                    }
                    _answerIndex++;
                }
                if (_incorrect)
                {
                    _answerIndex = 12;// refill values from starting phase;
                    _tempPart = Instantiate(_flow.LabledAnswers[1], _active.ListContentHolder[0].transform);
                    foreach (var v in _tempPart.GetComponent<LabledInputHolder>().Lables)
                    {
                        v.interactable = false;
                        v.image.enabled = false;
                        v.text = "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                        _answerIndex++;
                    }
                }
            }//lablled part end

            //------------------------------------------
            //fillups
            {
                int _answerIndex = 19;
                bool _incorrect = false;
                var _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[1].transform).GetComponent<TMP_Text>();
                _tempFillup.text = "";
                if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                {
                    _tempFillup.text += "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color> " + _flow.FillupsQuestions[0];
                    correctAnswers++;
                }
                else
                {
                    _incorrect = true;
                    _tempFillup.text += "<color=red>" + finalInpShow[_answerIndex] + "</color> " + _flow.FillupsQuestions[0];
                }

                _answerIndex++;
                if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                {
                    _tempFillup.text += " <color=green>" + _flow.ValidAnswers[_answerIndex] + "</color> " + _flow.FillupsQuestions[1];
                    correctAnswers++;
                }
                else
                {
                    _incorrect = true;
                    _tempFillup.text += " <color=red>" + finalInpShow[_answerIndex] + "</color> " + _flow.FillupsQuestions[1];
                }

                if (_incorrect)
                {
                    _answerIndex = 19;
                    _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[1].transform).GetComponent<TMP_Text>();
                    _tempFillup.text = "";
                    _tempFillup.text = "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color> " + _flow.FillupsQuestions[0] +
                        " <color=green>" + _flow.ValidAnswers[_answerIndex + 1] + "</color> " + _flow.FillupsQuestions[1];
                }
            }
            //result calculations
            _active.PercentageTxt.text = "You answered " + ((float)correctAnswers / _totalAnswers * 100).ToString() + "% answers correctly!";
            print(_totalAnswers + " " + correctAnswers);
            _active.ResultObj.SetActive(true);
            StartCoroutine(RefreshUI(_active));
        }
        private void GenerateReproResult(SimulationSetupManager.SimulationTypes _type, SimulationFlowSCO _flow)
        {
            var _active = GetActiveResult(_type);
            var _tempResults = new string[_flow.ValidAnswers.Length];
            var _tempInputs = setupMang.GetAnswers();
            int _totalAnswers = _tempResults.Length;
            finalInpShow = setupMang.GetAnswers();
            //get all inputs from user
            for (int i = 0; i < _tempResults.Length; i++)
            {
                _tempResults[i] = _flow.ValidAnswers[i];
                _tempResults[i] = _tempResults[i].Replace(" ", "");
                _tempResults[i] = _tempResults[i].ToLower();//convert to lower case for comparison
                _tempInputs[i] = _tempInputs[i].ToLower();
                _tempInputs[i] = _tempInputs[i].Replace(" ", "");
            }
            //genrates output
            // label start- male system labele
            {
                var _tempPart = Instantiate(_flow.LabledAnswers[0], _active.ListContentHolder[0].transform);
                int _answerIndex = 0;
                bool _incorrect = false;
                foreach (var v in _tempPart.GetComponent<LabledInputHolder>().Lables)
                {
                    v.interactable = false;
                    v.image.enabled = false;
                    if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                    {
                        //correct answer
                        v.text = "<color=green>" + finalInpShow[_answerIndex] + "</color>";
                        correctAnswers++;
                    }
                    else
                    {
                        //incorrect
                        _incorrect = true;
                        v.text = "<color=red>" + finalInpShow[_answerIndex] + "</color>";
                    }
                    _answerIndex++;
                }
                if (_incorrect)
                {
                    _answerIndex = 0;// refill values from starting phase;
                    _tempPart = Instantiate(_flow.LabledAnswers[0], _active.ListContentHolder[0].transform);
                    foreach (var v in _tempPart.GetComponent<LabledInputHolder>().Lables)
                    {
                        v.interactable = false;
                        v.image.enabled = false;
                        v.text = "<color=green>" + finalInpShow[_answerIndex] + "</color>";
                        _answerIndex++;
                    }
                }
            }//lablled part end - male system labele 

            //------------------------------------------------
            // label start- female system labele
            {
                var _tempPart = Instantiate(_flow.LabledAnswers[1], _active.ListContentHolder[0].transform);
                int _answerIndex = 8;
                bool _incorrect = false;
                foreach (var v in _tempPart.GetComponent<LabledInputHolder>().Lables)
                {
                    v.interactable = false;
                    v.image.enabled = false;
                    if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                    {
                        //correct answer
                        v.text = "<color=green>" + finalInpShow[_answerIndex] + "</color>";
                        correctAnswers++;
                    }
                    else
                    {
                        //incorrect
                        _incorrect = true;
                        v.text = "<color=red>" + finalInpShow[_answerIndex] + "</color>";
                    }
                    _answerIndex++;
                }
                if (_incorrect)
                {
                    _answerIndex = 8;// refill values from starting phase;
                    _tempPart = Instantiate(_flow.LabledAnswers[1], _active.ListContentHolder[0].transform);
                    foreach (var v in _tempPart.GetComponent<LabledInputHolder>().Lables)
                    {
                        v.interactable = false;
                        v.image.enabled = false;
                        v.text = "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                        _answerIndex++;
                    }
                }
            }//lablled part end - female system labele 

            //------------------------------------------
            //fillups - male system fillups
            {
                int _answerIndex = 16;
                bool _incorrect = false;
                //one fillup start
                var _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[1].transform).GetComponent<TMP_Text>();
                _tempFillup.text = "";
                if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                {
                    _tempFillup.text += _flow.FillupsQuestions[0] + "<color=green>" + finalInpShow[_answerIndex] + "</color> ";
                    correctAnswers++;
                }
                else
                {
                    _incorrect = true;
                    _tempFillup.text += _flow.FillupsQuestions[0] + "<color=red>" + finalInpShow[_answerIndex] + "</color> ";
                }
                if (_incorrect)
                {
                    _answerIndex = 16;
                    _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[1].transform).GetComponent<TMP_Text>();
                    _tempFillup.text = "";
                    _tempFillup.text = _flow.FillupsQuestions[0] + " <color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                }
                //one fillup end - male system fillups 

                //one fillup start -- female system fillups
                _answerIndex = 22;
                _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[1].transform).GetComponent<TMP_Text>();
                _tempFillup.text = "";
                if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                {
                    _tempFillup.text += _flow.FillupsQuestions[1] + "<color=green>" + finalInpShow[_answerIndex] + "</color> ";
                    correctAnswers++;
                }
                else
                {
                    _incorrect = true;
                    _tempFillup.text += _flow.FillupsQuestions[1] + "<color=red>" + finalInpShow[_answerIndex] + "</color> ";
                }
                if (_incorrect)
                {
                    _answerIndex = 22;
                    _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[1].transform).GetComponent<TMP_Text>();
                    _tempFillup.text = "";
                    _tempFillup.text = _flow.FillupsQuestions[1] + " <color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                }
                //one fillup end - female system fillups

                //one fillup start - common fillups
                _answerIndex = 29;
                _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[1].transform).GetComponent<TMP_Text>();
                _tempFillup.text = "";
                if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                {
                    _tempFillup.text += "<color=green>" + finalInpShow[_answerIndex] + "</color> " + _flow.FillupsQuestions[2];
                    correctAnswers++;
                }
                else
                {
                    _incorrect = true;
                    _tempFillup.text += "<color=red>" + finalInpShow[_answerIndex] + "</color> " + _flow.FillupsQuestions[2];
                }

                _answerIndex++;
                if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                {
                    _tempFillup.text += " <color=green>" + finalInpShow[_answerIndex] + "</color> " + _flow.FillupsQuestions[3];
                    correctAnswers++;
                }
                else
                {
                    _incorrect = true;
                    _tempFillup.text += " <color=red>" + finalInpShow[_answerIndex] + "</color> " + _flow.FillupsQuestions[3];
                }
                if (_incorrect)
                {
                    _answerIndex = 29;
                    _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[1].transform).GetComponent<TMP_Text>();
                    _tempFillup.text = "";
                    _tempFillup.text = "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color> " + _flow.FillupsQuestions[2] +
                        " <color=green>" + _flow.ValidAnswers[_answerIndex + 1] + "</color> " + _flow.FillupsQuestions[3];
                }
                //one fillup end - common fillups
            }
            //result calculations
            _active.PercentageTxt.text = "You answered " + ((float)correctAnswers / _totalAnswers * 100).ToString() + "% answers correctly!";
            print(_totalAnswers + " " + correctAnswers);
            _active.ResultObj.SetActive(true);
            StartCoroutine(RefreshUI(_active));
        }
        private void GenerateCockroachResult(SimulationSetupManager.SimulationTypes _type, SimulationFlowSCO _flow)
        {
            var _active = GetActiveResult(_type);
            var _tempResults = new string[_flow.ValidAnswers.Length];
            var _tempInputs = setupMang.GetAnswers();
            int _totalAnswers = _tempResults.Length;
            finalInpShow = setupMang.GetAnswers();
            //get all inputs from user
            for (int i = 0; i < _tempResults.Length; i++)
            {
                _tempResults[i] = _flow.ValidAnswers[i];
                _tempResults[i] = _tempResults[i].Replace(" ", "");
                _tempResults[i] = _tempResults[i].ToLower();//convert to lower case for comparison
                _tempInputs[i] = _tempInputs[i].ToLower();
                _tempInputs[i] = _tempInputs[i].Replace(" ", "");

            }
            //genrates output
            for (int i = 0; i < 4; i++)
            {
                var _tempPart = Instantiate(_flow.LabledAnswers[i], _active.ListContentHolder[0].transform);
                int _answerIndex = i switch
                {
                    0 => 0,
                    1 => 13,
                    2 => 17,
                    3 => 21,
                    _ => 0,
                };
                bool _incorrect = false;
                foreach (var v in _tempPart.GetComponent<LabledInputHolder>().Lables)
                {
                    v.interactable = false;
                    v.image.enabled = false;
                    if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                    {
                        //correct answer
                        v.text = "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                        
                        correctAnswers++;
                    }
                    else
                    {
                        //incorrect
                        _incorrect = true;
                        v.text = "<color=red>" + finalInpShow[_answerIndex] + "</color>";
                        Debug.Log("Attempted Ans is incorrect " + _tempInputs[_answerIndex]);
                        Debug.Log("Correct ans in record " + _tempResults[_answerIndex]);
                    }
                    _answerIndex++;
                }
                if (_incorrect)
                {
                    _answerIndex = i switch
                    {
                        0 => 0,
                        1 => 13,
                        2 => 17,
                        3 => 21,
                        _ => 0,
                    };// refill values from starting phase;
                    _tempPart = Instantiate(_flow.LabledAnswers[i], _active.ListContentHolder[0].transform);
                    foreach (var v in _tempPart.GetComponent<LabledInputHolder>().Lables)
                    {
                        v.interactable = false;
                        v.image.enabled = false;
                        v.text = "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                        
                        _answerIndex++;
                    }
                }
            }//lablled part end
            /*for (int i = 0; i < _tempInputs.Length; i++)
            {
                userInputTexts.Add(_tempInputs[i]);
            }*/
            //------------------------------------------------


            //result calculations
            _active.PercentageTxt.text = "You answered " + ((float)correctAnswers / _totalAnswers * 100).ToString() + "% answers correctly!";
            print(_totalAnswers + " " + correctAnswers);
            _active.ResultObj.SetActive(true);
            StartCoroutine(RefreshUI(_active));
        }
        private void GenerateFishResult(SimulationSetupManager.SimulationTypes _type, SimulationFlowSCO _flow)
        {
            var _active = GetActiveResult(_type);
            var _tempResults = new string[_flow.ValidAnswers.Length];
            var _tempInputs = setupMang.GetAnswers();
            int _totalAnswers = _tempResults.Length;
            finalInpShow = setupMang.GetAnswers();
            //get all inputs from user
            for (int i = 0; i < _tempResults.Length; i++)
            {
                _tempResults[i] = _flow.ValidAnswers[i];
                _tempResults[i] = _tempResults[i].Replace(" ", "");
                _tempResults[i] = _tempResults[i].ToLower();//convert to lower case for comparison
                _tempInputs[i] = _tempInputs[i].ToLower();
                _tempInputs[i] = _tempInputs[i].Replace(" ", "");
            }
            //genrates output
            for (int i = 0; i < 2; i++)
            {
                var _tempPart = Instantiate(_flow.LabledAnswers[i], _active.ListContentHolder[0].transform);
                int _answerIndex = i switch
                {
                    0 => 0,
                    1 => 9,
                    _ => 0,
                };
                bool _incorrect = false;
                foreach (var v in _tempPart.GetComponent<LabledInputHolder>().Lables)
                {
                    v.interactable = false;
                    v.image.enabled = false;
                    if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                    {
                        //correct answer
                        v.text = "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                        correctAnswers++;
                    }
                    else
                    {
                        //incorrect
                        _incorrect = true;
                        v.text = "<color=red>" + finalInpShow[_answerIndex] + "</color>";
                    }
                    _answerIndex++;
                }
                if (_incorrect)
                {
                    _answerIndex = i switch
                    {
                        0 => 0,
                        1 => 9,
                        _ => 0,
                    };// refill values from starting phase;
                    _tempPart = Instantiate(_flow.LabledAnswers[i], _active.ListContentHolder[0].transform);
                    foreach (var v in _tempPart.GetComponent<LabledInputHolder>().Lables)
                    {
                        v.interactable = false;
                        v.image.enabled = false;
                        v.text = "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                        _answerIndex++;
                    }
                }
            }//lablled part end

            //------------------------------------------------
            //fill up
            {
                int _answerIndex = 13;
                bool _incorrect = false;
                //one fillup start
                var _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>();
                _tempFillup.text = "";
                if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                {
                    _tempFillup.text += _flow.FillupsQuestions[0] + "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>" + _flow.FillupsQuestions[1];
                    correctAnswers++;
                }
                else
                {
                    _incorrect = true;
                    _tempFillup.text += _flow.FillupsQuestions[0] + "<color=red>" + finalInpShow[_answerIndex] + "</color>" + _flow.FillupsQuestions[1];
                }
                if (_incorrect)
                {
                    _answerIndex = 13;
                    _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>();
                    _tempFillup.text = "";
                    _tempFillup.text = _flow.FillupsQuestions[0] + " <color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>" + _flow.FillupsQuestions[1];
                }
                //one fillup end

                //one fillup start
                _answerIndex = 14;
                _incorrect = false;
                _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>();
                _tempFillup.text = "";
                if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                {
                    _tempFillup.text += _flow.FillupsQuestions[2] + "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                    correctAnswers++;
                }
                else
                {
                    _incorrect = true;
                    _tempFillup.text += _flow.FillupsQuestions[2] + "<color=red>" + finalInpShow[_answerIndex] + "</color>";
                }
                if (_incorrect)
                {
                    _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>();
                    _tempFillup.text = "";
                    _tempFillup.text = _flow.FillupsQuestions[2] + " <color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                }
                //one fillup end
                for (int i = 2; i < 4; i++)
                {
                    var _tempPart = Instantiate(_flow.LabledAnswers[i], _active.ListContentHolder[0].transform);
                    _answerIndex = i switch
                    {
                        2 => 15,
                        3 => 22,
                        _ => 0,
                    };
                    foreach (var v in _tempPart.GetComponent<LabledInputHolder>().Lables)
                    {
                        _incorrect = false;
                        v.interactable = false;
                        v.image.enabled = false;
                        if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                        {
                            //correct answer
                            v.text = "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                            correctAnswers++;
                        }
                        else
                        {
                            //incorrect
                            _incorrect = true;
                            v.text = "<color=red>" + finalInpShow[_answerIndex] + "</color>";
                        }
                        _answerIndex++;
                    }
                    if (_incorrect)
                    {
                        _answerIndex = i switch
                        {
                            2 => 15,
                            3 => 22,
                            _ => 0,
                        };// refill values from starting phase;
                        _tempPart = Instantiate(_flow.LabledAnswers[i], _active.ListContentHolder[0].transform);
                        foreach (var v in _tempPart.GetComponent<LabledInputHolder>().Lables)
                        {
                            v.interactable = false;
                            v.image.enabled = false;
                            v.text = "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                            _answerIndex++;
                        }
                    }
                }//lablled part end

                //fillups
                {

                    //one fillup start
                    _answerIndex = 26;
                    _incorrect = false;
                    _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>();
                    _tempFillup.text = "";
                    if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                    {
                        _tempFillup.text += _flow.FillupsQuestions[3] + "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                        correctAnswers++;
                    }
                    else
                    {
                        _incorrect = true;
                        _tempFillup.text += _flow.FillupsQuestions[3] + "<color=red>" + finalInpShow[_answerIndex] + "</color>";
                    }

                    _answerIndex++;
                    if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                    {
                        _tempFillup.text += _flow.FillupsQuestions[4] + "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color> ";
                        correctAnswers++;
                    }
                    else
                    {
                        _incorrect = true;
                        _tempFillup.text += _flow.FillupsQuestions[4] + "<color=red>" + finalInpShow[_answerIndex] + "</color> ";
                    }

                    _answerIndex++;
                    if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                    {
                        _tempFillup.text += _flow.FillupsQuestions[5] + "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>" + _flow.FillupsQuestions[6];
                        correctAnswers++;
                    }
                    else
                    {
                        _incorrect = true;
                        _tempFillup.text += _flow.FillupsQuestions[5] + "<color=red>" + finalInpShow[_answerIndex] + "</color>" + _flow.FillupsQuestions[6];
                    }

                    if (_incorrect)
                    {
                        _answerIndex = 26;
                        _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>();
                        _tempFillup.text = "";
                        _tempFillup.text = _flow.FillupsQuestions[3] + "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>"
                            + _flow.FillupsQuestions[4] + "<color=green>" + _flow.ValidAnswers[_answerIndex + 1] + "</color>" +
                            _flow.FillupsQuestions[5] + "<color=green>" + _flow.ValidAnswers[_answerIndex + 2] + "</color>" + _flow.FillupsQuestions[6];
                    }
                    //one fillup end

                    //one fillup start
                    _incorrect = false;
                    _answerIndex = 29;
                    _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>();
                    _tempFillup.text = "";
                    if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                    {
                        _tempFillup.text += _flow.FillupsQuestions[7] + "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>" + _flow.FillupsQuestions[8];
                        correctAnswers++;
                    }
                    else
                    {
                        _incorrect = true;
                        _tempFillup.text += _flow.FillupsQuestions[7] + "<color=red>" + finalInpShow[_answerIndex] + "</color>" + _flow.FillupsQuestions[8];
                    }
                    if (_incorrect)
                    {
                        _answerIndex = 29;
                        _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>();
                        _tempFillup.text = "";
                        _tempFillup.text = _flow.FillupsQuestions[7] + " <color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>" + _flow.FillupsQuestions[8];
                    }
                    //one fillup end
                }

            }

            //result calculations
            _active.PercentageTxt.text = "You answered " + ((float)correctAnswers / _totalAnswers * 100).ToString() + "% answers correctly!";
            print(_totalAnswers + " " + correctAnswers);
            _active.ResultObj.SetActive(true);
            StartCoroutine(RefreshUI(_active));
        }
        private void GenerateBioFertResult(SimulationSetupManager.SimulationTypes _type, SimulationFlowSCO _flow)
        {
            var _active = GetActiveResult(_type);
            var _tempResults = new string[_flow.ValidAnswers.Length];
            var _tempInputs = setupMang.GetAnswers();
            int _totalAnswers = _tempResults.Length;
            finalInpShow = setupMang.GetAnswers();
            //get all inputs from user
            for (int i = 0; i < _tempResults.Length; i++)
            {
                _tempResults[i] = _flow.ValidAnswers[i];
                _tempResults[i] = _tempResults[i].Replace(" ", "");
                _tempResults[i] = _tempResults[i].ToLower();//convert to lower case for comparison
                _tempInputs[i] = _tempInputs[i].ToLower();
                _tempInputs[i] = _tempInputs[i].Replace(" ", "");
            }
            //genrates output
            {
                var _tempPart = Instantiate(_flow.LabledAnswers[0], _active.ListContentHolder[0].transform);
                int _answerIndex = 0;
                bool _incorrect = false;
                foreach (var v in _tempPart.GetComponent<LabledInputHolder>().Lables)
                {
                    _incorrect = false;
                    v.interactable = false;
                    v.image.enabled = false;
                    if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                    {
                        //correct answer
                        v.text = "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                        correctAnswers++;
                    }
                    else
                    {
                        //incorrect
                        _incorrect = true;
                        v.text = "<color=red>" + finalInpShow[_answerIndex] + "</color>";
                    }
                    _answerIndex++;
                }
                if (_incorrect)
                {
                    _answerIndex = 0;// refill values from starting phase;
                    _tempPart = Instantiate(_flow.LabledAnswers[0], _active.ListContentHolder[0].transform);
                    foreach (var v in _tempPart.GetComponent<LabledInputHolder>().Lables)
                    {
                        v.interactable = false;
                        v.image.enabled = false;
                        v.text = "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>";
                        _answerIndex++;
                    }
                }
            }//lablled part end

            //result calculations
            _active.PercentageTxt.text = "You answered " + ((float)correctAnswers / _totalAnswers * 100).ToString() + "% answers correctly!";
            print(_totalAnswers + " " + correctAnswers);
            _active.ResultObj.SetActive(true);
            StartCoroutine(RefreshUI(_active));
        }
        private void GenerateAceticResult(SimulationSetupManager.SimulationTypes _type, SimulationFlowSCO _flow)
        {
            var _active = GetActiveResult(_type);
            var _tempResults = new string[_flow.ValidAnswers.Length];
            var _tempInputs = setupMang.GetAnswers();
            int _totalAnswers = _tempResults.Length;
            finalInpShow = setupMang.GetAnswers();
            //get all inputs from user
            for (int i = 0; i < _tempResults.Length; i++)
            {
                _tempResults[i] = _flow.ValidAnswers[i];
                _tempResults[i] = _tempResults[i].Replace(" ", "");
                _tempResults[i] = _tempResults[i].ToLower();//convert to lower case for comparison
                _tempInputs[i] = _tempInputs[i].ToLower();
                _tempInputs[i] = _tempInputs[i].Replace(" ", "");
            }
            //genrates output
            //fill up
            {
                int _answerIndex = 0;
                bool _incorrect = false;
                //one fillup start
                var _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>();
                _tempFillup.text = "";
                if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                {
                    _tempFillup.text += _flow.FillupsQuestions[0] + "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>" + _flow.FillupsQuestions[1];
                    correctAnswers++;
                }
                else
                {
                    _incorrect = true;
                    _tempFillup.text += _flow.FillupsQuestions[0] + "<color=red>" + finalInpShow[_answerIndex] + "</color>" + _flow.FillupsQuestions[1];
                }
                if (_incorrect)
                {
                    _answerIndex = 0;
                    _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>();
                    _tempFillup.text = "";
                    _tempFillup.text = _flow.FillupsQuestions[0] + " <color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>" + _flow.FillupsQuestions[1];
                }
            }
            //one fillup end

            //fill up
            {
                int _answerIndex = 1;
                bool _incorrect = false;
                //one fillup start
                var _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[1].transform).GetComponent<TMP_Text>();
                _tempFillup.text = "";
                if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                {
                    _tempFillup.text += _flow.FillupsQuestions[2] + "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>" + _flow.FillupsQuestions[3];
                    correctAnswers++;
                }
                else
                {
                    _incorrect = true;
                    _tempFillup.text += _flow.FillupsQuestions[2] + "<color=red>" + finalInpShow[_answerIndex] + "</color>" + _flow.FillupsQuestions[3];
                }
                if (_incorrect)
                {
                    _answerIndex = 1;
                    _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[1].transform).GetComponent<TMP_Text>();
                    _tempFillup.text = "";
                    _tempFillup.text = _flow.FillupsQuestions[2] + " <color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>" + _flow.FillupsQuestions[3];
                }
            }
            //one fillup end

            //fill up
            {
                int _answerIndex = 2;
                bool _incorrect = false;
                //one fillup start
                var _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[2].transform).GetComponent<TMP_Text>();
                _tempFillup.text = "";
                if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                {
                    _tempFillup.text += _flow.FillupsQuestions[4] + "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>" + _flow.FillupsQuestions[5];
                    correctAnswers++;
                }
                else
                {
                    _incorrect = true;
                    _tempFillup.text += _flow.FillupsQuestions[4] + "<color=red>" + finalInpShow[_answerIndex] + "</color>" + _flow.FillupsQuestions[5];
                }
                if (_incorrect)
                {
                    _answerIndex = 2;
                    _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[2].transform).GetComponent<TMP_Text>();
                    _tempFillup.text = "";
                    _tempFillup.text = _flow.FillupsQuestions[4] + " <color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>" + _flow.FillupsQuestions[5];
                }
            }
            //one fillup end
            //fill up
            {
                int _answerIndex = 3;
                bool _incorrect = false;
                //one fillup start
                var _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[3].transform).GetComponent<TMP_Text>();
                _tempFillup.text = "";
                if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                {
                    _tempFillup.text +=  "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>" + _flow.FillupsQuestions[6];
                    correctAnswers++;
                }
                else
                {
                    _incorrect = true;
                    _tempFillup.text +="<color=red>" + finalInpShow[_answerIndex] + "</color>" + _flow.FillupsQuestions[6];
                }
                if (_incorrect)
                {
                    _answerIndex = 3;
                    _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[3].transform).GetComponent<TMP_Text>();
                    _tempFillup.text = "";
                    _tempFillup.text = "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>" + _flow.FillupsQuestions[6];
                }
            }
            //one fillup end

            //result calculations
            _active.PercentageTxt.text = "You answered " + ((float)correctAnswers / _totalAnswers * 100).ToString() + "% answers correctly!";
            print(_totalAnswers + " " + correctAnswers);
            _active.ResultObj.SetActive(true);
            StartCoroutine(RefreshUI(_active));
        }
        private void GenerateRespirationResult(SimulationSetupManager.SimulationTypes _type, SimulationFlowSCO _flow)
        {
            var _active = GetActiveResult(_type);
            var _tempResults = new string[_flow.ValidAnswers.Length];
            var _tempInputs = setupMang.GetAnswers();
            int _totalAnswers = _tempResults.Length;
            finalInpShow = setupMang.GetAnswers();
            //get all inputs from user
            for (int i = 0; i < _tempResults.Length; i++)
            {
                _tempResults[i] = _flow.ValidAnswers[i];
                _tempResults[i] = _tempResults[i].Replace(" ", "");
                _tempResults[i] = _tempResults[i].ToLower();//convert to lower case for comparison
                _tempInputs[i] = _tempInputs[i].ToLower();
                _tempInputs[i] = _tempInputs[i].Replace(" ", "");
            }
            //genrates output
            //fill up
            {
                int _answerIndex = 0;
                bool _incorrect = false;
                //one fillup start
                var _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>();
                _tempFillup.text = "";
                if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                {
                    _tempFillup.text += _flow.FillupsQuestions[0] + "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>" + _flow.FillupsQuestions[1];
                    correctAnswers++;
                }
                else
                {
                    _incorrect = true;
                    _tempFillup.text += _flow.FillupsQuestions[0] + "<color=red>" + finalInpShow[_answerIndex] + "</color>" + _flow.FillupsQuestions[1];
                }
                if (_incorrect)
                {
                    _answerIndex = 0;
                    _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>();
                    _tempFillup.text = "";
                    _tempFillup.text = _flow.FillupsQuestions[0] + " <color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>" + _flow.FillupsQuestions[1];
                }
            }
            //one fillup end

            //fill up
            {
                int _answerIndex = 1;
                bool _incorrect = false;
                //one fillup start
                var _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>();
                _tempFillup.text = "";
                if (_tempInputs[_answerIndex] == _tempResults[_answerIndex])
                {
                    _tempFillup.text += "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>" + _flow.FillupsQuestions[1];
                    correctAnswers++;
                }
                else
                {
                    _incorrect = true;
                    _tempFillup.text += "<color=red>" + finalInpShow[_answerIndex] + "</color>" + _flow.FillupsQuestions[1];
                }
                if (_incorrect)
                {
                    _answerIndex = 1;
                    _tempFillup = Instantiate(fillAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>();
                    _tempFillup.text = "";
                    _tempFillup.text = "<color=green>" + _flow.ValidAnswers[_answerIndex] + "</color>" + _flow.FillupsQuestions[1];
                }
            }
            //one fillup end

            //result calculations
            _active.PercentageTxt.text = "You answered " + ((float)correctAnswers / _totalAnswers * 100).ToString() + "% answers correctly!";
            print(_totalAnswers + " " + correctAnswers);
            _active.ResultObj.SetActive(true);
            StartCoroutine(RefreshUI(_active));
        }
        #endregion

        /// <summary>
        /// Forcefully refresh layout group update to arrange elements properly
        /// </summary>
        /// <param name="_temp"></param>
        /// <returns></returns>
        IEnumerator RefreshUI(ResultHolder _temp)
        {
            foreach (var v in _temp.ListContentHolder)
            {
                v.SetActive(false);
            }
            yield return new WaitForSeconds(0.001f);
            foreach (var v in _temp.ListContentHolder)
            {
                v.SetActive(true);
            }
        }

        ResultHolder GetActiveResult(SimulationSetupManager.SimulationTypes _type)
        {
            foreach (var result in AvailableResults)
            {
                if (result.SimulationType == _type)
                {
                    return result;
                }
            }
            return AvailableResults[0];
        }
    }
}
