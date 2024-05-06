using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvolutionDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public PokemonLoader pokeLoader;
    [SerializeField] public PokemonLoader nopoke;
    [SerializeField] public PokemonLoader evo;
    [SerializeField] public GameObject[] methods;
    [SerializeField] public Button previous;
    [SerializeField] public Button next;
    [SerializeField] public TMP_Text[] methodText;
    [SerializeField] public ItemLoader methodImage;
    [SerializeField] public Sprite[] methodImages;
    [SerializeField] public TMP_Text paramText;
    [SerializeField] public ItemLoader paramItem;
    [SerializeField] public TMP_Text itemName;
    [SerializeField] public ItemLoader itemIcon;
    [SerializeField] public TMP_Text natureText;
    [SerializeField] public PokemonLoader tradeSpecies;


    public string[] evoList;
    public int current = 0;


    void Awake(){
        Refresh();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshPokes(){
        
        
        evo.id = evoList[current].Split('_')[0]; 
        evo.poke.shiny = pokeLoader.poke.shiny;
        
        evo.RefreshSprite();
        
        SetPrevNext();

        Methodify();


    }

    public int GetMoveTM(string move){
        switch(move){
            case "Ancient Power":
                return 515;
                break;
            
            case "Rollout":
                return 496;
                break;
            
            case "Dragon Pulse":
                return 501;
                break;
            
            case "Taunt":
                return 500;
                break;
            
            case "Mimic":
                return 512;
                break;
            
            case "Double Hit":
                return 493;
                break;

            case "Stomp":
                return 509;
                break;
            
            case "Barb Barrage":
                return 494;
                break;

            
            case "Psyshield Bash":
                return 495;
                break;


            default:return 512;
            
            
        }

    }

    public void SetWidth(int width){
        var rect = paramItem.transform as RectTransform;
        rect.sizeDelta = new Vector2(width,48);
    }

    public void Methodify(){
        var method = evoList[current].Split('_')[2];
        var param = evoList[current].Split('_')[3];
        for(int i=1;i<methods.Length-1;i++)
            methods[i].SetActive(false);
        
        if(method=="Trade"){
            methods[2].SetActive(true);
        }
        else if(method=="Level"||method=="LevelGalarian"||method=="LevelHisuian"){
            methods[3].SetActive(true);
            methodText[1].text = "Level "+param;
        }
        else if(method=="Happiness"){
            methods[4].SetActive(true);
        }
        else if(method=="HasInParty"){
            methods[5].SetActive(true);
        }else if(method=="Item"||method=="ItemAlolan"||method=="ItemHisuian"||method=="ItemRegional"){
            methods[6].SetActive(true);
            itemIcon.id = int.Parse(param);
            itemIcon.RefreshSprite();
            itemName.text = itemIcon.item.itemName;
        }else if(method=="LevelNature"){
            methods[7].SetActive(true);
            methodText[2].text = "Level "+param;
            if(evo.id=="849"){
                natureText.text = "Hardy, Brave, Adamant, Naughty, Docile, Impish, Lax, Hasty, Jolly, Naive, Rash, Sassy, or Quirky";

            }
            else{
                natureText.text = "Lonely, Bold, Relaxed, Timid, Serious, Modest, Mild, Quiet, Bashful, Calm, Gentle, or Careful";
            }

        }else if(method=="TradeSpecies"){
            methods[8].SetActive(true);
            tradeSpecies.id = param;
            tradeSpecies.RefreshSprite();
            tradeSpecies.poke_name.text = "for "+ tradeSpecies.poke.pokeName;

        }
        else{
            //Debug.Log(method);
            methods[1].SetActive(true);
            methodText[0].text = "Level "+param;
            methodImage.id = 68;

            
            SetWidth(48);

            

            switch(method){
                case "AtkDefEqual":{
                    
                    paramItem.id = 363;
                    paramText.text = "(Attack = Defense)";
                    break;
                }
                case "AttackGreater":{
                    
                    paramItem.id = 367;
                    paramText.text = "(Attack > Defense)";
                    break;
                }
                case "DefenseGreater":{
                    
                    paramItem.id = 366;
                    paramText.text = "(Attack < Defense)";
                    break;
                }
                case "DayHoldItem":{
                    
                    methodImage.id = int.Parse(param);
                    methodImage.RefreshSprite();
                    methodText[0].text = "Level up holding "+methodImage.item.itemName;
                    paramItem.id = -1;
                    paramItem.GetComponent<Image>().overrideSprite = methodImages[1];
                    paramText.text = "(during Day)";
                    break;
                }
                case "NightHoldItem":{
                    
                    methodImage.id = int.Parse(param);
                    methodImage.RefreshSprite();
                    methodText[0].text = "Level up holding "+methodImage.item.itemName;
                    paramItem.id = -1;
                    paramItem.GetComponent<Image>().overrideSprite = methodImages[2];
                    paramText.text = "(during Night)";
                    break;
                }
                case "HappinessDay":{
                    
                    methodImage.id = 102;
                    methodText[0].text = "Friendship";
                    paramItem.id = -1;
                    paramItem.GetComponent<Image>().overrideSprite = methodImages[1];
                    paramText.text = "(during Day)";
                    break;
                }
                case "HappinessNight":{
                    
                    methodImage.id = 102;
                    methodText[0].text = "Friendship";
                    paramItem.id = -1;
                    paramItem.GetComponent<Image>().overrideSprite = methodImages[2];
                    paramText.text = "(during Night)";
                    break;
                }
                case "HappinessMoveType":{
                    
                    methodImage.id = 102;
                    methodText[0].text = "Friendship";
                    paramItem.id = 504;
                    paramText.text = $"(and a {param} type move)";
                    break;
                }
                case "HasMove":case "HasMoveGalarian":{
                    
                    methodImage.id = 68;
                    methodText[0].text = "Level up";
                    paramItem.id = GetMoveTM(param);
                    paramText.text = $"knowing {param}";
                    break;
                }
                case "ItemNight":{
                    methodImage.id = int.Parse(param);
                    methodImage.RefreshSprite();
                    methodText[0].text = methodImage.item.itemName;
                    paramItem.id = -1;
                    paramItem.GetComponent<Image>().overrideSprite = methodImages[2];
                    paramText.text = "(during Night)";
                    break;
                }
                case "ItemMale":{
                    methodImage.id = int.Parse(param);
                    methodImage.RefreshSprite();
                    methodText[0].text = methodImage.item.itemName;
                    paramItem.id = -1;
                    paramItem.GetComponent<Image>().overrideSprite = methodImages[4];
                    SetWidth(32);
                    paramText.text = "(if male)";
                    break;
                }
                case "ItemFemale":{
                    methodImage.id = int.Parse(param);
                    methodImage.RefreshSprite();
                    methodText[0].text = methodImage.item.itemName;
                    paramItem.id = -1;
                    paramItem.GetComponent<Image>().overrideSprite = methodImages[5];
                    SetWidth(32);
                    paramText.text = "(if female)";
                    break;
                }
                case "LevelDarkInParty":{
                    methodImage.id = 68;
                    methodText[0].text = "Level "+param;
                    paramItem.id = 482;
                    paramText.text = "with a Dark type in the party";
                    break;
                }
                case "LevelDay":{
                    methodImage.id = 68;
                    methodText[0].text = "Level "+param;
                    paramItem.id = -1;
                    paramItem.GetComponent<Image>().overrideSprite = methodImages[1];
                    paramText.text = "(during Day)";
                    break;
                }
                case "LevelNight":case "LevelNightAlolan":{
                    methodImage.id = 68;
                    methodText[0].text = "Level "+param;
                    paramItem.id = -1;
                    paramItem.GetComponent<Image>().overrideSprite = methodImages[2];
                    paramText.text = "(during Night)";
                    break;
                }
                case "LevelDusk":{
                    methodImage.id = 68;
                    methodText[0].text = "Level "+param;
                    paramItem.id = -1;
                    paramItem.GetComponent<Image>().overrideSprite = methodImages[6];
                    paramText.text = "(during Dusk)";
                    break;
                }
                case "LevelMale":{
                    methodImage.id = 68;
                    methodText[0].text = "Level "+param;
                    paramItem.id = -1;
                    paramItem.GetComponent<Image>().overrideSprite = methodImages[4];
                    SetWidth(32);
                    paramText.text = "(if male)";
                    break;
                }
                case "LevelFemale":{
                    methodImage.id = 68;
                    methodText[0].text = "Level "+param;
                    paramItem.id = -1;
                    paramItem.GetComponent<Image>().overrideSprite = methodImages[5];
                    SetWidth(32);
                    paramText.text = "(if female)";
                    break;
                }
                case "LevelRain":{
                    methodImage.id = 68;
                    methodText[0].text = "Level "+param;
                    paramItem.id = 220;
                    paramText.text = "(while raining)";
                    break;
                }
                case "TradeItem":{
                    methodImage.id = -1;
                    methodImage.GetComponent<Image>().overrideSprite = methodImages[0];
                    methodText[0].text = "Trade";
                    paramItem.id = int.Parse(param);
                    paramItem.RefreshSprite();
                    paramText.text = "holding "+ paramItem.item.itemName;
                    break;
                }
                case "SpinItem":{
                    methodImage.id = -1;
                    methodImage.GetComponent<Image>().sprite = null;
                    methodText[0].text = "Spin";
                    paramItem.id = int.Parse(param);
                    paramItem.RefreshSprite();
                    paramText.text = "holding "+ paramItem.item.itemName;
                    break;
                }

                
            }
            methodImage.RefreshSprite();
            paramItem.RefreshSprite();

        }


    }

    public void SetPrevNext(){
        previous.gameObject.SetActive(true);
        next.gameObject.SetActive(true);
        if(current==0){
            previous.gameObject.SetActive(false);
        }
        if(current==evoList.Length-1){
            next.gameObject.SetActive(false);
        }
    }
  

    public void Refresh(){
        
        current = 0;

        //Debug.Log(PlayerPokemon.Instance.selectedPoke.id+ " " +pokeLoader.poke.id+" Initial");

        pokeLoader.poke = PlayerPokemon.Instance.selectedPoke;
        pokeLoader.getSprite();

        //Debug.Log(PlayerPokemon.Instance.selectedPoke.id+ " " +pokeLoader.poke.id+" Final");
        
        nopoke.poke = PlayerPokemon.Instance.selectedPoke;
        nopoke.doNotAwake = true;
        nopoke.getSprite();

        


        evoList = pokeLoader.poke.evolutions.Split(';');

        SetPrevNext();
        
        
        if(evoList[0].Length>0){
            
            
            methods[methods.Length-1].SetActive(false);
            methods[0].SetActive(true);
            RefreshPokes();
        }
        else{
            methods[0].SetActive(false);
            methods[methods.Length-1].SetActive(true);

            
            
        }

        
        
        

    }

    public void onNext(){
        current++;
        RefreshPokes();
    }

    public void onPrevious(){
        current--;
        RefreshPokes();

    }



}
