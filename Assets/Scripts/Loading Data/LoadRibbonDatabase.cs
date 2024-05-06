using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRibbonDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    public Ribbon r;
    public List<Ribbon> ribbonDatabase = new List<Ribbon>();
   
   
    public void LoadData(){
        ribbonDatabase.Clear();
        
        //Read Csv Filesystem
        List<string> files = new List<string> {"ribbons"};
        foreach (var file in files){
            List<Dictionary<string,object>> data = CSVReader.Read(file);
            for (var i = 0; i < data.Count;i++){

                int id = int.Parse(data[i]["ID"].ToString());
                string ribbonName = data[i]["Name"].ToString();

        
                string desc = data[i]["Desc"].ToString();

                int row = int.Parse(data[i]["Row"].ToString());
                int col = int.Parse(data[i]["Col"].ToString());
                
                
                
                AddRibbon(id,ribbonName,desc,row,col);
            }
        }
        
    }

    void AddRibbon(int id,string ribbonName,string desc,int row,int col){
        Ribbon temp = new Ribbon(r);
        temp.id = id;
        temp.ribbonName =ribbonName;
        temp.desc = desc;
        temp.cords = new int [] {row,col};
        ribbonDatabase.Add(temp);
    }

   
    

    public Ribbon GetRibbon(int id){

        int i;
        try{
            i = id-1;
            return ribbonDatabase[i];
        }catch{
            i = Random.Range(0, ribbonDatabase.Count);
            return ribbonDatabase[i];
        }
        
        //Debug.Log(trainerDatabase[i].trName);
        
    }
}
