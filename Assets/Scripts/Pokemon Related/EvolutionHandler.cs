using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static List<int> natures = new List<int> {1,5,7,10,12,15,16,17,18,20,21,23};
    
    
    
    

    public static string canEvolve(Pokemon p){
        var evolutions = p.evolutions;
        var level = p.level;
        var gender = p.gender;
        var heldItem = p.heldItem;
        if(evolutions.Contains("LevelHisuian")||evolutions.Contains("LevelGalarian")){
            var evo = evolutions.Split(';')[0];
            var lvl = int.Parse(evo.Split('_')[3]);
            if(level>=lvl){
                    var r = Random.Range(0,100);
                    return evolutions.Split(';')[r/50].Split('_')[0];
                }
        
        }
        if(evolutions.Contains("LevelNightAlolan")){
            var hours = int.Parse(System.DateTime.Now.ToString("HH"));
            if(hours<6 || hours>=20){
                var evo = evolutions.Split(';')[0];
                var lvl = int.Parse(evo.Split('_')[3]);
                if(level>=lvl){
                        var r = Random.Range(0,100);
                        return evolutions.Split(';')[r/50].Split('_')[0];
                    }
            }
        }if(evolutions.Contains("LevelNature")){
            var evo = evolutions.Split(';')[0];
            var lvl = int.Parse(evo.Split('_')[3]);
            if(level>=lvl){
                    int r = 0;
                    if(natures.Contains(p.nature))
                        r=1;
                    return evolutions.Split(';')[r].Split('_')[0];
                }
            }
        foreach(var evo in evolutions.Split(';')){
            //Debug.Log(evo);
            if(evo.Split('_')[2]=="Level"){
                var lvl = int.Parse(evo.Split('_')[3]);
                if(level>=lvl){
                    return evo.Split('_')[0];
                }
            }else if(evo.Split('_')[2]=="LevelMale"){
                if(gender==0){
                    var lvl = int.Parse(evo.Split('_')[3]);
                    if(level>=lvl){
                        return evo.Split('_')[0];
                    }
                }
            }else if(evo.Split('_')[2]=="LevelFemale"){
                if(gender==1){
                    var lvl = int.Parse(evo.Split('_')[3]);
                    if(level>=lvl){
                        return evo.Split('_')[0];
                    }
                }
            }else if(evo.Split('_')[2]=="LevelDusk"){
                var hours = int.Parse(System.DateTime.Now.ToString("HH"));
                if(hours<20 || hours>=17){
                    var lvl = int.Parse(evo.Split('_')[3]);
                    if(level>=lvl){
                        return evo.Split('_')[0];
                    }
                }
            }else if(evo.Split('_')[2]=="LevelNight"){
                var hours = int.Parse(System.DateTime.Now.ToString("HH"));
                if(hours<6 || hours>=20){
                    var lvl = int.Parse(evo.Split('_')[3]);
                    if(level>=lvl){
                        return evo.Split('_')[0];
                    }
                }
            }else if(evo.Split('_')[2]=="LevelDay"){
                var hours = int.Parse(System.DateTime.Now.ToString("HH"));
                if(hours<17 || hours>=10){
                    var lvl = int.Parse(evo.Split('_')[3]);
                    if(level>=lvl){
                        return evo.Split('_')[0];
                    }
                }
            }else if(evo.Split('_')[2]=="DayHoldItem"){
                var hours = int.Parse(System.DateTime.Now.ToString("HH"));
                if(hours<17 || hours>=10){
                    var item = int.Parse(evo.Split('_')[3]);
                    if(heldItem==item){
                        return evo.Split('_')[0];
                    }
                }
            }else if(evo.Split('_')[2]=="NightHoldItem"){
                var hours = int.Parse(System.DateTime.Now.ToString("HH"));
                if(hours<6 || hours>=20){
                    var item = int.Parse(evo.Split('_')[3]);
                    if(heldItem==item){
                        return evo.Split('_')[0];
                    }
                }
            }else if(evo.Split('_')[2]=="AtkDefEqual"){
                if(p.stats[1]==p.stats[2]){
                    var lvl = int.Parse(evo.Split('_')[3]);
                    if(level>=lvl){
                        return evo.Split('_')[0];
                    }
                }
            }else if(evo.Split('_')[2]=="AttackGreater"){
                if(p.stats[1]>p.stats[2]){
                    var lvl = int.Parse(evo.Split('_')[3]);
                    if(level>=lvl){
                        return evo.Split('_')[0];
                    }
                }
            }else if(evo.Split('_')[2]=="DefenseGreater"){
                if(p.stats[1]<p.stats[2]){
                    var lvl = int.Parse(evo.Split('_')[3]);
                    if(level>=lvl){
                        return evo.Split('_')[0];
                    }
                }
                
            }else if(evo.Split('_')[2]=="Happiness"){
                var val = int.Parse(evo.Split('_')[3]);
                if(p.friendship>=val){
                    return evo.Split('_')[0];
                }
            }else if(evo.Split('_')[2]=="HappinessDay"){
                var hours = int.Parse(System.DateTime.Now.ToString("HH"));
                if(hours<17 || hours>=10){
                    var val = int.Parse(evo.Split('_')[3]);
                    if(p.friendship>=val){
                        return evo.Split('_')[0];
                    }
                }
            }else if(evo.Split('_')[2]=="HappinessNight"){
                var hours = int.Parse(System.DateTime.Now.ToString("HH"));
                if(hours<6 || hours>=20){
                    var val = int.Parse(evo.Split('_')[3]);
                    if(p.friendship>=val){
                        return evo.Split('_')[0];
                    }
                }
            }else if(evo.Split('_')[2]=="LevelDarkInParty"){

                bool darkinParty = false;
                var lvl = int.Parse(evo.Split('_')[3]);
                    if(level>=lvl){
                        foreach(var v in PlayerPokemon.Instance.pokeDic){
                            if(v.Value.types[0]=="Dark"||v.Value.types[1]=="Dark"){
                                darkinParty = true;
                                break;
                            }
                        }
                        if(darkinParty){
                            return evo.Split('_')[0];
                        }
                    }
            }else if(evo.Split('_')[2]=="HasInParty"){

                bool hasinParty = false;
                foreach(var v in PlayerPokemon.Instance.pokeDic){
                    if(v.Value.id==evo.Split('_')[3]){
                        hasinParty = true;
                        break;
                    }
                }
                if(hasinParty){
                    return evo.Split('_')[0];
                }
            
            }
        }
        return "-1";
    }
}

