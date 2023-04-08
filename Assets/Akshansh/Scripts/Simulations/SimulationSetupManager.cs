using AkshanshKanojia.Inputs.Mobile;
using Simulations.UI;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace Simulations
{
    public class SimulationSetupManager : MobileInputs
    {
        #region Public Fields
        public enum SimulationTypes { MitosisMeiosis,AmoebaHydra }
        #endregion

        #region Serialized Fields
        [SerializeField] SimulationFlowSCO[] AvailableSimulations;

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
        public override void Start()
        {
            base.Start();
            answerHolder = new List<string>();
            uiMang = FindObjectOfType<UI_Manager>();
            popupMang = FindObjectOfType<PopupManager>();
            SetLevel(SimulationTypes.MitosisMeiosis);
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
                    switch (curtStepIndex)//steps in which tap needs to be detected
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 5:
                        case 7:
                            CheckSimulationStatus();
                            break;
                        default:
                            break;
                    }
                    break;//mitosis end
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
                    for(int i=0;i<7;i++)//7 steps in first stage
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
                    //get data for 2nd stage (hydra)
                    for (int i = 5; i < 17; i++)//12 steps in stage
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
                    for (int i = 17; i < 21; i++)//4 steps in stage
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
