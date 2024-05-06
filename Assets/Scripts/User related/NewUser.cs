using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewUser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TrainerLoader[] trainers;
    [SerializeField] public PokemonLoader[] pokes;
    [SerializeField] public TrainerLoader trainer;
    [SerializeField] public PokemonLoader poke;
    [SerializeField] public TMP_Text trname;
    [SerializeField] public int[] trIds;
    [SerializeField] public string[] pokeIds;
    [SerializeField] public int gen;
    void Awake()
    {
        
        setIds();
    }

    public void setIds(){
        if(gen<9){
            gen = Random.Range(1,10);
        }
        if(gen>9){
            gen = 9;
        }
        gen-=1;
        int j =0;
        for(int i=3*gen;i<3*gen+3;i++){
            trainers[j].id = trIds[i];
            trainers[j].RefreshSprite();
            pokes[j].id = pokeIds[i];
            pokes[j].RefreshSprite();
            j++;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTrainer(TrainerLoader tr){
        trainer = tr;
        tr.GetComponent<Button>().interactable = false;
        foreach(var t in trainers){
            if(t!=tr)
                t.GetComponent<Button>().interactable = true;
        }
    }

    public void SetPoke(PokemonLoader pk){
        poke = pk;
        pk.GetComponent<Button>().interactable = false;
        foreach(var p in pokes){
            if(p!=pk)
                p.GetComponent<Button>().interactable = true;
        }
    }

    public void MakeUser(){
        var curUser = AuthHandler.Instance.currentUser;
        var idToken = AuthHandler.Instance.idToken;
        if (trname.text.Length<=1){
            curUser.name = "Red";
        }else{
            curUser.name = trname.text;
        }
        curUser.trId = trainer.id;

        poke.poke.Starter();
        poke.poke.method = 1;

        curUser.hp[2] = poke.poke.basestats[0];
        curUser.hp[3] = poke.poke.ivs[0];
        curUser.calculateHP();
        curUser.trainers.Add(trainer.id);
        curUser.lastLogin = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        
        
        DatabaseHandler.PostUser(curUser,curUser.userId,idToken,() =>{});
        PlayerPrefs.SetString("UserLocalId",curUser.userId);
        
        //Debug.Log(idToken);
        PlayerPokemon.Instance.AddPoke(poke.poke);
        
        
        GameManager.Instance.changeScene(GameManager.Instance.HABITSSCREEN);
    }

    
}
