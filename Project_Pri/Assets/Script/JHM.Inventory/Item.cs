using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {


    public bool is_Dragging;


    private void Start()
    {
        is_Dragging = false;
    }

    
    public void Is_Clicked()
    {
        if (!is_Dragging)
            is_Dragging = true;

        if (is_Dragging)
        {
            this.transform.parent = this.transform.parent.parent.parent;
        }

    }


    public void Click_Released()
    {
        if (is_Dragging)
        {
            is_Dragging = false;
        }
    }
    
} // class










