using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorGetter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int type;
    //[SerializeField] public ColorList colList;
    [SerializeField] public bool isTextEntry;
    
    ColorList colList = PlayerPokemon.Instance.colList;

    void Awake(){
        var curType = PlayerPokemon.Instance.GetCurrentTypeIndex();
        var color = colList.GetColor(curType,type);
        if(isTextEntry){
            GetComponent<TextMeshProUGUI>().color = color;
        }
        else{
            GetComponent<Image>().color = color;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
