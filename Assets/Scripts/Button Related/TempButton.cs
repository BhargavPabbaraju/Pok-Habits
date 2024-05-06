using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class TempButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int noOfHabits;
    [SerializeField] public GameObject[] objectList;
    [SerializeField] GameObject trainer;
    [SerializeField] GameObject pokemon;
    [SerializeField] GameObject plusButton;
    [SerializeField] GameObject minusButton;
    [SerializeField] GameObject midButton;
    [SerializeField] public GameObject[] cloneList;
    [SerializeField] public TMP_FontAsset fontAsset;
    [SerializeField] public GameObject content;
    [SerializeField] public GameObject horizontalComponent;
    [SerializeField] public Habit habit;



    
    void Start(){
        //RefreshSprite();
    }

    void Awake(){
        createHabitButtons();
    }

    void setType2(GameObject comp){
        foreach(Transform child in comp.transform){
            child.GetComponent<ButtonSpriteGetter>().isType2 = 1;
        }
        
    }
    void setTitle(GameObject comp){
            //Getting Middle button and setting its title
            var obj = comp.transform.GetChild(1);
            obj.transform.GetComponent<ButtonSpriteGetter>().title.text = habit.title;
            obj.transform.GetComponent<ButtonSpriteGetter>().posText.text = "+ " + habit.posCount.ToString();
            obj.transform.GetComponent<ButtonSpriteGetter>().negText.text = "− " + habit.negCount.ToString();

    
    }

    

    void setInactive(GameObject comp,int child){
        var obj = comp.transform.GetChild(child);
        obj.GetComponent<ButtonSpriteGetter>().isType2 = 2;
        obj.GetComponent<Button>().interactable = false;
    }

    void createHabitButtons(){

        var dic = PlayerHabit.Instance.habitDic;
        
        
        //noOfHabits = AuthHandler.Instance.currentUser.numberOfHabits;
        //Debug.Log(noOfHabits+" Habits ");

        //Destroy all current children
        while(content.transform.childCount != 1){
                DestroyImmediate(content.transform.GetChild(0).gameObject);
            }

        if(dic != null && dic.Count > 0){
            noOfHabits = dic.Count;
            //Create new habits
            int i=0;
            foreach(var h in dic){
                habit = h.Value;
                var comp = Instantiate(horizontalComponent);
                comp.transform.SetParent(content.transform,false);
                if( (i%2 == 1) ) {
                    setType2(comp);
                }

                setTitle(comp);
                if(habit.negative==false){setInactive(comp,2);}
                if(habit.positive==false){setInactive(comp,0);}

                comp.transform.localPosition = new Vector3(comp.transform.localPosition.x,comp.transform.localPosition.y-(212*i),0);
                i++;
            }
        }

        DestroyImmediate(content.transform.GetChild(0).gameObject);

    }
    
    
    void RefreshSprite()
    {
        noOfHabits = Random.Range(100,200);
        objectList = new GameObject[noOfHabits*3+2];
        objectList[0] = pokemon;
        objectList[1] = trainer;
        int i = 2;
        
        GameObject[] ButtonList = new GameObject[] {plusButton,midButton,minusButton};
        foreach(GameObject obj in ButtonList ){
             
             while(obj.transform.childCount != 0){
                DestroyImmediate(obj.transform.GetChild(0).gameObject);
            }
            // if(obj.name=="MidButton"){
            //     GameObject ngo = new GameObject("MidButtonText");
            //     ngo.transform.SetParent(obj.transform);
            //     ngo.transform.localPosition = new Vector3(0,0,0);
            //     TMP_Text tmpro = ngo.AddComponent<TMPro.TextMeshProUGUI>() as TMP_Text;
            //     tmpro.color = new Color(50,50,50);
            //     tmpro.fontSize = 36;
            //     tmpro.text = "Habit";
            //     tmpro.alignment = TextAlignmentOptions.Midline;
            //     //tmpro.font = fontAsset;
            // }
            
            //     makeClone(obj);
                
            //     for(int j=0;j<noOfHabits;j++){
            //         objectList[i]=cloneList[j];
            //         i++;
            //     }
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void makeClone(GameObject obj){
        obj.GetComponent<Button>().interactable = true;
        cloneList = new GameObject[noOfHabits];
        for(int j=0;j<noOfHabits;j++){
            cloneList[j]= Instantiate(obj);
        }
        for(int j=0;j<noOfHabits;j++){
            cloneList[j].name = obj.name+" "+(j+1).ToString();
            cloneList[j].GetComponent<ButtonSpriteGetter>().isType2 = j%2;
            Image image = cloneList[j].GetComponent<Image>();
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            cloneList[j].transform.SetParent(obj.transform);
            cloneList[j].transform.localPosition = new Vector3(0,(-180-32)*j,0);
            if(obj.name=="MidButton"){
                cloneList[j].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "<color=#323232>Habit "+(j+1).ToString();
               
            }
        }
        if(obj.name=="MidButton"){
            try{
                //Debug.Log(obj.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text);
                obj.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "";
            }
            catch{
                Debug.Log(obj.GetComponent<TMPro.TextMeshProUGUI>().text);
            }
           
           
        }
        
        obj.GetComponent<Button>().interactable = false;

        
    }

    public void OnClick(){
        // RefreshSprite();
        // for (int i=0;i<objectList.Length;i++){
        //     if(i==0)
        //     objectList[i].GetComponent<PokemonLoader>().RefreshSprite();

        //     else if(i==1)
        //     objectList[i].GetComponent<TrainerLoader>().RefreshSprite();

        //     // else
        //     // objectList[i].GetComponent<ButtonSpriteGetter>().RefreshSprite();
            
        // }

    }

    public void PokemonPage(){
        GameManager.Instance.changeScene(GameManager.Instance.POKEMONINVSCENE);
    }

    

    
}
