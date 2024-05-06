using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using FullSerializer;
using TMPro;

public class PlayerFood : MonoBehaviour
{

    private static fsSerializer serializer = new fsSerializer();
    private const string projectId = "poketasker-206d8"; // You can find this in your Firebase project settings
    private static readonly string databaseURL = $"https://{projectId}-default-rtdb.firebaseio.com/";

    public Dictionary<int,int> foodDic = new Dictionary<int,int>();


    [SerializeField] public Sprite foodSprite;
    public Food selectedFood;

    // Start is called before the first frame update
    private static PlayerFood _instance;
    // Start is called before the first frame update
    public static PlayerFood Instance{
        get{
            if(_instance == null)
                Debug.LogError("Player Trainer is Null");

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

    public void AddFood(Food f){


        if(foodDic==null)
            foodDic = new Dictionary<int,int>();
        
        if(foodDic.ContainsKey(f.id)){
            foodDic[f.id]++;
        }
        else{
            foodDic.Add(f.id , 1);
        }
        
        UpdateFood();
    }


    
    public void GetFoods()
    {
        DatabaseHandler.GetFoods(foods=>{
            foodDic = foods;

        });

        if(foodDic==null)
        foodDic = new Dictionary<int,int>();

        
        
        
    }

    public void DeleteFood(){

        foodDic[selectedFood.id]--;
        if(foodDic[selectedFood.id]==0)
            foodDic.Remove(selectedFood.id);
            
        UpdateFood();
    }
    
    

    public void UpdateFood()
    {
        var curUser = AuthHandler.Instance.currentUser;
        var userId = curUser.userId;
        var idToken = AuthHandler.Instance.idToken;

        fsData data;
        serializer.TrySerialize(typeof(Dictionary<int,int>), foodDic, out data);
        var result = fsJsonPrinter.CompressedJson(data);
        //Debug.Log(result);

        RestClient.Put($"{databaseURL}foods/{userId}.json?auth={idToken}", result).Then(response => {  
            //Debug.Log("Success");
        }).Catch(err=>{
            AuthHandler.Instance.PrintError(err);
        });

    }
    
}
