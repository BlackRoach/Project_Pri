using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level_Manager : MonoBehaviour {

	
    public void Load_To_Add_Scene()
    {
        SceneManager.LoadScene("Add_Scene");
    }
    public void Load_To_Inventory_Scene()
    {
        SceneManager.LoadScene("Inventory");
    }
} // class










