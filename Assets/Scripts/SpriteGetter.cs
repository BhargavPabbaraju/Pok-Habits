using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteGetter : MonoBehaviour

{

    [SerializeField] public Sprite sprite;
    [SerializeField] public Image image;
    [SerializeField] public int ROW;
    [SerializeField] public int COL;
    [SerializeField] public int W;
    [SerializeField] public int H;
    [SerializeField] public int noOfSpritesPerCol;
    [SerializeField] public int noOfSpritesPerRow;
    [SerializeField] public int cropRow;
    [SerializeField] public int cropCol;
    // Start is called before the first frame update
    public static bool loaded = false;

     void Awake()
    {
        
        //int ROW,COL,W,H,noOfSpritesPerCol,noOfSpritesPerRow,r,c;
        //ROW=2;COL=1;W=32;H=32;noOfSpritesPerCol=3;noOfSpritesPerRow=4;r=1;c=2;

        // cropCol=(COL*noOfSpritesPerCol+cropCol)*W;
        // cropRow=(ROW*noOfSpritesPerRow+cropRow+1)*H;

        // var rect = new Rect(cropCol,texture.height-cropRow, W, H);
        // var newSprite = Sprite.Create(texture, rect, Vector2.up);
        
        // image.overrideSprite = newSprite;

        
        
        if(loaded)
            DestroyImmediate(gameObject);
        else{
            loaded = true;
            GameManager.Instance.spritegetter = this;
            DontDestroyOnLoad(gameObject);
            GameObject.Find("Database").GetComponent<LoadPokemonDatabase>().LoadData();
            GameObject.Find("Database").GetComponent<LoadTrainersDatabase>().LoadData();
            GameObject.Find("Database").GetComponent<LoadEggDatabase>().LoadData();
            GameObject.Find("Database").GetComponent<LoadAbilityDatabase>().LoadData();
            GameObject.Find("Database").GetComponent<LoadItemsDatabase>().LoadData();
            GameObject.Find("Database").GetComponent<LoadRibbonDatabase>().LoadData();
            GameObject.Find("Database").GetComponent<LoadFoodData>().LoadData();
        }
    }

    public Sprite getSprite(Sprite sprite,int ROW,int COL, int W, int H, int noOfSpritesPerCol,int noOfSpritesPerRow,int cropRow,int cropCol ){

        var texture = sprite.texture;
        cropCol=(COL*noOfSpritesPerCol+cropCol)*W;
        cropRow=(ROW*noOfSpritesPerRow+cropRow+1)*H;

        var rect = new Rect(cropCol,texture.height-cropRow, W, H);
        var newSprite = Sprite.Create(texture, rect, Vector2.up);
        return newSprite;
    }

    



}
