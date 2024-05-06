using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignInButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject regButton;
    [SerializeField] public bool isLogin;
    [SerializeField] InputField email;
    [SerializeField] InputField password;
    [SerializeField] public Text errorMessage;
    void Awake()
    {
        
        if(GameManager.Instance.scene == GameManager.Instance.SINGUPSCENE){
            if(GameManager.Instance.login){
            this.gameObject.SetActive(true);
            regButton.SetActive(false);

            }else{
                this.gameObject.SetActive(false);
            }
            
        }
        
        
    }

    public void clearErrorMessage(){
        errorMessage.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Signpage(){
        GameManager.Instance.Signpage(isLogin);
    }

    public void Register(){
        AuthHandler.Instance.email = email;
        AuthHandler.Instance.password = password;
        AuthHandler.Instance.SignUpUser();
        
        
    }
}
