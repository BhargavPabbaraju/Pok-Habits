using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using FullSerializer;
using TMPro;

public class PlayerItem : MonoBehaviour
{

    private static fsSerializer serializer = new fsSerializer();
    private const string projectId = "poketasker-206d8"; // You can find this in your Firebase project settings
    private static readonly string databaseURL = $"https://{projectId}-default-rtdb.firebaseio.com/";

    public Dictionary<int,int> itemDic = new Dictionary<int,int>();


    [SerializeField] public Sprite ribbonSprite;
    [SerializeField] public ItemSpritesList spriteList;
    public Item selectedItem;
    

    // Start is called before the first frame update
    private static PlayerItem _instance;
    // Start is called before the first frame update
    public static PlayerItem Instance{
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

    public void AddItem(Item i){


        if(itemDic==null)
            itemDic = new Dictionary<int,int>();
        
        if(itemDic.ContainsKey(i.id)){
            itemDic[i.id]++;
        }
        else{
            itemDic.Add(i.id , 1);
        }
        
        UpdateItem();
    }

    public void AddItem(int id){


        if(itemDic==null)
            itemDic = new Dictionary<int,int>();
        
        if(itemDic.ContainsKey(id)){
            itemDic[id]++;
        }
        else{
            itemDic.Add(id , 1);
        }
        
        UpdateItem();
    }


    
    public void GetItems()
    {
        DatabaseHandler.GetItems(items=>{
            itemDic = items;

        });

        if(itemDic==null)
        itemDic = new Dictionary<int,int>();

        
        
        
    }

    public void DeleteItem(){

        itemDic[selectedItem.id]--;
        if(itemDic[selectedItem.id]==0)
            itemDic.Remove(selectedItem.id);
            
        UpdateItem();
    }


    public void UpdateItem()
    {
        var curUser = AuthHandler.Instance.currentUser;
        var userId = curUser.userId;
        var idToken = AuthHandler.Instance.idToken;

        fsData data;
        serializer.TrySerialize(typeof(Dictionary<int,int>), itemDic, out data);
        var result = fsJsonPrinter.CompressedJson(data);
        //Debug.Log(result);

        RestClient.Put($"{databaseURL}items/{userId}.json?auth={idToken}", result).Then(response => {  
            //Debug.Log("Success");
        }).Catch(err=>{
            AuthHandler.Instance.PrintError(err);
        });

    }



    

    

    
}
