using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class AddButton : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    public int scene ;
    void Start()
    {
        
    }


    // Update is called once per frame
    public void OnPointerUp(PointerEventData eventData){
        scene = GameManager.Instance.scene;
        //Debug.Log(scene);
            if(scene == GameManager.Instance.HABITSSCREEN){
                GameManager.Instance.scene = GameManager.Instance.CREATEHABITSCREEN;
                GameManager.Instance.makeHabit();
            }

        
    }

    public void OnPointerDown(PointerEventData eventData){
       
        
    }

    public void changeScene(int scene){
        
        GameManager.Instance.changeScene(scene);
    }

}
