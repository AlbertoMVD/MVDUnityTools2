using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BounceSphere))]
public class BounceSphereEditor : Editor
{
    // Override the inspector drawing method
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BounceSphere spherescript = (BounceSphere)target;

        if(GUILayout.Button("Randomize"))
        {
            spherescript.health = Random.Range(0, 100);
        }
    }
}
