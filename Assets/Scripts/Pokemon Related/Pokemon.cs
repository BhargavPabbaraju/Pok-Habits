using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pokemon
{
    // Start is called before the first frame update
    
    public string id;
    public string pokeName;
    public int gen;
    public int number;
    public string[] types;
    public string color;
    public float ht;
    public float wt;
    public int[] stats;
    public int[] basestats;
    public int[] overcords;
    public int[] spritecords;
    public bool shiny=false;
    public int gender=0;
    public int nature;
    public int[] exp;
    public int friendship=50;
    public string nick;
    public int ball=26;
    public int level=1;
    public int[] ivs;
    public string expType;
    public string uniqueId="";
    public string[] ot;
    public int[] abilities;
    public int ability;
    public string evolutions; 
    public string lastUpdate="";
    public int method =0;
    public List<int> ribbons=null;
    public int heldItem=-1;
    public string eggId = "";
    public int fullness = 0;
    public int hp = 0;
   

    public Pokemon(Pokemon p){
        id = p.id;
        pokeName = p.pokeName;
        gen = p.gen;
        number = p.number;
        color = p.color;
        types = p.types;
        ht = p.ht;
        wt = p.wt;
        basestats = p.basestats;
        overcords = p.overcords;
        spritecords = p.spritecords;
        abilities = p.abilities;
        evolutions = p.evolutions;
        



        nick = p.pokeName;
        
        


        GenerateStats();
        
        

        
        expType = "MS";

        
        

        
    }


    

    public string GetNature(){
        string[] natures = new string[25];

        natures = new string[]{
            "Hardy","Lonely","Brave","Adamant","Naughty","Bold","Docile","Relaxed","Impish",
            "Lax","Timid","Hasty","Serious","Jolly","Naive","Modest","Mild","Quiet","Bashful",
            "Rash","Calm","Gentle","Sassy","Careful","Quirky"
        };

        return natures[nature];
    }

    public string GetCharacteristic(){
        var max = System.Array.IndexOf(ivs,Mathf.Max(ivs));
        
        string[] charList = new string[6];
        switch(ivs[max]%5){
            case 0:{
                charList= new string[] {"Loves to eat","Proud of its power","Sturdy body","Highly curious","Strong willed","Likes to run"};
                break;
            }
            case 1:{
                charList= new string[] {"Often dozes off","Likes to thrash about","Capable of taking hits","Mischievous","Somewhat vain","Alert to sounds"};
                break;
            }
            case 2:{
                charList= new string[] {"Often scatters things","A little quick tempered","Highly persistent","Thoroughly cunning","Strongly defiant","Impetuous and silly"};
                break;
            }
            case 3:{
                charList= new string[] {"Likes to take siestas","Likes to fight","Good endurance","Often lost in thought","Hates to lose","Somewhat of a clown"};
                break;
            }
            case 4:{
                charList= new string[] {"Likes to relax","Quick tempered","Good perseverance","Very finicky","Somewhat stubborn","Quick to flee"};
                break;
            }
        }

        //Debug.Log($"{pokeName} {max} {ivs[max]%5} {charList[max]}");
        return charList[max];

        
    }

    public void SetOT(){
        var user = AuthHandler.Instance.currentUser;
        ot = new string[2];
        ot[0] = user.name;
        ot[1] = user.number;

    }

    public void GenerateUniqueId(){
        uniqueId = id;
        int i =1;
        //Debug.Log(uniqueId);
        while(PlayerPokemon.Instance.pokeDic.ContainsKey(uniqueId)){
            uniqueId =id+ i.ToString();
            i+=1;
        }

    }

    public void GenerateStats(){
        ivs = new int[6];
        stats = new int[6];
        exp = new int[2];
        for(int i=0;i<6;i++){
            ivs[i] = Random.Range(0,32); 
            stats[i] = 0;
            
        }
        
        

    }

    float natureModifier(int stat){
        //Hardy(0), Docile(6), Serious(12), Bashful(18), Quirky(24)
        if(nature%6==0){return 1f;}

        if(stat==1){
            //Lonely(1) Brave(2) Adamant(3) Naughty(4)
            if(nature<5){return 1.1f;}
            
            //Bold(5) Timid(10) Modest(15) Calm(20)
            else if(nature%5==0){return 0.9f;}
        }

        else if(stat==2){
            //Bold(5) Relaxed(7) Impish(8) Lax(9)
            if(nature>5 && nature<10){return 1.1f;}
            
            //Lonely(1) Hasty(11) Mild(16) Gentle(21)
            else if(nature%5==1){return 0.9f;}

        }
        else if(stat==3){
            //Modest(15) Mild(16) Quiet(17) Rash(19)
            if(nature>10 && nature<20){return 1.1f;}
            
            //Adamant(3) Impish(8) Jolly(13) Careful(23)
            else if(nature%5==3){return 0.9f;}
                

        }
        else if(stat==4){
            //Calm(20) Gentle(21) Careful(22) Sassy(23)
            if(nature>=20 && nature<25){return 1.1f;}
            //Naughty(4) Lax(9) Naive(14) Rash(19)
            else if(nature%5==4){return 0.9f;}
                

        }
        else if(stat==5){
            //Timid(10) Hasty(11) Jolly(13) Naive(14)
            if(nature>=10 && nature<15){return 1.1f;}
                
            
            //Brave(2) Relaxed(7) Quiet(17) Sassy(22)
            else if(nature%5==2){return 0.9f;}
                

        }

        return 1f;
    }

    public void calculateStats(){
        int s;
        for(int i=0;i<6;i++){
            s=Mathf.FloorToInt(((2*basestats[i]+ivs[i])*level)/100f);

            if(i==0){
                stats[i] = s+level+10;
            }
                
            else{
                s=Mathf.FloorToInt((s+5)*natureModifier(i));
                stats[i] = s;
            }

        }
        hp = stats[0];
        calculateExp();

    }

    
    
    public void calculateExp(){
            int n = level+1;
            
            if(expType=="MF"){ //Medium Fast
                exp[1] = n*n*n;
            }
            else if(expType== "S"){//Slow
                exp[1] = (5*n*n*n)/4;
            }
            else if(expType== "F"){//Fast
                exp[1] = (4*n*n*n)/5;
            }
            else if(expType== "E"){//Erratic
                if(n<50){exp[1] = n*n*n*(100-n)/50;} 
                else if(n<68){exp[1] = n*n*n*(150-n)/100;}
                else if(n<98){exp[1] = n*n*n* Mathf.FloorToInt((1911-10*n)/3f) / 500;}
                else {exp[1] = n*n*n* (160-n)/100;} 
            }
            else if(expType== "FL"){//Fluctuating
                if(n<15){exp[1] = (n*n*n)*((Mathf.FloorToInt((n+1)/3f)+24)/50);} 
                else if(n<36){exp[1] = (n*n*n)*((n+14)/50);} 
                else {exp[1] = (n*n*n)*((Mathf.FloorToInt(n/2f)+32)/50);} 
            }
            else{//Medium Slow
                exp[1] = Mathf.FloorToInt(6/5f*n*n*n - 15*n*n + 100*n - 140);
            }  
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetExp(int points){
        exp[0]+=points;
        while(exp[0]>exp[1]){
            incrementLevel();
        }
    }

    public void incrementLevel(){
        level+=1;
        calculateStats();
        
    }
    public void SetLevel(int lvl){
        level = lvl-1;
        calculateExp();
        exp[0] = exp[1];
        level+=1;
        calculateStats();

    }

    public int GetAbility(){
        int r;
        r=Random.Range(0,100);
        if(r>80 && abilities[2]>0)
            return abilities[2];

        r=Random.Range(0,100);
        if(r>50 && abilities[1]>0)
            return abilities[1];

        return abilities[0];

    }

    public void Hatch(Egg egg){
        gender = egg.gender;
        shiny = egg.shiny;
        nature = Random.Range(0,25);
        friendship = egg.friendship;
        expType = egg.expType;
        level = 1;
        ball = egg.ball;
        SetOT();
        GenerateStats();
        calculateStats();
        lastUpdate = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        ability = GetAbility();
        eggId = egg.id;
        
        if(gender==1){
            var res =GameObject.Find("Database").GetComponent<LoadPokemonDatabase>().FemaleExists(id);
            if(res)
                id+="F";
        }
        fullness = 100;
        

            



    }

    

    public void Starter(){
        gender = 0;
        if(Random.Range(0,4097)<7){shiny = true;}
        nature = Random.Range(0,25);
        SetOT();
        GenerateStats();
        SetLevel(5);
        uniqueId = "-1";
        lastUpdate = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        ability = GetAbility();
        fullness = 100;


    }


    public void ChangeForm(Pokemon f){

        id = f.id;
        pokeName = f.pokeName;
        gen = f.gen;
        number = f.number;
        color = f.color;
        types = f.types;
        ht = f.ht;
        wt = f.wt;
        basestats = f.basestats;
        overcords = f.overcords;
        spritecords = f.spritecords;
        abilities = f.abilities;
        evolutions = f.evolutions;

        ability = f.GetAbility();

        calculateStats();
        
    }
}
