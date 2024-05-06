using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActiveButton : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public List<PokemonCard> pokemonCards;
    [SerializeField] public Sprite[] pokebgImages;
    [SerializeField] public bool isEgg;
    [SerializeField] public List<EggCard> eggCards;
    [SerializeField] public Sprite[] eggbgImages;
    [SerializeField] public GameObject child;
    [SerializeField] public Transform parent;
    [SerializeField] public bool isSummary;
    [SerializeField] public TMP_Text noDisplay;
    [SerializeField] public GameObject panel;
    
    public int cur = -1;

    int count=0;


    IEnumerator checkEggsToHatch(){
        
        var dic = PlayerEgg.Instance.eggDic;
        //Debug.Log(dic);
        if(dic==null ||dic.Count<=0){
            yield break;
        }
        var l = new List<string>(dic.Keys);
        count = 0;
        foreach(var e in l){
            while(GameManager.Instance.hatching)
                yield return null;

            if(dic.ContainsKey(e)){
                var egg = dic[e];
                var mins = TimeHandler.getTimeRemainingInSeconds(egg.lastUpdate);
                if(egg.time<=mins){
                    while(GameManager.Instance.hatching)
                        yield return null;
                    
                    panel.SetActive(true);
                    //Debug.Log(egg.id);
                    var poke =egg.Hatch();
                    count++;
                    GameManager.Instance.popUpSystem.HatchEgg(poke);

                }
            }
            
            
            
        }

        while(GameManager.Instance.hatching)
                yield return null;
        
        
            
    }

    IEnumerator HatchEggs(){
        yield return StartCoroutine(checkEggsToHatch());

        if(count>0){
            //Debug.Log(count);
            
            GameManager.Instance.changeScene(GameManager.Instance.POKEMONINVSCENE);
            Debug.Log("Reload");
            panel.SetActive(false);
        }

        EggRefresh();
    }

    

    void Start(){
        if(isEgg){
            StartCoroutine(HatchEggs());
        }
        else{
            PokemonRefresh();
        }

        
        
        
        
    }


    public void EggRefresh(){
        var dic = PlayerEgg.Instance.eggDic;
            eggCards.Clear();
            if(dic==null||dic.Count<1){
                DestroyImmediate(parent.GetChild(0).gameObject);
                noDisplay.gameObject.SetActive(true);
                noDisplay.text = "You do not have any eggs";
                return;
            }

            if(!isSummary)
                noDisplay.gameObject.SetActive(false);
                
            while(parent.childCount != 1){
                DestroyImmediate(parent.GetChild(0).gameObject);
            }
            foreach(var e in dic){
                var clone = Instantiate(child);
                clone.transform.SetParent(parent);
                
                EggCard eggCard;
                if(isSummary){
                    eggCard = clone.GetComponent<EggCard>();
                    var eggSprite = eggCard.eggLoader;
                    eggSprite.egg = e.Value;
                    eggSprite.getSprite();
                    
                    

                }else{
                    var bg = clone.transform.GetChild(0).transform;
                    eggCard = bg.GetChild(6).GetComponent<EggCard>();

                    var eggSprite = eggCard.eggLoader;
                    eggSprite.egg = e.Value;
                    eggSprite.getSprite();
                    eggCard.SetRarity();
                    eggCard.SetCracks();
                    
                    
                }
                

                
                
                eggCards.Add(eggCard);
                
            }

            DestroyImmediate(parent.GetChild(0).gameObject);

            for(var i=0;i<eggCards.Count;i++){
            var index = i;
            eggCards[index].button.onClick.AddListener(()=>EggButtonPressed(index));
            }

    }
    
    public void PokemonRefresh()
    {
        
            var dic = PlayerPokemon.Instance.pokeDic;
            pokemonCards.Clear();
            if(dic==null||dic.Count<1){
                DestroyImmediate(parent.GetChild(0).gameObject);
                noDisplay.gameObject.SetActive(true);
                noDisplay.text = "You do not have any Pokemon";
                return;
            }

            if(!isSummary)
            noDisplay.gameObject.SetActive(false);

            while(parent.childCount != 1){
                DestroyImmediate(parent.GetChild(0).gameObject);
            }
            foreach(var p in dic){
                if(child==null)
                    return;
                var clone = Instantiate(child); 
                clone.transform.SetParent(parent);
                
                PokemonCard pokemonCard;
                if(isSummary){
                    pokemonCard = clone.transform.GetComponent<PokemonCard>();
                    var pokeSprite = pokemonCard.pokeLoader;
                    pokeSprite.poke = p.Value;
                    pokeSprite.getSprite();
                    
                    

                }else{
                    pokemonCard = clone.transform.GetComponent<PokemonCard>();

                    var pokeSprite = pokemonCard.pokeLoader;
                    pokeSprite.poke = p.Value;
                    pokeSprite.getSprite();
                    pokemonCard.SetStuff();
                   
                    
                    
                }
                

                
                
                pokemonCards.Add(pokemonCard);
                
            }

            DestroyImmediate(parent.GetChild(0).gameObject);
            child = parent.GetChild(0).gameObject;

            pokemonCards[0].starter = true;
            pokemonCards[0].bgIndex = 0;
            pokemonCards[0].background.overrideSprite = pokebgImages[0];
            pokemonCards[0].fainted = false;
            pokemonCards[0].SetHP();

            for(var i=0;i<pokemonCards.Count;i++){
                var index = i;
                pokemonCards[index].button.onClick.AddListener(()=>PokeButtonPressed(index));
            }

            

        
        
        
    }

   
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PokeButtonPressed(int ind){
        //Debug.Log(ind+" "+pokemonCards.Count+" "+cur);
        if(cur==ind){
            return;
        }

        
        
        if(cur>-1)
            pokemonCards[cur].background.overrideSprite = pokebgImages[pokemonCards[cur].bgIndex];

        cur = ind;
        pokemonCards[cur].background.overrideSprite = pokebgImages[pokemonCards[cur].bgIndex+1];

        pokemonCards[cur].OnClick();

     
        

    }

    public void EggButtonPressed(int ind){
        //Debug.Log(ind+" "+pokemonCards.Count+" "+cur);
        if(cur==ind){
            return;
        }

        if(cur>-1)
            eggCards[cur].background.overrideSprite = eggbgImages[0];

        cur = ind;
        eggCards[cur].background.overrideSprite = eggbgImages[1];

        eggCards[cur].OnClick();

     
        

    }


}
