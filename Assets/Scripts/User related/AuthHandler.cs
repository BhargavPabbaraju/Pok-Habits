using System.Collections;
using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;


public class AuthHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private const string projectId = "poketasker-206d8"; // You can find this in your Firebase project settings
    private static readonly string databaseURL = $"https://{projectId}-default-rtdb.firebaseio.com/";
    private const string apiKey = "AIzaSyA4GTgOFOpb5tU427WN3XZPVH0_C3NLnIs"; // You can find this in your Firebase project settings
    public string localId;
    public string idToken;

    [SerializeField] public InputField email;
    [SerializeField] public InputField password;
    [SerializeField] public User currentUser;
    [SerializeField] public Text errorMessage;

    private static fsSerializer serializer = new fsSerializer();
    private static AuthHandler _instance;
    // Start is called before the first frame update
    public static AuthHandler Instance{
        get{
            if(_instance == null)
                Debug.LogError("Game Manager is Null");

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

    public void SignUpUser(){
        SignUp(email.text,password.text);
    }
    
    public void SignInUser(){
        SignIn(email.text,password.text);
    }

    private void SignUp(string email, string password){
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password +"\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key="+ apiKey,bodyString: userData).Then(
            onResolved: response=>{
                localId = response.localId;
                idToken = response.idToken;
                
                currentUser =new  User("Red",1,1,localId);
                ShopHandler.Instance.GetShop();
                DatabaseHandler.PostUser(currentUser, localId, idToken,()=>{});
                GameManager.Instance.changeScene(GameManager.Instance.NEWUSERSCENE);
                
                
                

        }).Catch(err =>PrintError(err));
    }

    public void SignIn(string email,string password){
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password +"\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=" + apiKey,bodyString: userData).Then(
            onResolved: response=>{
                localId = response.localId;
                idToken = response.idToken;
                //Debug.Log(idToken);
                DatabaseHandler.GetUser(localId, idToken,user=>{
                    currentUser = user;
                    //Debug.Log($" {user.name} {user.level}");
                    PlayerHabit.Instance.GetHabits();
                    PlayerPokemon.Instance.GetPokes();
                    PlayerEgg.Instance.GetEggs();
                    PlayerFood.Instance.GetFoods();
                    PlayerItem.Instance.GetItems();
                    ShopHandler.Instance.GetShop();
                    //Debug.Log(PlayerHabit.Instance.habitList.Count+ " Habits ");
                    GameManager.Instance.changeScene(GameManager.Instance.HABITSSCREEN,true);
                    if(TimeHandler.IncrementLoginCount(currentUser)){
                        DatabaseHandler.PostUser(currentUser, localId, idToken,()=>{});
                    }
                    PlayerPrefs.SetString("UserLocalId",localId);
                    PlayerPrefs.SetString("UserIdToken",idToken);
                    
                });

                
                //this.email.text = user.name+ "  " + user.level;
                
            }).Catch(err =>PrintError(err));
       

        
        
        
    }


    public void UpdateUser(){
        DatabaseHandler.PostUser(currentUser, currentUser.userId, idToken,()=>{});
    }


    public void PrintError(System.Exception err) {
                var erry = err as RequestException;
                var responseJson = erry.Response;

                var data = fsJsonParser.Parse(responseJson);
                
                object deserialized = null;
                serializer.TryDeserialize(data, typeof(Dictionary<string, AuthError>), ref deserialized);
                var error = deserialized as Dictionary<string, AuthError>;
                
                Debug.Log(error["error"].getErrorMessage());
                errorMessage.text = error["error"].getErrorMessage();
        }

    
    
}
