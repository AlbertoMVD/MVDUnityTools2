using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PrefabLoader : EditorWindow
{
    // Init function, we create the window and initialize settings
    [MenuItem("MVD/Prefab Loader")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        PrefabLoader window = (PrefabLoader)EditorWindow.GetWindow(typeof(PrefabLoader));
        window.Show();
    }

    // Method used to draw anything on our window screen.
    void OnGUI()
    {

    }
}