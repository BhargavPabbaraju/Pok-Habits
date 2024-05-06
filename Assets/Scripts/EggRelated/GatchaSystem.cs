using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatchaSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string method1(){
        int r = Random.Range(1,101);
        string rarity = "";
        if(r>99){
            rarity = "UX";
        }
        else if(r>80){
            rarity = "EX";
        }
        else if(r>60){
            rarity = "R";
        }
        else if(r>20){
            rarity = "UC";
        }else{
            rarity="C";
        }

        return rarity;


    }

    public static string method2(){
        int r = Random.Range(1,101);
        string rarity = "";
        if(r>97){
            rarity = "EX";
        }
        else if(r>80){
            rarity = "R";
        }
        else if(r>30){
            rarity = "UC";
        }
        else{
            rarity="C";
        }
        return rarity;
    }

    public static string method3(){
        int r = Random.Range(1,101);
        string rarity = "";
        if(r>95){
            rarity = "R";
        }
        else if(r>35){
            rarity = "C";
        }
        else{
            rarity="UC";
        }
        return rarity;

        

    }

    public static Egg getHabitEgg(int method){
        int r = Random.Range(0,100);
        string rarity = "";
        if(r<1){
            rarity = method1();
        }
        else if(r<5){
            rarity = method2();
        }else{
            rarity = method3();
        }

        if(method==1){
            rarity = method1();
        }

        
        var x = GameObject.Find("Database").GetComponent<LoadEggDatabase>().GetEggByRarity(rarity);
        x.method = 0;
        return x;
    }
    
}
