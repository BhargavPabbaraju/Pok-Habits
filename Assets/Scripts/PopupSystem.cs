using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PopupSystem : MonoBehaviour
{
    [SerializeField] public GameObject popUpBox;
    [SerializeField] public TMP_Text[] buttonTexts;
    [SerializeField] public Sprite[] sprites;
    [SerializeField] public Image coinSprite;
    [SerializeField] public GameObject[] objects; //0-increment 1-decrement 2-levelUp 3-loss
    [SerializeField] public Color[] colors;
    [SerializeField] public Button[] buttons;
    [SerializeField] public EggLoader eggSprite;
    [SerializeField] public PokemonLoader pokeSprite;

    public Animator animator;

    private int HEALTHPOINTS;
    
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.popUpSystem = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public int calcHealthPoints(int points){
        if (points == 5)
            return 2;
        
        return 4;
    }

    public void setStateFalse(){
        for(int i=0;i<objects.Length;i++){
            objects[i].SetActive(false);
        }
        
    }

    public void doPop(int index){
        objects[index].SetActive(true);
        animator.SetTrigger("pop");
    }

    public void SetColor(int i){
        foreach(Button but in buttons){
            but.GetComponent<Image>().color = colors[i];
        }
        
    

    }

    public void PopUp(int exp,int points,int ind,bool pos){
        setStateFalse();
        int index =0;

        if(pos){
            coinSprite.overrideSprite = sprites[ind];
            index = 0;
            buttonTexts[1].text = "<color=\"white\">+" + points.ToString();
            buttonTexts[0].text = "<color=\"white\">+" + exp.ToString();
        }else{
            index = 1;
            HEALTHPOINTS = calcHealthPoints(points);
            buttonTexts[2].text = "<color=\"white\">−" + HEALTHPOINTS.ToString();
        }

        
        doPop(index);
        StartCoroutine(waiter());

    }

    public void LevelUp(){
        setStateFalse();

        var level = AuthHandler.Instance.currentUser.level;
        buttonTexts[3].text = $"<color=black>You reached Level {level}!";
        
        
        doPop(2);
        


    }

    public void Loss(){
        setStateFalse();
        


        doPop(3);

    }

    public void HatchEgg(Pokemon poke){

        //StartCoroutine(waiter2());
        //Debug.Log(GameManager.Instance.hatching+" "+poke.pokeName);

        GameManager.Instance.hatching = true;
        pokeSprite.poke = poke;
        pokeSprite.doNotAwake = true;
        pokeSprite.poke_name.text = poke.pokeName;
        pokeSprite.getSprite();

        doPop(0);
        

    }


    public void gotHabitEgg(){
        var egg = GatchaSystem.getHabitEgg(-1);
        eggSprite.egg = egg;
        PlayerEgg.Instance.AddEgg(egg);
        eggSprite.RefreshSprite();

        doPop(4);
    }


    public void levelClose(){
        animator.SetTrigger("close");
        setStateFalse();
    }

    public void HatchClose(){
        //Debug.Log(GameManager.Instance.hatching);
        
        StartCoroutine(waiter2());
        //GameManager.Instance.changeScene(GameManager.Instance.POKEMONINVSCENE);
        
        
    }
    

    IEnumerator waiter(){
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("close");
        setStateFalse();
        
    }

    IEnumerator waiter2(){
        //yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("close");
        setStateFalse();
        
        
        yield return new WaitForSeconds(1f);
        GameManager.Instance.hatching = false;
        
        
    }
    
}
