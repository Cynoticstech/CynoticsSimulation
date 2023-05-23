using UnityEditor;
using UnityEngine;

namespace Simulations.Tools
{
    [CustomEditor(typeof(SimSCOManager))]
    public class SimSCOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var _tempMang = (SimSCOManager)target;
            if(GUILayout.Button("Apply Changes"))
            {
                _tempMang.ApplyChanges();
            }
        }
    }
}
