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
    [SerializeField] private GameObject infoButtons;
    [SerializeField] private GameObject joyController;
    [SerializeField] private GameObject worldObjects;

    [SerializeField] private GameObject worldUI;

    [SerializeField] private Camera WorldCamera;

    [SerializeField] private Text nameText;
    [SerializeField] private Text scoreText;

    [SerializeField] public Transform[] trans_list;
    [SerializeField] public int[] position_money;

    public PlayerDataContainer PlayerDataContainer_readonly
    { get { return playerDataContainer; } }


    void Start()
    {

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
    public void AbleInfoButton()
    {
        infoButtons.SetActive(true);
    }
    public void EnableInfoButton()
    {
        infoButtons.SetActive(false);
    }
    

}
