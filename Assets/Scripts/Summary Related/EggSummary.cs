using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EggSummary : MonoBehaviour

{
    // Start is called before the first frame update

    [SerializeField] public EggLoader eggLoader;
    [SerializeField] public TMP_Text dateText;
    [SerializeField] public TMP_Text eggMethod;
    [SerializeField] public TMP_Text[] eggWatch;
    [SerializeField] public Sprite[] cracks;
    [SerializeField] public Image crack;

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

    public void Refresh(){
        //Debug.Log(PlayerEgg.Instance.selectedEgg.id);
        eggLoader.egg = PlayerEgg.Instance.selectedEgg;
        eggLoader.RefreshSprite();
        var egg = eggLoader.egg;
        SetEggMethod(egg.method);
        SetDate(egg.month,egg.day,egg.year);
        var mins = TimeHandler.getTimeRemainingInSeconds(eggLoader.egg.lastUpdate);
        SetEggWatch(mins);
        SetCracks();

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

    public void SetEggWatch(int mins){

        int minutes = eggLoader.egg.time - mins;

        //Debug.Log(mins+" "+minutes+" "+eggLoader.egg.time);

        if(minutes<=3){
            eggWatch[0].text = "Sounds can be heard";
            eggWatch[1].text = "coming from inside! It will";
            eggWatch[2].text = "hatch soon!";
        }
        else if(minutes<=15){
            eggWatch[0].text = "It appears to move";
            eggWatch[1].text = "occasionally. It may be";
            eggWatch[2].text = "close to hatching.";
        }
        else if(minutes<=30){
            eggWatch[0].text = "What will hatch from this?";
            eggWatch[1].text = "It doesn't seem close to";
            eggWatch[2].text = "hatching.";
        }
        else{
            eggWatch[0].text = "It looks like this Egg will";
            eggWatch[1].text = "take a long time to hatch.";
            eggWatch[2].text = "";
        }
        
    }

    public void SetDate(string month,string day,string year){
        dateText.text = $"{month}.{day},{year}";

    }

    public void SetEggMethod(int method){
        switch(method){
            case 0:{
                eggMethod.text = "maintaining a habit.";
                break;
            }
        }
    }
}
