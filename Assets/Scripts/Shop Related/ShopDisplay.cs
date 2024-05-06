using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ShopDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_Text title;
    [SerializeField] public GameObject child;
    [SerializeField] public Transform parent;
    [SerializeField] public TMP_Text refreshText;
    


    void Start()
    {
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        if(title.text=="Medicines"){
            var lastUpdate = ShopHandler.Instance.shop.lastUpdate;
            var x = TimeHandler.getTimeRemaining(lastUpdate,360,true);
            refreshText.text = "Refreshes in "+x;
            
        }
    }

    public void Refresh(){

        while(parent.childCount != 1){
                DestroyImmediate(parent.GetChild(0).gameObject);
            }
        
        switch(title.text){
            case "Medicines":
                RefreshItems(0);
                break;
            
            case "Foods":
                RefreshFoods();
                break;
            
            case "Items":
                RefreshItems(1);
                break;
            
            case "Pokéballs":
                RefreshItems(2);
                break;

            case "Eggs":
                RefreshEggs();
                break;
            
            case "Trainers":
                RefreshTrainer();
                break;
            
            case "Backdrops":
                RefreshBackdrop();
                break;
                

        }
        

        DestroyImmediate(parent.GetChild(0).gameObject);

    }

    public void Disable(){
        title.gameObject.SetActive(false);
    }
    

    
    
    public void RefreshFoods(){
        var list = ShopHandler.Instance.shop.foods;
        if(list.Count<1){
            Disable();
            return;
        }
        foreach(var f in list){
            var clone = Instantiate(child);
            clone.transform.SetParent(parent);
            
            var shopCard = clone.GetComponent<ShopCard>();
            var foodLoader = shopCard.foodLoader;

            foodLoader.id = f;
            foodLoader.RefreshSprite();
            shopCard.Refresh();
        }
    }

    public void RefreshItems(int typ){
        List<int> list;
        list = ShopHandler.Instance.shop.items;
        if(typ==0)
            list = ShopHandler.Instance.shop.medicines;
        else if(typ==2)
             list = ShopHandler.Instance.shop.pokeballs;

        if(list.Count<1){
            Disable();
            return;
        }

        foreach(var i in list){
            var clone = Instantiate(child);
            clone.transform.SetParent(parent);
            
            var shopCard = clone.GetComponent<ShopCard>();
            var itemLoader = shopCard.itemLoader;

            itemLoader.id = i;
            itemLoader.RefreshSprite();
            shopCard.Refresh();
        }
    }

    public void RefreshEggs(){
        var list = ShopHandler.Instance.shop.eggs;
        if(list.Count<1){
            Disable();
            return;
        }
        foreach(var e in list){
            var clone = Instantiate(child);
            clone.transform.SetParent(parent);
            
            var shopCard = clone.GetComponent<ShopCard>();
            var eggLoader = shopCard.eggLoader;

            eggLoader.id = e;
            eggLoader.RefreshSprite();
            shopCard.Refresh();
        }
    }

    public void RefreshTrainer(){
        var id = Random.Range(1,488);
        var trainerList = AuthHandler.Instance.currentUser.trainers;
        
        if (trainerList.Contains(id)){
            Disable();
            return;
        }
            
        
        var clone = Instantiate(child);
        clone.transform.SetParent(parent);
        
        var shopCard = clone.GetComponent<ShopCard>();
        var trLoader = shopCard.trainerLoader;

        trLoader.id = id;
        trLoader.RefreshSprite();
        shopCard.Refresh();
    }

    public void RefreshBackdrop(){
        var id = Random.Range(0,36);
        var backList = AuthHandler.Instance.currentUser.backdrops;
        
        if (backList.Contains(id)){
            Disable();
            return;
        }
            
        
        var clone = Instantiate(child);
        clone.transform.SetParent(parent);
        
        var shopCard = clone.GetComponent<ShopCard>();
        
        shopCard.backdropIndex = id;
        shopCard.Refresh();
    }

}
