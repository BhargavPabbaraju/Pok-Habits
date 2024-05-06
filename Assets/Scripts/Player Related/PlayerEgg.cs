using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using TMPro;

public class PlayerEgg : MonoBehaviour
{

    private const string projectId = "poketasker-206d8"; // You can find this in your Firebase project settings
    private static readonly string databaseURL = $"https://{projectId}-default-rtdb.firebaseio.com/";

    public Dictionary<string,Egg> eggDic = new Dictionary<string,Egg>();

    [SerializeField] public EggSpritesList spriteList;
    public Egg selectedEgg;
    public Sprite raritySprite;

    

    // Start is called before the first frame update
    private static PlayerEgg _instance;
    // Start is called before the first frame update
    public static PlayerEgg Instance{
        get{
            if(_instance == null)
                Debug.LogError("Player Egg is Null");

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

    public void AddEgg(Egg e){
    
        if(eggDic==null)
            eggDic = new Dictionary<string,Egg>();
        
        
        e.GenerateUniqueId();
        
        eggDic.Add(e.uniqueId , e);
        UpdateEgg(e);
    }


    
    public void GetEggs()
    {
        DatabaseHandler.GetEggs(eggs=>{
            eggDic = eggs;

        });
        
    }
    
    public void UpdateEgg(Egg e){
        DatabaseHandler.PostEgg(e);

    }



    

    public void DeleteEgg(Egg e){
        var u = AuthHandler.Instance.currentUser;
        var userId = u.userId;
        var idToken = AuthHandler.Instance.idToken;

        
        
        if(eggDic.ContainsKey(e.uniqueId)){
            //Debug.Log($"{databaseURL}eggs/{userId}/{e.uniqueId}.json?auth={idToken}");
            DatabaseHandler.DeleteEgg(e,()=>{
                eggDic.Remove(e.uniqueId);
                    

                //GameManager.Instance.changeScene(GameManager.Instance.HABITSSCREEN);
                
            });
            
        }
    }

    public Egg GetEgg(string uniqueId){
        return eggDic[uniqueId];
    }
}
