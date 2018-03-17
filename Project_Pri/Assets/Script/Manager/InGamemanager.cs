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


    [SerializeField] private PlayerDataContainer playerDataContainer;
    [SerializeField] private GameObject battleWindow;
    [SerializeField] private GameObject infoButtons;
    [SerializeField] private GameObject joyController;
    [SerializeField] private Text nameText;
    [SerializeField] private Text scoreText;

        
    public PlayerDataContainer PlayerDataContainer_readonly
    { get { return playerDataContainer; } }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void BattleButton()
    {

    }
    public void OpenBattleWindow()
    {
        battleWindow.SetActive(true);
     
    }
    public void CloseBattleWindow()
    {
        battleWindow.SetActive(false);
    }
    public void AbleInfoButton()
    {
        infoButtons.SetActive(true);
    }
    public void EnableInfoButton()
    {
        infoButtons.SetActive(false);
    }
}
