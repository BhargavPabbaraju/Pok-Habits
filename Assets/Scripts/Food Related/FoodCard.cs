using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodCard : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TMP_Text qtyText;
    [SerializeField] public TMP_Text foodName;
    [SerializeField] public FoodLoader foodLoader;
    [SerializeField] public TMP_Text smoothness;
    [SerializeField] public FoodLoader mainFood;
    [SerializeField] public int qty;
    [SerializeField] public GameObject optionsPane;
    [SerializeField] public GameObject[] flavors;
    Dictionary<string,GameObject> flavorLights; 


    void Awake(){
        flavorLights = new Dictionary<string,GameObject>{
            {"Spicy",flavors[0]},
            {"Dry",flavors[1]},
            {"Bitter",flavors[2]},
            {"Sour",flavors[3]},
            {"Sweet",flavors[4]},
        };
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFlavors(Food food){
        for(int i=0;i<flavors.Length;i++){
            flavors[i].SetActive(false);
            
        }
        foreach(var flavor in food.flavors.Split('_')){
            if(flavor=="All"){
                for(int i=0;i<flavors.Length;i++){
                    flavors[i].SetActive(true);
            
                }
                return;
            }
            else
                flavorLights[flavor].SetActive(true);
            
        }
        

    }

    public void OnClick(){
        mainFood.GetComponent<Image>().color = new Color(255,255,255,255);
        PlayerFood.Instance.selectedFood =  foodLoader.food;
        mainFood.id = foodLoader.food.id;
        SetFlavors(foodLoader.food);
        mainFood.RefreshSprite();
        smoothness.text = foodLoader.food.smoothness.ToString();
        optionsPane.SetActive(true);

    }
}
