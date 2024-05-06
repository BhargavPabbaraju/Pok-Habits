using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    // Start is called before the first frame update
    
    public int id;
    public string itemName;
    public string cat;
    public string desc;
    public string cost;
    public int[] cords;
    public string effect;

   

    
   

    public Item(Item i){
        id = i.id;
        itemName = i.itemName;
        cat = i.cat;
        desc = i.desc;
        cost = i.cost;
        cords = i.cords;
        effect = i.effect;
  
        
    }

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
