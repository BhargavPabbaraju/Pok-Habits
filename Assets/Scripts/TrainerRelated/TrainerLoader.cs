using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainerLoader : MonoBehaviour
{   
    [SerializeField] public int id = 1;
    [SerializeField] public Trainer tr;

    [SerializeField] public string type ="over";
    [SerializeField] public int dirRow =1;
    [SerializeField] public int dirCol =2;
    [SerializeField] public bool doNotAwake = false;
    //[SerializeField] public TMP_Text poke_name;

    private TrainerSpritesList spriteList = PlayerTrainer.Instance.spriteList;
    private SpriteGetter spritegetter = GameManager.Instance.spritegetter;

    // Start is called before the first frame update
    void Awake()

    {   
        if(doNotAwake)
            return;
        if(id==0){
            id = AuthHandler.Instance.currentUser.trId;
        }
        tr = GameObject.Find("Database").GetComponent<LoadTrainersDatabase>().GetTrainer(id);
        getSprite();
        
    }

    public void getSprite(){
        int ROW,COL,W,H,noOfSpritesPerCol,noOfSpritesPerRow;
        Sprite sprite;
        if(type=="over")
        {
            
            sprite = spriteList.getTrainerOverworldSprite(tr.gen);
            W=32;H=32;
            ROW = tr.cords[0];
            COL = tr.cords[1];
            noOfSpritesPerCol = 3;noOfSpritesPerRow=4;
            //poke_name.text = poke.pokeName;
            GetComponent<Image>().overrideSprite = spritegetter.getSprite(sprite,ROW,COL, W,H,noOfSpritesPerCol,noOfSpritesPerRow,dirRow,dirCol);
        
        }
        else if(type=="sprite"){
            sprite = spriteList.getTrainerSprite(tr.gen);
            W=80;H=80;
            ROW = tr.cords[0];
            COL = tr.cords[1];
            noOfSpritesPerCol = 1;noOfSpritesPerRow=1;
            //poke_name.text = poke.pokeName;
            GetComponent<Image>().overrideSprite = spritegetter.getSprite(sprite,ROW,COL, W,H,noOfSpritesPerCol,noOfSpritesPerRow,dirRow,dirCol);

        }
        

    }

    
    // public Move[] GetMoveList(){
    //     return poke.moveList;
    // }
    
    public void RefreshSprite(){;
        if(id==0){
            
            id = AuthHandler.Instance.currentUser.trId;
        }
        tr = GameObject.Find("Database").GetComponent<LoadTrainersDatabase>().GetTrainer(id);
        getSprite();
    }

    
    
}


