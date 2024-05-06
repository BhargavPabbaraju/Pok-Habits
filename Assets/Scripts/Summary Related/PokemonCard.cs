using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PokemonCard : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public PokemonLoader pokeLoader;
    
    [SerializeField] public Image background;
    [SerializeField] public Button button;
    [SerializeField] public TMP_Text[] texts;
    [SerializeField] public Image hpBar;
    [SerializeField] public PokemonSummaryDisplay summary;
    [SerializeField] public PartyOptions options;
    [SerializeField] public Image gender;
    [SerializeField] public Sprite[] genderSprites;
    [SerializeField] public Sprite[] hpColors;
    [SerializeField] public GameObject shiny;
    [SerializeField] public GameObject heldItem;
    [SerializeField] public int bgIndex=2;
    [SerializeField] public bool starter=false;
    [SerializeField] public Sprite[] backgroundSprites;
    [SerializeField] public bool fainted = false;

    System.DateTime lastUpdate = System.DateTime.Now;

    void Awake(){

        
        //texts[0].text = $"{poke.level}";
        //texts[1].text = $"{poke.hp[0]}";
        //texts[2].text = $"{poke.hp[1]}";
        //hpBar.fillAmount = Mathf.Clamp(0,(poke.hp[0]/poke.hp[1]),1);
    }

    public void SetStuff(){
        var poke = pokeLoader.poke;
        texts[0].text = $"{poke.level}";
        gender.overrideSprite = genderSprites[poke.gender];
        if(poke.shiny){
            shiny.SetActive(true);
        }else{
            shiny.SetActive(false);
        }
        if(poke.heldItem!=-1){
            heldItem.SetActive(true);
        }else{
            heldItem.SetActive(false);
        }
        SetHP();
        
    }

    public void SetFriedship(){
        var poke = pokeLoader.poke;

        
        if(poke.heldItem==123){     //Soothe Bell
            poke.friendship+=1;

        }
        else if(poke.heldItem==126&&poke.color=="Black"){ //Black Flute
            poke.friendship+=1;
        }else if(poke.heldItem==127&&poke.color=="Blue"){ //Blue Flute
            poke.friendship+=1;
        }else if(poke.heldItem==128&&poke.color=="Red"){ //Red Flute
            poke.friendship+=1;
        }else if(poke.heldItem==129&&poke.color=="White"){ //White Flute
            poke.friendship+=1;
        }else if(poke.heldItem==130&&poke.color=="Yellow"){ //Yellow Flute
            poke.friendship+=1;
        }
        else if(poke.ball==18 || poke.ball==9){
            poke.friendship+=1;
        }

        if(poke.friendship>255){
            poke.friendship=255;
        }


    }

    public void SetHP(){
        var poke = pokeLoader.poke;
        if(summary==null){
            texts[1].text = $"{poke.hp}";
            texts[2].text = $"{poke.stats[0]}";
            var x = Mathf.Clamp(0,(poke.hp/(float)poke.stats[0]),1);
            hpBar.fillAmount = x;
            if(x<0.2){
                hpBar.overrideSprite = hpColors[2];
            }else if(x<0.5){
                hpBar.overrideSprite = hpColors[1];
            }else{
                hpBar.overrideSprite = hpColors[0];
            }

            if(poke.hp<=0 && !fainted){
                if(starter){
                    bgIndex = 4;
                    background.overrideSprite = backgroundSprites[4];
                }else{
                    bgIndex = 6;
                    background.overrideSprite = backgroundSprites[6];
                }
                fainted=true;
            
            }
        }
        else{
            summary.SetHP();
        }
        
        
       if(poke.fullness>0){
            poke.fullness-=1;
       }
       else{
            if(poke.hp>0){
                poke.hp-=1;

            }
       }
       lastUpdate = System.DateTime.Now;

       
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       var now = System.DateTime.Now;
       var dif = now - lastUpdate;
       if(dif.Seconds>=5){
            SetHP();
            SetFriedship();
       }
    }

    public void OnClick(){
        PlayerPokemon.Instance.selectedPoke = pokeLoader.poke;
        

        if(summary!=null){
            summary.Refresh();

        }
        else{
            
            
            //Debug.Log(PlayerPokemon.Instance.selectedPoke.id);
            
            options.gameObject.SetActive(true);
            //GameManager.Instance.changeScene(GameManager.Instance.POKEMONSUMMARYSCENE);
        }
    }
}
