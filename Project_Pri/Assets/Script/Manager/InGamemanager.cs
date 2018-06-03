using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private GameObject infoButtons;
    [SerializeField] private GameObject joyController;
    [SerializeField] private GameObject worldObjects;
    [SerializeField] private GameObject battleObjects;
    [SerializeField] private GameObject worldUI;
    [SerializeField] private GameObject battleHudUI;
    [SerializeField] private GameObject battleUI;
    [SerializeField] private Camera WorldCamera;
    [SerializeField] private Camera BattleCamera;
    [SerializeField] private Text nameText;
    [SerializeField] private Text scoreText;


    public PlayerDataContainer PlayerDataContainer_readonly
    { get { return playerDataContainer; } }


    void Start()
    {

        WorldCamera.gameObject.SetActive(true);
        worldObjects.SetActive(true);
        worldUI.SetActive(true);
        BattleCamera.gameObject.SetActive(false);
        battleObjects.SetActive(false);
        battleUI.SetActive(false);
        battleHudUI.SetActive(false);

    }


    void Update()
    {

    }


    public void BattleButton()
    {
        BattleManager.Instance.enabled = true;
        WorldCamera.gameObject.SetActive(false);
        worldObjects.SetActive(false);
        worldUI.SetActive(false);
        BattleCamera.gameObject.SetActive(true);
        battleObjects.SetActive(true);
        battleUI.SetActive(true);
        battleHudUI.SetActive(true);
    }
    public void BattleExitButton()
    {
        BattleManager.Instance.enabled = false;
        WorldCamera.gameObject.SetActive(true);
        worldObjects.SetActive(true);
        worldUI.SetActive(true);
        BattleCamera.gameObject.SetActive(false);
        battleObjects.SetActive(false);
        battleUI.SetActive(false);
        battleHudUI.SetActive(false);
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
    public void FinishBattle()
    {
        BattleManager.Instance.enabled = false;
        worldObjects.SetActive(true);
        worldUI.SetActive(true);
        battleObjects.SetActive(false);
        battleUI.SetActive(false);
        battleHudUI.SetActive(false);
    }

}
