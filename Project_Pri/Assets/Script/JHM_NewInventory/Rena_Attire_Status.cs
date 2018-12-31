using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rena_Attire_Status
{
    private int save_Num;
    private int id;
    private string name;
    private int muscular_Strength;
    private int magic_power;
    private int stamina;
    private int intellect;
    private int charm;
    private int sense;
    private int pride;
    private int artistic;
    private int elegance;
    private int morality;
    private int reliability;
    private int stress;
    private int old;
    private int mood;
    private int attire_Id;
    private int equip_Muscular_Strength;
    private int equip_Magic_Power;
    private int equip_Stamina;
    private int equip_Intellect;
    private int equip_Charm;
    private int equip_Sense;
    private int equip_Pride;
    private int equip_Artistic;
    private int equip_Elegance;
    private int equip_Morality;
    private int equip_Reliability;
    private int equip_Stress;

    public Rena_Attire_Status(int _save_Num,int _id, string _name, int _muscular_Strength, int _magic_power, int _stamina
        , int _intellect, int _charm, int _sense, int _pride, int _artistic, int _elegance, int _morality, int _reliability,
       int _stress, int _old, int _mood, int _attire_Id, int _equip_Muscular_Strength, int _equip_Magic_Power, int _equip_Stamina
       , int _equip_Intellect, int _equip_Charm, int _equip_Sense, int _equip_Pride, int _equip_Artistic, int _equip_Elegance
       , int _equip_Morality, int _equip_Reliability, int _equip_Stress)
    {
     this.save_Num                  = _save_Num;
     this.id                        = _id;
     this.name                      = _name;
     this.muscular_Strength         = _muscular_Strength;
     this.magic_power               = _magic_power;
     this.stamina                   = _stamina;
     this.intellect                 = _intellect;
     this.charm                     = _charm;
     this.sense                     = _sense;
     this.pride                     = _pride;
     this.artistic                  = _artistic;
     this.elegance                  = _elegance;
     this.morality                  = _morality;
     this.reliability               = _reliability;
     this.stress                    = _stress;
     this.old                       = _old;
     this.mood                      = _mood;
     this.attire_Id                 = _attire_Id;
     this.equip_Muscular_Strength   = _equip_Muscular_Strength;
     this.equip_Magic_Power         = _equip_Magic_Power;
     this.equip_Stamina             = _equip_Stamina;
     this.equip_Intellect           = _equip_Intellect;
     this.equip_Charm               = _equip_Charm;
     this.equip_Sense               = _equip_Sense;
     this.equip_Pride               = _equip_Pride;
     this.equip_Artistic            = _equip_Artistic;
     this.equip_Elegance            = _equip_Elegance;
     this.equip_Morality            = _equip_Morality;
     this.equip_Reliability         = _equip_Reliability;
     this.equip_Stress              = _equip_Stress; 
    }
    // 초기화
    public Rena_Attire_Status(int _save_Num)
    {
        this.save_Num = _save_Num;
        this.id = 0;
        this.name = " ";
    }

    public int SAVE_NUM
    {
        get { return this.save_Num; }
        set { this.save_Num = value; }
    }
    public int ID
    {
        get { return this.id; }
        set { this.id = value; }
    }
    public string NAME
    {
        get { return this.name; }
        set { this.name = value; }
    }
    public int MUSCULAR_STRENGTH
    {
        get { return this.muscular_Strength; }
        set { this.muscular_Strength = value; }
    }
    public int MAGIC_POWER
    {
        get { return this.magic_power; }
        set { this.magic_power = value; }
    }
    public int STAMINA
    {
        get { return this.stamina; }
        set { this.stamina = value; }
    }
    public int INTELLECT
    {
        get { return this.intellect; }
        set { this.intellect = value; }
    }
    public int CHARM
    {
        get { return this.charm; }
        set { this.charm = value; }
    }
    public int SENSE
    {
        get { return this.sense; }
        set { this.sense = value; }
    }
    public int PRIDE
    {
        get { return this.pride; }
        set { this.pride = value; }
    }
    public int ARTISTIC
    {
        get { return this.artistic; }
        set { this.artistic = value; }
    }
    public int ELEGANCE
    {
        get { return this.elegance; }
        set { this.elegance = value; }
    }
    public int MORALITY
    {
        get { return this.morality; }
        set { this.morality = value; }
    }
    public int RELIABILITY
    {
        get { return this.reliability; }
        set { this.reliability = value; }
    }
    public int STRESS
    {
        get { return this.stress; }
        set { this.stress = value; }
    }
    public int OLD
    {
        get { return this.old; }
        set { this.old = value; }
    }
    public int MOOD
    {
        get { return this.mood; }
        set { this.mood = value; }
    }
    public int ATTIRE_ID
    {
        get { return this.attire_Id; }
        set { this.attire_Id = value; }
    }
    public int EQUIP_MUSCULAR_STRENGTH
    {
        get { return this.equip_Muscular_Strength; }
        set { this.equip_Muscular_Strength = value; }
    }
    public int EQUIP_MAGIC_POWER
    {
        get { return this.equip_Magic_Power; }
        set { this.equip_Magic_Power = value; }
    }
    public int EQUIP_STAMINA
    {
        get { return this.equip_Stamina; }
        set { this.equip_Stamina = value; }
    }
    public int EQUIP_INTELLECT
    {
        get { return this.equip_Intellect; }
        set { this.equip_Intellect = value; }
    }
    public int EQUIP_CHARM
    {
        get { return this.equip_Charm; }
        set { this.equip_Charm = value; }
    }
    public int EQUIP_SENSE
    {
        get { return this.equip_Sense; }
        set { this.equip_Sense = value; }
    }
    public int EQUIP_PRIDE
    {
        get { return this.equip_Pride; }
        set { this.equip_Pride = value; }
    }
    public int EQUIP_ARTISTIC
    {
        get { return this.equip_Artistic; }
        set { this.equip_Artistic = value; }
    }
    public int EQUIP_ELEGANCE
    {
        get { return this.equip_Elegance; }
        set { this.equip_Elegance = value; }
    }
    public int EQUIP_MORALITY
    {
        get { return this.equip_Morality; }
        set { this.equip_Morality = value; }
    }
    public int EQUIP_RELIABILITY
    {
        get { return this.equip_Reliability; }
        set { this.equip_Reliability = value; }
    }
    public int EQUIP_STRESS
    {
        get { return this.equip_Stress; }
        set { this.equip_Stress = value; }
    }

} // class