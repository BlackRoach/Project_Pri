using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InGamemanager : MonoBehaviour
{
    private static InGamemanager instance = null;
    public static InGamemanager Instance
    {
        get
        {
            return instance;

        }
    }
    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }


    [SerializeField] private PlayerDataContainer playerDataContainer;
    [SerializeField] private GameObject battleWindow;
    [SerializeField] private GameObject teleportWindow;
    [SerializeField] private GameObject questWindow;
  
    [SerializeField] private GameObject infoButtons;
    [SerializeField] private GameObject questButton;
    [SerializeField] private GameObject joyController;
    [SerializeField] private GameObject worldObjects;

    [SerializeField] private GameObject worldUI;

    [SerializeField] private Camera WorldCamera;

    private QuestManager questManager;

    public Text nameText;
    public Text scoreText;

 

    public Transform[] trans_list;
    public int[] position_money;

    public PlayerDataContainer PlayerDataContainer_readonly
    { get { return playerDataContainer; } }


    void Start()
    {
        questManager = QuestManager.Instance;
        WorldCamera.gameObject.SetActive(true);
        worldObjects.SetActive(true);
        worldUI.SetActive(true);


    }


    void Update()
    {

    }


    public void BattleButton()
    {
        SceneManager.LoadScene("BattleScene");
   
    }



   
    public void OpenBattleWindow()
    {
        battleWindow.SetActive(true);

    }
    public void CloseBattleWindow()
    {
        battleWindow.SetActive(false);
    }

    




    public void OpenTeleportWindow()
    {
        teleportWindow.SetActive(true);
    }
    public void CloseTeleportWindow()
    {
        teleportWindow.SetActive(false);
    }






    public void AbleQuestButton()
    {
        questButton.SetActive(true);
    }
    public void EnableQuestButton()
    {
        questButton.SetActive(false);
    }
    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
    }
    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
        questManager.InitText();
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
