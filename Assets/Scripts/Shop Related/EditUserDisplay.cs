using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditUserDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public bool isBackdrop;
    [SerializeField] public GameObject child;
    [SerializeField] public Transform parent;
    
    
    void Awake(){
        Refresh();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Refresh(){

        while(parent.childCount != 1){
                DestroyImmediate(parent.GetChild(0).gameObject);
            }

        var user = AuthHandler.Instance.currentUser;
        EditUserButton editButton;
        if(isBackdrop){
            foreach(var id in user.backdrops){
                var clone = Instantiate(child);
                clone.transform.SetParent(parent);

                editButton = clone.GetComponent<EditUserButton>();
                editButton.backdropId = id;
                editButton.backdropSprite.overrideSprite = ShopHandler.Instance.backdropSprites[id];

            }
        }
        else{

            foreach(var id in user.trainers){
                var clone = Instantiate(child);
                clone.transform.SetParent(parent);

                editButton = clone.GetComponent<EditUserButton>();
                editButton.tr.doNotAwake = true;
                editButton.tr.id = id;
                editButton.tr.RefreshSprite();

                

                

            }
        }


        DestroyImmediate(parent.GetChild(0).gameObject);
    }
}
