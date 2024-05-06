using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PokemonLoader : MonoBehaviour
{   
    [SerializeField] public string id = "1";
    [SerializeField] public Pokemon poke;
    //[SerializeField] public SpriteGetter spritegetter;
    //[SerializeField] public PokemonSpritesList spriteList;
    [SerializeField] public string type ="over";
    [SerializeField] public int dirRow =1;
    [SerializeField] public int dirCol =2;
    [SerializeField] public TMP_Text poke_name;
    [SerializeField] public bool starter;
    [SerializeField] public TMP_Text[] currency;
    [SerializeField] public HpBox hpBox;
    [SerializeField] public bool doNotAwake = false;
    [SerializeField] public Image backdrop;
    

    private SpriteGetter spritegetter = GameManager.Instance.spritegetter;
    private PokemonSpritesList spriteList = PlayerPokemon.Instance.spriteList;
   

    // Start is called before the first frame update
    void Awake()
    {   
        if(doNotAwake)
            return;
        RefreshSprite();
        
    }

    public void SetCurrency(){
        
        if(hpBox==null)
            return;
        
        
        
        var user = AuthHandler.Instance.currentUser;
        backdrop.overrideSprite = ShopHandler.Instance.backdropSprites[user.backdropId];
        var curr = user.currency;
        for(int i=0;i<4;i++)
            currency[i].text = curr[i].ToString();
        
        hpBox.Refresh();
    }

    public void getSprite(){
        int ROW,COL,W,H,noOfSpritesPerCol,noOfSpritesPerRow,big;
        Sprite sprite;
        
        if(type=="over")
        {
            
            
            big = poke.overcords[2];
            sprite = spriteList.getPokemonOverworldSprite(poke.types[0],big,poke.shiny);
            if(big==0 || big==3){
                W=32;H=32;
            }else if(big==1 || big==4){
                W=64;H=64;
            }else{W=40;H=40;}
            ROW = poke.overcords[0];
            COL = poke.overcords[1];
            noOfSpritesPerCol = 3;noOfSpritesPerRow=4;
            poke_name.text = AuthHandler.Instance.currentUser.name + '\n' +poke.pokeName;
            GetComponent<Image>().overrideSprite = spritegetter.getSprite(sprite,ROW,COL, W,H,noOfSpritesPerCol,noOfSpritesPerRow,dirRow,dirCol);
        
        }
        else if(type=="sprite"){
            sprite = spriteList.getPokemonSprite(poke.types[0],poke.shiny);
            W=96;H=96;
            ROW = poke.spritecords[0];
            COL = poke.spritecords[1];
            noOfSpritesPerCol = 1;noOfSpritesPerRow=1;
            GetComponent<Image>().overrideSprite =  spritegetter.getSprite(sprite,ROW,COL, W,H,noOfSpritesPerCol,noOfSpritesPerRow,dirRow,dirCol);
        }
        else if(type=="menu"){
            sprite = spriteList.getPokemonIcon(poke.types[0],poke.shiny);
            W=68;H=56;
            ROW = poke.spritecords[0];
            COL = poke.spritecords[1];
            noOfSpritesPerCol = 1;noOfSpritesPerRow=1;
            poke_name.text = poke.pokeName;
            GetComponent<Image>().overrideSprite =  spritegetter.getSprite(sprite,ROW,COL, W,H,noOfSpritesPerCol,noOfSpritesPerRow,dirRow,dirCol);
        }

        

    }

    
    // public Move[] GetMoveList(){
    //     return poke.moveList;
    // }
    
    public  async  void RefreshSprite(){
        SetCurrency();
        if(starter){
            poke = PlayerPokemon.Instance.GetStarter();
        }else{
            poke = GameObject.Find("Database").GetComponent<LoadPokemonDatabase>().GetPokemon(id);
            poke.calculateStats();
        }
        
        getSprite();
        
    }
    
}


