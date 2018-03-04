using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGamemanager : MonoBehaviour {
    private static InGamemanager instance = null;
    public static InGamemanager Instance
    {
        get
        {
            if (instance)
                return instance;
            else
                return instance = new GameObject("*Manager").AddComponent<InGamemanager >();
        }
    }
    private void Awake()
    {
        instance = this;
    }


    [SerializeField] private GameObject BattleWindow;
    [SerializeField] private GameObject InfoButtons;
    [SerializeField] private GameObject JoyController;
    [SerializeField] private Text Name;
    [SerializeField] private Text Score;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenBattleWindow()
    {
        BattleWindow.SetActive(true);
    }
    public void CloseBattleWindow()
    {
        BattleWindow.SetActive(false);
    }
    public void AbleInfoButton()
    {
        InfoButtons.SetActive(true);
    }
    public void EnableInfoButton()
    {
        InfoButtons.SetActive(false);
    }
}
