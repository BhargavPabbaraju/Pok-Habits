using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ResetCounterButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public ResetCounterButton[] butList;
    [SerializeField] public int butInd;
    [SerializeField] public bool active;
    //[SerializeField] public ColorList colList;
    [SerializeField] public Text text;

    ColorList colList = PlayerPokemon.Instance.colList;

    void Awake()
    {
        changeColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonClick(){
            for(int i=0;i<butList.Length;i++){
                if(i==butInd){
                    butList[i].active = true;
                    butList[i].changeColor();
                }
                else{
                    butList[i].active = false;
                    butList[i].changeColor();
                }
                    
            }
    }

    public void changeColor(){
        if(active){
            var curType = PlayerPokemon.Instance.GetCurrentTypeIndex();
            var color = colList.GetColor(curType,0);
            GetComponent<Image>().color = color;
            //color = colList.GetColor(typeIndex,2);
            text.color = new Color32(255,255,255,255);
        }
        else{
            GetComponent<Image>().color = new Color32(236,236,236,255);
            text.color = new Color32(49,49,49,255);
            
        }
    }

    
}
