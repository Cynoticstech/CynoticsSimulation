using AkshanshKanojia.Inputs.Mobile;
using Simulations.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Simulations
{
    public class SimulationSetupManager : MobileInputs
    {
        #region Public Fields
        public enum SimulationTypes
        {
            MitosisMeiosis, AmoebaHydra, Hibiscus, ReproductiveSystem, CockroachEarthworm, FishPigeon,
            Microbes, BioFertilizer, AceticAcid, Respiration, MeltingIce,CoolingWater,CL_BR,TypeOfReaction,
            ClassifyReaction,OxidationAddition,MagneticField,HopesApparatus,Refraction,
            LightRay,FocalLength,RactivityOfMetal
        }
        #endregion

        #region Serialized Fields
        [SerializeField] SimulationFlowSCO[] AvailableSimulations;
        [SerializeField] SimulationTypes CurtType;
        [SerializeField] bool useDebugSim = false;
        //stores data about simulations present in scene
        [System.Serializable]
        struct SimulationDataHolder
        {
            public GameObject SimulationObj;
            public GameObject[] SimulationStepObjects;
            public TMP_InputField[] InputFields;
            public SimulationTypes SimulationType;
        }
        [SerializeField] SimulationDataHolder[] simulationsAvailable;
        #endregion

        #region Private Fields
        SimulationDataHolder activeSimulation;
        int curtStepIndex = 0;
        bool isSimulating = false;
        GameObject activePopup;
        List<string> answerHolder;

        PopupManager popupMang;
        SimulationFlowSCO activeFlow;
        UI_Manager uiMang;

        #endregion

        #region Builtin Methods
        private void Awake()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        public override void Start()
        {
            base.Start();

            answerHolder = new List<string>();
            uiMang = FindObjectOfType<UI_Manager>();
            popupMang = FindObjectOfType<PopupManager>();
            SetLevel(useDebugSim ? CurtType : SceneChangeScript.optionselect.selectsim);
        }
        #endregion

        #region private methods
        void ActivateSimulation()
        {
            isSimulating = true;
            CheckSimulationStatus();
        }

        /// <summary>
        /// sets active flow for current simulation
        /// </summary>
        /// <param name="_type"></param>
        void GetActiveFlow(SimulationTypes _type)
        {
            foreach (var v in AvailableSimulations)
            {
                if (v.ProjectName == _type)
                {
                    activeFlow = v;
                    return;
                }
            }
        }
        private void InputHandler()
        {
            switch (activeSimulation.SimulationType)
            {
                case SimulationTypes.MitosisMeiosis:
                case SimulationTypes.AmoebaHydra:
                case SimulationTypes.CockroachEarthworm:
                case SimulationTypes.FishPigeon:
                    switch (curtStepIndex)//steps in which tap needs to be detected
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 5:
                        case 7:
                        case 9:
                            CheckSimulationStatus();
                            break;
                        default:
                            break;
                    }
                    break;//mitosis end

                case SimulationTypes.Hibiscus:
                case SimulationTypes.ReproductiveSystem:
                    switch (curtStepIndex)//steps in which tap needs to be detected
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 5:
                            CheckSimulationStatus();
                            break;
                        default:
                            break;
                    }
                    break;//Repro
                case SimulationTypes.Microbes:
                    switch (curtStepIndex)//steps in which tap needs to be detected
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            CheckSimulationStatus();
                            break;
                        default:
                            break;
                    }
                    break;//Microbes
                case SimulationTypes.BioFertilizer:
                    switch (curtStepIndex)//steps in which tap needs to be detected
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 5:
                            CheckSimulationStatus();
                            break;
                        default:
                            break;
                    }
                    break;//bio fert
                /*case SimulationTypes.AceticAcid:
                    switch (curtStepIndex)//steps in which tap needs to be detected
                    {
                        case 1:
                            CheckSimulationStatus();
                            break;
                        default:
                            break;
                    }
                    break;*/
                /*case SimulationTypes.Respiration:
                    switch (curtStepIndex)//steps in which tap needs to be detected
                    {
                        case 1:
                            CheckSimulationStatus();
                            break;
                        default:
                            break;
                    }
                    break;*/

                default:
                    break;
            }
        }

        //manages step by step progression for different simulations
        #region Simulation steps 
        void MitosisManager()
        {
            switch (curtStepIndex)
            {
                case 0:
                    popupMang.SetActivePopup(PopupManager.PopupTypes.CenterFill);
                    activeSimulation.SimulationObj.SetActive(true);//activates the simulation holder object
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[0], true);
                    break;
                case 1:
                    DisableSteps();
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[0].SetActive(true);
                    break;
                case 2:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[1], true);
                    break;
                case 3:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[1].SetActive(true);
                    break;
                case 4:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[2], true);
                    //collect all data of first stage
                    for (int i = 0; i < 7; i++)//7 steps in first stage
                    {
                        answerHolder.Add(activeSimulation.InputFields[i].text);
                    }
                    break;
                case 5:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[2].SetActive(true);
                    break;
                case 6:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[3], true);
                    //get data for 2nd stage (meiosis)
                    for (int i = 7; i < 17; i++)//10 steps in first stage
                    {
                        answerHolder.Add(activeSimulation.InputFields[i].text);
                    }
                    break;
                case 7:
                    //fillups
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[3].SetActive(true);
                    break;
                case 8:
                    //show submit options
                    DisableSteps();
                    popupMang.SetActivePopup(PopupManager.PopupTypes.SubmitPopup);
                    activePopup = popupMang.ShowPopup("Select a subbmission option.", true);
                    //get fillup data
                    for (int i = 17; i < 21; i++)//7 steps in first stage
                    {
                        answerHolder.Add(activeSimulation.InputFields[i].text);
                    }
                    break;
                default:
                    print("Reached end of simulation");
                    break;
            }
            curtStepIndex++;
        }

        void AmoebaManager()
        {
            switch (curtStepIndex)
            {
                case 0:
                    popupMang.SetActivePopup(PopupManager.PopupTypes.CenterFill);
                    activeSimulation.SimulationObj.SetActive(true);//activates the simulation holder object
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[0], true);
                    break;
                case 1:
                    DisableSteps();
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[0].SetActive(true);
                    break;
                case 2:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[1], true);
                    break;
                case 3:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[1].SetActive(true);
                    break;
                case 4:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[2], true);
                    //collect all data of stage
                    for (int i = 0; i < 5; i++)//5 steps in stage
                    {
                        answerHolder.Add(activeSimulation.InputFields[i].text);
                    }
                    break;
                case 5:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[2].SetActive(true);
                    break;
                case 6:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[3], true);
                    //get data for 2nd stage amoeba fillups
                    for (int i = 5; i < 9; i++)//4 steps in fillup
                    {
                        answerHolder.Add(activeSimulation.InputFields[i].text);
                    }
                    break;
                case 7:
                    //hydra
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[3].SetActive(true);
                    break;
                case 8:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[4], true);

                    for (int i = 9; i < 21; i++)//12 steps in stage
                    {
                        answerHolder.Add(activeSimulation.InputFields[i].text);
                    }
                    break;
                case 9:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[4].SetActive(true);
                    break;
                case 10:
                    //show submit options
                    DisableSteps();
                    popupMang.SetActivePopup(PopupManager.PopupTypes.SubmitPopup);
                    activePopup = popupMang.ShowPopup("Select a subbmission option.", true);
                    //get fillup data
                    for (int i = 21; i < 26; i++)//4 steps in stage
                    {
                        try
                        {
                            answerHolder.Add(activeSimulation.InputFields[i].text);
                        }
                        catch
                        {
                            print(i);
                        }
                    }
                    break;
                default:
                    print("Reached end of simulation");
                    return;
            }
            curtStepIndex++;
        }
        void HibiscusMang()
        {
            switch (curtStepIndex)
            {
                case 0:
                    popupMang.SetActivePopup(PopupManager.PopupTypes.CenterFill);
                    activeSimulation.SimulationObj.SetActive(true);//activates the simulation holder object
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[0], true);
                    break;
                case 1:
                    DisableSteps();
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[0].SetActive(true);
                    break;
                case 2:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[1], true);
                    break;
                case 3:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[1].SetActive(true);
                    break;
                case 4:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[2], true);
                    //collect all data of stage
                    for (int i = 0; i < 12; i++)//12 steps in stage
                    {
                        answerHolder.Add(activeSimulation.InputFields[i].text);
                    }
                    break;
                case 5:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[2].SetActive(true);
                    break;
                case 6:
                    DisableSteps();
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[3].SetActive(true);
                    break;
                case 7:
                    DisableSteps();
                    popupMang.SetActivePopup(PopupManager.PopupTypes.SubmitPopup);
                    activePopup = popupMang.ShowPopup("Select a subbmission option.", true);

                    for (int i = 12; i < 21; i++)//7+2 steps in stage
                    {
                        answerHolder.Add(activeSimulation.InputFields[i].text);
                    }
                    break;
                default:
                    print("Reached end of simulation");
                    return;
            }
            curtStepIndex++;
        }
        void ReproductiveSystem()
        {
            switch (curtStepIndex)
            {
                case 0:
                    popupMang.SetActivePopup(PopupManager.PopupTypes.CenterFill);
                    activeSimulation.SimulationObj.SetActive(true);//activates the simulation holder object
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[0], true);
                    break;
                case 1:
                    DisableSteps();
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[0].SetActive(true);
                    break;
                case 2:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[1], true);
                    break;
                case 3:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[1].SetActive(true);
                    break;
                case 4:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[2], true);
                    break;
                case 5:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[2].SetActive(true);
                    break;
                case 6:
                    DisableSteps();
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[3].SetActive(true);
                    break;
                case 7:
                    DisableSteps();
                    popupMang.SetActivePopup(PopupManager.PopupTypes.SubmitPopup);
                    activePopup = popupMang.ShowPopup("Select a subbmission option.", true);

                    for (int i = 0; i < 32; i++)//Get All Data
                    {
                        answerHolder.Add(activeSimulation.InputFields[i].text);
                    }
                    break;
                default:
                    print("Reached end of simulation");
                    return;
            }
            curtStepIndex++;
        }
        void CockroachMang()
        {
            switch (curtStepIndex)
            {
                case 0:
                    popupMang.SetActivePopup(PopupManager.PopupTypes.CenterFill);
                    activeSimulation.SimulationObj.SetActive(true);//activates the simulation holder object
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[0], true);
                    break;
                case 1:
                    DisableSteps();
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[0].SetActive(true);
                    break;
                case 2:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[1], true);
                    break;
                case 3:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[1].SetActive(true);
                    break;
                case 4:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[2], true);
                    break;
                case 5:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[2].SetActive(true);
                    break;
                case 6:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[3], true);
                    break;
                case 7:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[3].SetActive(true);
                    break;
                case 8:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[4], true);
                    break;
                case 9:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[4].SetActive(true);
                    break;
                case 10:
                    DisableSteps();
                    popupMang.SetActivePopup(PopupManager.PopupTypes.SubmitPopup);
                    activePopup = popupMang.ShowPopup("Select a subbmission option.", true);

                    for (int i = 0; i < 24; i++)
                    {
                        answerHolder.Add(activeSimulation.InputFields[i].text);
                    }
                    break;
                default:
                    print("Reached end of simulation");
                    return;
            }
            curtStepIndex++;
        }
        void FishMang()
        {
            switch (curtStepIndex)
            {
                case 0:
                    popupMang.SetActivePopup(PopupManager.PopupTypes.CenterFill);
                    activeSimulation.SimulationObj.SetActive(true);//activates the simulation holder object
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[0], true);
                    break;
                case 1:
                    DisableSteps();
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[0].SetActive(true);
                    break;
                case 2:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[1], true);
                    break;
                case 3:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[1].SetActive(true);
                    break;
                case 4:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[2], true);
                    break;
                case 5:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[2].SetActive(true);
                    break;
                case 6:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[3], true);
                    break;
                case 7:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[3].SetActive(true);
                    break;
                case 8:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[4], true);
                    break;
                case 9:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[4].SetActive(true);
                    break;
                case 10:
                    DisableSteps();
                    popupMang.SetActivePopup(PopupManager.PopupTypes.SubmitPopup);
                    activePopup = popupMang.ShowPopup("Select a subbmission option.", true);
                    for (int i = 0; i < 30; i++)
                    {
                        answerHolder.Add(activeSimulation.InputFields[i].text);
                    }
                    break;
                default:
                    print("Reached end of simulation");
                    return;
            }
            curtStepIndex++;
        }
        void MicrobeMang()
        {
            switch (curtStepIndex)
            {
                case 0:
                    popupMang.SetActivePopup(PopupManager.PopupTypes.CenterFill);
                    activeSimulation.SimulationObj.SetActive(true);//activates the simulation holder object
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[0], true);
                    break;
                case 1:
                    DisableSteps();
                    activePopup.SetActive(false);
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[1], true);
                    break;
                case 2:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[0].SetActive(true);
                    break;
                case 3:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[2], true);
                    break;
                case 4:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[1].SetActive(true);
                    break;
                case 5:
                    DisableSteps();
                    popupMang.SetActivePopup(PopupManager.PopupTypes.SubmitPopup);
                    activePopup = popupMang.ShowPopup("Select a subbmission option.", true);
                    for (int i = 0; i < 15; i++)//send it to database
                    {
                        answerHolder.Add(activeSimulation.InputFields[i].text);
                    }
                    break;
                default:
                    print("Reached end of simulation");
                    return;
            }
            curtStepIndex++;
        }
        void BioFertilizerMang()
        {
            switch (curtStepIndex)
            {
                case 0:
                    popupMang.SetActivePopup(PopupManager.PopupTypes.CenterFill);
                    activeSimulation.SimulationObj.SetActive(true);//activates the simulation holder object
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[0], true);
                    break;
                case 1:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[0].SetActive(true);
                    break;
                case 2:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[1], true);
                    break;
                case 3:
                    activePopup.SetActive(false);
                    activeSimulation.SimulationStepObjects[1].SetActive(true);
                    break;
                case 4:
                    DisableSteps();
                    activePopup = popupMang.ShowPopup(activeFlow.PopupSequences[2], true);
                    break;
                case 5:
                    activePopup.SetActive(false);
                    DisableSteps();
                    popupMang.SetActivePopup(PopupManager.PopupTypes.SubmitPopup);
                    activePopup = popupMang.ShowPopup("Select a subbmission option.", true);
                    for (int i = 0; i < 12; i++)
                    {
                        answerHolder.Add(activeSimulation.InputFields[i].text);
                    }
                    break;
                default:
                    print("Reached end of simulation");
                    return;
            }
            curtStepIndex++;
        }
        void AsceticAcidMang()
        {
            switch (curtStepIndex)
            {
                case 0:
                    popupMang.SetActivePopup(PopupManager.PopupTypes.CenterFill);
                    activeSimulation.SimulationObj.SetActive(true);//activates the simulation holder object
                    break;
                case 1:
                    DisableSteps();
                    activeSimulation.SimulationStepObjects[1].SetActive(true);
                    activeSimulation.SimulationStepObjects[2].SetActive(true);
                    break;
                case 2:
                    DisableSteps();
                    activeSimulation.SimulationStepObjects[3].SetActive(true);
                    break;
                case 3:
                    DisableSteps();
                    popupMang.SetActivePopup(PopupManager.PopupTypes.SubmitPopup);
                    activePopup = popupMang.ShowPopup("Select a subbmission option.", true);
                    for (int i = 0; i < 4; i++)
                    {
                        answerHolder.Add(activeSimulation.InputFields[i].text);
                    }
                    break;
                default:
                    print("Reached end of simulation");
                    return;
            }
            curtStepIndex++;
        }
        void RespirationMang()
        {
            switch (curtStepIndex)
            {
                case 0:
                    popupMang.SetActivePopup(PopupManager.PopupTypes.CenterFill);
                    activeSimulation.SimulationObj.SetActive(true);//activates the simulation holder object
                    break;
                case 1:
                    DisableSteps();
                    activeSimulation.SimulationStepObjects[1].SetActive(true);
                    activeSimulation.SimulationStepObjects[2].SetActive(true);
                    break;
                case 2:
                    DisableSteps();
                    activeSimulation.SimulationStepObjects[3].SetActive(true);
                    break;
                case 3:
                    DisableSteps();
                    popupMang.SetActivePopup(PopupManager.PopupTypes.SubmitPopup);
                    activePopup = popupMang.ShowPopup("Select a subbmission option.", true);
                    for (int i = 0; i < 2; i++)
                    {
                        answerHolder.Add(activeSimulation.InputFields[i].text);
                    }
                    break;
                default:
                    print("Reached end of simulation");
                    return;
            }
            curtStepIndex++;
        }
        void IceMang()
        {
            switch (curtStepIndex)
            {
                case 0:
                    popupMang.SetActivePopup(PopupManager.PopupTypes.CenterFill);
                    activeSimulation.SimulationObj.SetActive(true);//activates the simulation holder object
                    DisableSteps();
                    activeSimulation.SimulationStepObjects[0].SetActive(true);
                    break;
                default:
                    print("Reached end of simulation");
                    return;
            }
            curtStepIndex++;
        }

        /// <summary>
        /// Disables all the steps (if visible) of active simulation
        /// </summary>
        private void DisableSteps()
        {
            foreach (var v in activeSimulation.SimulationStepObjects)
            {
                v.SetActive(false);
            }
        }
        #endregion//simulation steps end

        bool IsValidInput()
        {
            switch (activeSimulation.SimulationType)
            {
                case SimulationTypes.MitosisMeiosis:
                    switch (curtStepIndex)
                    {
                        case 4:
                            for (int i = 0; i <= 6; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                        case 6:
                            for (int i = 7; i <= 16; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                        case 8:
                            for (int i = 17; i <= 20; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                    }
                    break;
                case SimulationTypes.AmoebaHydra:
                    switch (curtStepIndex)
                    {
                        case 4:
                            for (int i = 0; i <= 4; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                        case 6:
                            for (int i = 5; i <= 8; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                        case 8:
                            for (int i = 9; i <= 20; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                        case 10:
                            for (int i = 21; i <= 25; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                    }
                    break;
                case SimulationTypes.Hibiscus:
                    switch (curtStepIndex)
                    {
                        case 4:
                            for (int i = 0; i <= 11; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                        case 6:
                            for (int i = 12; i <= 18; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                        case 7:
                            for (int i = 19; i <= 20; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                    }
                    break;
                case SimulationTypes.ReproductiveSystem:
                    switch (curtStepIndex)
                    {
                        case 4:
                            for (int i = 0; i <= 14; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                        case 6:
                            for (int i = 14; i <= 27; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                        case 7:
                            for (int i = 28; i <= 31; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                        case 10:
                            for (int i = 21; i <= 25; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                    }
                    break;
                case SimulationTypes.CockroachEarthworm:
                    switch (curtStepIndex)
                    {
                        case 4:
                            for (int i = 0; i <= 12; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                        case 6:
                            for (int i = 13; i <= 16; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                        case 8:
                            for (int i = 17; i <= 20; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                        case 10:
                            for (int i = 21; i <= 23; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                    }
                    break;
                case SimulationTypes.FishPigeon:
                    switch (curtStepIndex)
                    {
                        case 4:
                            for (int i = 0; i <= 8; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                        case 6:
                            for (int i = 9; i <= 14; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                        case 8:
                            for (int i = 15; i <= 21; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                        case 10:
                            for (int i = 21; i <= 25; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                        case 12:
                            for (int i = 25; i <= 29; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                    }
                    break;
                case SimulationTypes.Microbes:
                    switch (curtStepIndex)
                    {
                        case 5:
                            for (int i = 0; i <= 14; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                    }
                    break;
                case SimulationTypes.BioFertilizer:
                    switch (curtStepIndex)
                    {
                        case 4:
                            for (int i = 0; i <= 11; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                    }
                    break;
                case SimulationTypes.AceticAcid:
                    switch (curtStepIndex)
                    {
                        case 3:
                            for (int i = 0; i <= 3; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                    }
                    break;
                case SimulationTypes.Respiration:
                    switch (curtStepIndex)
                    {
                        case 3:
                            for (int i = 0; i <= 1; i++)//7 steps in first stage
                            {
                                if (activeSimulation.InputFields[i].text == "")
                                {
                                    return false;
                                }
                            }
                            break;
                    }
                    break;
            }
            return true;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Generates result based on input. Called on buttons.
        /// </summary>
        public void GetResults()
        {
            activePopup.SetActive(false);
            uiMang.GenerateOutput(activeSimulation.SimulationType, activeFlow);
        }

        /// <summary>
        /// used to access the answers given by user.
        /// </summary>
        /// <returns>array of string (answers)</returns>
        public string[] GetAnswers()
        {
            return answerHolder.ToArray();
        }
        public void CheckSimulationStatus()
        {
            //if (!IsValidInput())
            //{
            //    print("WORNG INPUT");
            //    popupMang.SetActivePopup(PopupManager.PopupTypes.IncorrectPopup);
            //    popupMang.ShowPopup("All blanks must be filled to proceed further!", true);
            //    popupMang.SetActivePopup(PopupManager.PopupTypes.CenterFill);
            //    //show popup
            //    return;
            //}
            if (!isSimulating)
                return;
            switch (activeSimulation.SimulationType)
            {
                case SimulationTypes.MitosisMeiosis:
                    MitosisManager();
                    break;
                case SimulationTypes.AmoebaHydra:
                    AmoebaManager();
                    break;
                case SimulationTypes.Hibiscus:
                    HibiscusMang();
                    break;
                case SimulationTypes.ReproductiveSystem:
                    ReproductiveSystem();
                    break;
                case SimulationTypes.CockroachEarthworm:
                    CockroachMang();
                    break;
                case SimulationTypes.FishPigeon:
                    FishMang();
                    break;
                case SimulationTypes.Microbes:
                    MicrobeMang();
                    break;
                case SimulationTypes.BioFertilizer:
                    BioFertilizerMang();
                    break;
                case SimulationTypes.AceticAcid:
                    AsceticAcidMang();
                    break;
                case SimulationTypes.Respiration:
                    RespirationMang();
                    break;
                default:
                    IceMang();
                    break;
            }
        }

        /// <summary>
        /// activates the current simulation. Call it when scene is loaded
        /// </summary>
        /// <param name="_type"></param>
        public void SetLevel(SimulationTypes _type)
        {
            foreach (var v in simulationsAvailable)
            {
                if (v.SimulationType == _type)
                {
                    activeSimulation = v;
                    GetActiveFlow(_type);
                    ActivateSimulation();
                    return;
                }
            }
            Debug.LogWarning("No simulations activated since passed type in SetLevel() is not present!");
        }
        #region Inputs
        public override void OnTapped(MobileInputManager.TouchData _data)
        {
            InputHandler();
        }

        public override void OnTapMove(MobileInputManager.TouchData _data)
        {
        }

        public override void OnTapStay(MobileInputManager.TouchData _data)
        {
        }

        public override void OnTapEnd(MobileInputManager.TouchData _data)
        {
        }
        #endregion
        #endregion//public methods end


    }
}
