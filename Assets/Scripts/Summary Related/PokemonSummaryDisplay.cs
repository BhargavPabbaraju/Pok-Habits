using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonSummaryDisplay : MonoBehaviour
{
    [SerializeField] public PokemonSummary[] screens;
    [SerializeField] public int screen=0;
    [SerializeField] public Color[] screenColors;
    [SerializeField] public Image bottomScreen;
    //[SerializeField] public PokemonSummary summary;
    // Start is called before the first frame update

    void Awake(){
        for(int i=0;i<screens.Length;i++){
            screens[i].screen = i;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHP(){
        if(screen==2){
            screens[2].SetHp();
        }
    }

    public void changeScene(int sc){
        bottomScreen.color = screenColors[sc];
        for(int i=0;i<screens.Length;i++){
            if(i==sc)
                screens[i].gameObject.SetActive(true);
            else
                screens[i].gameObject.SetActive(false);
        }
        screen = sc;
        screens[sc].Refresh();

    }

    public void Refresh(){
        screens[screen].Refresh();
    }


}
