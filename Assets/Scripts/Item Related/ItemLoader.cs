using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemLoader : MonoBehaviour
{   
    [SerializeField] public int id = 1;
    [SerializeField] public Item item;

    [SerializeField] public string type ="sprite";
    [SerializeField] public int dirRow =0;
    [SerializeField] public int dirCol =0;
    [SerializeField] public bool doNotAwake = false;
    //[SerializeField] public TMP_Text poke_name;

    private ItemSpritesList spriteList = PlayerItem.Instance.spriteList;
    private SpriteGetter spritegetter = GameManager.Instance.spritegetter;

    // Start is called before the first frame update
    void Awake()

    {   
    
        if(doNotAwake)
            return;
        item = GameObject.Find("Database").GetComponent<LoadItemsDatabase>().GetItem(id);
        getSprite();
        
    }

    public void getSprite(){
        int ROW,COL,W,H,noOfSpritesPerCol,noOfSpritesPerRow;
        Sprite sprite;
        if(type=="icon")
        {
            
            sprite = spriteList.getItemIconSprite(item.cat);
            W=32;H=32;
            ROW = item.cords[0];
            COL = item.cords[1];
            noOfSpritesPerCol = 1;noOfSpritesPerRow=1;
            //poke_name.text = poke.pokeName;
            GetComponent<Image>().overrideSprite = spritegetter.getSprite(sprite,ROW,COL, W,H,noOfSpritesPerCol,noOfSpritesPerRow,dirRow,dirCol);
        
        }
        else if(type=="sprite"){
            sprite = spriteList.getItemSprite(item.cat);
            W=80;H=80;
            ROW = item.cords[0];
            COL = item.cords[1];
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
            
        item = GameObject.Find("Database").GetComponent<LoadItemsDatabase>().GetItem(id);
        getSprite();
    }

    
    
}


