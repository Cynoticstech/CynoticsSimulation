using System;
using System.Collections;
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
                    _tempTxt.text = "Stage " + _currentStage + ": <color=green>" + _tempInputs[i] + "</color>";
                    correctAnswers++;
                }
                else
                {
                    //if incorrect
                    _tempTxt.text = "Stage " + _currentStage + ": <color=red>" + _tempInputs[i] + ", is incorrect" + "</color>";
                    //add correct answer below it
                    Instantiate(listAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>().
                        text = "Stage " + _currentStage + ": <color=green>" + _tempResults[i] + "</color>";
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
                    _tempTxt.text = "Stage " + _currentStage + ": <color=green>" + _tempInputs[i] + "</color>";
                    correctAnswers++;

                }
                else
                {
                    //if incorrect
                    _tempTxt.text = "Stage " + _currentStage + ": <color=red>" + _tempInputs[i] + ", is incorrect " + "</color>";
                    //add correct answer below it
                    Instantiate(listAnswerPref, _active.ListContentHolder[1].transform).GetComponent<TMP_Text>().
                        text = "Stage " + _currentStage + ": <color=green>" + _tempResults[i] + "</color>";
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
                        _tempTxt.text += _flow.FillupsQuestions[z + j] + " <color=red>" + _tempInputs[i + j] + ", is incorrect" + "</color>" + ((j == 1) ? " cells." : "");
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
            _active.PercentageTxt.text = "You answered " + (correctAnswers / _totalAnswers * 100).ToString() + "% answers correctly!";
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
                    _tempTxt.text = "Stage " + _currentStage + ": <color=green>" + _tempInputs[i] + "</color>";
                    correctAnswers++;
                }
                else
                {
                    //if incorrect
                    _tempTxt.text = "Stage " + _currentStage + ": <color=red>" + _tempInputs[i] + ", is incorrect" + "</color>";
                    //add correct answer below it
                    Instantiate(listAnswerPref, _active.ListContentHolder[0].transform).GetComponent<TMP_Text>().
                        text = "Stage " + _currentStage + ": <color=green>" + _tempResults[i] + "</color>";
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
                        _tempTxt.text += _flow.FillupsQuestions[z] + " <color=green>" + _flow.ValidAnswers[i + j] + "</color> " + _flow.FillupsQuestions[z + j + 1];
                        correctAnswers++;
                    }
                    else
                    {
                        //if incorrect
                        _tempTxt.text += _flow.FillupsQuestions[z] + " <color=red>" + _tempInputs[i + j] + ",is incorrect" + "</color> " + _flow.FillupsQuestions[z + j + 1];
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
                    _tempLable.Lables[z].text = "<color=green>" + _tempInputs[i] + "</color>";
                }
                else
                {
                    _tempLable.Lables[z].text = "<color=red>" + _tempInputs[i] + "</color>";
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
                        _tempTxt.text += _flow.FillupsQuestions[z + j] + " <color=green>" + _flow.ValidAnswers[i + j] + "</color> " + _flow.FillupsQuestions[z + 1 + j];
                        correctAnswers++;
                    }
                    else
                    {
                        //if incorrect
                        _tempTxt.text += _flow.FillupsQuestions[z + j] + " <color=red>" + _tempInputs[i] + ", is incorrect" + "</color>" + _flow.FillupsQuestions[z + 1 + j];
                        _incorrect = true;
                    }
                }//Second loop end
                if (_incorrect)
                {
                    var _tempTxt2 = Instantiate(fillAnswerPref, _active.ListContentHolder[3].transform).GetComponent<TMP_Text>();
                    _tempTxt2.text = "";
                    for (int j = 0; j < 1; j++)//2 blanks in one question
                    {
                        _tempTxt2.text += _flow.FillupsQuestions[z+j] + " <color=green>" + _flow.ValidAnswers[i + j] + "</color> " + _flow.FillupsQuestions[z + 1+j];
                    }
                }
                z += 2;
            }
            //last 2 
            {
                for (int i = 23, z = 12; i < 25; i+=2)//skipping 2 gaps at a time
                {
                    bool _incorrect = false;//to check which value is incorrect
                    var _tempTxt = Instantiate(fillAnswerPref, _active.ListContentHolder[3].transform).GetComponent<TMP_Text>();
                    _tempTxt.text = "";
                    for (int j = 0; j < 2; j++)//1 blanks in one question except 24th 
                    {
                        if (_tempResults[i + j] == _tempInputs[i + j])//if correct answer
                        {
                            _tempTxt.text += _flow.FillupsQuestions[z + j] + " <color=green>" + _flow.ValidAnswers[i + j] + "</color> ";
                            correctAnswers++;
                        }
                        else
                        {
                            //if incorrect
                            _tempTxt.text += _flow.FillupsQuestions[z + j] + " <color=red>" + _tempInputs[i] + ", is incorrect" + "</color>";
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
                        _tempTxt.text += _flow.FillupsQuestions[z + j] + " <color=green>" + _flow.ValidAnswers[i + j] + "</color> " + _flow.FillupsQuestions[z + 1 + j];
                        correctAnswers++;
                    }
                    else
                    {
                        //if incorrect
                        _tempTxt.text += _flow.FillupsQuestions[z + j] + " <color=red>" + _tempInputs[i] + ", is incorrect" + "</color>" + _flow.FillupsQuestions[z + 1 + j];
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
            _active.PercentageTxt.text = "You answered " + (correctAnswers / _totalAnswers * 100).ToString() + "% answers correctly!";
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
