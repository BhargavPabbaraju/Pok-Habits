using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using TMPro;
using FullSerializer;

public class ShopHandler : MonoBehaviour
{

    private static fsSerializer serializer = new fsSerializer();
    private const string projectId = "poketasker-206d8"; // You can find this in your Firebase project settings
    private static readonly string databaseURL = $"https://{projectId}-default-rtdb.firebaseio.com/";

    

    // Start is called before the first frame update
    private static ShopHandler _instance;
    // Start is called before the first frame update
    public static ShopHandler Instance{
        get{
            if(_instance == null)
                Debug.LogError("Shop Handler is Null");

            return _instance;
        }
        
    }

    public Shop shop;
    public Sprite currencySprite;
    public Sprite selectedBackdrop;
    public int selectedBackdropIndex;
    public string shopCost;
    public int selectedCurr;
    public ShopCard shopCard;

    public Sprite[] backdropSprites;

    public List<string> locationsList;
    public string location;


    void Awake(){
        if(_instance==null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        

    }

    public void PostShop()
    {
        var curUser = AuthHandler.Instance.currentUser;
        var userId = curUser.userId;
        var idToken = AuthHandler.Instance.idToken;
        RestClient.Put<Shop>($"{databaseURL}shop.json?auth={idToken}", shop).Then(response => {  
            //curUser.numberOfHabits+=1;
        });
        
    }

    public void GetShop(){
        var curUser = AuthHandler.Instance.currentUser;
        var userId = curUser.userId;
        var idToken = AuthHandler.Instance.idToken;

        RestClient.Get<Shop>($"{databaseURL}shop.json?auth={idToken}").Then(s=>{
        shop=s;
        CheckUpdate();

            
            
        }).Catch(err=>{
            CreateShop();
        });
    }


    void CheckUpdate(){
        var now = System.DateTime.UtcNow;
        var date = System.DateTime.ParseExact(shop.lastUpdate,"dd-MM-yyyy HH:mm:ss",null);
        var dif = now - date;
        //Debug.Log(dif.Hours);
        if(dif.Hours>=6){
            CreateShop();
        }
    }


    void CreateShop(){
        shop = new Shop();
        shop.NewShop();
        var now = System.DateTime.UtcNow;
        shop.lastUpdate = now.ToString("dd-MM-yyyy HH:mm:ss");
        PostShop();
    }

    public bool CheckCanBuy(){
        var user = AuthHandler.Instance.currentUser;
        var curr = user.currency;

        if (curr[selectedCurr]>=int.Parse(shopCost)){
            AuthHandler.Instance.currentUser.currency[selectedCurr]-=int.Parse(shopCost);
            AuthHandler.Instance.UpdateUser();
            return true;
        }
        return false;
    }

    public string GetLocation(){
        
        
        RestClient.Get<Location>("https://extreme-ip-lookup.com/json/?key=XXVRMcLQ8AYHwHFBt3GO").Then(res=>{
            location = res.city+","+res.countryCode;
            if(locationsList==null){
                GetLocationList();
                if(locationsList==null)
                    locationsList = new List<string>();
            }

            if(!locationsList.Contains(location)){
                locationsList.Add(location);
                PostLocationList();
            }

            
            return location;
        }).Catch(err=>{
            AuthHandler.Instance.PrintError(err);
            return "Hyderabad,IN";
        });
        return "Hyderabad,IN";
    }


    public void GetLocationList(){
      
        var userId = AuthHandler.Instance.currentUser.userId;
        var idToken = AuthHandler.Instance.idToken;
        RestClient.Get($"{databaseURL}locations.json?auth={idToken}").Then(response =>
        {
            var responseJson = response.Text;

            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(List<string>), ref deserialized);

            var locs = deserialized as List<string>;

            locationsList = locs;
        });
    
    }

    public void PostLocationList(){
      
        var curUser = AuthHandler.Instance.currentUser;
        var userId = curUser.userId;
        var idToken = AuthHandler.Instance.idToken;

        fsData data;
        serializer.TrySerialize(typeof(List<string>), locationsList, out data);
        var result = fsJsonPrinter.CompressedJson(data);
        //Debug.Log(result);

        RestClient.Put($"{databaseURL}locations.json?auth={idToken}", result).Then(response => {  
            //Debug.Log("Success");
        }).Catch(err=>{
            AuthHandler.Instance.PrintError(err);
        });
    
    }

}
