using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopCard : MonoBehaviour

{

    public static Dictionary<string,string> eggCosts = new Dictionary<string,string>{
        {"C","500_S"},
        {"UC","500_G"},
        {"R","1000_B"},
        {"EX","5000_P"},
        {"UX","9999_P"}
    };
    public static Dictionary<string,string> trainerCosts = new Dictionary<string,string>{
        {"BF","5000_G"},
        {"ETA","3000_G"},
        {"ETG","1000_G"},
        {"NPC","100_S"},
        {"PL","500_S"},
        {"PROF","100_B"},
        {"RIV","750_S"},
        {"TR","250_G"},
        {"TRK","50_G"},
        {"CHA","9999_P"},
        {"EF","5000_P"},
        {"ETL","7500_P"},
        {"GL","2000_P"}
    };

    [SerializeField] public TMP_Text qty;
    [SerializeField] public TMP_Text cost;
    
    [SerializeField] public Image curr;
    [SerializeField] public Sprite[] currencySprites;

    [SerializeField] public Image rarity;
    [SerializeField] public Sprite[] raritySprites;

    [SerializeField] public Image backdrop;
    [SerializeField] public Sprite[] backdropSprites;


    [SerializeField] public ItemLoader itemLoader;
    [SerializeField] public FoodLoader foodLoader;
    [SerializeField] public EggLoader eggLoader;
    [SerializeField] public TrainerLoader trainerLoader;
    [SerializeField] public int backdropIndex;

    [SerializeField] public GameObject banner;


    [SerializeField] public string type;
    int cur;
    // Start is called before the first frame update
    void Awake(){
        Refresh();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetItemQty(){
        var item = itemLoader.item;
        if(PlayerItem.Instance.itemDic!=null && PlayerItem.Instance.itemDic.ContainsKey(item.id)){
            return PlayerItem.Instance.itemDic[item.id];
        }
        return 0;
    }

    public int GetFoodQty(){
        var food = foodLoader.food;
        if(PlayerFood.Instance.foodDic!=null && PlayerFood.Instance.foodDic.ContainsKey(food.id)){
            return PlayerFood.Instance.foodDic[food.id];
        }
        return 0;
    }

    public void SetCurr(string cure){
        switch(cure){
            case "S":
            cur = 0;
            //curr.overrideSprite = currencySprites[0];
            break;

            case "G":
            cur = 1;
            //curr.overrideSprite = currencySprites[1];
            break;

            case "B":
            cur = 2;
            //curr.overrideSprite = currencySprites[2];
            break;

            case "P":
            cur = 3;
            //curr.overrideSprite = currencySprites[3];
            break;

        }
        curr.overrideSprite = currencySprites[cur];
    }

    public int GetRarityIndex(string rarity){
        switch(rarity){
            case "C":
                return 0;
                break;
            case "UC":
                return 1;
                break;
            case "R":
                return 2;
                break;
            case "EX":
                return 3;
                break;
            case "UX":
                return 4;
                break;
        }
        return 0;
    }


    public void OnClick(){
        ShopHandler.Instance.shopCard = this;
        ShopHandler.Instance.currencySprite = curr.overrideSprite;
        ShopHandler.Instance.shopCost = cost.text;
        ShopHandler.Instance.selectedCurr = cur;

        if(type=="item"){
            PlayerItem.Instance.selectedItem = itemLoader.item;
            GameManager.Instance.shopPopUp.BuyItemCard();
        }
        else if(type=="food"){
            PlayerFood.Instance.selectedFood = foodLoader.food;
            GameManager.Instance.shopPopUp.BuyFoodCard();
        }
        else if(type=="trainer"){
            PlayerTrainer.Instance.selectedTrainer = trainerLoader.tr;
            GameManager.Instance.shopPopUp.BuyTrainerCard();
        }
        else if(type=="backdrop"){
            ShopHandler.Instance.selectedBackdrop = backdrop.overrideSprite;
            ShopHandler.Instance.selectedBackdropIndex = backdropIndex;
            GameManager.Instance.shopPopUp.BuyBackdropCard();
        }
        else if(type=="egg"){
            PlayerEgg.Instance.raritySprite = rarity.overrideSprite;
            PlayerEgg.Instance.selectedEgg = eggLoader.egg;
            GameManager.Instance.shopPopUp.BuyEggCard();
        }
    }


    public void Refresh(){
        if(type=="item"){
            var item = itemLoader.item;
            qty.text = GetItemQty().ToString();
            cost.text = item.cost.Split('_')[0];

            try{
                SetCurr(item.cost.Split('_')[1]);
            }catch{
                Debug.Log(item.id+" "+item.cost);
            }
            
        }
        else if(type=="food"){
            var food = foodLoader.food;
            qty.text = GetFoodQty().ToString();
            cost.text = food.cost.Split('_')[0];
            try{
                SetCurr(food.cost.Split('_')[1]);
            }catch{
                Debug.Log(food.id+" "+food.cost);
            }
            
            
        }

        else if(type=="egg"){
            var egg = eggLoader.egg;
            try{
                cost.text = eggCosts[egg.rarity].Split('_')[0];
                rarity.overrideSprite = raritySprites[GetRarityIndex(egg.rarity)];
                SetCurr(eggCosts[egg.rarity].Split('_')[1]);
            }catch{
                Debug.Log(egg.id);
            }
        }
        
        else if(type=="trainer"){
            var tr = trainerLoader.tr;
            try{
                cost.text = trainerCosts[tr.type].Split('_')[0];
                SetCurr(trainerCosts[tr.type].Split('_')[1]);
            }catch{
                Debug.Log(tr.id);
            }
        }
        else if(type=="backdrop"){
            backdrop.overrideSprite = backdropSprites[backdropIndex];
            
        }
        
    }


    public void Buy(){
        if(type=="item"){
            PlayerItem.Instance.AddItem(itemLoader.item);
            qty.text = GetItemQty().ToString();

        }else if(type=="food"){
            PlayerFood.Instance.AddFood(foodLoader.food);
            qty.text = GetFoodQty().ToString();
        }else if(type=="trainer"){
            PlayerTrainer.Instance.AddTrainer(trainerLoader.tr);
            GetComponent<Button>().interactable = false;
            banner.SetActive(true);
            
        }else if(type=="backdrop"){
            PlayerTrainer.Instance.AddBackdrop(backdropIndex);
            GetComponent<Button>().interactable = false;
            banner.SetActive(true);
        }
        else if(type=="egg"){
            PlayerEgg.Instance.AddEgg(eggLoader.egg);
        }


        
    }
}
