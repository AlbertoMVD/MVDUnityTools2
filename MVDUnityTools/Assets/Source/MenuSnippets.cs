using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuSnippets
{
    [MenuItem("CONTEXT/Transform/Randomize Position")]
    private static void RandomizeTransform(MenuCommand cmd)
    {
        Transform trnobj = cmd.context as Transform;
        trnobj.position = new Vector3(Random.Range(-10,10f), Random.Range(-10, 10f), Random.Range(-10, 10f));

        // Modify this script to place an object into a proper or realistic place.
        // Raycast MUST be used.
    }

    [MenuItem("Assets/Process Texture")]
    private static void ProcessTexture(MenuCommand cmd)
    {
        //if the asset exists I want to do something.
        if(Selection.activeObject.GetType() == typeof(Texture2D))
        {
            Debug.Log("This is a texture");

            Texture2D tex = Selection.activeObject as Texture2D;
            string path = AssetDatabase.GetAssetPath(tex);

            // I need to import the texture, because its an asset.
            TextureImporter teximporter = AssetImporter.GetAtPath(path) as TextureImporter;
            {
                // Here we set the settings we want for our asset.
                teximporter.textureType = TextureImporterType.NormalMap;
                teximporter.filterMode = FilterMode.Trilinear;
                // Setup more settings here....
            }
            AssetDatabase.ImportAsset(path); // We are updating the asset we modified.
        }
    }

}
