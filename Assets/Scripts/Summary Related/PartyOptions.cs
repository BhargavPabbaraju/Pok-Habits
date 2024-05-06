using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PartyOptions : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Image[] selectors;
    [SerializeField] public ItemEffects itemEffects;
    

    [SerializeField] public GameObject takeitemButton;
    [SerializeField] public GameObject giveitemButton;

    [SerializeField] public GameObject tradePanel;


    void Awake(){
        if(takeitemButton!=null){
            
            var poke = PlayerPokemon.Instance.selectedPoke;
            if(poke.heldItem==-1){
                takeitemButton.SetActive(false);
            }else{
                takeitemButton.SetActive(true);
            }
            //Debug.Log(poke.heldItem);
        }
        if(giveitemButton!=null){
            var poke = PlayerPokemon.Instance.selectedPoke;
            if(poke.heldItem==-1){
                giveitemButton.SetActive(true);
            }else{
                giveitemButton.SetActive(false);
            }
        }
    }
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EggOnclick(int op){
        switch(op){
            case 0:{
                //Debug.Log("Summary");
                GameManager.Instance.changeScene(GameManager.Instance.EGGSUMMARYSCENE);
                break;
            }
            case 1:{
                Debug.Log("Release");
                break;
            }
            case 2:{
                gameObject.SetActive(false);
                break;
            }
        }
    }
    

    public void PokemonOnclick(int op){
        switch(op){
            case 0:{
                //Debug.Log("Summary");
                GameManager.Instance.changeScene(GameManager.Instance.POKEMONSUMMARYSCENE);
                break;
            }
            case 1:{
                PlayerPokemon.Instance.SetPrimary();
                GameManager.Instance.changeScene(GameManager.Instance.HABITSSCREEN);
                //Debug.Log("Set Primary");
                break;
            }
            case 2:{
                //Debug.Log("Feed");
                GameManager.Instance.changeScene(GameManager.Instance.FOODSCENE);
                break;
            }
            case 3:{
                GameManager.Instance.changeScene(GameManager.Instance.BAGSCENE);
                //Debug.Log("Item");
                break;
            }
            case 4:{
                Debug.Log("Nickname");
                break;
            }
            case 5:{
                tradePanel.SetActive(true);
                break;
            }
            case 6:{
                Debug.Log("Release");
                break;
            }
            case 7:{
                //Debug.Log("Cancel");
                gameObject.SetActive(false);
                break;
            }
            case 8:{
                PlayerPokemon.Instance.TakeItem();
                break;
            }
        }
        

    }


    void SetTransparent(int op){
        for(int i=0;i<selectors.Length;i++){
            selectors[i].color = new Color(0,0,0,0);
            if(i==op){
                selectors[i].color = new Color(255,255,255,255);
            }
        }
        itemEffects.noeffectObject.SetActive(false);

    }

    

    public void FoodOnClick(int op){
        SetTransparent(op);
        switch(op){

            case 0:{
                //Debug.Log("Feed");
                itemEffects.Feed();
                break;
            }
            case 2:{
                //Debug.Log("Cancel");
                SetTransparent(8);
                gameObject.SetActive(false);
                break;
            }
            case 1:{
                PlayerFood.Instance.DeleteFood();
                GameManager.Instance.changeScene(GameManager.Instance.FOODSCENE);
                //Debug.Log("Trash");
                break;
            }
            
        }
    }


    public void TradeOnClick(int op){
        switch(op){
            case 0:{
                //Deposit
                GameManager.Instance.changeScene(GameManager.Instance.GTSSCENE);
                break;
            }
            case 1:{
                //Seek
                break;
            }
            case 2:{
                tradePanel.SetActive(false);
                break;
            }
        }


        
    }



    public void BagOnClick(int op){
        SetTransparent(op);
        switch(op){

            case 0:{
                //Debug.Log("Use");
                itemEffects.UseItem();
                break;
            }
            case 1:{
                itemEffects.GiveItem();
                break;
            }
            case 3:{
                //Debug.Log("Cancel");
                SetTransparent(8);
                gameObject.SetActive(false);
                break;
            }
            case 2:{
                PlayerItem.Instance.DeleteItem();
                GameManager.Instance.changeScene(GameManager.Instance.BAGSCENE);
                //Debug.Log("Trash");
                break;
            }
            
        }
    }
}
