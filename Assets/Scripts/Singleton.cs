using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Component
{
    private static T _instance;
    public static T Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<T>();
                if(_instance == null){
                    GameObject newGo = new GameObject();
                    _instance = newGo.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake(){
        _instance = this as T;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
