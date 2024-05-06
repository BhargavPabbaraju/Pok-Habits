using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Ability{
        public string title;
        public string des1;
        public string des2;

        public Ability(string title,string des1,string des2){
            this.title = title;
            this.des1 = des1;
            this.des2 = des2;
        }
    }

public class LoadAbilityDatabase : MonoBehaviour
{

    

    // Start is called before the first frame update
    public List<Ability> abilityDatabase = new List<Ability>();
    //public List<string> abiIds = new List<string>();
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
        abilityDatabase.Clear();

        //Read Csv Filesystem
        List<string> files = new List<string> {"abilities"};
        foreach (var file in files){
            List<Dictionary<string,object>> data = CSVReader.Read(file);
            for (var i = 0; i < data.Count;i++){
                string title = data[i]["Ability"].ToString();


                string des1 = data[i]["Desc1"].ToString();
                string des2 = data[i]["Des2"].ToString();
            
                
                
                AddAbility(title,des1,des2);
            }
        }
        
    }

    void AddAbility(string title,string des1,string des2){
        Ability temp = new Ability(title,des1,des2);

        abilityDatabase.Add(temp);
    }

   
    

    public Ability GetAbility(int id){

        return abilityDatabase[id-1];
   
            
            //Debug.Log(pokemonIds.Count);
        

        //Debug.Log(pokemonDatabase[i].pokeName);
        
    }
}
