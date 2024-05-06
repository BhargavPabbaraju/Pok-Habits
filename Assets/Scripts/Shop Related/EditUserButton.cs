using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EditUserButton : MonoBehaviour
{

    [SerializeField] public bool isBackdrop;

    [SerializeField] public TrainerLoader tr;
    [SerializeField] public int backdropId;
    [SerializeField] public Image backdropSprite;
    [SerializeField] public PokemonLoader poke;
    [SerializeField] public TrainerLoader trainer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        if(isBackdrop){
            PlayerTrainer.Instance.SetBackdrop(backdropId);
        }else{
            PlayerTrainer.Instance.SetTrainer(tr.tr);
        }
        poke.SetCurrency();
        trainer.id = 0;
        trainer.RefreshSprite();
    }
}
