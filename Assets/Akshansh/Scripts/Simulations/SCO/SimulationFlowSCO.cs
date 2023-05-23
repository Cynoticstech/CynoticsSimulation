using UnityEngine;

namespace Simulations
{
    [CreateAssetMenu(fileName ="NewSimulationFlow",menuName ="Cynotics/Simulation Flow")]
    public class SimulationFlowSCO : ScriptableObject
    {
        public SimulationSetupManager.SimulationTypes ProjectName;
        public string[] PopupSequences,ValidAnswers,FillupsQuestions;
        [System.Serializable]
        public struct AnswerSpriteDataHolder
        {
            public string AnswerName;
            public Sprite[] AnswerSprite;
            public Vector3 SpriteScale, SpriteEulerAngles;
        }
        public AnswerSpriteDataHolder[] AnswerSprites;
        public GameObject AnswerPrefab;
        public GameObject[] LabledAnswers;
    }
}
