using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpritesList : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] eggSprites;

    // Update is called once per frame

    public static bool loaded = false;

    void Awake(){
        if(loaded)
            DestroyImmediate(gameObject);
        else{
            loaded = true;
            DontDestroyOnLoad(gameObject);
        }
        
    }
    
    public Sprite getEggSprite(int gen,bool shiny)
    {
        int index;
        index = getGenIndex(gen);
        if(shiny)
            index +=8;
        
        return eggSprites[index];
    }

   

    int getGenIndex(int gen){
        if(gen==11)
            return 8;
        
        else
            return gen-1;
    }
}
