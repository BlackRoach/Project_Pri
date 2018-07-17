using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour {

    

    public void Go_To_Game_Origin_Scene()
    {
        Inventory_Controller.instance.If_Exit_Inventory_Scene();
        StartCoroutine(Load_To_Game_Origin_Scene());
    }
    IEnumerator Load_To_Game_Origin_Scene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game_Origin");
    }
    
    public void Go_To_Inventory_Scene()
    {
        SceneManager.LoadScene("Game_Inventory");
    }








} // class












