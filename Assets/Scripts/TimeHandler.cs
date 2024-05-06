using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool IncrementLoginCount(User user){
        var date = System.DateTime.ParseExact(user.lastLogin,"dd-MM-yyyy HH:mm:ss",null);
        var today = System.DateTime.Now;

        if(today.Date!=date.Date){
            user.loginCount+=1;
            user.lastLogin = today.ToString("dd-MM-yyyy HH:mm:ss");
            return true;
        }
        
        return false;
    }

    public static string getTimeRemaining(string dateString,int minutes,bool utc){
        try{
            var date = System.DateTime.ParseExact(dateString,"dd-MM-yyyy HH:mm:ss",null);
            //Debug.Log(date);
            
            
            System.DateTime today;
            today = System.DateTime.Now;
            if(utc)
                today = System.DateTime.UtcNow;
            
            var end = date.Add(System.TimeSpan.FromMinutes(minutes));
            
            var dif = end-today;
            var h = dif.Hours;
            var m = dif.Minutes;
            var s = dif.Seconds;
            //Debug.Log(date.ToString()+" "+dif.Minutes);
            if(h<0||m<0||s<0){
                h=0;m=0;s=0;
            }
        
            return $"{h.ToString("00")}:{m.ToString("00")}:{s.ToString("00")}";
        }

       catch{
            //Debug.Log(dateString);
            return "0";
        }
        
        
    }

    public static int getTimeRemainingInSeconds(string dateString){
        try{
            var date = System.DateTime.ParseExact(dateString,"dd-MM-yyyy HH:mm:ss",null);
            var today = System.DateTime.Now;
            var dif = today-date;
            return (int) dif.TotalMinutes;
        }catch{
            //Debug.Log(dateString);
            return 0;
        }
        
        
    }

    public static bool checkHabitUpdate(string dateString,int resetType){
        var date = System.DateTime.ParseExact(dateString,"dd-MM-yyyy HH:mm:ss",null);
        var today = System.DateTime.Now;
        var dif = today-date;
        //Debug.Log(dif.Days);
        switch(resetType){
            case 0:{
                if(dif.Days>0)
                    return true;
                break;
            }
            case 1:{
                if(dif.Days>=7)
                    return true;
                break;
            }
            case 2:{
                if(dif.Days>=30)
                    return true;
                
                break;
            }
        }
        return false;
        
    }

}
