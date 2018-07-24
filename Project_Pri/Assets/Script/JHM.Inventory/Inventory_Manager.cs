using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Manager : MonoBehaviour {

    public static Inventory_Manager instance;

    public bool is_Defualt;
    public bool is_Restart;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        is_Defualt = false;
        is_Restart = false;
    }






} // class








