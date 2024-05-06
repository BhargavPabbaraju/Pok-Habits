using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using System.Linq;
using Proyecto26;
using FullSerializer;
//using System.Threading.Tasks;

public class PlayerPokemon : MonoBehaviour

{

    private static fsSerializer serializer = new fsSerializer();
    private const string projectId = "poketasker-206d8"; // You can find this in your Firebase project settings
    private static readonly string databaseURL = $"https://{projectId}-default-rtdb.firebaseio.com/";

    private static PlayerPokemon _instance;
    // Start is called before the first frame update
    public static PlayerPokemon Instance{
        get{
            if(_instance == null)
                Debug.LogError("Player Pokemon is Null");

            return _instance;
        }
        
    }
    void Awake(){
        if(_instance==null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
            
    }
    //public List<Pokemon> pokemonList;
    public Dictionary<string,Pokemon> pokeDic = new Dictionary<string,Pokemon>();
    public int[] typeIndex = new int[2];
    public int currentType = 0;
    public PokemonSpritesList spriteList;
    public ColorList colList;
    public Pokemon selectedPoke;

    public void AddPoke(Pokemon pk){
        //pokemonList.Add(pk);
        if(pk.uniqueId!="-1")
            pk.GenerateUniqueId();
            
        pokeDic.Add(pk.uniqueId,pk);
        UpdatePoke(pk);
    }


    public void GetPokes()
    {
        pokeDic = new Dictionary<string,Pokemon>();
        //pokemonList.Clear();
        DatabaseHandler.GetPokemons(pokes=>{
            pokeDic = pokes;
            // foreach(var pk in pokes){
            //     //Debug.Log(pk.Value.pokeName);
            //     //pokemonList.Add(pk.Value);
            // }

           
            
        });
        
        
    }



   
   
   public int GetCurrentTypeIndex(){
    return typeIndex[currentType];
   }

   

    public Pokemon  GetStarter(){
        var starter = pokeDic["-1"];
        typeIndex = new int[] {spriteList.getTypeIndex(starter.types[0]),spriteList.getTypeIndex(starter.types[1])};
        if(typeIndex[1]==18)
            typeIndex[1]=typeIndex[0];
        return starter;
        //return  pokemonList[0];


    }
    
    public void UpdatePoke(Pokemon pk){
        DatabaseHandler.PostPokemon(pk);

    }

    public void WritePokes(){
        var curUser = AuthHandler.Instance.currentUser;
        var userId = curUser.userId;
        var idToken = AuthHandler.Instance.idToken;

        fsData data;
        serializer.TrySerialize(typeof(Dictionary<string,Pokemon>), pokeDic, out data);
        var result = fsJsonPrinter.CompressedJson(data);
        //Debug.Log(result);

        RestClient.Put($"{databaseURL}pokemonlist/{userId}.json?auth={idToken}", result).Then(response => {  
            //Debug.Log("Success");
        }).Catch(err=>{
            AuthHandler.Instance.PrintError(err);
        });

        
    }


    public void SetPrimary(){
       var poke = selectedPoke;
       pokeDic.Remove(poke.uniqueId);
       var starter = pokeDic["-1"];
       pokeDic["-1"] = poke;
       starter.GenerateUniqueId();   
       pokeDic.Add(starter.uniqueId,starter);
       WritePokes();

    }

    public void ChangeForm(Pokemon p,Pokemon f){
        var id1 = p.uniqueId;
        p.ChangeForm(f);
        if(id1!="-1"){
            p.GenerateUniqueId();
        }
        pokeDic.Remove(id1);
        pokeDic.Add(p.uniqueId,p);
        WritePokes();

    }

    public void TakeItem(){
        var heldItem = selectedPoke.heldItem;
        selectedPoke.heldItem = -1;
        if(selectedPoke.id.Contains("M")){
            
            var poke2 = GameObject.Find("Database").GetComponent<LoadPokemonDatabase>().GetPokemon(selectedPoke.id.Split('M')[0]);
            ChangeForm(selectedPoke,poke2);
        }

        PlayerItem.Instance.AddItem(heldItem);
        UpdatePoke(selectedPoke);
        GameManager.Instance.changeScene(GameManager.Instance.POKEMONINVSCENE);
    }



}
