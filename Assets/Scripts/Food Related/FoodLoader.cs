using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodLoader : MonoBehaviour
{   
    [SerializeField] public int id = 1;
    [SerializeField] public Food food;

    [SerializeField] public string type ="icon";
    [SerializeField] public int dirRow =0;
    [SerializeField] public int dirCol =0;
    [SerializeField] public bool doNotAwake = false;
    //[SerializeField] public TMP_Text poke_name;

    private Sprite foodSprite = PlayerFood.Instance.foodSprite;
    private SpriteGetter spritegetter = GameManager.Instance.spritegetter;

    // Start is called before the first frame update
    void Awake()

    {   
        if(doNotAwake)
            return;
        food = GameObject.Find("Database").GetComponent<LoadFoodData>().GetFood(id);
        getSprite();
        
    }

    public void getSprite(){
        int ROW,COL,W,H,noOfSpritesPerCol,noOfSpritesPerRow;
        Sprite sprite;
        if(type=="icon")
        {
            
            sprite = foodSprite;
            W=24;H=24;
            ROW = food.cords[0];
            COL = food.cords[1];
            noOfSpritesPerCol = 1;noOfSpritesPerRow=1;
            //poke_name.text = poke.pokeName;
            GetComponent<Image>().overrideSprite = spritegetter.getSprite(sprite,ROW,COL, W,H,noOfSpritesPerCol,noOfSpritesPerRow,dirRow,dirCol);
        
        }
        else if(type=="sprite"){
            sprite = foodSprite;
            W=80;H=80;
            ROW = food.cords[0];
            COL = food.cords[1];
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
        food = GameObject.Find("Database").GetComponent<LoadFoodData>().GetFood(id);
        getSprite();
    }

    
    
}


