using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditorInternal;
using LSE.NARRATION;

namespace LSE.EDITOR
{
    [CustomEditor(typeof(N200_Sequence))]
    public class Editor_N200 : Editor
    {
        private N200_Sequence sequence;
        private ReorderableList agentList;
        private ReorderableList actionsList;

        void OnEnable()
        {
            if (sequence == null)
                sequence = (N200_Sequence)target;
            if (agentList == null)
                agentList =
                    new ReorderableList(
                        sequence.Agents,
                        typeof(N200_Sequence.N200_Sequence_AgentStruct),
                        false, false, true, true);

            agentList.drawElementCallback += DrawAgentListElement;
            agentList.onAddCallback += AddAgentListItem;
            agentList.onRemoveCallback = (ReorderableList l) =>
            {
                if (EditorUtility.DisplayDialog("Warning!",
                                                "Sind Sie sicher, dass Sie diese Sezene löschen möchten?\nDies löscht auch alle der Szene zugewiesenen Elemente.", "Yes", "No"))
                {
                    RemoveAgentListItem(l);
                }
            };

            if (actionsList == null)
                actionsList =
                    new ReorderableList(
                        sequence.Actions,
                        typeof(N200_Sequence),
                        true, true, true, true);

            actionsList.drawHeaderCallback += DrawActionListHeader;
            actionsList.drawElementCallback += DrawActionListElement;
            actionsList.onAddCallback += AddActionListItem;
            actionsList.onRemoveCallback = (ReorderableList l) =>
            {
                if (EditorUtility.DisplayDialog("Warning!",
                                                "Sind Sie sicher, dass Sie diese Sezene löschen möchten?\nDies löscht auch alle der Szene zugewiesenen Elemente.", "Yes", "No"))
                {
                    RemoveActionListItem(l);
                }
            };
        }

        private void OnDisable()
        {
            // Make sure we don't get memory leaks etc.
            actionsList.drawHeaderCallback -= DrawActionListHeader;
            actionsList.drawElementCallback -= DrawActionListElement;
            actionsList.onAddCallback -= AddActionListItem;
            actionsList.onRemoveCallback -= RemoveActionListItem;
        }
        public override void OnInspectorGUI()
        {
            sequence.Stage = (LSE.VISUALIZATION.V300_Stage) EditorGUILayout.ObjectField(
                sequence.StageName, sequence.Stage, typeof(LSE.VISUALIZATION.V300_Stage), true);

            EditorGUILayout.PrefixLabel("Agenten:");
            agentList.DoLayoutList();
            EditorGUILayout.PrefixLabel("Szenen Reihenfolge:");
            actionsList.DoLayoutList();
        }

        // ----------------------------------------------------------------- Agent List -----------------------------------------------------------------

        /// <summary>
        /// Zeichnet die einzelnen Elemente der Aktionsliste
        /// </summary>
        private void DrawAgentListElement(Rect rect, int index, bool active, bool focused)
        {
            N200_Sequence.N200_Sequence_AgentStruct a = sequence.Agents[index];
            EditorGUI.BeginChangeCheck();
            a.agent = (LSE.VISUALIZATION.V200_Agent) EditorGUI.ObjectField(
                new Rect(rect.x, rect.y, rect.width, rect.height - 2), a.agentName, a.agent, typeof(LSE.VISUALIZATION.V200_Agent), true);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);
            }
        }

        private void AddAgentListItem(ReorderableList list)
        {
            /*sequence.AddAction("Neue Szene", reorderableList.count + 1);
            EditorUtility.SetDirty(chapter.story.gameObject);*/
            //EditorUtility.SetDirty(target);
        }

        private void RemoveAgentListItem(ReorderableList list)
        {
            /* chapter.RemoveScene(list.index);
             EditorUtility.SetDirty(target);*/
        }

        private void SelectAgentList(int _index)
        {
            /*  GameObject[] selected = new GameObject[1];
              selected[0] = chapter.SceneList[_index].gameObject;
              Selection.objects = selected;*/
        }

        // ----------------------------------------------------------------- /Agent List -----------------------------------------------------------------
        // ----------------------------------------------------------------- Action List -----------------------------------------------------------------

        /// <summary>
        /// Kopfzeile der Aktionsliste
        /// </summary>
        private void DrawActionListHeader(Rect rect)
        {
            GUI.Label(new Rect(rect.x, rect.y, 15, rect.height), " ");
            GUI.Label(new Rect(rect.x + 15, rect.y, 40, rect.height), "Name");
            GUI.Label(new Rect(rect.x + 75, rect.y, rect.width - 35, rect.height), "Next ID");
            GUI.Label(new Rect(rect.x + rect.width - 116, rect.y, 40, rect.height), "Type");
        }

        /// <summary>
        /// Zeichnet die einzelnen Elemente der Aktionsliste
        /// </summary>
        private void DrawActionListElement(Rect rect, int index, bool active, bool focused)
        {
            N300_Action a = sequence.Actions[index];
            EditorGUI.BeginChangeCheck();
            a.name = EditorGUI.TextField(new Rect(rect.x, rect.y, 60, rect.height - 1), a.name);
            a.NextActionID = EditorGUI.TextField(new Rect(rect.x + 61, rect.y, rect.width - 179, rect.height - 1), a.NextActionID);
            a.Type = (N300_Action.N300_Action_Type) EditorGUI.EnumPopup (new Rect(rect.x + rect.width - 116, rect.y, 75, rect.height - 1), a.Type);

            if (GUI.Button(
                new Rect(
                rect.x + rect.width - 40, rect.y, 40, rect.height - 1), "Edit"))
                SelectActionList(index);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);
            }
        }

        private void AddActionListItem(ReorderableList list)
        {
            /*sequence.AddAction("Neue Szene", reorderableList.count + 1);
            EditorUtility.SetDirty(chapter.story.gameObject);*/
            //EditorUtility.SetDirty(target);
        }

        private void RemoveActionListItem(ReorderableList list)
        {
           /* chapter.RemoveScene(list.index);
            EditorUtility.SetDirty(target);*/
        }

        private void SelectActionList(int _index)
        {
          /*  GameObject[] selected = new GameObject[1];
            selected[0] = chapter.SceneList[_index].gameObject;
            Selection.objects = selected;*/
        }

        // ---------------------------------------------------------------- / Action List ----------------------------------------------------------------
    }
}