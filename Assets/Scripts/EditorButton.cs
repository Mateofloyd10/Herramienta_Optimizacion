using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Poly_Count_Reduce))]
public class EditorButton : Editor
{
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
   
        Poly_Count_Reduce sp = target as Poly_Count_Reduce;
        if (GUILayout.Button("Optimize"))
        {
            sp.OptimizarMesh();
        }
        if (GUILayout.Button("Reset"))
        {
            sp.ResetOriginalMesh();
        }
        if (GUILayout.Button("Load Mesh"))
        {
            sp.LoadMesh();
        }
    }
}
