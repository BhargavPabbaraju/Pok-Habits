using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpritesList : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] itemIcons;
    public Sprite[] itemSprites;

    public static bool loaded = false;

    void Awake(){
        if(loaded)
            DestroyImmediate(gameObject);
        else{
            loaded = true;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    // Update is called once per frame
    public Sprite getItemIconSprite(string cat)
    {
        int index;
        index = getIndex(cat);
        
        return itemIcons[index];
    }

    public Sprite getItemSprite(string cat)
    {
        int index;
        index = getIndex(cat);
        
        
        return itemSprites[index];
    }

    int getIndex(string cat){
        switch(cat){
            case "Apricorns":{
                return 0;
                break;
            }
                
            
            case "Balls":{
                return 1;
                break;
            }
                

            case "Battle Item":{
                return 2;
                break;
            }
                
            
            case "Berry":{
                return 3;
                break;
            }
                
            

            case "Candies":{
                return 4;
                break;
            }
                
            

            case "Curry ings":{
                return 5;
                break;
            }
                
            

            case "EV Item":{
                return 6;
                break;
            }
                
            
            case "Evolution items":{
                return 7;
                break;
            }

            case "Flutes":{
                return 8;
                break;
            }

            case "Fossil":{
                return 9;
                break;
            }
            
            case "Gems":{
                return 10;
                break;
            }

            case "Held Item":{
                return 11;
                break;
            }

            case "Incense":{
                return 12;
                break;
            }

            case "Medicine":{
                return 13;
                break;
            }

            case "Mega stones":{
                return 14;
                break;
            }

            case "Memories":{
                return 15;
                break;
            }

            case "Mints":{
                return 16;
                break;
            }

            case "Mulch":{
                return 17;
                break;
            }

            case "Others":{
                return 18;
                break;
            }

            case "Petals":{
                return 19;
                break;
            }

            case "Plates":{
                return 20;
                break;
            }

            case "Rotom Powers":{
                return 21;
                break;
            }

            case "Scarves":{
                return 22;
                break;
            }

            case "Shards":{
                return 23;
                break;
            }

            case "Tms":{
                return 24;
                break;
            }

            case "Trs":{
                return 25;
                break;
            }

            case "Valuables":{
                return 26;
                break;
            }

            case "Z crystals":{
                return 27;
                break;
            }
                

            default: return 1;

        }
       
    }
}
