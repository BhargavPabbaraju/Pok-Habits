using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ItemEffects : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject noeffectObject;
    [SerializeField] public TMP_Text noeffectText;

    List<string> gmaxable = new  List<string> {"6","12","25","52","68","94","99","131","133","143","569","809","823","826","834","839","841","842","844","849","851","858","861","869","879","884","3","9","812","815","818","892","892R","890"};
    Dictionary<string,int> megaStones = new Dictionary<string,int> {
        {"460",371},
        {"359",372},
        {"142",373},
        {"306",374},
        {"65",375},
        {"334",376},
        {"181",377},
        {"531",378},
        {"534",379},
        {"15",380},
        {"9",381},
        {"257",382},
        {"323",383},
        // {"6",384},
        // {"6",385},
        {"719",386},
        {"475",387},
        {"445",388},
        {"282",389},
        {"94",390},
        {"362",391},
        {"130",392},
        {"214",393},
        {"229",394},
        {"115",395},
        {"380",396},
        {"381",397},
        {"428",398},
        {"448",399},
        {"310",400},
        {"303",401},
        {"308",402},
        {"376",403},
        // {"120",404},
        // {"120",405},
        {"18",406},
        {"127",407},
        {"302",408},
        {"373",409},
        {"254",410},
        {"212",411},
        {"319",412},
        {"80",413},
        {"208",414},
        {"260",415},
        {"248",416},
        {"3",417},

    };
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNoEffectText(){
        noeffectObject.SetActive(true);
    }



    public void Heal(string effect){
        if (effect=="HEALALL_99999"){
            foreach(var poke in PlayerPokemon.Instance.pokeDic){
                        poke.Value.hp = poke.Value.stats[0];
                    }
            
            PlayerPokemon.Instance.WritePokes();
        }
        else{
            var poke = PlayerPokemon.Instance.selectedPoke;
            if(poke.hp==poke.stats[0]){
                SetNoEffectText();
                return;
            }
            var points = int.Parse(effect.Split('_')[1]);
            if(points==99999){
                poke.hp = poke.stats[0];
            }else if(points==999){
                poke.hp+= poke.stats[0]/2;
            }else{
                poke.hp+=points;
            }
            if(poke.hp > poke.stats[0]){
                poke.hp = poke.stats[0];
            }

            PlayerPokemon.Instance.UpdatePoke(poke);
        }
        
        PlayerItem.Instance.DeleteItem();
        GameManager.Instance.changeScene(GameManager.Instance.POKEMONINVSCENE);

    }

    void Ball(string effect){
        var poke = PlayerPokemon.Instance.selectedPoke;
        var ball = int.Parse(effect.Split('_')[1]);
        if(poke.ball == ball){
            SetNoEffectText();
            return;
        }
        poke.ball = ball;
        PlayerPokemon.Instance.UpdatePoke(poke);
        PlayerItem.Instance.DeleteItem();
        GameManager.Instance.changeScene(GameManager.Instance.POKEMONSUMMARYSCENE);
    }

    void Level(string effect){
        var poke = PlayerPokemon.Instance.selectedPoke;
        poke.incrementLevel();
        PlayerItem.Instance.DeleteItem();
        GameManager.Instance.changeScene(GameManager.Instance.POKEMONINVSCENE);

    }
    void Money(string effect,int typ){
        var user = AuthHandler.Instance.currentUser;
        var points = int.Parse(effect.Split('_')[1]);
        user.currency[typ]+=points;
        AuthHandler.Instance.UpdateUser();
        PlayerItem.Instance.DeleteItem();
        GameManager.Instance.changeScene(GameManager.Instance.POKEMONINVSCENE);
    
    }

    void Egg(string effect){
        var eggId = effect.Split('_')[1];
        var egg = GameObject.Find("Database").GetComponent<LoadEggDatabase>().GetEgg(eggId);
        PlayerEgg.Instance.AddEgg(egg);
        PlayerItem.Instance.DeleteItem();
        GameManager.Instance.changeScene(GameManager.Instance.POKEMONINVSCENE);
    }


    void ChangeForms(string id,Pokemon poke){
        var poke2 = GameObject.Find("Database").GetComponent<LoadPokemonDatabase>().GetPokemon(id);
        PlayerPokemon.Instance.ChangeForm(poke,poke2);    
        PlayerItem.Instance.DeleteItem();
        GameManager.Instance.changeScene(GameManager.Instance.POKEMONSUMMARYSCENE);
    }

    void Form(string effect,bool random){
        var poke = PlayerPokemon.Instance.selectedPoke;
        var formnumber = int.Parse(effect.Split('_')[1]);
        if (poke.number != formnumber){
            SetNoEffectText();
            return;
        }

        string id="";
        if(random){
            var r = Random.Range(0,100);
            id = effect.Split('_')[2+r/50];
        }
        else{
            id = effect.Split('_')[2];
        }

        ChangeForms(id,poke);


    }

    void GMAX(){
        
        var poke = PlayerPokemon.Instance.selectedPoke;
        if (!gmaxable.Contains(poke.id)){
            SetNoEffectText();
            return;
        }
        ChangeForms(poke.id+"GM",poke);
        
    }

    

    void Evolve(string effect,bool random){
        var poke = PlayerPokemon.Instance.selectedPoke;
        var id1 = effect.Split('_')[1];
        if(id1!=poke.id){
            SetNoEffectText();
            return;
        }
        string id;
        if(random){
            var r = Random.Range(0,100);
            id = effect.Split('_')[2+r/50];
        }
        else{
            id = effect.Split('_')[2];
        }
        ChangeForms(id,poke);


    }

    void EvolveFemale(string effect){
        var poke = PlayerPokemon.Instance.selectedPoke;
        if(poke.gender!=1){
            SetNoEffectText();
            return;
        }
        Evolve(effect,false);
    }
    void EvolveMale(string effect){
        var poke = PlayerPokemon.Instance.selectedPoke;
        if(poke.gender!=0){
            SetNoEffectText();
            return;
        }
        Evolve(effect,false);
    }

    void EvolveNight(string effect){
        var hours = int.Parse(System.DateTime.Now.ToString("HH"));
        if(hours<6 || hours>=20){
            Evolve(effect,false);
        }else{
            SetNoEffectText();
            return;
        }
    
        
    }
    void EVOLVEFRIENDSHIP(string effect){
        var poke = PlayerPokemon.Instance.selectedPoke;
        if(poke.friendship>220){
            Evolve(effect,false);
        }else{
            SetNoEffectText();
            return;
        }
    
        
    }
    void Nature(string effect){
        var poke = PlayerPokemon.Instance.selectedPoke;
        var start = int.Parse(effect.Split('_')[1]);
        if(start == 0){
            poke.nature = 6*Random.Range(0,5);
            
        }else{
            var end = int.Parse(effect.Split('_')[2]);
            poke.nature = Random.Range(start,end);

        }
        poke.calculateStats();
        PlayerPokemon.Instance.UpdatePoke(poke);
        PlayerItem.Instance.DeleteItem();
        GameManager.Instance.changeScene(GameManager.Instance.POKEMONSUMMARYSCENE);


    }

    bool CheckMegaEvolve(int itemId,Pokemon poke){
        if(poke.id=="6"){
            if (itemId==384){
                ChangeForms(poke.id+"X",poke);
                return true;

            }else if(itemId==385){
                ChangeForms(poke.id+"Y",poke);
                return true;
            }else{
                return false;
            }
        }
        else if(poke.id=="150"){
            if (itemId==404){
                ChangeForms(poke.id+"X",poke);
                return true;

            }else if(itemId==405){
                ChangeForms(poke.id+"Y",poke);
                return true;
            }else{
                return false;
            }
        }else{
            if(megaStones.ContainsKey(poke.id)){
                if(megaStones[poke.id]==itemId){
                    ChangeForms(poke.id+"M",poke);
                    return true;
                }else{
                    return false;
                }
            }else{
                return false;
                
            }
        }

    }


    public void GiveItem(){
        var item = PlayerItem.Instance.selectedItem;
        var poke = PlayerPokemon.Instance.selectedPoke;
        poke.heldItem = item.id;
        bool res = false;
        if(item.id>=371 && item.id<= 417){
            res = CheckMegaEvolve(item.id,poke);
        }

        if(!res){
            PlayerPokemon.Instance.UpdatePoke(poke);
            PlayerItem.Instance.DeleteItem();
            GameManager.Instance.changeScene(GameManager.Instance.POKEMONSUMMARYSCENE);
        }
    }

    public void UseItem(){
        noeffectObject.SetActive(false);
        var item = PlayerItem.Instance.selectedItem;
        var effects = item.effect.Split(';');
        
        //Debug.Log(poke.pokeName+" "+poke.hp);
        

        foreach(var effect in effects){
            var method = effect.Split('_')[0];
            switch(method){
                case "HEAL":case "HEALALL":{
                    Heal(effect);
                    break;
                }
                case "BALL":{
                    Ball(effect);
                    break;
                }
                case "LEVEL":{
                    Level(effect);
                    break;
                }
                case "SILVER":{
                    Money(effect,0);
                    break;
                }case "GOLD":{
                    Money(effect,1);
                    break;
                }case "BLUE":{
                    Money(effect,2);
                    break;
                }case "PINK":{
                    Money(effect,2);
                    break;
                }
                case "EGG":{
                    Egg(effect);
                    break;
                }
                case "FORM":{
                    Form(effect,false);
                    break;
                }case "FORMRANDOM":{
                    Form(effect,true);
                    break;
                }
                case "GMAX":{
                    GMAX();
                    break;
                }
                case "EVOLVE":{
                    Evolve(effect,false);
                    break;
                }case "EVOLVERANDOM":{
                    Evolve(effect,true);
                    break;
                }case "EVOLVEFEMALE":{
                    EvolveFemale(effect);
                    break;
                }case "EVOLVEMALE":{
                    EvolveMale(effect);
                    break;
                }case "EVOLVENIGHT":{
                    EvolveNight(effect);
                    break;
                }

                case "NATURE":{
                    Nature(effect);
                    break;
                }


                default: SetNoEffectText();break;
            
                
                

                
            }
        }
    }


    public void Feed(){
        noeffectObject.SetActive(false);
        
        var food = PlayerFood.Instance.selectedFood;
        var poke = PlayerPokemon.Instance.selectedPoke; 
        if(poke.fullness>=255){
            SetNoEffectText();
            noeffectText.text = poke.nick + "is already full.";
        }else if(poke.hp<=0){
            SetNoEffectText();
            noeffectText.text = "Fainted Pokémon cannot be fed.";

        }else{
            poke.fullness+=food.fullness;
            if(poke.fullness>255){
                poke.fullness=255;
            }
            var exp = food.exp;
            foreach(string flavor in food.flavors.Split('_')){
                if(flavor!="All"){
                    exp=food.GetActualExp(exp,poke.nature,flavor,poke.heldItem);
                }
                
            }
            poke.SetExp(exp);
            var evo = EvolutionHandler.canEvolve(poke);
            if(evo!="-1"){
                var poke2 = GameObject.Find("Database").GetComponent<LoadPokemonDatabase>().GetPokemon(evo);
                PlayerPokemon.Instance.ChangeForm(poke,poke2);    
            }
            PlayerFood.Instance.DeleteFood();
            GameManager.Instance.changeScene(GameManager.Instance.POKEMONSUMMARYSCENE);
        }
        
    }
}
