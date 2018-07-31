using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;
public class QuestManager : MonoBehaviour {
    enum Questprogress { ACCECPTED, AVAILABLE,COMPLETE,DONE}
    private static QuestManager instance = null;
    public static QuestManager Instance
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
            DestroyImmediate(gameObject.GetComponent<QuestManager>());
            return;
        }
        instance = this;
    }

    private TextAsset jsonString;
   
    private JsonData mudoMember;
    private JsonData presentData;
    private InGamemanager inGameManager;
    private Questprogress questProgress;

    [SerializeField] private GameObject myquestWindow;
    [SerializeField] private GameObject acceptbutton;
    [SerializeField] private GameObject giveupbutton;
    [SerializeField] private GameObject successbutton;
    [SerializeField] private Text questName;
    [SerializeField] private Text questSummary;
    [SerializeField] private Text questInfo;
    [SerializeField] private Text questReward;

    [SerializeField] private Text myquestmission;
    [SerializeField] private Text myquestSummary;
    [SerializeField] private Text myquestInfo;
    [SerializeField] private Text myquestReward;

    private string parsethis;

    private string current_id = "";
    private string current_progress;
    private int current_cnt;
    private int complete_cnt;
    private int current_line;
    private int myQuest_page;

   [SerializeField] private List<string> current_questList;
    // Use this for initialization
    void Start() {
        inGameManager = InGamemanager.Instance;
        jsonString = Resources.Load < TextAsset >("JsonDB/Quest/QuestDB") as TextAsset;
        parsethis = jsonString.text;
        mudoMember = JsonMapper.ToObject(parsethis);

        acceptbutton.SetActive(false);
        giveupbutton.SetActive(false);
        successbutton.SetActive(false);
        myQuest_page = 1;
        questName.text = "";
        InitText();
    }
    JsonData Suchdata(string id)
    {
        for (int i = 0; i < mudoMember.Count; i++)
        {
            if (mudoMember[i]["ID"].ToString() == id)
            {
                current_line = i;
                return mudoMember[i];
            }
        }
        return null;
    }
    public void QuestButtonInit()
    {
        if (presentData["QUEST_PROGRESS"].ToString() == "AVAILABLE")
        {
            acceptbutton.SetActive(true);
            giveupbutton.SetActive(false);
            successbutton.SetActive(false);
        }
        else if (presentData["QUEST_PROGRESS"].ToString() == "ACCECPTED")
        {
            acceptbutton.SetActive(false);
            giveupbutton.SetActive(true);
            successbutton.SetActive(false);
        }
        else if (presentData["QUEST_PROGRESS"].ToString() == "COMPLETE")
        {
            acceptbutton.SetActive(false);
            giveupbutton.SetActive(false);
            successbutton.SetActive(true);
        }
        else if (presentData["QUEST_PROGRESS"].ToString() == "DONE")
        {
            acceptbutton.SetActive(false);
            giveupbutton.SetActive(false);
            successbutton.SetActive(false);
        }
    }
    public void QuestButtons(string index)
    {
        current_id = index;
        presentData = Suchdata(index); // 이거다 이거~
        QuestButtonInit();
        questName.text = presentData["QUEST_NAME"].ToString();
        questSummary.text = presentData["QUEST_SUMMARY"].ToString();
        questInfo.text = presentData["QUEST_DESCRIPTION"].ToString();
        questReward.text = presentData["QUEST_REWARD"].ToString();
    }
    public void InitText()
    {
        questSummary.text = "";
        questInfo.text = "";
        questReward.text = "";
    }
    public void InitMyQuest()
    {
        myquestSummary.text = "";
        myquestInfo.text = "";
        myquestReward.text = "";
        myquestmission.text = "";
    }
    public void AcceptQuest()
    {
        SetCurrentData();
      
        if (current_cnt < complete_cnt && current_progress.Equals("AVAILABLE"))
        {
            current_questList.Add(current_id);
            mudoMember[current_line]["QUEST_PROGRESS"] = "ACCECPTED";
        }
        QuestButtonInit();

    }
    public void GiveUpQuest()
    {
      
        if (current_questList.Count != 0)
        {
            int num = current_questList.BinarySearch(current_id);
            current_questList.RemoveAt(num);
            mudoMember[current_line]["QUEST_PROGRESS"] = "AVAILABLE";
            mudoMember[current_line]["QUEST_CURRENTCOUNT"] = 0;
            myQuest_page=0;
            if(current_questList.Count == 0)
                InitMyQuest();
            else
                SetMyQuestText(myQuest_page);
        }
        QuestButtonInit();
    }
    public void CompleteQuest()
    {
        SetCurrentData();
        
        if (current_cnt == complete_cnt)
        {
            mudoMember[current_line]["QUEST_PROGRESS"] = "DONE";
            //보상주기
        }
        else
        {
            // 아직 못끝냄
        }
        QuestButtonInit();
    }
    public void SetMyQuestText(int index)
    {
        if (current_questList.Count != 0)
        {
            string i = current_questList[index];
            current_id = i;
            presentData = Suchdata(i);
            myquestSummary.text = presentData["QUEST_SUMMARY"].ToString();
            myquestInfo.text = presentData["QUEST_DESCRIPTION"].ToString();
            myquestReward.text = presentData["QUEST_REWARD"].ToString();
            myquestmission.text = presentData["QUEST_CURRENTCOUNT"].ToString() + " / "
                + presentData["QUEST_COMPLETECOUNT"].ToString() + presentData["QUEST_OBJECT"].ToString();
        }
    }
    public void NextQuest()
    {
        myQuest_page++;
        if (myQuest_page > current_questList.Count-1)
            myQuest_page %= current_questList.Count-1;
        SetMyQuestText(myQuest_page);
    }

    private void SetCurrentData()
    {
        current_cnt = Int16.Parse(mudoMember[current_line]["QUEST_CURRENTCOUNT"].ToString());
        complete_cnt = Int16.Parse(mudoMember[current_line]["QUEST_COMPLETECOUNT"].ToString());
        current_progress = mudoMember[current_line]["QUEST_PROGRESS"].ToString();
    }


    public void OpenMyQuestWindow()
    {
        myquestWindow.SetActive(true);
        myQuest_page = 0;
  
        SetMyQuestText(myQuest_page);
    }
    public void CloseMyQuestWindow()
    {
        myquestWindow.SetActive(false);
        myQuest_page = 0;
        InitText();

    }
    // Update is called once per frame
    void Update () {
		
	}
    
    private void SaveData()
    {
        string save;
        save = JsonMapper.ToJson(mudoMember);
        File.WriteAllText(Application.persistentDataPath + "/"+"JsonQuestDB.json", save);
    }
}
