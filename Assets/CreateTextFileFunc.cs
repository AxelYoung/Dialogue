using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class CreateTextFileFunc : MonoBehaviour
{
    private static double renameTime;

    [MenuItem("Assets/Create Text File")]
    public static void CreateLevelData()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject) + "/New Text.txt";
        System.IO.File.WriteAllText(path, null);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        var asset = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));
        Selection.activeObject = asset;
        EditorUtility.FocusProjectWindow();
        renameTime = EditorApplication.timeSinceStartup + 0.2d;
        EditorApplication.update += EngageRenameMode;
    }

    private static void EngageRenameMode()
    {
        if (EditorApplication.timeSinceStartup >= renameTime)
        {
            EditorApplication.update -= EngageRenameMode;
            var e = new Event { keyCode = KeyCode.F2, type = EventType.KeyDown };
            EditorWindow.focusedWindow.SendEvent(e);
        }
    }
}
