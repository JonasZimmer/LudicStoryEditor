using UnityEngine;
using UnityEditor;
using LSE.INTERACTION;

namespace LSE.EDITOR
{
    [CustomEditor(typeof(I501_ControllableObject))]
    public class Editor_I501 : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (target.GetType() == typeof(I501_ControllableObject))
            {
                I501_ControllableObject i501 = (I501_ControllableObject)target;
                i501.CType = i501.selectedTypeInInspector;
            }
        }
    }
}