using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PositiveButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public bool active;
    [SerializeField] public Text text;
    //[SerializeField] public ColorList colList;

    ColorList colList = PlayerPokemon.Instance.colList;

    void Awake()
    {
        ChangeColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor(){
        if(!active){
            
            GetComponent<Image>().color = new Color32(130,130,130,255);
            text.color = new Color32(149,149,149,255);
        }
        else{
            
            var curType = PlayerPokemon.Instance.GetCurrentTypeIndex();
            var color = colList.GetColor(curType,0);
            GetComponent<Image>().color = color;
            color = colList.GetColor(curType,2);
            text.color = color;
        }
    }

    public void changeActive(){
        if(active){
            active = false;
        }else{
            active = true;
        }

        ChangeColor();
    }

    
}
