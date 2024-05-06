using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BagCard : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TMP_Text qtyText;
    [SerializeField] public TMP_Text itemName;
    [SerializeField] public ItemLoader itemLoader;
    [SerializeField] public TMP_Text itemDesc;
    [SerializeField] public ItemLoader mainItem;
    [SerializeField] public int qty;
    [SerializeField] public GameObject optionsPane;


    void Awake(){
        
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        mainItem.GetComponent<Image>().color = new Color(255,255,255,255);
        PlayerItem.Instance.selectedItem =  itemLoader.item;
        mainItem.doNotAwake = true;
        mainItem.id = itemLoader.item.id;
        mainItem.RefreshSprite();
        itemDesc.text = itemLoader.item.desc;
        optionsPane.SetActive(true);

    }
}
