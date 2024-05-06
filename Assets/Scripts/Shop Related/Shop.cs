using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shop
{
    // Start is called before the first frame update
    
    public List<int> medicines;
    public List<int> foods;
    public List<int> items;
    public List<int> pokeballs;
    public List<string> eggs;
    public string lastUpdate;

    public int[] validMedicines = new int[] {49,50,51,54,58,59,62,64,65,66,68,70,71,76,77,78,79,81,83,84,89,90,92,93,96,98};
    
   

    public Shop(){
        
        
    }

    public void NewShop(){
        medicines=GetMedicines(1,8);
        foods=GetStuff(42,216,4,20);
        items=GetStuff(100,544,4,20);
        pokeballs=GetStuff(1,48,0,8);
        eggs = getEggs();
    }

    public List<string> getEggs(){
        int min = 0;
        int max = 4;
        var e = new List<string>();
        var r = Random.Range(min,max+1);
        Egg x;
        for(var i=0;i<r;i++){
            x = GatchaSystem.getHabitEgg(1);
            while(e.Contains(x.id)){
                x = GatchaSystem.getHabitEgg(1);
            }
            e.Add(x.id);
        }
        return e;
    }

    public  List<int> GetStuff(int start,int end,int min,int max){
        List<int> l = new List<int>();
        var r = Random.Range(min,max+1);
        int x;
        for(var i=0;i<r;i++){
            x = Random.Range(start,end+1);
            while(l.Contains(x) || x==115){
                x = Random.Range(start,end+1);
            }
            l.Add(x);
        }
        return l;

    }

    public  List<int> GetMedicines(int min,int max){
        List<int> l = new List<int>();
        var r = Random.Range(min,max+1);
        int x;
        for(var i=0;i<r;i++){
            x = Random.Range(0,validMedicines.Length);
            while(l.Contains(validMedicines[x])){
                x = Random.Range(0,validMedicines.Length);
            }
            l.Add(validMedicines[x]);
        }
        return l;

    }

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
