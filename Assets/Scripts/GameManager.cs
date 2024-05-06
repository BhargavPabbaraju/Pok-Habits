using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    // Start is called before the first frame update
    public static GameManager Instance{
        get{
            if(_instance == null)
                Debug.LogError("Game Manager is Null");

            return _instance;
        }
    }

    public int INITIALSCREEN = 0;
    public int MAINSCREEN = 1;
    public int SINGUPSCENE = 2;
    public int HABITSSCREEN = 3;
    public  int CREATEHABITSCREEN = 4;
    public  int NEWUSERSCENE = 5;
    public  int HABITSCENE = 6;
    public  int POKEMONINVSCENE = 7;
    public  int EGGSUMMARYSCENE = 8;
    public  int POKEMONSUMMARYSCENE = 9;
    public  int SHOPSCENE = 10;
    public  int EDITUSERSCENE = 11;
    public  int BAGSCENE = 12;
    public  int FOODSCENE = 13;
    public  int GTSSCENE = 14;


    public int scene = 0;
    public bool login;

    public PopupSystem popUpSystem;
    public ShopPopUp shopPopUp;
    public bool hatching = false;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _pokeBall;
    [SerializeField] public SpriteGetter spritegetter;
    

    
    

    public async void changeScene(int sc){
        _loaderCanvas.SetActive(true);
        StartCoroutine(LoadAsynchronously(sc,false));
        
        
        
    }

    IEnumerator LoadAsynchronouslyWait(int sc){
        scene = sc;
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);
            while(PlayerPokemon.Instance.pokeDic.Count<1){
                _pokeBall.transform.Rotate(0,0,30);
                yield return null;
            }
            
            
    }


    public async void changeScene(int sc,bool wait){
        _loaderCanvas.SetActive(true);
        StartCoroutine(LoadAsynchronously(sc,wait));
    }
    

    IEnumerator LoadAsynchronously(int sc,bool wait){
        scene = sc;
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);


        if(wait){
            op.allowSceneActivation = false;
            yield return LoadAsynchronouslyWait(sc);

            //Debug.Log(PlayerPokemon.Instance.pokemonList.Count);

            op.allowSceneActivation = true;

        }
        

        while(!op.isDone){
                _pokeBall.transform.Rotate(0,0,30);
            
                yield return null;
            }

        _loaderCanvas.SetActive(false);
        _pokeBall.transform.eulerAngles = new Vector3(0,0,0);
        
    }

    



    void Start()
    {
        
    }
    
    public void checkUserExists(){
        if(PlayerPrefs.HasKey("UserLocalId")){
            string idToken = PlayerPrefs.GetString("UserIdToken");
            string localId = PlayerPrefs.GetString("UserLocalId");
            User currentUser = null;
            DatabaseHandler.GetUser(localId,idToken ,user=>{
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
                    
                    
                });
            if(currentUser ==null){
                scene = MAINSCREEN;
                changeScene(MAINSCREEN);
            }
            
        }else{
            scene = MAINSCREEN;
            changeScene(MAINSCREEN);

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

        if(scene == INITIALSCREEN){
            checkUserExists();
        }
      
    }

    // Update is called once per frame

    public void Signpage(bool log){
        login = log;
        changeScene(SINGUPSCENE);
        

    }
   
    public void makeHabit(){
        changeScene(CREATEHABITSCREEN);
    }
    

    void Update(){
    // Make sure user is on Android platform
        //if (Application.platform == RuntimePlatform.Android) {

             if(Input.GetKeyDown(KeyCode.D)){
                AuthHandler.Instance.SignIn("a@a.com","123456");

             }
            
            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape)) {

                if(scene == POKEMONINVSCENE){
                    PlayerPokemon.Instance.WritePokes();
                }
                if(scene==HABITSSCREEN){
                    Application.Quit();
                    
                }
                else if(scene == SINGUPSCENE){
                    changeScene(MAINSCREEN);
                }
                else if(scene == POKEMONSUMMARYSCENE || scene == EGGSUMMARYSCENE || scene== BAGSCENE || scene== FOODSCENE || scene == GTSSCENE){
                    changeScene(POKEMONINVSCENE);
                }

                

                else
                // Quit the application
                changeScene(HABITSSCREEN);
            }
        //}
    }

    void OnApplicationQuit()
    {
        PlayerPokemon.Instance.WritePokes();
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
}
 

