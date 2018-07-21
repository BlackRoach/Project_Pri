using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Care_Manager : MonoBehaviour {


    public static Item_Care_Manager instance = null;


    public int[] previous_Slot_Index;


    
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        previous_Slot_Index = new int[2];
        for(int i = 0; i< previous_Slot_Index.Length; i++)
        {
            previous_Slot_Index[i] = -1;
        }
    }







} // class












