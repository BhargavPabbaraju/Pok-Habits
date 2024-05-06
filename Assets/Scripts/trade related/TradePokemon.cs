using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TradePokemon
{
    public string uniqueId;
    public int number;
    public int wantedPokemonNumber;
    public int wantedPokemonGender;
    public string wantedPokemonLevel;
    public string wantedPokemonName;
    public string userId;
    public string location;

    public List<TradePokemon> Offers = new List<TradePokemon>();

    // public TradePokemon(TradePokemon p){
    //     uniqueId = p.uniqueId;
    //     wantedPokemonGender = p.wantedPokemonGender;
    //     wantedPokemonLevel = p.wantedPokemonLevel;
    //     wantedPokemonNumber = p.wantedPokemonNumber
    //     userId = p.userId;
        
    //     if(Offers==null){
    //         Offers = new List<TradePokemon>();
    //     }
    // }

}
