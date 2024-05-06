using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using TMPro;

public class PlayerTrainer : MonoBehaviour
{

   
    


    [SerializeField] public TrainerSpritesList spriteList;
    public Trainer selectedTrainer;

    // Start is called before the first frame update
    private static PlayerTrainer _instance;
    // Start is called before the first frame update
    public static PlayerTrainer Instance{
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

    public void AddTrainer(Trainer tr){
        var curUser = AuthHandler.Instance.currentUser;
        curUser.trainers.Add(tr.id);
        AuthHandler.Instance.UpdateUser();
    }

    public void AddBackdrop(int id){
        var curUser = AuthHandler.Instance.currentUser;
        curUser.backdrops.Add(id);
        AuthHandler.Instance.UpdateUser();
    }

    public void SetTrainer(Trainer tr){
        var curUser = AuthHandler.Instance.currentUser;
        curUser.trId = tr.id;
        AuthHandler.Instance.UpdateUser();
    }

    public void SetBackdrop(int id){
        var curUser = AuthHandler.Instance.currentUser;
        curUser.backdropId = id;
        AuthHandler.Instance.UpdateUser();
    }

    
}
