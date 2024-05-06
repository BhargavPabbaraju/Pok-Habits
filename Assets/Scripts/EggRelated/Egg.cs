using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Egg
{
    // Start is called before the first frame update
    
    public string id;
    public int gen;
    public int[] cords;
    public bool shiny=false;
    public int genderRatio;
    public int friendship;
    public string expType;
    public string uniqueId="";
    public int time;
    public string lastUpdate="";
    public string rarity;
    public string month;
    public string day;
    public string year;
    public int method=0;
    public int gender;
    public int ball=26;
    //public Ribbon[] ribbons;
   

    public Egg(Egg p){
        id = p.id;
        gen = p.gen;
        cords = p.cords;
        genderRatio = p.genderRatio;
        time = p.time;
        friendship = p.friendship;
        rarity = p.rarity;
        expType = p.expType;
        lastUpdate = p.lastUpdate;

        
    }

    public void GenerateUniqueId(){
        if(Random.Range(0,4097)<7){shiny = true;}
        uniqueId = id;
        int i =1;
        //Debug.Log(uniqueId);
        while(PlayerEgg.Instance.eggDic.ContainsKey(uniqueId)){
            uniqueId =id+ i.ToString();
            i+=1;
        }

        year = System.DateTime.Now.ToString("yyyy");
        day = System.DateTime.Now.ToString("dd");
        month = System.DateTime.Now.ToString("MMM");
        lastUpdate = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        //Debug.Log(lastUpdate);

        if(Random.Range(0,4097)<7){shiny = true;}
        gender = determineGender(); 
        

    }

    public int determineGender(){
        switch(genderRatio){
            case 0:
                return 0;
                break;
            
            case 254:
                return 1;
                break;
            
            case 255:
                return 2;
                break;
        }
        int r = Random.Range(1,253);
        if(r>=genderRatio)
            return 0;
        
        return 1;
    }

    public Pokemon Hatch(){
        var poke = GameObject.Find("Database").GetComponent<LoadPokemonDatabase>().GetPokemon(id);
        poke.Hatch(this);
        //Debug.Log(poke.id);
        PlayerEgg.Instance.DeleteEgg(this);
        PlayerPokemon.Instance.AddPoke(poke);
        return poke;

    }

    
}
