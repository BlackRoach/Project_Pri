using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour {

    public GameObject schedulePanel;

    private bool IsOpenSchedule = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public  void OpenSchedule() // 토글 버튼
    {
        if (!IsOpenSchedule)
        {
            schedulePanel.SetActive(true);
        }
        else
        {
            schedulePanel.SetActive(false);
        }

        IsOpenSchedule = !IsOpenSchedule;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }


}
