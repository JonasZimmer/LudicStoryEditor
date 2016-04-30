using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditorInternal;
using LSE.VISUALIZATION;

namespace LSE.EDITOR
{
    [CustomEditor(typeof(V300_Stage))]
    public class Editor_V300 : Editor
    {
        private V300_Stage stage;
        private ReorderableList background;
        private ReorderableList foreground;

        void OnEnable()
        {
            if (stage == null)
                stage = (V300_Stage)target;
            if (background == null)
                background =
                    new ReorderableList(
                        stage.Backgrounds,
                        typeof(V310_Background),
                        true, true, true, true);
            if (foreground == null)
                foreground =
                    new ReorderableList(
                        stage.SceneObjects,
                        typeof(V320_Object),
                        true, true, true, true);

            background.drawHeaderCallback += DrawHeader;
            foreground.drawHeaderCallback += DrawHeader;
            background.drawElementCallback += DrawBackground;
            foreground.drawElementCallback += DrawForeground;
            background.onAddCallback += AddBackground;
            foreground.onAddCallback += AddForeground;
            background.onRemoveCallback = (ReorderableList l) =>
            {
                if (EditorUtility.DisplayDialog("Warning!",
                                                "Sind Sie sicher, dass Sie diese Sezene löschen möchten?\nDies löscht auch alle der Szene zugewiesenen Elemente.", "Yes", "No"))
                {
                    RemoveBackground(l);
                }
            };
            foreground.onRemoveCallback = (ReorderableList l) =>
            {
                if (EditorUtility.DisplayDialog("Warning!",
                                                "Sind Sie sicher, dass Sie diese Sezene löschen möchten?\nDies löscht auch alle der Szene zugewiesenen Elemente.", "Yes", "No"))
                {
                    RemoveForeground(l);
                }
            };
        }

        private void OnDisable()
        {
            // Make sure we don't get memory leaks etc.
            background.drawHeaderCallback -= DrawHeader;
            foreground.drawHeaderCallback -= DrawHeader;
            background.drawElementCallback -= DrawBackground;
            foreground.drawElementCallback -= DrawForeground;
            background.onAddCallback -= AddBackground;
            foreground.onAddCallback -= AddForeground;
            background.onRemoveCallback -= RemoveBackground;
            foreground.onRemoveCallback -= RemoveForeground;
        }

        /// <summary>
        /// Draws the header of the list
        /// </summary>
        private void DrawHeader(Rect rect)
        {
            GUI.Label(new Rect(rect.x, rect.y, 15, rect.height), " ");
            GUI.Label(new Rect(rect.x + 15, rect.y, 40, rect.height), "Name");
            GUI.Label(new Rect(rect.x + (rect.width / 4) + 15, rect.y, 40, rect.height), "Tiefe");
            /*GUI.Label(new Rect(rect.x + 75, rect.y, rect.width - 35, rect.height), "Next ID");
            GUI.Label(new Rect(rect.x + rect.width - 116, rect.y, 40, rect.height), "Type");*/
        }

        /// <summary>
        /// Draws one element of the list (ListItemExample)
        /// </summary>
        private void DrawBackground(Rect rect, int index, bool active, bool focused)
        {
            V310_Background b = stage.Backgrounds[index];
            EditorGUI.BeginChangeCheck();
            b.name = EditorGUI.TextField(new Rect(rect.x, rect.y, rect.width/4, rect.height - 1), b.name);
            b.SortingOrder = EditorGUI.IntSlider(new Rect(rect.x + (rect.width / 4) + 5, rect.y, (3*rect.width / 4)-50, rect.height - 1), b.SortingOrder, b.SortOrdMin, b.SortOrdMax);
            /*a.NextActionID = EditorGUI.TextField(new Rect(rect.x + 61, rect.y, rect.width - 179, rect.height - 1), a.NextActionID);
            a.Type = (N300_Action.N300_Action_Type) EditorGUI.EnumPopup (new Rect(rect.x + rect.width - 116, rect.y, 75, rect.height - 1), a.Type);*/

            if (GUI.Button(
                new Rect(
                rect.x + rect.width - 40, rect.y, 40, rect.height - 1), "Edit"))
                Select(index, true);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);
            }
        }

        private void DrawForeground(Rect rect, int index, bool active, bool focused)
        {
            V320_Object o = stage.SceneObjects[index];
            EditorGUI.BeginChangeCheck();
            o.name = EditorGUI.TextField(new Rect(rect.x, rect.y, rect.width / 4, rect.height - 1), o.name);
            o.SortingOrder = EditorGUI.IntSlider(new Rect(rect.x + (rect.width / 4) + 5, rect.y, (3 * rect.width / 4) - 50, rect.height - 1), o.SortingOrder, o.SortOrdMin, o.SortOrdMax);
            /*a.NextActionID = EditorGUI.TextField(new Rect(rect.x + 61, rect.y, rect.width - 179, rect.height - 1), a.NextActionID);
            a.Type = (N300_Action.N300_Action_Type) EditorGUI.EnumPopup (new Rect(rect.x + rect.width - 116, rect.y, 75, rect.height - 1), a.Type);*/

            if (GUI.Button(
                new Rect(
                rect.x + rect.width - 40, rect.y, 40, rect.height - 1), "Edit"))
                Select(index, false);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);
            }
        }

        private void AddBackground(ReorderableList list)
        {
            stage.AddBackground();
            EditorUtility.SetDirty(stage.gameObject);
            //EditorUtility.SetDirty(target);
        }

        private void AddForeground(ReorderableList list)
        {
            stage.AddSceneobject();
            EditorUtility.SetDirty(stage.gameObject);
            //EditorUtility.SetDirty(target);
        }

        private void RemoveBackground(ReorderableList list)
        {
            stage.RemoveBackground(list.index);
            EditorUtility.SetDirty(target);
        }

        private void RemoveForeground(ReorderableList list)
        {
            stage.RemoveSceneobject(list.index);
            EditorUtility.SetDirty(target);
        }

        private void Select(int _index, bool bg)
        {
            GameObject[] selected = new GameObject[1];
            if (bg) selected[0] = stage.Backgrounds[_index].gameObject;
            else selected[0] = stage.SceneObjects[_index].gameObject;
            Selection.objects = selected;/**/
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PrefixLabel("Neue Bühne");
            stage.name =
                EditorGUILayout.TextField("Bühnenname: ", stage.name);
            stage.UseParallexScrolling =
                EditorGUILayout.Toggle("Benutzt Parallex Scrolling: ", stage.UseParallexScrolling);
            EditorGUILayout.PrefixLabel("Hintergrundelemente:");
            background.DoLayoutList();
            EditorGUILayout.PrefixLabel("Vordergrundelemente:");
            foreground.DoLayoutList();
        }
    }
}