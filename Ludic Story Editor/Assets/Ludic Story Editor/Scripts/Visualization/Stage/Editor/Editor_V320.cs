using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditorInternal;
using LSE.VISUALIZATION;

namespace LSE.EDITOR
{
    [CustomEditor(typeof(V320_Object))]
    public class Editor_V320 : Editor
    {
        private V320_Object fg;

        void OnEnable()
        {
            if (fg == null)
                fg = (V320_Object)target;
        }

        public override void OnInspectorGUI()
        {
            fg._S = (Sprite)EditorGUILayout.ObjectField(
                "Bild", fg._S, typeof(Sprite), true);/**/
            fg.StartPostion =
                EditorGUILayout.Vector3Field("Ausgangsposition: ", fg.StartPostion);
            if (fg._S != null)
            {
                EditorGUILayout.BeginVertical();
                {
                    fg.name =
                        EditorGUILayout.TextField("Name:", fg.name);
                    fg.SortingOrder =
                        EditorGUILayout.IntSlider("Entfernung [" + fg.SortOrdMin + "-" + fg.SortOrdMax + "]", fg.SortingOrder, fg.SortOrdMin, fg.SortOrdMax);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.LabelField("------------------------------------------------");
            base.OnInspectorGUI();
        }
    }
}