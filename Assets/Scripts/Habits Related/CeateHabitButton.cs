using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CeateHabitButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TMP_InputField habitTitle;
    [SerializeField] public TMP_InputField notes;
    [SerializeField] public PositiveButton posBut;
    [SerializeField] public PositiveButton negBut;
    [SerializeField] public ResetCounterButton[] resetButtons;
    [SerializeField] public InputField[] adjustCounters;
    [SerializeField] public Habit h;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake(){
        if(adjustCounters!=null && adjustCounters.Length>0){
            h=PlayerHabit.Instance.selectedHabit;
            habitTitle.text = h.title;
            notes.text = string.Join("\n",h.notes);
            posBut.active = h.positive;
            negBut.active = h.negative;
            setResetType();
            adjustCounters[0].text = h.posCount.ToString();
            adjustCounters[1].text = h.negCount.ToString();
        
        }

    }

    public void setResetType(){
        //Debug.Log(h.resetType);
        for(int i=0;i<resetButtons.Length;i++)
            if(h.resetType==i){
                resetButtons[i].active = true;
                resetButtons[i].changeColor();
            }
                
            else{
                resetButtons[i].active = false;
                resetButtons[i].changeColor();
            }
              

        
    }

    public int findResetType(){
        for(int i=0;i<resetButtons.Length;i++)
            if(resetButtons[i].active)
                return i;

        return 0;
    }

    public void updateHabit(){

        h.posCount = int.Parse(adjustCounters[0].text);
        h.negCount = int.Parse(adjustCounters[1].text);

        makeHabit();
        PlayerHabit.Instance.DeleteHabit(h,habitTitle.text);
        
    }

    public void makeHabit(){
        h.notes = notes.text.Split('\n');
        h.positive = posBut.active;
        h.negative = negBut.active;
        h.resetType = findResetType();
        

    }

    public void createHabit(){
        h = new Habit(h);
        h.title = habitTitle.text;
        makeHabit();
        var lastUpdate = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        h.lastUpdate = lastUpdate;
        PlayerHabit.Instance.AddHabit(h);

        
    }

    public void deleteHabit(){
        PlayerHabit.Instance.DeleteHabit(h,null);

    }



    public void BackButton(){
        GameManager.Instance.changeScene(GameManager.Instance.HABITSSCREEN);
    }
}
