using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Food
{
    // Start is called before the first frame update
    
    public int id;
    public string foodName;
    public int exp;
    public string desc;
    public string cost;
    public int[] cords;
    public int fullness;
    public int berry;
    public string flavors;

    public int smoothness;

    public int quantity;

    
   

    public Food(Food f){
        id = f.id;
        foodName = f.foodName;
        cost = f.cost;
        //desc = f.desc;
        flavors = f.flavors;
        exp = f.exp;
        cords = f.cords;
        fullness = f.fullness;
        berry = f.berry;

        
        quantity=0;
        smoothness = 5*Random.Range(4,13);
        
    }

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetActualExp(int exp,int nature,string flavor,int heldItem){
        if(nature%6==0)
            return exp;
        switch(flavor){
            case "Spicy":{
                if(nature<5 || heldItem==334)
                    return Mathf.CeilToInt((exp*1.5f));
                else if(nature%5==0){
                    return Mathf.FloorToInt((exp*0.5f));
                }
                else{
                    return exp;
                }
            }
            case "Sour":{
                if(nature<10 || heldItem==335)
                    return Mathf.CeilToInt((exp*1.5f));
                else if(nature%5==1){
                    return Mathf.FloorToInt((exp*0.5f));
                }
                else{
                    return exp;
                }
            }
            case "Sweet":{
                if(nature<15 || heldItem==333)
                    return Mathf.CeilToInt((exp*1.5f));
                else if(nature%5==2){
                    return Mathf.FloorToInt((exp*0.5f));
                }
                else{
                    return exp;
                }
            }
            case "Dry":{
                if(nature<20 || heldItem==331)
                    return Mathf.CeilToInt((exp*1.5f));
                else if(nature%5==3){
                    return Mathf.FloorToInt((exp*0.5f));
                }
                else{
                    return exp;
                }
            }
            case "Bitter":{
                if(nature<25 || heldItem==332)
                    return Mathf.CeilToInt((exp*1.5f));
                else if(nature%5==4){
                    return Mathf.FloorToInt((exp*0.5f));
                }
                else{
                    return exp;
                }
            }
        }
        return exp;

    }
}
