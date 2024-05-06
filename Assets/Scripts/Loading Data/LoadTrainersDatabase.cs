using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTrainersDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    public Trainer trainer;
    public List<Trainer> trainerDatabase = new List<Trainer>();
   
   
    public void LoadData(){
        trainerDatabase.Clear();
        
        //Read Csv Filesystem
        List<string> files = new List<string> {"trainer data"};
        foreach (var file in files){
            List<Dictionary<string,object>> data = CSVReader.Read(file);
            for (var i = 0; i < data.Count;i++){
                int id = int.Parse(data[i]["ID"].ToString());
                string trName = data[i]["Name"].ToString();
                int gen = int.Parse(data[i]["gen"].ToString());
        
                string type = data[i]["Type"].ToString();
                string poke = data[i]["Poke"].ToString();

                int row = int.Parse(data[i]["row"].ToString());
                int col = int.Parse(data[i]["col"].ToString());
                
                int item = int.Parse(data[i]["Item"].ToString());
                
                AddTrainer(id,trName,gen,type,poke,row,col,item);
            }
        }
        
    }

    void AddTrainer(int id,string trName,int gen,string type,string poke,int row,int col,int item){
        Trainer temp = new Trainer(trainer);
        temp.id = id;
        temp.trName =trName;
        temp.gen = gen;
        temp.type = type;
        temp.poke = poke;
        temp.cords = new int [] {row,col};
        temp.item = item;
        trainerDatabase.Add(temp);
    }

   
    

    public Trainer GetTrainer(int id){

        int i;
        try{
            i = id-1;
            return trainerDatabase[i];
        }catch{
            i = Random.Range(0, trainerDatabase.Count);
            return trainerDatabase[i];
        }
        
        //Debug.Log(trainerDatabase[i].trName);
        
    }
}
