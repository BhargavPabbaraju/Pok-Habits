using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Habit 
{
    // Start is called before the first frame update
    public string title;
    public string[] notes;
    public bool positive;
    public bool negative;
    public int posCount=0;
    public int negCount=0;
    public int resetType =0;
    public string lastUpdate;
    //tags

    public Habit(Habit h){
        title = h.title;
        notes = h.notes;
        positive = h.positive;
        negative = h.negative;
        resetType = h.resetType;
        lastUpdate = h.lastUpdate;


    }
    


}
