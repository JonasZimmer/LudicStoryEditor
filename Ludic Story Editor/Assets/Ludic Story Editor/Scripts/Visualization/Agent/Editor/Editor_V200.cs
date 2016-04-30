using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditorInternal;
using LSE.VISUALIZATION;

namespace LSE.EDITOR
{
    [CustomEditor(typeof(V200_Agent))]
    public class Editor_V200 : Editor
    {
        private V200_Agent agent;

        void OnEnable()
        {
            if (agent == null)
                agent = (V200_Agent)target;
        }

        public override void OnInspectorGUI()
        {
            agent._S = (Sprite) EditorGUILayout.ObjectField(
                "Charakter Bild", agent._S, typeof(Sprite), true);/**/
            if (agent._S != null)
            {
                EditorGUILayout.BeginVertical();
                {
                    agent.CharName =
                        EditorGUILayout.TextField("Charakter Name", agent.CharName);
                    agent.SortingOrder =
                        EditorGUILayout.IntSlider(agent.SortingOrder, agent.SortOrdMin, agent.SortOrdMax);
                }
                EditorGUILayout.EndVertical();
            }
        }
    }
}