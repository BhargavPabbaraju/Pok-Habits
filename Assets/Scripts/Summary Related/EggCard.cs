using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EggCard : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public EggLoader eggLoader;
    [SerializeField] public TMP_Text timerText;
    [SerializeField] public Image timerBar;
    [SerializeField] public EggSummary summary;
    [SerializeField] public PartyOptions options;
    [SerializeField] public Image background;
    [SerializeField] public Button button;
    [SerializeField] public Sprite[] rarities;
    [SerializeField] public Image raritySprite;
    [SerializeField] public Sprite[] cracks;
    [SerializeField] public Image crack;
    [SerializeField] public GameObject clone;
    [SerializeField] public ActiveButton activeButton;

    void Start()
    {
        
    }

    public void SetRarity(){
        int r = 0;
        switch(eggLoader.egg.rarity){
            case "C":
                r = 0;
                break;
            
            case "UC":
                r = 1;
                break;
            
            case "R":
                r = 2;
                break;
            
            case "EX":
                r = 3;
                break;
            

            case "UX":
                r = 4;
                break;

        }
        raritySprite.overrideSprite = rarities[r];
    }

    public void SetCracks(){
        var mins = TimeHandler.getTimeRemainingInSeconds(eggLoader.egg.lastUpdate);
        var minutes = eggLoader.egg.time - mins;
        int c = 0;
        if(minutes<=3){
            c=0;
        }else if(minutes<=15){
            c=1;
        }else if(minutes<=30){
            c=2;
        }else if(minutes<=60){
            c=3;
        }else if(minutes<=120){
            c=4;
        }else{
            c=5;
        }
        crack.overrideSprite = cracks[c];


    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.hatching)
            return;
        
        if(summary==null){


            timerText.text = TimeHandler.getTimeRemaining(eggLoader.egg.lastUpdate,eggLoader.egg.time,false);
                    
            var mins = TimeHandler.getTimeRemainingInSeconds(eggLoader.egg.lastUpdate);
            if(eggLoader.egg.time<=mins){
                Debug.Log("From here"+eggLoader.egg.id);
                var poke =eggLoader.egg.Hatch();
                activeButton.PokemonRefresh();
                Debug.Log(gameObject.activeSelf);
                GameManager.Instance.popUpSystem.HatchEgg(poke);
                
                //GameManager.Instance.changeScene(GameManager.Instance.POKEMONINVSCENE);
                
                
                gameObject.SetActive(false);
                Destroy(clone);
                
            }
            //Debug.Log(mins+" "+eggLoader.egg.time+" "+mins/eggLoader.egg.time);
            timerBar.fillAmount = Mathf.Clamp(0,mins/(float) eggLoader.egg.time,1f); 
        }
        
    }

    public void OnClick(){
        PlayerEgg.Instance.selectedEgg = eggLoader.egg;

        if(summary!=null){
            summary.Refresh();

        }
        else{
            //background.overrideSprite = bgImages[1];
            options.gameObject.SetActive(true);
            //Debug.Log(PlayerEgg.Instance.selectedEgg.id);
            //GameManager.Instance.changeScene(GameManager.Instance.EGGSUMMARYSCENE);
        }
    }
}
