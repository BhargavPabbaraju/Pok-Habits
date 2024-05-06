using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Trainer
{
    // Start is called before the first frame update
    
    public int id;
    public string trName;
    public int gen;
    public string type;
    public string poke;
    public int[] cords;
    public int item = 0;
    
   

    public Trainer(Trainer t){
        id = t.id;
        trName = t.trName;
        gen = t.gen;
        type = t.type;
        poke = t.poke;
        cords = t.cords;
        item = t.item;
        
        
    }

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
