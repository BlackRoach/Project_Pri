using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class NewInventory_JsonData : MonoBehaviour {
    public static NewInventory_JsonData instance = null;
    public Save_Type_Option select_Type_Option;
    // 저장 불러오기 게임플레이 데이터
    public Party_Status[] party_Status;               
    public Rena_Attire_Status[] rena_Attire_Status;   
    public string mobile_Path; // 모바일 저장 경로
    public int selected_Save_Location; // 저장 위치 알려주는 정수
    // 디폴트 json_data
    private JsonData save_Type_Option;
    private JsonData rena_Attire_Status_Data;
    private JsonData party_Status_Data;
    private bool is_Begin; // 처음 게임 들어올때 한번 실행
    private void Awake()
    {       
        if (instance == null)
        {
            instance = this;
        }else
        {
            Destroy(this.gameObject);
        }
        is_Begin = false; 
        party_Status = new Party_Status[12];
        rena_Attire_Status = new Rena_Attire_Status[3];
        mobile_Path = Application.persistentDataPath;
        Json_Data_Parsing(); // defualt data 파싱 
        LOAD_NEW_DATA_JSON_Rena_Attire_Status();
        LOAD_NEW_DATA_JSON_Party_Status();
        LOAD_NEW_DATA_JSON_Save_Type_Option();
        if (is_Begin)
        {
            is_Begin = false;
            int helper_1 = 4;
            for (int i = 0; i < party_Status.Length; i++)
            {
                party_Status[i] = new Party_Status((int)party_Status_Data[helper_1]["SAVE_NUM"], (int)party_Status_Data[helper_1]["PARTY_NUM"]);
                helper_1++;
            }
            int helper_2 = 1;
            for (int i = 0; i < rena_Attire_Status.Length; i++)
            {
                rena_Attire_Status[i] = new Rena_Attire_Status((int)rena_Attire_Status_Data[helper_2]["SAVE_NUM"]);
                helper_2++;
            }
            select_Type_Option = new Save_Type_Option((int)save_Type_Option[0]["LANGUAGE_TYPE"],(int)save_Type_Option[0]["SAVE_TYPE"]);
        }
    }
    private void Start()
    {
        selected_Save_Location = 0;
    }
    // defualt json 리소스 파일 푸싱
    private void Json_Data_Parsing()
    {
        TextAsset json_File_1 = Resources.Load<TextAsset>("JHM.Resources.Json/New_Inventory_Data/SAVE_TYPE_OPTION");
        save_Type_Option = JsonMapper.ToObject(json_File_1.text);
        TextAsset json_File_2 = Resources.Load<TextAsset>("JHM.Resources.Json/New_Inventory_Data/RENA_ATTIRE_STATUS_DATA");
        rena_Attire_Status_Data = JsonMapper.ToObject(json_File_2.text);
        TextAsset json_File_3 = Resources.Load<TextAsset>("JHM.Resources.Json/New_Inventory_Data/PARTY_STATUS_DATA");
        party_Status_Data = JsonMapper.ToObject(json_File_3.text);
    }
    // 만약 ID값이 0일 경우 초기값 넣기 Rena_Attire_Status
    public void Default_Save_Data_Rena_Attire_Status(int i)
    {
        rena_Attire_Status[i - 1] = new Rena_Attire_Status((int)rena_Attire_Status_Data[i]["SAVE_NUM"], (int)rena_Attire_Status_Data[0]["ID"], rena_Attire_Status_Data[0]["NAME"].ToString(), (int)rena_Attire_Status_Data[0]["MUSCULAR_STRENGTH"], (int)rena_Attire_Status_Data[0]["MAGIC_POWER"]
            , (int)rena_Attire_Status_Data[0]["STAMINA"], (int)rena_Attire_Status_Data[0]["INTEELECT"], (int)rena_Attire_Status_Data[0]["CHARM"]
            , (int)rena_Attire_Status_Data[0]["SENSE"], (int)rena_Attire_Status_Data[0]["PRIDE"], (int)rena_Attire_Status_Data[0]["ARTISTIC"]
            , (int)rena_Attire_Status_Data[0]["ELEGANCE"], (int)rena_Attire_Status_Data[0]["MORALITY"], (int)rena_Attire_Status_Data[0]["RELIABILITY"], (int)rena_Attire_Status_Data[0]["STRESS"]
            , (int)rena_Attire_Status_Data[0]["OLD"], (int)rena_Attire_Status_Data[0]["MOOD"], (int)rena_Attire_Status_Data[0]["ATTIRE_ID"], (int)rena_Attire_Status_Data[0]["EQUIP_MUSCULAR_STRENGTH"]
            , (int)rena_Attire_Status_Data[0]["EQUIP_MAGIC_POWER"], (int)rena_Attire_Status_Data[0]["EQUIP_STAMINA"], (int)rena_Attire_Status_Data[0]["EQUIP_INTEELECT"]
            , (int)rena_Attire_Status_Data[0]["EQUIP_CHARM"], (int)rena_Attire_Status_Data[0]["EQUIP_SENSE"], (int)rena_Attire_Status_Data[0]["EQUIP_PRIDE"], (int)rena_Attire_Status_Data[0]["EQUIP_ARTISTIC"]
            , (int)rena_Attire_Status_Data[0]["EQUIP_ELEGANCE"], (int)rena_Attire_Status_Data[0]["EQUIP_MORALITY"], (int)rena_Attire_Status_Data[0]["EQUIP_RELIABILITY"]
            , (int)rena_Attire_Status_Data[0]["EQUIP_STRESS"]);
    }
    // 만약 ID값이 0일 경우 초기값 넣기 Party_Status
    public void Default_Save_Data_Party_Status(int i)
    {
        int k = 0;  
        int p = 4;  
        for (int j = 0; j < party_Status.Length; j++)
        {
            if (party_Status[j].SAVE_NUM == i)
            {                
                party_Status[j] = new Party_Status((int)party_Status_Data[p]["SAVE_NUM"], (int)party_Status_Data[p]["PARTY_NUM"], (int)party_Status_Data[k]["PARTY_ID"], party_Status_Data[k]["PARTY_FACE_ICON"].ToString(), party_Status_Data[k]["PARTY_NAME"].ToString(), (int)party_Status_Data[k]["DISMISSIBILITY_TYPE"]
                        , party_Status_Data[k]["PARTY_GRADE"].ToString(), (int)party_Status_Data[k]["FAME"], (int)party_Status_Data[k]["ATK"], (int)party_Status_Data[k]["DEF"]
                        , (int)party_Status_Data[k]["MAG"], (int)party_Status_Data[k]["REP"], (int)party_Status_Data[k]["SP"], (int)party_Status_Data[k]["SP2"], (int)party_Status_Data[k]["HP"]
                        , (int)party_Status_Data[k]["HP_MAX"], (int)party_Status_Data[k]["WEAPON_ID"], (int)party_Status_Data[k]["ARMOR_ID"], (int)party_Status_Data[k]["EQUIP_ATK"]
                        , (int)party_Status_Data[k]["EQUIP_DEF"], (int)party_Status_Data[k]["EQUIP_MAG"], (int)party_Status_Data[k]["EQUIP_REP"], (int)party_Status_Data[k]["EQUIP_SP"]
                        , (int)party_Status_Data[k]["EQUIP_SP2"], (int)party_Status_Data[k]["EQUIP_HP_MAX"], party_Status_Data[k]["SD_CHARACTER_MODEL"].ToString()
                        , (int)party_Status_Data[k]["PARTY_ATTACK_NUM"], (int)party_Status_Data[k]["PARTY_ATTACK1"], (int)party_Status_Data[k]["PARTY_ATTACK2"]
                        , (int)party_Status_Data[k]["PARTY_ATTACK3"]);
                k++;
            }
            p++;
        }
    }
    // Json 저장 PARTY_STATUS_DATA
    public void SAVE_NEW_DATA_JSON_Save_Type_Option()
    {
        JsonData save_Json = JsonMapper.ToJson(select_Type_Option);
        File.WriteAllText(mobile_Path + "/" + "Save_Type_Option_Data.json", save_Json.ToString());
    }
    // Json 저장 PARTY_STATUS_DATA
    public void SAVE_NEW_DATA_JSON_Party_Status()
    {
        JsonData save_Json = JsonMapper.ToJson(party_Status);
        File.WriteAllText(mobile_Path + "/" + "Party_Status_Data.json", save_Json.ToString());
    }
    // Json 저장 Rena_Attire_Status_Data
    public void SAVE_NEW_DATA_JSON_Rena_Attire_Status()
    {
        JsonData save_Json = JsonMapper.ToJson(rena_Attire_Status);
        File.WriteAllText(mobile_Path + "/" + "Rena_Attire_Status_Data.json", save_Json.ToString());
    }
    // Json 로드 Save_Type_Option_Data
    public void LOAD_NEW_DATA_JSON_Save_Type_Option()
    {
        if (File.Exists(mobile_Path + "/" + "Save_Type_Option_Data.json"))
        {
            string json_String = File.ReadAllText(mobile_Path + "/" + "Save_Type_Option_Data.json");
            JsonData load_Json = JsonMapper.ToObject(json_String);
            select_Type_Option = new Save_Type_Option((int)load_Json["LANGUAGE_TYPE"],(int)load_Json["SAVE_TYPE"]);             
        }
    }
    // Json 로드 PARTY_STATUS_DATA
    public void LOAD_NEW_DATA_JSON_Party_Status()
    {
        if (File.Exists(mobile_Path + "/" + "Party_Status_Data.json"))
        {
            string json_String = File.ReadAllText(mobile_Path + "/" + "Party_Status_Data.json");
            JsonData load_Json = JsonMapper.ToObject(json_String);
            for (int i = 0; i < party_Status.Length; i++)
            {
                party_Status[i] = new Party_Status((int)load_Json[i]["SAVE_NUM"], (int)load_Json[i]["PARTY_NUM"], (int)load_Json[i]["PARTY_ID"], load_Json[i]["PARTY_FACE_ICON"].ToString(), load_Json[i]["PARTY_NAME"].ToString(), (int)load_Json[i]["DISMISSIBILITY_TYPE"]
                    , load_Json[i]["PARTY_GRADE"].ToString(), (int)load_Json[i]["FAME"], (int)load_Json[i]["ATK"], (int)load_Json[i]["DEF"]
                    , (int)load_Json[i]["MAG"], (int)load_Json[i]["REP"], (int)load_Json[i]["SP"], (int)load_Json[i]["SP2"], (int)load_Json[i]["HP"]
                    , (int)load_Json[i]["HP_MAX"], (int)load_Json[i]["WEAPON_ID"], (int)load_Json[i]["ARMOR_ID"], (int)load_Json[i]["EQUIP_ATK"]
                    , (int)load_Json[i]["EQUIP_DEF"], (int)load_Json[i]["EQUIP_MAG"], (int)load_Json[i]["EQUIP_REP"], (int)load_Json[i]["EQUIP_SP"]
                    , (int)load_Json[i]["EQUIP_SP2"], (int)load_Json[i]["EQUIP_HP_MAX"], load_Json[i]["SD_CHARACTER_MODEL"].ToString()
                    , (int)load_Json[i]["PARTY_ATTACK_NUM"], (int)load_Json[i]["PARTY_ATTACK1"], (int)load_Json[i]["PARTY_ATTACK2"]
                    , (int)load_Json[i]["PARTY_ATTACK3"]);
            }
        }
    }
    // Json 로드 Rena_Attire_Status_Data
    public void LOAD_NEW_DATA_JSON_Rena_Attire_Status()
    {
        if (File.Exists(mobile_Path + "/" + "Rena_Attire_Status_Data.json"))
        {
            string json_String = File.ReadAllText(mobile_Path + "/" + "Rena_Attire_Status_Data.json");
            JsonData load_Json = JsonMapper.ToObject(json_String);
            for (int i = 0; i < rena_Attire_Status.Length; i++)
            {
                rena_Attire_Status[i] = new Rena_Attire_Status((int)load_Json[i]["SAVE_NUM"], (int)load_Json[i]["ID"],load_Json[i]["NAME"].ToString()
                    , (int)load_Json[i]["MUSCULAR_STRENGTH"], (int)load_Json[i]["MAGIC_POWER"], (int)load_Json[i]["STAMINA"]
                    , (int)load_Json[i]["INTELLECT"], (int)load_Json[i]["CHARM"], (int)load_Json[i]["SENSE"], (int)load_Json[i]["PRIDE"]
                    , (int)load_Json[i]["ARTISTIC"], (int)load_Json[i]["ELEGANCE"], (int)load_Json[i]["MORALITY"], (int)load_Json[i]["RELIABILITY"], (int)load_Json[i]["STRESS"]
                    , (int)load_Json[i]["OLD"], (int)load_Json[i]["MOOD"], (int)load_Json[i]["ATTIRE_ID"], (int)load_Json[i]["EQUIP_MUSCULAR_STRENGTH"]
                    , (int)load_Json[i]["EQUIP_MAGIC_POWER"], (int)load_Json[i]["EQUIP_STAMINA"], (int)load_Json[i]["EQUIP_INTELLECT"], (int)load_Json[i]["EQUIP_CHARM"]
                    , (int)load_Json[i]["EQUIP_SENSE"], (int)load_Json[i]["EQUIP_PRIDE"], (int)load_Json[i]["EQUIP_ARTISTIC"]
                    , (int)load_Json[i]["EQUIP_ELEGANCE"], (int)load_Json[i]["EQUIP_MORALITY"], (int)load_Json[i]["EQUIP_RELIABILITY"]
                    , (int)load_Json[i]["EQUIP_STRESS"]);
            }
        }
        else
        {
            is_Begin = true;
        }
    }    
} // class



