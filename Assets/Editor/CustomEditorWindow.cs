using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomEditorWindow : EditorWindow
{
    // Start is called before the first frame update
    [MenuItem("Tools/CustomWindow")]
    public static void ShowWindow(){
        GetWindow<CustomEditorWindow>("CustomEditorWindow");
    }

    void OnGUI(){
        GUILayout.Label("Reload Database",EditorStyles.boldLabel);
        if(GUILayout.Button("Reload pokemon database")){
            GameObject.Find("Database").GetComponent<LoadPokemonDatabase>().LoadData();
        }
    }
}
