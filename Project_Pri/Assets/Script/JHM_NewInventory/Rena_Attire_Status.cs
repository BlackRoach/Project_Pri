using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rena_Attire_Status
{
    public int save_Num;
    public int id;
    public string name;
    public int muscular_Strength;
    public int magic_power;
    public int stamina;
    public int intellect;
    public int charm;
    public int sense;
    public int pride;
    public int artistic;
    public int elegance;
    public int morality;
    public int reliability;
    public int stress;
    public int old;
    public int mood;
    public int attire_Id;
    public int equip_Muscular_Strength;
    public int equip_Magic_Power;
    public int equip_Stamina;
    public int equip_Intellect;
    public int equip_Charm;
    public int equip_Sense;
    public int equip_Pride;
    public int equip_Artistic;
    public int equip_Elegance;
    public int equip_Morality;
    public int equip_Reliability;
    public int equip_Stress;

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

} // class
