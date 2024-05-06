using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonSpritesList : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] pokemonOverworlds;
    public Sprite[] pokemonSprites;
    public Sprite[] pokemonIcons;
    public Sprite[] TypeSprites;

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
    public Sprite getPokemonOverworldSprite(string type,int big,bool shiny)
    {
        int index;
        if(big==0)
            index = getTypeIndex(type);
        else
             index = big+18;
        
        if(shiny)
            index += 23;
        
        return pokemonOverworlds[index];
    }

    public Sprite getPokemonSprite(string type,bool shiny){
        int index;
        index = getTypeIndex(type);
        if(shiny)
            index += 19;
        return pokemonSprites[index];
    }

    public Sprite getPokemonIcon(string type,bool shiny){
        int index;
        index = getTypeIndex(type);
        //Debug.Log($"{type} {index}");
        if(shiny)
            index += 19;
        return pokemonIcons[index];
    }

    public Sprite getTypeSprite(string type){
        int index;
        index = getTypeIndex(type);
        return TypeSprites[index];
    }

    public int getTypeIndex(string type){
        switch(type){
            case "Bug":
                return 0;
            
            case "Dark":
                return 1;
            
            case "Dragon":
                return 2;
            
            case "Electric":
                return 3;
            
            case "Fairy":
                return 4;
            
            case "Fighting":
                return 5;

            case "Fire":
                return 6;
            
            case "Flying":
                return 7;
            
            case "Ghost":
                return 8;
            
            case "Grass":
                return 9;
            
            case "Ground":
                return 10;
            
            case "Ice":
                return 11;
            
            case "Normal":
                return 12;
            
            case "Poison":
                return 13;
            
            case "Psychic":
                return 14;
            
            case "Rock":
                return 15;
            
            case "Steel":
                return 16;
            
            case "Water":
                return 17;
            
            case "Alcremie":
                return 18;
            
            default:
                return 18;

        }
    }
}
