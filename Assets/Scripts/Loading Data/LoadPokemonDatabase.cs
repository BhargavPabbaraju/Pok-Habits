using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPokemonDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    public Pokemon pokemon;
    //public List<Pokemon> pokemonDatabase = new List<Pokemon>();
    public List<string> pokemonIds = new List<string>();

    public Dictionary<string,Pokemon> pokemonDatabase = new Dictionary<string,Pokemon>();

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
        pokemonDatabase = new Dictionary<string,Pokemon>();
        pokemonIds.Clear();

        //Read Csv Filesystem
        List<string> files = new List<string> {"pokemon data","form differences"};
        foreach (var file in files){
            List<Dictionary<string,object>> data = CSVReader.Read(file);
            for (var i = 0; i < data.Count;i++){
                string id = data[i]["ID"].ToString();
                string pokeName = data[i]["NAME"].ToString();
                int gen = int.Parse(data[i]["GENERATION"].ToString());
                int number = int.Parse(data[i]["NUMBER"].ToString());

                string type1 = data[i]["TYPE1"].ToString();
                string type2 = data[i]["TYPE2"].ToString();
                string color = data[i]["COLOR"].ToString();

                float ht = float.Parse(data[i]["HEIGHT"].ToString());
                float wt = float.Parse(data[i]["WEIGHT"].ToString());

                int hp = int.Parse(data[i]["HP"].ToString());
                int atk = int.Parse(data[i]["ATK"].ToString());
                int def = int.Parse(data[i]["DEF"].ToString());
                int spatk = int.Parse(data[i]["SP_ATK"].ToString());
                int spdef = int.Parse(data[i]["SP_DEF"].ToString());
                int speed = int.Parse(data[i]["SPD"].ToString());

                int overx = int.Parse(data[i]["OVERX"].ToString());
                int overy = int.Parse(data[i]["OVERY"].ToString());
                int big = int.Parse(data[i]["BIG"].ToString());

                int spritex = int.Parse(data[i]["SPRITEX"].ToString());
                int spritey = int.Parse(data[i]["SPRITEY"].ToString());

                string ability = data[i]["ABILITY"].ToString();
                string evolutions = data[i]["EVO"].ToString();

                
                
                AddPoke(id,pokeName,gen,number,type1,type2,color,ht,wt,hp,atk,def,spatk,spdef,speed,overx,overy,big,spritex,spritey,ability,evolutions);
            }
        }
        
    }

    void AddPoke(string id,string pokeName,int gen,int number,string type1,string type2,string color,float ht,float wt,
    int hp,int atk,int def,int spatk,int spdef,int speed,int overx,int overy,int big,int spritex,int spritey,string ability,string evolutions){
        Pokemon temp = new Pokemon(pokemon);
        temp.id = id;
        temp.pokeName =pokeName;
        temp.gen = gen;
        temp.number = number;
        temp.types = new string [] {type1,type2};
        temp.color = color;
        temp.ht = ht;
        temp.wt = wt;
        temp.basestats = new int [] {hp,atk,def,spatk,spdef,speed};
        temp.overcords = new int [] {overx,overy,big};
        temp.spritecords = new int [] {spritex,spritey};
        temp.abilities = new int[3];
        temp.evolutions = evolutions;

        for(int i=0;i<3;i++){
            var x = ability.Split('_')[i];
            temp.abilities[i] = int.Parse(x);
        }



        pokemonDatabase.Add(temp.id,temp);
        pokemonIds.Add(id);
    }

    public bool FemaleExists(string id){
        if(pokemonDatabase.ContainsKey(id+"F")){
            return true;
        }
        return false;
    }

   
    

    public Pokemon GetPokemon(string id){
        

        if(pokemonDatabase.ContainsKey(id)){
            return pokemonDatabase[id];
        }
        else{
            int i = Random.Range(0, pokemonIds.Count);
            return pokemonDatabase[pokemonIds[i]];
        }

        
        
   
            
            //Debug.Log(pokemonIds.Count);
        

        //Debug.Log(pokemonDatabase[i].pokeName);
        
    }
}
