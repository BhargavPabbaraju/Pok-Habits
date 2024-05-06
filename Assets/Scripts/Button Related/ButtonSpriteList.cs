using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpriteList : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public  Sprite[] pressedPlusButtons;
    [SerializeField] public  Sprite[] pressedMinusButtons;
    [SerializeField] public  Sprite[] normalPlusButtons;
    [SerializeField] public  Sprite[] normalMinusButtons;
    [SerializeField] public  Sprite[] normalMidButtons;
    [SerializeField] public Sprite[] pressedMidButtons;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
