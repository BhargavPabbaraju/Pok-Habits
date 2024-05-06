using System;
using UnityEngine;
/// <summary>
/// The user class, which gets uploaded to the Firebase Database
/// </summary>

[Serializable] // This makes the class able to be serialized into a JSON
public class AuthError
{
    public int code;
    public string message;
    public string[] errors;
    public string errormessage;


    public AuthError(int code,string message,string[] errors)
    
    {
        this.code = code;
        this.message = message;
        this.errors = errors;
        
    }

    public string getErrorMessage(){
        errormessage = "";
        var words = message.Split('_');
        for(int i=0;i<words.Length;i++){
            words[i] = words[i][0] + words[i].Substring(1).ToLower();
            //Debug.Log(words[i]);
        }
        errormessage = string.Join(" ",words);
        return errormessage;
	
    }

    
}