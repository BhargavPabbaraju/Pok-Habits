using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopPopUp : MonoBehaviour{
    

    [SerializeField] public GameObject[] objects;//0-decrement 1-item 2-food 3-trainer 4-backdrop 5-egg 6-notenuf
    [SerializeField] public GameObject[] staticObjects; 

    [SerializeField] public EggLoader eggSprite;
    [SerializeField] public Image rarity;
    [SerializeField] public TrainerLoader trSprite;
    [SerializeField] public FoodLoader foodSprite;
    [SerializeField] public ItemLoader itemSprite;

    [SerializeField] public TMP_Text[] name; 
    [SerializeField] public TMP_Text desc; 


    [SerializeField] public Image backdrop;

    [SerializeField] public Image[] currency;
    [SerializeField] public TMP_Text[] cost; 

    [SerializeField] public TMP_Text[] currencyTexts; 


    string[] backdropNames = new string[] {
        "Dress Up",
        "Ranch",
        "City at Night",
        "Snowy Town",
        "Fiery",
        "Outer Space",
        "Cumulus Cloud",
        "Desert",
        "Flower Patch",
        "Future Room",
        "Open Sea",
        "Total Darkness",
        "Tatami Room",
        "Gingerbread Room",
        "Seafloor",
        "Underground",
        "Sky",
        "Theater",
        "Bug",
        "Dark",
        "Dragon",
        "Electric",
        "Fairy",
        "Fighting",
        "Fire",
        "Flying",
        "Ghost",
        "Grass",
        "Ground",
        "Ice",
        "Normal",
        "Poison",
        "Psychic",
        "Rock",
        "Steel",
        "Water"
    };


    public Animator animator;


    
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.shopPopUp = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void setStateFalse(){
        for(int i=0;i<objects.Length;i++){
            objects[i].SetActive(false);
        }
        for(int i=0;i<staticObjects.Length;i++){
            staticObjects[i].SetActive(false);
        }
        
    }

    public void doPop(int index){
        objects[index].SetActive(true);
        if(index>0){
            for(int i=0;i<staticObjects.Length;i++){
                staticObjects[i].SetActive(true);
            }
            SetCurrency();
            if(index>2 && index!=6){
                desc.gameObject.SetActive(false);
            }
            if(index==5){
                name[0].gameObject.SetActive(false);
                
            }
        }
        animator.SetTrigger("pop");
    }

    
    

    

        
    

    public void BuyItemCard(){
        setStateFalse();
        var item = PlayerItem.Instance.selectedItem;
        itemSprite.doNotAwake = true;
        itemSprite.item = item;
        itemSprite.getSprite();

        name[0].text = "<color=\"black\">" + item.itemName;
        desc.text = "<color=\"grey\">" + item.desc;
        
        currency[0].overrideSprite = ShopHandler.Instance.currencySprite;
        cost[0].text = ShopHandler.Instance.shopCost;
        
        doPop(1);
        


    }

    public void BuyFoodCard(){
        setStateFalse();
        var food = PlayerFood.Instance.selectedFood;
        foodSprite.doNotAwake = true;
        foodSprite.food = food;
        foodSprite.getSprite();

        name[0].text = "<color=\"black\">" + food.foodName;
        desc.text = "<color=\"grey\">" + food.desc;
        
        currency[0].overrideSprite = ShopHandler.Instance.currencySprite;
        cost[0].text = ShopHandler.Instance.shopCost;
        
        doPop(2);
        


    }

    public void BuyTrainerCard(){
        setStateFalse();
        var tr = PlayerTrainer.Instance.selectedTrainer;
        trSprite.doNotAwake = true;
        trSprite.tr = tr;
        trSprite.getSprite();

        name[0].text = "<color=\"black\">" + tr.trName;
        
        
        currency[0].overrideSprite = ShopHandler.Instance.currencySprite;
        cost[0].text = ShopHandler.Instance.shopCost;
        
        doPop(3);
        


    }
    public void BuyBackdropCard(){
        setStateFalse();


        name[0].text = "<color=\"black\">" + backdropNames[ShopHandler.Instance.selectedBackdropIndex];
        
        backdrop.overrideSprite = ShopHandler.Instance.selectedBackdrop;
        currency[0].overrideSprite = ShopHandler.Instance.currencySprite;
        cost[0].text = ShopHandler.Instance.shopCost;
        
        doPop(4);
        


    }
    public void BuyEggCard(){
        setStateFalse();
        var egg = PlayerEgg.Instance.selectedEgg;
        eggSprite.doNotAwake = true;
        eggSprite.egg = egg;
        eggSprite.getSprite();


        rarity.overrideSprite = PlayerEgg.Instance.raritySprite;
        currency[0].overrideSprite = ShopHandler.Instance.currencySprite;
        cost[0].text = ShopHandler.Instance.shopCost;
        name[0].text = "egg";
        
        doPop(5);
        


    }

    public void SetCurrency(){
        var user = AuthHandler.Instance.currentUser;
        var curr = user.currency;
        for(int i=0;i<4;i++)
            currencyTexts[i].text = curr[i].ToString();
    }

    public void Close(){
        animator.SetTrigger("close");
        setStateFalse();
    }

    public void Buy(){
        if(ShopHandler.Instance.CheckCanBuy()){
            ShopHandler.Instance.shopCard.Buy();
            setStateFalse();
            name[1].text = "<color=black>Bought "+name[0].text;
            cost[1].text = "<color=black>-"+cost[0].text;
            currency[2].overrideSprite = currency[0].overrideSprite;
            doPop(0);
            StartCoroutine(waiter());
        }
        else{
            currency[1].overrideSprite = currency[0].overrideSprite;
            doPop(6);
        }
    }

    public void GoBack(){
        objects[6].SetActive(false);
    }

   

    


    


    

    
    

    IEnumerator waiter(){
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("close");
        setStateFalse();
        
    }

    
    
}
