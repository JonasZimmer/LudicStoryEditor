using UnityEngine;
using UnityEditor;
using System.Collections;

public class LSE000_Menu : Editor
{
    [MenuItem("Ludic Story Editor/Import/Import Final Draft (*.fdx)", false, 11)]
    public static void ImportFDXMenu()
    {
        string path = EditorUtility.OpenFilePanel(
					"Import *.fdx",
					"",
					"fdx");
        LSE.IMPORT.IMP001_FDX.Instance.LoadData(path);
    }
/*
    [MenuItem("Story Editor/Create/Create New Chapter", false, 12)]
    public static void AddChapterMenu()
    {
        GameObject[] selected = new GameObject[1];
        selected[0] = StoryEditorController.Instance.StoryObject.AddChapter();
        Selection.objects = selected;
    }

    [MenuItem("Story Editor/Create/Create New Character", false, 13)]
    public static void AddCharacterMenu()
    {
        GameObject[] selected = new GameObject[1];
        selected[0] = StoryEditorController.Instance.CharacterCreatorObject.AddCharacter();
        Selection.objects = selected;
    }

    [MenuItem("Story Editor/Create/Create New Dialog", false, 14)]
    public static void AddDialogMenu()
    {
        GameObject[] selected = new GameObject[1];
        selected[0] = StoryEditorController.Instance.DialogCreatorObject.AddDialog();
        Selection.objects = selected;
    }

    [MenuItem("Story Editor/Show Story Editor", false, 1)]
    public static void ShowStoryEditor()
    {
        GameObject[] selected = new GameObject[1];
        selected[0] = StoryEditorController.Instance.StoryObject.gameObject;
        Selection.objects = selected;
    }

    [MenuItem("Story Editor/Show Stage Creator", false, 2)]
    public static void ShowStageEditor()
    {
        GameObject[] selected = new GameObject[1];
        selected[0] = StoryEditorController.Instance.StageCreatorObject.gameObject;
        Selection.objects = selected;
    }

    [MenuItem("Story Editor/Show Character Creator", false, 3)]
    public static void ShowCharacterEditor()
    {
        GameObject[] selected = new GameObject[1];
        selected[0] = StoryEditorController.Instance.CharacterCreatorObject.gameObject;
        Selection.objects = selected;
    }

    [MenuItem("Story Editor/Show Dialog Creator", false, 4)]
    public static void ShowDialogEditor()
    {
        GameObject[] selected = new GameObject[1];
        selected[0] = StoryEditorController.Instance.DialogCreatorObject.gameObject;
        Selection.objects = selected;
    }

    [MenuItem("Story Editor/Use Custom Editor/On")]
    public static void ForceUseEditorTrue()
    {
        StoryEditorController.Instance.UseEditor = true;
    }

    [MenuItem("Story Editor/Use Custom Editor/Off")]
    public static void ForceUseEditorFalse()
    {
        StoryEditorController.Instance.UseEditor = false;
    }

    [MenuItem("Story Editor/Use Custom Editor/On", true)]
    public static bool CanForceUseEditorTrue()
    {
        return StoryEditorController.Instance.UseEditor == false;
    }

    [MenuItem("Story Editor/Use Custom Editor/Off", true)]
    public static bool CanForceUseEditorFalse()
    {
        return StoryEditorController.Instance.UseEditor == true;
    }*/

}
