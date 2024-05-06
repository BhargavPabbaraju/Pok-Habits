using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ribbon
{
    // Start is called before the first frame update
    
    public int id;
    public string ribbonName;
    public string desc;
    public string price;
    public string date;
    public int[] cords;

   

    public Ribbon(Ribbon r){
        id = r.id;
        ribbonName = r.ribbonName;
        desc = r.desc;
        cords = r.cords;
    
        
    }

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
