using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorList : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Color[] typeColors;
    [SerializeField] public Color[] typeColorsDark;
    [SerializeField] public Color[] typeColorsLight;

    public static bool loaded = false;

    void Awake(){
        if(loaded)
            DestroyImmediate(gameObject);
        else{
            loaded = true;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Color GetColor(int typeIndex,int type){
        if(type==0)
            return typeColors[typeIndex];
        else if(type==1)
            return typeColorsDark[typeIndex];
        
        else
            return typeColorsLight[typeIndex];
    }
}
