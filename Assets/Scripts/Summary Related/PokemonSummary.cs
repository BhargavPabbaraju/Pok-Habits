using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PokemonSummary : MonoBehaviour

{
    // Start is called before the first frame update

    
    [SerializeField] public int screen=0;
    
    [SerializeField] public PokemonLoader pokeLoader;
    [SerializeField] public TMP_Text nick;
    [SerializeField] public TMP_Text level;
    [SerializeField] public GameObject[] gender;
    [SerializeField] public Sprite[] balls;
    [SerializeField] public Image ball;
    [SerializeField] public GameObject heldObject;
    [SerializeField] public ItemLoader heldItem;
    [SerializeField] public TMP_Text itemName;
    [SerializeField] public GameObject shiny;


    //INFO
    [SerializeField] public TMP_Text name;
    [SerializeField] public TMP_Text dexNumber;
    [SerializeField] public TMP_Text[] exp;
    [SerializeField] public TMP_Text[] originalTrainer;
    [SerializeField] public Image expBar;
    [SerializeField] public Image[] types;


    //MEMO
    [SerializeField] public TMP_Text nature;
    [SerializeField] public TMP_Text date;
    [SerializeField] public TMP_Text method;
    [SerializeField] public TMP_Text metLevel;
    [SerializeField] public TMP_Text characteristic;
    [SerializeField] public TMP_Text[] flavorText;

    //SKILLS
    [SerializeField] public TMP_Text[] ability;
    [SerializeField] public TMP_Text[] stats;
    [SerializeField] public TMP_Text[] hp;
    [SerializeField] public Image hpBar;
    [SerializeField] public Sprite[] hpColors;



    //EVOLUTIONS
    [SerializeField] public EvolutionDisplay evopane;


    public List<Ability> abilityDatabase;


    private Pokemon poke;


    void Awake(){
        abilityDatabase = GameObject.Find("Database").GetComponent<LoadAbilityDatabase>().abilityDatabase;
        Refresh();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Refresh(){
        poke = PlayerPokemon.Instance.selectedPoke;
        pokeLoader.poke = poke;
        //Debug.Log(pokeLoader.poke.id+" "+pokeLoader.poke.pokeName);
        pokeLoader.doNotAwake = true;
        pokeLoader.getSprite();
        nick.text = poke.nick;

        if(poke.shiny){
            shiny.SetActive(true);
        }else{ 
            shiny.SetActive(false);
        }
        if(poke.nick==""){
            nick.text = poke.pokeName;
        }

        if(poke.heldItem!=-1){
            heldObject.SetActive(true);
            heldItem.id = poke.heldItem;
            heldItem.RefreshSprite();
            itemName.text = heldItem.item.itemName;
        }else{
            heldObject.SetActive(false);
        }

        level.text = poke.level.ToString();
        for(int i=0;i<2;i++)
            gender[i].SetActive(false);
        
        if(poke.gender<2)
            gender[poke.gender].SetActive(true);


        ball.overrideSprite = balls[poke.ball-1];


        switch(screen){
            case 0:
                RefreshInfo();
                break;
            
            case 1:
                RefreshMemo();
                break;
            
            case 2:
                RefreshSkills();
                break;

            case 3:
                RefreshEvos();
                break;
        }
    }

    public void GetFlavors(){
        if(poke.nature%6==0){
            flavorText[0].text ="Has no preference in food.";
            flavorText[1].text ="";
            return;
        }
        if(poke.nature<5){
            flavorText[0].text ="Likes Spicy food.";
        }else if(poke.nature<10){
            flavorText[0].text ="Likes Sour food.";
        }else if(poke.nature<15){
            flavorText[0].text ="Likes Sweet food.";
        }else if(poke.nature<20){
            flavorText[0].text ="Likes Dry food.";
        }else{
            flavorText[0].text ="Likes Bitter food.";
        }

        if(poke.nature%5==0){
            flavorText[1].text ="Dislikes Spicy food.";
        }else if(poke.nature%5==1){
            flavorText[1].text ="Dislikes Sour food.";
        }else if(poke.nature%5==2){
            flavorText[1].text ="Dislikes Sweet food.";
        }else if(poke.nature%5==3){
            flavorText[1].text ="Dislikes Dry food.";
        }else{
            flavorText[1].text ="Dislikes Bitter food.";
        }

    }

    public void RefreshMemo(){
        var gotDate = System.DateTime.ParseExact(poke.lastUpdate,"dd-MM-yyyy HH:mm:ss",null);
        date.text = $"{gotDate.ToString("dd")} {gotDate.ToString("MMM")},{gotDate.ToString("yyyy")}";
        nature.text = poke.GetNature();
        characteristic.text = poke.GetCharacteristic();
        switch(poke.method){
            case 0:
                method.text = "Hatched from Egg.";
                break;
            case 1:
                method.text = "Picked as Starter.";
                break;
            case 2:
                method.text = "Receieved from Trade.";
                break;
        }
        GetFlavors();
        
        

    }

    public void SetHp(){
        hp[0].text = $"{poke.hp}";
        hp[1].text = $"{poke.stats[0]}";
        var x = Mathf.Clamp(0,(poke.hp/(float)poke.stats[0]),1);
        hpBar.fillAmount = x;
        if(x<0.2){
            hpBar.overrideSprite = hpColors[2];
        }else if(x<0.5){
            hpBar.overrideSprite = hpColors[1];
        }else{
            hpBar.overrideSprite = hpColors[0];
        }
    }

    public void RefreshSkills(){
        var ab = abilityDatabase[poke.ability-1];
        ability[0].text = ab.title;
        ability[1].text = ab.des1;
        ability[2].text = ab.des2;
        for(int i=1;i<6;i++){
            stats[i-1].text = poke.stats[i].ToString();
        }
        SetHp(); 
    }

    public void RefreshEvos(){
        evopane.Refresh();
    }

    

    public void RefreshInfo(){
        //Debug.Log(PlayerPokemon.Instance.selectedPoke.id);

        
        name.text = poke.pokeName;
        
        dexNumber.text = poke.number.ToString();

        if(poke.ot==null){
            var user = AuthHandler.Instance.currentUser;
            originalTrainer[0].text = user.name;
            originalTrainer[1].text = user.number;
        }else{
            originalTrainer[0].text = poke.ot[0];
            originalTrainer[1].text = poke.ot[1];
        }
        

        exp[0].text = poke.exp[0].ToString();
        exp[1].text = $"{poke.exp[1]-poke.exp[0]}";
        expBar.fillAmount = Mathf.Clamp(0,poke.exp[0]/(float)poke.exp[1],1);

        if(poke.types[1]!=""){
            types[0].gameObject.SetActive(true);
            types[1].gameObject.SetActive(true);
            types[2].gameObject.SetActive(false);
            for(int i=0;i<2;i++){
                types[i].overrideSprite = PlayerPokemon.Instance.spriteList.getTypeSprite(poke.types[i]);
            }
        }
        else{
            types[0].gameObject.SetActive(false);
            types[1].gameObject.SetActive(false);
            types[2].gameObject.SetActive(true);
            types[2].overrideSprite = PlayerPokemon.Instance.spriteList.getTypeSprite(poke.types[0]);
        }
        
        

    }

    
    
    
}
