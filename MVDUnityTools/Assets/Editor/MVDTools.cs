﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MVDTools : EditorWindow
{
    // Editor version
    public static string version = "0.1a";

    // Display textures
    public static Texture2D texture_logo;

    // Predefined sizes
    public Vector2 tx_logo_size = new Vector2(200, 75);

    private int width = 8;
    private float offset = 8f;
    private Vector2 gridSize;

    private string prefabsPath = "Assets/3DGamekitLite/Art/Models/Characters";
    private List<string> instances;

    private bool[] foldout = new bool[] { false, false, true };

    // Init function, we create the window and initialize settings
    [MenuItem("MVD/MVD Tools Panel")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        MVDTools window = (MVDTools)EditorWindow.GetWindow(typeof(MVDTools));
        window.Show();

        UpdateResources();
    }

    /* Editor UI METHODS */

    // Reloading resources method
    [UnityEditor.Callbacks.DidReloadScripts]
    static void UpdateResources()
    {
        texture_logo = Resources.Load("logo_lasalle") as Texture2D;
    }

    // Method used to draw anything on our window screen.
    void OnGUI()
    {
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                GUI.DrawTexture(new Rect(Screen.width * .5f - tx_logo_size.x * .5f, 15, tx_logo_size.x, tx_logo_size.y), texture_logo, ScaleMode.StretchToFill, true);
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(tx_logo_size.y + 10f);
            DisplaySeparator(50);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Tools Version: " + version, EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
        }

        DisplayPanel("Utilities", () => DisplayUtilities(), ref foldout[0]);
        DisplayPanel("Prefab Loader", () => DisplayPrefabLoader(), ref foldout[1]);
        DisplayPanel("Prefab Spawner", () => DisplayPrefabSpawner(), ref foldout[2]);
    }

    private void DisplayPrefabLoader()
    {
        // Canvas drawing elements
        prefabsPath = EditorGUILayout.TextField("Prefabs path", prefabsPath);
        width = EditorGUILayout.IntField("Grid width", width);
        offset = EditorGUILayout.FloatField("Offset", offset);
        gridSize = EditorGUILayout.Vector2Field("Grid Size", gridSize);

        GUIStyle editorStyle = new GUIStyle(GUI.skin.button);
        Color myStyleColor = Color.blue;
        editorStyle.fontStyle = FontStyle.Bold;
        editorStyle.normal.textColor = myStyleColor;

        if (GUILayout.Button("Build Tilesets", editorStyle))
        {
            instances = new List<string>();

            RetrievePrefabs(prefabsPath);
            SpawnPrefabs();
        }
    }

    // Method used to display the utility tools.
    private void DisplayUtilities()
    {
        // TO-DO
        // Implement the UI for the necessary utilities required for the final deliver.
    }

    // Method used to display the prefab placement tools.
    private void DisplayPrefabSpawner()
    {

    }

    // Display a separator line as needed.
    static void DisplaySeparator(int width)
    {
        string line = string.Empty;
        for (int i = 0; i < width; i++) line += "_";

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField(line);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    // Display a foldout inside a vertical box.
    static void DisplayPanel(string title, System.Action toexecute, ref bool foldout)
    {
        //GUI.color = color;
        EditorGUILayout.BeginVertical("Box");
        {
            GUI.color = Color.white;
            GUILayout.Space(3);
            GUILayout.BeginHorizontal();
            foldout = EditorGUILayout.Foldout(foldout, title, foldout);
            GUILayout.EndHorizontal();
            GUILayout.Space(3);

            if (foldout)
            {
                // Method to be called.
                toexecute();
            }
        }
        GUILayout.EndVertical();
    }

    // Functions to execute functionality

    // Get all the prefabs from the folder
    void RetrievePrefabs(string path)
    {
        string[] prefabs = AssetDatabase.FindAssets("", new string[] { prefabsPath });

        foreach (string assetguid in prefabs)
        {
            string assetpath = AssetDatabase.GUIDToAssetPath(assetguid);

            Debug.Log(assetpath);

            if (AssetDatabase.IsValidFolder(assetpath))
            {
                //RetrievePrefabs(assetpath);
                continue;
            }

            if (!instances.Contains(assetpath))
                instances.Add(assetpath);
        }
    }

    // Spawn the prefabs on the scene
    void SpawnPrefabs()
    {
        int x_gridpos = 0, y_gridpos = 0;

        foreach (string path in instances)
        {
            // Get the grid position
            float pos_x = x_gridpos * (gridSize.x + offset);
            float pos_y = y_gridpos * (gridSize.y + offset);

            // Update the grid position
            x_gridpos = (x_gridpos + 1) % width;
            y_gridpos += x_gridpos % width == 0 ? 1 : 0;

            Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
            GameObject prefab = PrefabUtility.InstantiatePrefab(obj) as GameObject;
            prefab.transform.position = new Vector3(pos_x, 0, pos_y);
        }
    }
}
