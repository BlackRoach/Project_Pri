using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class State_Controller : MonoBehaviour {


    public Statement basic_State;

    private JsonData state_Data;

    
    private void Start()
    {
        TextAsset event_List = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/Start_State");

        state_Data = JsonMapper.ToObject(event_List.text);
    }

    public void Button_Defualt_State_Input()
    {
        basic_State.s_muscular_strength = (int)state_Data[0]["MUSCULAR_STRENGTH"];
        basic_State.s_magic_power = (int)state_Data[0]["MAGIC_POWER"];
        basic_State.s_intellect = (int)state_Data[0]["INTEELECT"];
        basic_State.s_charm = (int)state_Data[0]["CHARM"];
        basic_State.s_sense = (int)state_Data[0]["SENSE"];
        basic_State.s_reliability = (int)state_Data[0]["RELIABILITY"];
        basic_State.s_pride = (int)state_Data[0]["PRIDE"];
        basic_State.s_stress = (int)state_Data[0]["STRESS"];
        basic_State.s_artistic = (int)state_Data[0]["ARTISTIC"];
        basic_State.s_elegance = (int)state_Data[0]["ELEGANCE"];
        basic_State.s_morality = (int)state_Data[0]["MORALITY"];
        basic_State.s_stamina = (int)state_Data[0]["STAMINA"];
        basic_State.s_offense_power = (int)state_Data[0]["OFFENSE_POWER"];
        basic_State.s_armor = (int)state_Data[0]["ARMOR"];
        basic_State.s_magic_attack_power = (int)state_Data[0]["MAGIC_ATTACK_POWER"];
        basic_State.s_magical_resistance = (int)state_Data[0]["MAGICAL_RESISTANCE"];
        basic_State.s_attack_speed = (int)state_Data[0]["ATTACK_SPEED"];
        basic_State.s_full_hp = (int)state_Data[0]["FULL_HP"];
        basic_State.s_full_mp = (int)state_Data[0]["FULL_MP"];
    }


} // class











