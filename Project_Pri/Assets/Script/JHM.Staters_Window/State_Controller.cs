using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class State_Controller : MonoBehaviour {

    public GameObject state_Count_List;
    public GameObject state_Slider_List;
    public GameObject Character_Skills_List;
    public Text text_Count;

    public Statement basic_State;


    private JsonData state_Data;

    [SerializeField]
    private int current_Count;
    
    private void Start()
    {
        TextAsset event_List = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/Start_State");

        state_Data = JsonMapper.ToObject(event_List.text);

        current_Count = 0;
        text_Count.text = current_Count.ToString();
    }
    public void Count_Add_One()
    {
        current_Count++;
        text_Count.text = current_Count.ToString();
    }
    public void Count_Sub_One()
    {
        if(current_Count > 0)
        {
            current_Count--;
            text_Count.text = current_Count.ToString();
        }        
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


        Input_State_Count_Value_List();
        Input_State_Value_Slider_List();
        current_Count = 0;
        text_Count.text = current_Count.ToString();

        Input_Character_Skills_List();
    }

    private void Input_State_Count_Value_List()
    {        
        state_Count_List.transform.GetChild(0).GetComponent<Text>().text = basic_State.s_muscular_strength.ToString();
        state_Count_List.transform.GetChild(1).GetComponent<Text>().text = basic_State.s_magic_power.ToString();
        state_Count_List.transform.GetChild(2).GetComponent<Text>().text = basic_State.s_intellect.ToString();
        state_Count_List.transform.GetChild(3).GetComponent<Text>().text = basic_State.s_charm.ToString();
        state_Count_List.transform.GetChild(4).GetComponent<Text>().text = basic_State.s_sense.ToString();
        state_Count_List.transform.GetChild(5).GetComponent<Text>().text = basic_State.s_pride.ToString();
        state_Count_List.transform.GetChild(6).GetComponent<Text>().text = basic_State.s_artistic.ToString();
        state_Count_List.transform.GetChild(7).GetComponent<Text>().text = basic_State.s_elegance.ToString();
        state_Count_List.transform.GetChild(8).GetComponent<Text>().text = basic_State.s_morality.ToString();
        state_Count_List.transform.GetChild(9).GetComponent<Text>().text = basic_State.s_stamina.ToString();
        state_Count_List.transform.GetChild(10).GetComponent<Text>().text = basic_State.s_reliability.ToString();
        state_Count_List.transform.GetChild(11).GetComponent<Text>().text = basic_State.s_stress.ToString();
    }
    private void Input_State_Value_Slider_List()
    {
        state_Slider_List.transform.GetChild(0).GetComponent<Slider>().value = basic_State.s_muscular_strength;
        state_Slider_List.transform.GetChild(1).GetComponent<Slider>().value = basic_State.s_magic_power;
        state_Slider_List.transform.GetChild(2).GetComponent<Slider>().value = basic_State.s_intellect;
        state_Slider_List.transform.GetChild(3).GetComponent<Slider>().value = basic_State.s_charm;
        state_Slider_List.transform.GetChild(4).GetComponent<Slider>().value = basic_State.s_sense;
        state_Slider_List.transform.GetChild(5).GetComponent<Slider>().value = basic_State.s_pride;
        state_Slider_List.transform.GetChild(6).GetComponent<Slider>().value = basic_State.s_artistic;
        state_Slider_List.transform.GetChild(7).GetComponent<Slider>().value = basic_State.s_elegance;
        state_Slider_List.transform.GetChild(8).GetComponent<Slider>().value = basic_State.s_morality;
        state_Slider_List.transform.GetChild(9).GetComponent<Slider>().value = basic_State.s_stamina;
        state_Slider_List.transform.GetChild(10).GetComponent<Slider>().value = basic_State.s_reliability;
        state_Slider_List.transform.GetChild(11).GetComponent<Slider>().value = basic_State.s_stress;
    }
    private void Input_Character_Skills_List()
    {
        int temp_A = basic_State.s_offense_power + (basic_State.s_muscular_strength / 50);
        int temp_B = basic_State.s_magic_attack_power + (basic_State.s_magic_power / 25);
        Character_Skills_List.transform.GetChild(0).GetComponent<Text>().text = temp_A.ToString();
        Character_Skills_List.transform.GetChild(2).GetComponent<Text>().text = temp_B.ToString();
    }
    public void Button_Input_Include_Mussle_Strength_Add()
    {
        basic_State.s_muscular_strength += current_Count;
        if(basic_State.s_muscular_strength <= 1000)
        {
            state_Count_List.transform.GetChild(0).GetComponent<Text>().text = basic_State.s_muscular_strength.ToString();
            state_Slider_List.transform.GetChild(0).GetComponent<Slider>().value = basic_State.s_muscular_strength;
        }
        else
        {
            basic_State.s_muscular_strength = 1000;
            state_Count_List.transform.GetChild(0).GetComponent<Text>().text = basic_State.s_muscular_strength.ToString();
            state_Slider_List.transform.GetChild(0).GetComponent<Slider>().value = basic_State.s_muscular_strength;
        }
        Input_Character_Skills_List();
    }
    public void Button_Input_Include_Masic_Power_Add()
    {
        basic_State.s_magic_power += current_Count;
        if (basic_State.s_magic_power <= 1000)
        {
            state_Count_List.transform.GetChild(1).GetComponent<Text>().text = basic_State.s_magic_power.ToString();
            state_Slider_List.transform.GetChild(1).GetComponent<Slider>().value = basic_State.s_magic_power;
        }
        else
        {
            basic_State.s_magic_power = 1000;
            state_Count_List.transform.GetChild(1).GetComponent<Text>().text = basic_State.s_magic_power.ToString();
            state_Slider_List.transform.GetChild(1).GetComponent<Slider>().value = basic_State.s_magic_power;
        }
        Input_Character_Skills_List();
    }
    

} // class











