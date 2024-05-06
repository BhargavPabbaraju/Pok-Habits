using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BagDisplay : MonoBehaviour
{

    
    Dictionary<string,List<int>> itemDic;
    Dictionary<int,int> qts;

    [SerializeField] public GameObject child;
    [SerializeField] public Transform parent;
    [SerializeField] public TMP_Text category;
    [SerializeField] public int screen = 0;
    
    [SerializeField] public Sprite[] backgroundSprites;
    [SerializeField] public Image background;

    string[] cats = new string[] {"ITEMS","MEDICINE","POKéBALLS","TM’s & TR’s","BERRIES","MAIL","BATTLE ITEMS","EVOLUTION ITEMS"};

    void Awake(){
        InitializeLists();
        if(PlayerItem.Instance.itemDic!=null)
            foreach(var id in PlayerItem.Instance.itemDic){
                AddToDic(id.Key,id.Value);
        }
       Refresh();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void InitializeLists(){
        itemDic = new Dictionary<string,List<int>>();
        
        foreach(var cat in cats){
            itemDic[cat] = new List<int>();
        }
        qts = new Dictionary<int,int>();
    }

    void AddToDic(int id,int qty){
        qts.Add(id,qty);
        if(id>0 && id<49){
            itemDic[cats[2]].Add(id);
            return;
        }else if(id>48 && id<100){
            itemDic[cats[1]].Add(id);
            return;
        }else if(id>99 && id<108){
            itemDic[cats[6]].Add(id);
            return;
        }else if((id>156 && id<197) || (id>371 && id<419) || id==543 || id==544){
            itemDic[cats[7]].Add(id);
            return;
        }else if((id>481 && id<519)){
            itemDic[cats[3]].Add(id);
            return;
        }else{
            itemDic[cats[0]].Add(id);
        }

    }

    // void SetStates(int screen){
    //     for(int i=0;i<screens.Length;i++){
    //         if(i==screen){
    //             screens[i].SetActive(true);
    //         }else{
    //             screens[i].SetActive(false);
    //         }
            
    //     }
    // }


    public void Refresh(){
        //SetStates(screen);
        background.overrideSprite = backgroundSprites[screen];
        category.text = cats[screen];
        DisplayLists(cats[screen]);
        
    }


    public void DisplayLists(string cat){

        while(parent.childCount != 1){
            DestroyImmediate(parent.GetChild(0).gameObject);
        }

        child = parent.GetChild(0).gameObject;
        parent.GetChild(0).gameObject.SetActive(true);

        foreach(int id in itemDic[cat]){
            var clone = Instantiate(child,parent);
            clone.transform.localScale = new Vector3(1f,1f,1f);
            var bagCard = clone.GetComponent<BagCard>();
            bagCard.itemLoader.doNotAwake = true;
            bagCard.itemLoader.id = id;
            bagCard.itemLoader.RefreshSprite();
            //Debug.Log(bagCard.itemLoader.id+" "+bagCard.itemLoader.item.id);
            bagCard.itemName.text = bagCard.itemLoader.item.itemName;
            bagCard.qty = qts[id];
            bagCard.qtyText.text = $"x{bagCard.qty}";
        }

       
       
       parent.GetChild(0).gameObject.SetActive(false);


    }

    public void changeScene(int scene){
        if(screen==scene)
            return;
        screen = scene;
        Refresh();
    }

    // Update is called once per frame
    
}
