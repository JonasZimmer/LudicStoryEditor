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
        private ReorderableList reorderableList;

        void OnEnable()
        {
            if (sequence == null)
                sequence = (N200_Sequence)target;
            if (reorderableList == null)
                reorderableList =
                    new ReorderableList(
                        sequence.Actions,
                        typeof(N200_Sequence),
                        true, true, true, true);

            reorderableList.drawHeaderCallback += DrawHeader;
            reorderableList.drawElementCallback += DrawElement;
            reorderableList.onAddCallback += AddItem;
            reorderableList.onRemoveCallback = (ReorderableList l) =>
            {
                if (EditorUtility.DisplayDialog("Warning!",
                                                "Sind Sie sicher, dass Sie diese Sezene löschen möchten?\nDies löscht auch alle der Szene zugewiesenen Elemente.", "Yes", "No"))
                {
                    RemoveItem(l);
                }
            };
        }

        private void OnDisable()
        {
            // Make sure we don't get memory leaks etc.
            reorderableList.drawHeaderCallback -= DrawHeader;
            reorderableList.drawElementCallback -= DrawElement;
            reorderableList.onAddCallback -= AddItem;
            reorderableList.onRemoveCallback -= RemoveItem;
        }

        /// <summary>
        /// Draws the header of the list
        /// </summary>
        private void DrawHeader(Rect rect)
        {
            GUI.Label(new Rect(rect.x, rect.y, 15, rect.height), " ");
            GUI.Label(new Rect(rect.x + 15, rect.y, 40, rect.height), "Name");
            GUI.Label(new Rect(rect.x + 75, rect.y, rect.width - 35, rect.height), "Next ID");
            GUI.Label(new Rect(rect.x + rect.width - 116, rect.y, 40, rect.height), "Type");
        }

        /// <summary>
        /// Draws one element of the list (ListItemExample)
        /// </summary>
        private void DrawElement(Rect rect, int index, bool active, bool focused)
        {
            N300_Action a = sequence.Actions[index];
            EditorGUI.BeginChangeCheck();
            a.name = EditorGUI.TextField(new Rect(rect.x, rect.y, 60, rect.height - 1), a.name);
            a.NextActionID = EditorGUI.TextField(new Rect(rect.x + 61, rect.y, rect.width - 179, rect.height - 1), a.NextActionID);
            a.Type = (N300_Action.N300_Action_Type) EditorGUI.EnumPopup (new Rect(rect.x + rect.width - 116, rect.y, 75, rect.height - 1), a.Type);

            if (GUI.Button(
                new Rect(
                rect.x + rect.width - 40, rect.y, 40, rect.height - 1), "Edit"))
                Select(index);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);
            }
        }

        private void AddItem(ReorderableList list)
        {
            /*sequence.AddAction("Neue Szene", reorderableList.count + 1);
            EditorUtility.SetDirty(chapter.story.gameObject);*/
            //EditorUtility.SetDirty(target);
        }

        private void RemoveItem(ReorderableList list)
        {
           /* chapter.RemoveScene(list.index);
            EditorUtility.SetDirty(target);*/
        }

        private void Select(int _index)
        {
          /*  GameObject[] selected = new GameObject[1];
            selected[0] = chapter.SceneList[_index].gameObject;
            Selection.objects = selected;*/
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PrefixLabel("Szenen Reihenfolge:");
            reorderableList.DoLayoutList();
        }
    }
}