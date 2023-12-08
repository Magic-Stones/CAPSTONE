using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProceduralWorldGeneration), true)]
public class ProceduralGenerationEditor : Editor
{
    private ProceduralWorldGeneration _generator;

    void Awake()
    {
        _generator = (ProceduralWorldGeneration)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create a world!"))
        {
            _generator.CreateWorld();
        }

        if (GUILayout.Button("Banish this world!"))
        {
            _generator.DeleteWorld();
        }
    }
}
