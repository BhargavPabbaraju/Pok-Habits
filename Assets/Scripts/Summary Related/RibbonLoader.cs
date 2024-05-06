using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RibbonLoader : MonoBehaviour
{   
    [SerializeField] public int id = 1;
    [SerializeField] public Ribbon ribbon;


    [SerializeField] public string type ="sprite";
    [SerializeField] public int dirRow =0;
    [SerializeField] public int dirCol =0;
    //[SerializeField] public TMP_Text poke_name;

    
    private Sprite ribbonSprite = PlayerItem.Instance.ribbonSprite;
    private SpriteGetter spritegetter = GameManager.Instance.spritegetter;

    // Start is called before the first frame update
    void Awake()

    {   
        
        RefreshSprite();
        
    }

    void getSprite(){
        int ROW,COL,W,H,noOfSpritesPerCol,noOfSpritesPerRow;
        Sprite sprite;
        if(type=="sprite"){
            sprite = ribbonSprite;
            W=64;H=64;
            ROW = ribbon.cords[0];
            COL = ribbon.cords[1];
            noOfSpritesPerCol = 1;noOfSpritesPerRow=1;
            //poke_name.text = poke.pokeName;
            GetComponent<Image>().overrideSprite = spritegetter.getSprite(sprite,ROW,COL, W,H,noOfSpritesPerCol,noOfSpritesPerRow,dirRow,dirCol);

        }
        

    }

    
    // public Move[] GetMoveList(){
    //     return poke.moveList;
    // }
    
    public void RefreshSprite(){
        if(id==-1)
            return;
        ribbon = GameObject.Find("Database").GetComponent<LoadRibbonDatabase>().GetRibbon(id);
        getSprite();
    }

    
    
}


