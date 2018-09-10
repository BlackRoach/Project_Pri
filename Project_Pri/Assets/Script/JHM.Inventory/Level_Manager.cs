using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour {

    

    public void Go_To_Game_Origin_Scene()
    {
        // 인벤토리씬에 나갈때 이함수 반드시 이용하기 
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


    public void Load_To_Main_Scene()
    {
        SceneManager.LoadScene("Main");
    }
    public void Load_To_Ending_Scene()
    {
        SceneManager.LoadScene("Ending");
    }





} // class












