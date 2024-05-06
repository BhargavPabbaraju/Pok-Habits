using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using TMPro;

public class PlayerHabit : MonoBehaviour
{

    private const string projectId = "poketasker-206d8"; // You can find this in your Firebase project settings
    private static readonly string databaseURL = $"https://{projectId}-default-rtdb.firebaseio.com/";

    public Dictionary<string,Habit> habitDic = new Dictionary<string,Habit>();
    public const int HABITINCREMENTS = 5;
    public  int EXPINCREMENTS = 5;

    [SerializeField] public Habit selectedHabit;

    // Start is called before the first frame update
    private static PlayerHabit _instance;
    // Start is called before the first frame update
    public static PlayerHabit Instance{
        get{
            if(_instance == null)
                Debug.LogError("Player Habit is Null");

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

    public void AddHabit(Habit h){

        if(habitDic==null)
            habitDic = new Dictionary<string,Habit>();
        
        habitDic.Add(h.title , h);
        UpdateHabit(h);
    }


    public void ResetCounternumbers(){
        var today = System.DateTime.Now;
        foreach(var hs in habitDic){
            var h = hs.Value;
            if(TimeHandler.checkHabitUpdate(h.lastUpdate,h.resetType)){
                h.posCount = 0;
                h.negCount = 0;
                DatabaseHandler.DeleteHabit(h,()=>{
                    habitDic.Remove(h.title);
                    AddHabit(h);

            });
         }

        }
    }

    public void GetHabits()
    {
        DatabaseHandler.GetHabits(habits=>{
            habitDic = habits;
            ResetCounternumbers();

        });

        
        
        
    }
    
    public void UpdateHabit(Habit h){
        DatabaseHandler.PostHabit(h);

    }

    public void UpdateHabitCount(string title,bool pos,TMP_Text posText , TMP_Text negText,PokemonLoader poke){
        var h = habitDic[title];
        var u = AuthHandler.Instance.currentUser;

        var popUp =  GameManager.Instance.popUpSystem;
        
        var userId = u.userId;
        var idToken = AuthHandler.Instance.idToken;
        int ind = 2;
        int r = Random.Range(0,100);
        if(pos) {
            h.posCount++;
            if(r<10){
                ind = 2;
            }else{
                ind = 0;
                
            }
            
            u.currency[ind]+=HABITINCREMENTS;
            u.exp[0] += HABITINCREMENTS;
            
            }

        else{h.negCount++;u.hp[0]-=popUp.calcHealthPoints(HABITINCREMENTS);}

        

        if(u.exp[0]>u.exp[1]){
            u.incrementLevel();
            popUp.LevelUp();
            
        }
        else if(u.hp[0]<=0){
            popUp.Loss();
            u.decrementLevel();

        }
           
        else{
            r = Random.Range(0,100);
            if(pos && r<50){
                popUp.gotHabitEgg();
            }
            else
                popUp.PopUp(EXPINCREMENTS,HABITINCREMENTS,ind,pos);
        }

        
            
        
        
        DatabaseHandler.PostUser(u, userId, idToken,()=>{
            poke.SetCurrency();
        });
        
        RestClient.Put<Habit>($"{databaseURL}habits/{userId}/{h.title}.json?auth={idToken}", h).Then(response=>{
            posText.text = "+ " + h.posCount.ToString();
            negText.text = "− " + h.negCount.ToString();
            
        });

        //Debug.Log($"{databaseURL}habits/{userId}/{h.title}.json?auth={idToken}");

    }


    public void WriteHabits(){
        foreach(var h in habitDic){
            DatabaseHandler.PostHabit(h.Value);
        }
    }

    

    public void DeleteHabit(Habit h,string title){
        var u = AuthHandler.Instance.currentUser;
        var userId = u.userId;
        var idToken = AuthHandler.Instance.idToken;

        
        if(habitDic.ContainsKey(h.title)){
            Debug.Log($"{databaseURL}habits/{userId}/{h.title}.json?auth={idToken}");
            DatabaseHandler.DeleteHabit(h,()=>{
                habitDic.Remove(h.title);
                if(title!=null){
                    h.title = title;
                    AddHabit(h);
                    return;

                }

                GameManager.Instance.changeScene(GameManager.Instance.HABITSSCREEN);
                
            });
            
        }
    }

    public Habit GetHabit(string title){
        return habitDic[title];
    }
}
