using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GtsDisplay : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Animator animator;
    [SerializeField] GameObject[] scenes;
    int scene=0;

    [SerializeField] PokemonLoader pokeLoader;

    [SerializeField] TMP_Text[] offeredPokeName;
    [SerializeField] TMP_Text[] offeredPokeLevel;
    [SerializeField] GameObject[] offeredPokeGender;

    //[SerializeField] PokemonLoader wantedpoke;
    [SerializeField] TMP_Text[] wantedPokeName;
    [SerializeField] TMP_Text[] wantedPokeLevel;
    [SerializeField] GameObject[] wantedPokeGender;
    [SerializeField] string wantedPokeNumber;
    [SerializeField] int wantedGender;
    [SerializeField] string wantedLevel;
    [SerializeField] bool noGenderScene = false;


    [SerializeField] TMP_Text[] trNames;
    [SerializeField] TMP_Text itemName;
    [SerializeField] ItemLoader heldItem;


    [SerializeField] GameObject BackButton;


    //SingleAlphabetButtons
    [SerializeField] TMP_Text[] singleLetters;
    [SerializeField] TMP_Text lettersText;
    [SerializeField] GameObject lastLetter;

    //PokemonNames
    [SerializeField] Transform[] pokemonNameButtons;
    [SerializeField] TMP_Text[] pageTexts;

    //LevelSelection
    [SerializeField] TMP_Text[] levelTexts;
    //[SerializeField] TMP_Text levelcurPage;

    List<Dictionary<string,int>> pokemonList = new List<Dictionary<string,int>>();
    TradePokemon tp = new TradePokemon();
    string selectedLetter="A";
    int curPage = 1;
    int totalPages = 1;

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

    void SetState(int s){
        scene = s;
        for(int i=0;i<scenes.Length;i++){
            scenes[i].SetActive(false);
        }
        scenes[scene].SetActive(true);
        if(scene>0&&scene<5)
            BackButton.SetActive(true);
        else
            BackButton.SetActive(false);
    }   

    public void Refresh(){
        pokeLoader.poke = PlayerPokemon.Instance.selectedPoke;
        //Debug.Log(pokeLoader.poke.id+" "+PlayerPokemon.Instance.selectedPoke.id);
        pokeLoader.getSprite();
    
        foreach(var txt in offeredPokeName){
            txt.text = pokeLoader.poke.pokeName;
        }
        foreach(var txt in offeredPokeLevel){
            txt.text = pokeLoader.poke.level.ToString();
        }
        foreach(var obj in offeredPokeGender){
            obj.SetActive(false);
        }
        offeredPokeGender[pokeLoader.poke.gender].SetActive(true);
        offeredPokeGender[pokeLoader.poke.gender+2].SetActive(true);

        trNames[0].text = pokeLoader.poke.ot[0];
        trNames[1].text = AuthHandler.Instance.currentUser.name;

        var hi = pokeLoader.poke.heldItem;
        if(hi==-1){
            itemName.text = "None";
            
        }else{
            heldItem.id = hi;
            heldItem.RefreshSprite();
            itemName.text = heldItem.item.itemName;
        }

    }

    public void SetPokemonName(int i){
        if(!pokemonNameButtons[i%4].gameObject.activeSelf){
            pokemonNameButtons[i%4].gameObject.SetActive(true);
        }
        var nameText = pokemonNameButtons[i%4].GetChild(0).GetComponent<TMP_Text>();
        var idText = pokemonNameButtons[i%4].GetChild(1).GetComponent<TMP_Text>();
        foreach(var name in pokemonList[i]){
            nameText.text = name.Key;
            idText.text = name.Value.ToString();
        }
    }

    public void PokemonNamesScene(){
        
        pokemonList = PokemonNamesList.GetPokemonByLetter(selectedLetter);
        totalPages = Mathf.CeilToInt(pokemonList.Count/4f);
        
        for(int i=(curPage-1)*4;i<curPage*4;i++){
            if(i>=pokemonList.Count){
                pokemonNameButtons[i%4].GetChild(0).GetComponent<TMP_Text>().text="";
                pokemonNameButtons[i%4].gameObject.SetActive(false);
            }else{
                SetPokemonName(i);
            }
        }
        
        pageTexts[0].text = curPage.ToString();
        pageTexts[1].text = totalPages.ToString();
        
    }

    public void PreviousPage(){
        if(curPage>1){
            curPage-=1;
        }
        if(scene==2)
            PokemonNamesScene();
        else
            LevelSelectScene();
    }

    public void NextPage(){
        if(curPage<totalPages){
            curPage+=1;
        }
        if(scene==2)
            PokemonNamesScene();
        else
            LevelSelectScene();

    }

    public void PokemonNameClick(Transform button){
        
        
        var nameText = button.GetChild(0).GetComponent<TMP_Text>();
        var idText = button.GetChild(1).GetComponent<TMP_Text>();

        foreach(var txt in wantedPokeName)
            txt.text = nameText.text;
        
        wantedPokeNumber = idText.text;
        

        foreach(var obj in wantedPokeGender){
                obj.SetActive(false);
        }
        noGenderScene = true;
        if (PokemonNamesList.genderlessPokemon.Contains(nameText.text)){
            wantedGender =  2;
            StartCoroutine(ToggleScene(4));
        }else if(PokemonNamesList.maleOnlyPokemon.Contains(nameText.text)){
            wantedGender =  0;
            wantedPokeGender[wantedGender].SetActive(true);
            wantedPokeGender[wantedGender+2].SetActive(true); 
            StartCoroutine(ToggleScene(4));
        }else if(PokemonNamesList.femaleOnlyPokemon.Contains(nameText.text)){
            wantedGender =  1;
            wantedPokeGender[wantedGender].SetActive(true);
            wantedPokeGender[wantedGender+2].SetActive(true); 
            StartCoroutine(ToggleScene(4));
        }else{
            noGenderScene = false;
            StartCoroutine(ToggleScene(3));
        }
        

    }

    public void GenderClick(int gender){

        foreach(var obj in wantedPokeGender){
            obj.SetActive(false);
        }
        
        if(gender<2){
            wantedPokeGender[gender].SetActive(true);
            wantedPokeGender[gender+2].SetActive(true);
        }
        wantedGender = gender;
        StartCoroutine(ToggleScene(4));
        LevelSelectScene();
    }

    public void LevelClick(TMP_Text lvlButton){
        wantedLevel = lvlButton.text.Replace("\tLv.", "");
        foreach(var txt in wantedPokeLevel){
            txt.text = wantedLevel;
        }
        StartCoroutine(ToggleScene(5));

    }


    public void TripleAlphabets(string letters){
        lastLetter.SetActive(false);
        lettersText.text = letters;
        for(int i=0;i<3;i++){
            singleLetters[i].text=$"{letters[i]}";
        }
        if(singleLetters[2].text==" "){
            lastLetter.SetActive(true);
        }
        
        
        StartCoroutine(ToggleScene(1));
        
    }

    public void SingleAlphbets(TMP_Text txt){
        selectedLetter = txt.text;
        StartCoroutine(ToggleScene(2));
        PokemonNamesScene();
        


    }

    public void LevelSelectScene(){
        //curPage = 1;
        totalPages = 3;
        string lvl= "";
        for(int i=(curPage-1)*4;i<curPage*4;i++){
            //var levelText = pokemonNameButtons[i%4].GetChild(0).GetComponent<TMP_Text>();
            if(i==0)
                lvl="\tANY";
            else if(i==1)
                lvl="\t Lv.9 and under";
            else if(i==totalPages*4-1)
                lvl="\tLv.100";
            else
                lvl=$"\tLv.{10*(i-1)} and up";
            
            levelTexts[i%4].text = lvl;
            
        }
        
        pageTexts[2].text = curPage.ToString();
        pageTexts[3].text = totalPages.ToString();

    }

    


    IEnumerator ToggleScene(int s){
        curPage = 1;
        animator.SetTrigger("close");
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSecondsRealtime(animationLength);
        SetState(s);
        animator.SetTrigger("pop");
        
    }

    public void BackClick(){
        if(noGenderScene && scene==4){
            StartCoroutine(ToggleScene(scene-2));
        }else{
            StartCoroutine(ToggleScene(scene-1));
        }
        
        
    }

    public void Confirm(){
        tp.location = ShopHandler.Instance.GetLocation();
        tp.wantedPokemonGender = wantedGender;
        tp.wantedPokemonName = wantedPokeName[0].text;
        tp.wantedPokemonNumber = int.Parse(wantedPokeNumber);
        tp.wantedPokemonLevel = wantedLevel;
        tp.userId = AuthHandler.Instance.currentUser.userId;
        tp.uniqueId = pokeLoader.poke.uniqueId;
        tp.number = pokeLoader.poke.number;
        DatabaseHandler.PostTradePokemon(tp);
        GameManager.Instance.changeScene(GameManager.Instance.POKEMONINVSCENE);
        
    }
}
