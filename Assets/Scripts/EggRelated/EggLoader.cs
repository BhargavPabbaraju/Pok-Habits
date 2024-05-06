using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EggLoader : MonoBehaviour
{   
    [SerializeField] public string id = "-1";
    [SerializeField] public Egg egg;
    
    [SerializeField] public bool debug;
    [SerializeField] public bool doNotAwake = false;
    
    private EggSpritesList spriteList = PlayerEgg.Instance.spriteList;
    private SpriteGetter spritegetter = GameManager.Instance.spritegetter;
   

    // Start is called before the first frame update
    void Awake()
    {   
        if(doNotAwake)
            return;
        RefreshSprite();
        
    }


    public void getSprite(){
        int ROW,COL,W,H,noOfSpritesPerCol,noOfSpritesPerRow;
        Sprite sprite;
        
        sprite = spriteList.getEggSprite(egg.gen,egg.shiny);
        W=28;H=30;
        ROW = egg.cords[0];
        COL = egg.cords[1];
        noOfSpritesPerCol = 1;noOfSpritesPerRow=1;

        GetComponent<Image>().overrideSprite = spritegetter.getSprite(sprite,ROW,COL, W,H,noOfSpritesPerCol,noOfSpritesPerRow,0,0);
        
        

    }

    
    // public Move[] GetMoveList(){
    //     return poke.moveList;
    // }
    
    public  async  void RefreshSprite(){
    
        if(debug)
            egg = GameObject.Find("Database").GetComponent<LoadEggDatabase>().GetEgg(id);
        
        getSprite();
    }

    
    
}


