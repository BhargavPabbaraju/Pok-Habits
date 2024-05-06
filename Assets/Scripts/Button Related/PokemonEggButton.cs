using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PokemonEggButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool isEgg;
    [SerializeField] Animator animator;
    [SerializeField] TMP_Text text;
    [SerializeField] Image image;
    [SerializeField] int type;
    [SerializeField] Color inactiveColor;
    [SerializeField] PokemonEggButton other;

    void Start()
    {
        
    }
    

    void SetActiveColor(){
        var curType = PlayerPokemon.Instance.GetCurrentTypeIndex();
        var color = PlayerPokemon.Instance.colList.GetColor(curType,type);
        text.color = color;
        image.color = color;

    }

    void SetInactiveColor(){
        text.color = inactiveColor;
        image.color = inactiveColor;

    }

    void Awake(){
        if(isEgg){
            SetInactiveColor();
        }
        else{
            SetActiveColor();
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        
        if(isEgg){
            animator.SetTrigger("show");
            
        }
        else{
            animator.SetTrigger("hide");
        }

        other.SetInactiveColor();
        SetActiveColor();
    }
}
