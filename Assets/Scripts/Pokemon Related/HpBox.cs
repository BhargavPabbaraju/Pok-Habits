using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpBox : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Image[] bars;
    [SerializeField] public TMP_Text[] hpTexts;
    [SerializeField] public Sprite[] hpColors;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void changeHpColor(){
        if(bars[1].fillAmount<0.2){
            bars[1].overrideSprite = hpColors[2];
        }
        else if(bars[1].fillAmount<0.5){
            bars[1].overrideSprite = hpColors[1];
        }
        else{
            bars[1].overrideSprite = hpColors[0];
        }
    }
    public void Refresh(){
        var user = AuthHandler.Instance.currentUser;
        hpTexts[0].text = user.level.ToString();
        hpTexts[1].text = user.hp[0].ToString();
        hpTexts[2].text = user.hp[1].ToString();

        bars[0].fillAmount = Mathf.Clamp(user.exp[0] / (float)user.exp[1],0,1f);
        bars[1].fillAmount = Mathf.Clamp(user.hp[0] / (float)user.hp[1],0,1f);
        changeHpColor();
    }
}
