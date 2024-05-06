using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonSpriteGetter : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    [SerializeField] public bool plus;
    [SerializeField] public bool minus;
    [SerializeField] public bool midbut;
    [SerializeField] public int isType2;
    [SerializeField] PokemonLoader poke;
    [SerializeField] ButtonSpriteList spriteList;
    [SerializeField] public TMP_Text title;
    [SerializeField] public TMP_Text posText;
    [SerializeField] public TMP_Text negText;


    Button button;
    float initialYPosition;

    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        initialYPosition = button.transform.localPosition.y;
        RefreshSprite();
    }

    // Update is called once per frame
    Sprite GetPressedSprite(){
        int t=0;
        if(isType2==2)
            t=spriteList.normalPlusButtons.Length -1 ;
        else
            t = PlayerPokemon.Instance.typeIndex[isType2];
            
        if(plus)
            return spriteList.pressedPlusButtons[t];
        if(minus)
            return spriteList.pressedMinusButtons[t];
        else
            return spriteList.pressedMidButtons[t];
    }

    Sprite GetNormalSprite(){
        if(isType2==2){
            return GetPressedSprite();
        }
        var t = PlayerPokemon.Instance.typeIndex[isType2];
            
        if(plus)
            return spriteList.normalPlusButtons[t];
        if(minus)
            return spriteList.normalMinusButtons[t];
        else
            return spriteList.normalMidButtons[t];
    }
    void Update()
    {
        
    }
   

    public void RefreshSprite(){
        
       var pressedSprite = GetPressedSprite();
       var normalSprite = GetNormalSprite();
       SpriteState spriteState = new SpriteState();
       try{
           spriteState = button.spriteState;
           if (enabled)
              {
                  spriteState.pressedSprite = pressedSprite;
                  
              }
              else
              {
                  spriteState.pressedSprite = normalSprite;
              }
        button.spriteState = spriteState;

        button.image.sprite = normalSprite;
       }
       catch(NullReferenceException){
           //Debug.Log(name+" "+transform.localPosition.y);
       }
       
       
    }

    public void OnPointerDown(PointerEventData eventData){
        if(button.interactable==false)
            return;
        //return;
        if(midbut){
            
        button.transform.localPosition = new Vector3(button.transform.localPosition.x,initialYPosition-8,button.transform.localPosition.x);
        
        //Making positive counter jump up

        for(int i=1;i<3;i++){
            var posText = this.gameObject.transform.GetChild(i).transform;
            posText.localPosition = new Vector3(posText.localPosition.x,initialYPosition+8,posText.localPosition.x);
        }
            

            
        }
        
    }
 
    public void OnPointerUp(PointerEventData eventData){
        if(button.interactable==false)
            return;

        if(midbut){
        
        button.transform.localPosition = new Vector3(button.transform.localPosition.x,initialYPosition,button.transform.localPosition.x);
            //Making positive counter jump down
            for(int i=1;i<3;i++){
                var posText = this.gameObject.transform.GetChild(i).transform;
                posText.localPosition = new Vector3(posText.localPosition.x,initialYPosition-10,posText.localPosition.x);
            }

            PlayerHabit.Instance.selectedHabit = PlayerHabit.Instance.GetHabit(title.text);
            GameManager.Instance.changeScene(GameManager.Instance.HABITSCENE);
            
        }

        else{
            
            GameManager.Instance.popUpSystem.SetColor(PlayerPokemon.Instance.GetCurrentTypeIndex());
            PlayerHabit.Instance.UpdateHabitCount(title.text,plus,posText,negText,poke);
        }
    }
}
