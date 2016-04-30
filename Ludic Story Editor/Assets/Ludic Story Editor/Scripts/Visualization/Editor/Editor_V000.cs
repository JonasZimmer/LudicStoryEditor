using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditorInternal;
using LSE.VISUALIZATION;

namespace LSE.EDITOR
{
    [CustomEditor(typeof(V000_Visual), true)]
    public class Editor_V000 : Editor
    {
        private V000_Visual visual;

        void OnEnable()
        {
            if (visual == null)
                visual = (V000_Visual)target;
        }

        public override void OnInspectorGUI()
        {
            visual._S = (Sprite)EditorGUILayout.ObjectField(
                "Charakter Bild", visual._S, typeof(Sprite), true);/**/
            if (visual._S != null && visual.SortOrdMin != visual.SortOrdMax)
            {
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Wert: " + visual.SortingOrder.ToString());
                    visual.SortingOrder =
                        EditorGUILayout.IntSlider(visual.SortingOrder, visual.SortOrdMin, visual.SortOrdMax);
                }
                EditorGUILayout.EndVertical();
            }
        }
    }
}