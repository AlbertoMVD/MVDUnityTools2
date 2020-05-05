using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PrefabLoader : EditorWindow
{
    private int width = 8;
    private float offset = 8f;
    private Vector2 gridSize;

    private string prefabsPath = "Assets/3DGamekitLite/Art/Models/Characters";
    private List<string> instances;

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

    // Get all the prefabs from the folder
    void RetrievePrefabs(string path)
    {
        string[] prefabs = AssetDatabase.FindAssets("", new string[] { prefabsPath });

        foreach(string assetguid in prefabs)
        {
            string assetpath = AssetDatabase.GUIDToAssetPath(assetguid);

            Debug.Log(assetpath);

            if(AssetDatabase.IsValidFolder(assetpath))
            {
                //RetrievePrefabs(assetpath);
                continue;
            }

            if(!instances.Contains(assetpath))
                instances.Add(assetpath);
        }
    }

    // Spawn the prefabs on the scene
    void SpawnPrefabs()
    {
        int x_gridpos = 0, y_gridpos = 0;

        foreach(string path in instances)
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