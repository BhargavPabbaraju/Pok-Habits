using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class LoadEggDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<string,List<Egg>> eggDic = new Dictionary<string,List<Egg>>();
    public Egg egg;
    public List<Egg> eggDatabase = new List<Egg>();
    public List<string> eggIds = new List<string>();
    public static bool loaded = false;

    void Awake(){
        if(loaded)
            DestroyImmediate(gameObject);
        else{
            loaded = true;
            DontDestroyOnLoad(gameObject);
        }
        
    }
    
    public void LoadData(){
        eggDatabase.Clear();
        eggIds.Clear();

        //Read Csv Filesystem
        List<string> files = new List<string> {"egg data"};
        foreach (var file in files){
            List<Dictionary<string,object>> data = CSVReader.Read(file);
            for (var i = 0; i < data.Count;i++){
                string id = data[i]["ID"].ToString();
                int gen = int.Parse(data[i]["GEN"].ToString());

                int genderRatio = int.Parse(data[i]["GENDER"].ToString());

                int row = int.Parse(data[i]["ROW"].ToString());
                int col = int.Parse(data[i]["COL"].ToString());

                string expType = data[i]["EXP"].ToString();
                
                int time = int.Parse(data[i]["TIME"].ToString());
                
                int friendship = int.Parse(data[i]["Friendship"].ToString());

                string rarity = data[i]["RARITY"].ToString();
                
                
                AddEgg(id,gen,row,col,expType,time,friendship,rarity,genderRatio);
            }
        }
        
    }

    void AddEgg(string id,int gen,int row,int col,string expType,int time,int friendship,string rarity,int genderRatio){
        Egg temp = new Egg(egg);
        temp.id = id;
        temp.gen = gen;
        temp.cords = new int [] {row,col};
        temp.expType = expType;
        temp.time = time;
        temp.friendship = friendship;
        temp.genderRatio = genderRatio;
        temp.rarity = rarity;
        eggDatabase.Add(temp);
        eggIds.Add(id);
        if(eggDic.ContainsKey(rarity))
            eggDic[rarity].Add(temp);
        else{
            var l = new List<Egg>();
            l.Add(temp);
            eggDic.Add(rarity,l);

        }
            
    }

   
    

    public Egg GetEgg(string id){

        int i;
        //id="";
        try{
            i = eggIds.IndexOf(id);
            return eggDatabase[i];
        }
        catch{
            i = Random.Range(0, eggIds.Count);
            return eggDatabase[i];
        }
        
   
            
            //Debug.Log(pokemonIds.Count);
        

        //Debug.Log(pokemonDatabase[i].pokeName);
        
    }

    public Egg GetEggByRarity(string rarity){
        int r = Random.Range(0,eggDic[rarity].Count);
        return eggDic[rarity][r];

    }
}
