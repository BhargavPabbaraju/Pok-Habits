using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadItemsDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    public Item item;
    public List<Item> itemDatabase = new List<Item>();
   
   
    public void LoadData(){
        itemDatabase.Clear();
        
        //Read Csv Filesystem
        List<string> files = new List<string> {"items data"};
        foreach (var file in files){
            List<Dictionary<string,object>> data = CSVReader.Read(file);
            for (var i = 0; i < data.Count;i++){

                int id = int.Parse(data[i]["ID"].ToString());
                string itemName = data[i]["Name"].ToString();

        
                string cat = data[i]["Category"].ToString();
                string desc = data[i]["Desc"].ToString();

                string cost = data[i]["Price"].ToString();

                int row = int.Parse(data[i]["row"].ToString());
                int col = int.Parse(data[i]["col"].ToString());
                
                string effect = data[i]["Effect"].ToString();
                
                
                AddItem(id,itemName,cat,desc,cost,row,col,effect);
            }
        }
        
    }

    void AddItem(int id,string itemName,string cat,string desc,string cost,int row,int col,string effect){
        Item temp = new Item(item);
        temp.id = id;
        temp.itemName =itemName;
        temp.cat = cat;
        temp.desc = desc;
        temp.cost = cost;
        temp.cords = new int [] {row,col};
        temp.effect = effect;
        itemDatabase.Add(temp);
    }

   
    

    public Item GetItem(int id){

        //Debug.Log(id);

        int i;
        try{
            i = id-1;
            return itemDatabase[i];
        }catch{
            i = Random.Range(0, itemDatabase.Count);
            return itemDatabase[i];
        }
        
        //Debug.Log(trainerDatabase[i].trName);
        
    }
}
