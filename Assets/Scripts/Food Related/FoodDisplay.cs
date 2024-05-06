using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodDisplay : MonoBehaviour
{

    
    Dictionary<string,List<int>> foodDic;
    Dictionary<int,int> qts;

    [SerializeField] public GameObject child;
    [SerializeField] public Transform parent;
    [SerializeField] public int screen = 0;
    
    
 

    string[] cats = new string[] {"All","Spicy","Dry","Bitter","Sour","Sweet"};
    



    void Awake(){
        
        InitializeLists();
        foreach(var id in PlayerFood.Instance.foodDic){
            AddToDic(id.Key,id.Value);
       }
       Refresh();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void InitializeLists(){
        foodDic = new Dictionary<string,List<int>>();
        
        foreach(var cat in cats){
            foodDic[cat] = new List<int>();
        }
        qts = new Dictionary<int,int>();
        
    }

    
    void AllFlavors(int id){
        foreach(var cat in cats){
                foodDic[cat].Add(id);
            }
    }

    void AddToDic(int id,int qty){
        qts.Add(id,qty);
        var food = GameObject.Find("Database").GetComponent<LoadFoodData>().GetFood(id);
        foreach(var flavor in food.flavors.Split('_')){
            if(flavor!="All")
                foodDic[flavor].Add(id);
            else{
                AllFlavors(id);
                return;
            }
        }
        
        foodDic["All"].Add(id);
        
    }

   

    public void Refresh(){
        //SetStates(screen);
        DisplayLists(cats[screen]);
        
    }


    public void DisplayLists(string cat){

        while(parent.childCount != 1){
            DestroyImmediate(parent.GetChild(0).gameObject);
        }

        child = parent.GetChild(0).gameObject;
        parent.GetChild(0).gameObject.SetActive(true);

        foreach(int id in foodDic[cat]){
            var clone = Instantiate(child,parent);
            clone.transform.localScale = new Vector3(1f,1f,1f);
            var foodCard = clone.GetComponent<FoodCard>();

            
            foodCard.foodLoader.id = id;
            foodCard.foodLoader.RefreshSprite();
            //Debug.Log(foodCard.foodLoader.id+" "+foodCard.foodLoader.food.id);
            foodCard.foodName.text = foodCard.foodLoader.food.foodName;
            foodCard.qty = qts[id];
            foodCard.qtyText.text = $"x{foodCard.qty}";
        }

       
       
       parent.GetChild(0).gameObject.SetActive(false);


    }

    public void changeScene(int scene){
        if(screen==scene)
            return;
        screen = scene;
        Refresh();
    }

    // Update is called once per frame
    
}
