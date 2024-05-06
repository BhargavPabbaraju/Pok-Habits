using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerSpritesList : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] trainerOverworlds;
    public Sprite[] trainerSprites;

    public static bool loaded = false;

    void Awake(){
        if(loaded)
            DestroyImmediate(gameObject);
        else{
            loaded = true;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    // Update is called once per frame
    public Sprite getTrainerOverworldSprite(int gen)
    {
        int index;
        index = getGenIndex(gen);
        
        return trainerOverworlds[index];
    }

    public Sprite getTrainerSprite(int gen)
    {
        int index;
        index = getGenIndex(gen);
        
        
        return trainerSprites[index];
    }

    int getGenIndex(int gen){
        if(gen==11)
            return 8;
        
        else
            return gen-1;
    }
}
