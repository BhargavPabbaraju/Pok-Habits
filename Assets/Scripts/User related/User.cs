using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The user class, which gets uploaded to the Firebase Database
/// </summary>

[System.Serializable]// This makes the class able to be serialized into a JSON
public class User
{
    public string name;
    public int level;
    public int[] hp;
    public int[] exp;
    public int[] currency;
    public string userId;
    public int trId;
    public string number="";
    public List<int> backdrops= new List<int>{1};
    public string lastLogin="";
    public int loginCount = 1;
    public int loginGifts = 0;
    public List<int> trainers = new List<int>();
    public int backdropId=1;

    //public int numberOfHabits = 0;

    public User(string name, int level,int trId,string userId)
    {
        this.name = name;
        this.level = level;
        this.userId = userId;
        this.trId = trId;
        this.hp = new int[] {0,0,0,0};
        this.exp = new int[] {0,0};
        this.currency = new int[] {0,0,0,0};
        for(int i=0;i<6;i++){
            this.number+=UnityEngine.Random.Range(0,10).ToString();
        }
        calculateExp();

        backdropId=1;
        
    }


    public void calculateExp(){
        int n = level+1;
        exp[1] = (4*n*n*n)/5;

    }

    public void incrementLevel(){
        level+=1;
        calculateExp();
        calculateHP();

    }
    public void decrementLevel(){
        level -= 2;

        if(level<1){
            exp[0]=0;
            level=0;
        }

        else{
            calculateExp();
            exp[0] = exp[1];
        }
        incrementLevel();
        currency[0]=0;
        currency[1]=0;  
    }

    public void SetExp(int points){
        exp[0]+=points;
        while(exp[0]>exp[1]){
            incrementLevel();
        }
    }

    public void calculateHP(){
        int s=Mathf.FloorToInt(((2*hp[2]+hp[3])*level)/100f);
        hp[1] = s+level+10;

        hp[0] = hp[1];

    }

}