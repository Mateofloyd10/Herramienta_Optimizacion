using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Poly_Count_Reduce : MonoBehaviour
{

    public float Quality = 0.5f;
    public int MeshNumberSelect = 0;
 
    private Mesh defaultMesh;
    private Mesh optimizeMesh;
    private int  mesh_Id = 0;
    private bool defaultMeshBool = true;

    public void OptimizarMesh()
    {    
        var original_Mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        var mesh_Simplifier = new UnityMeshSimplifier.MeshSimplifier();

        mesh_Simplifier.Initialize(original_Mesh);
        mesh_Simplifier.SimplifyMesh(Quality);
        optimizeMesh = mesh_Simplifier.ToMesh();
        optimizeMesh.name = optimizeMesh.vertices.Length.ToString();
        AsignarMesh();

        if (defaultMeshBool)
        {
            defaultMesh = original_Mesh;   
            defaultMeshBool = false;
        }          
    }

    public void AsignarMesh() 
    {
        if (!AssetDatabase.IsValidFolder("Assets/Optimizaciones " + gameObject.name))
        {
            AssetDatabase.CreateFolder("Assets", "Optimizaciones " + gameObject.name);
            mesh_Id = 0;  
        }
            
        mesh_Id++;
        Debug.Log(FolderPath(mesh_Id));
        AssetDatabase.CreateAsset(optimizeMesh, FolderPath(mesh_Id));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void ResetOriginalMesh() 
    {
        if (defaultMeshBool)
        {
            defaultMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
            defaultMeshBool = false;
        }
        else 
        {
            gameObject.GetComponent<MeshFilter>().sharedMesh = defaultMesh;
            LoadMeshChildren(defaultMesh);
        }
       
    }

    public void LoadMesh() 
    {
        Mesh loadMesh = (Mesh)AssetDatabase.LoadAssetAtPath(FolderPath(MeshNumberSelect), typeof(Mesh));
        gameObject.GetComponent<MeshFilter>().sharedMesh = loadMesh;
        LoadMeshChildren(loadMesh);
    }
    public string  FolderPath(int number_ID) 
    {
        string folder_Name_Path = "Assets/Optimizaciones " + gameObject.name + "/meshReduction_" + gameObject.name + "_" + number_ID + ".asset";
        return folder_Name_Path;
    }

    public void LoadMeshChildren(Mesh loadMesh)
    {
        MeshFilter[] meshesChildren = GetComponentsInChildren<MeshFilter>();
        foreach (var r in meshesChildren)
        {
            r.sharedMesh = loadMesh;
        }
    }


}
