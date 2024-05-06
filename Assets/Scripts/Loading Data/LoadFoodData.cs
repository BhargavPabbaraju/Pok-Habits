using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFoodData : MonoBehaviour
{
    // Start is called before the first frame update
    public Food food;
    public List<Food> foodDatabase = new List<Food>();
   
   
    public void LoadData(){
        foodDatabase.Clear();
        
        //Read Csv Filesystem
        List<string> files = new List<string> {"food data"};
        foreach (var file in files){
            List<Dictionary<string,object>> data = CSVReader.Read(file);
            for (var i = 0; i < data.Count;i++){

                int id = int.Parse(data[i]["ID"].ToString());
                string foodName = data[i]["Name"].ToString();

        
                string flavors = data[i]["Flavor"].ToString();
                //string desc = data[i]["Desc"].ToString();

                string cost = data[i]["Cost"].ToString();

                int row = int.Parse(data[i]["Row"].ToString());
                int col = int.Parse(data[i]["Col"].ToString());

                int exp = int.Parse(data[i]["Exp"].ToString());

                int fullness = int.Parse(data[i]["Fullness"].ToString());

                int berry = int.Parse(data[i]["Berry"].ToString());
                
                
                
                AddFood(id,foodName,flavors,cost,exp,row,col,fullness,berry);
            }
        }
        
    }

    void AddFood(int id,string foodName,string flavors,string cost,int exp,int row,int col,int fullness,int berry){
        Food temp = new Food(food);
        temp.id = id;
        temp.foodName =foodName;
        temp.flavors = flavors;
        temp.cost = cost;
        temp.exp = exp;
        temp.cords = new int [] {row,col};
        temp.fullness = fullness;
        temp.berry = berry;
        foodDatabase.Add(temp);
    }

   
    

    public Food GetFood(int id){

        int i;
        try{
            i = id-1;
            return foodDatabase[i];
        }catch{
            i = Random.Range(0, foodDatabase.Count);
            return foodDatabase[i];
        }
        
        //Debug.Log(trainerDatabase[i].trName);
        
    }
}
