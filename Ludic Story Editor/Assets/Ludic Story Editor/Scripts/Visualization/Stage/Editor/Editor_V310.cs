using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditorInternal;
using LSE.VISUALIZATION;

namespace LSE.EDITOR
{
    [CustomEditor(typeof(V310_Background))]
    public class Editor_V310 : Editor
    {
        private V310_Background bg;

        void OnEnable()
        {
            if (bg == null)
                bg = (V310_Background)target;
        }

        public override void OnInspectorGUI()
        {
            bg._S = (Sprite)EditorGUILayout.ObjectField(
                "Bild", bg._S, typeof(Sprite), true);/**/
            bg.StartPostion =
                EditorGUILayout.Vector3Field("Ausgangsposition: ", bg.StartPostion);
            if (bg._S != null)
            {
                EditorGUILayout.BeginVertical();
                {
                    bg.name =
                        EditorGUILayout.TextField("Name:", bg.name);
                    bg.SortingOrder =
                        EditorGUILayout.IntSlider("Entfernung [" + bg.SortOrdMin + "-" + bg.SortOrdMax +"]", bg.SortingOrder, bg.SortOrdMin, bg.SortOrdMax);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.LabelField("------------------------------------------------");
            base.OnInspectorGUI();
        }
    }
}