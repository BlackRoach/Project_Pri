using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Party_Status  {
    public int save_Num;
    public int party_Num;
    public int party_Id;
    public string party_Face_Icon;
    public string party_Name;
    public int dismissibility_Type;
    public string party_Grade;
    public int fame;
    public int atk;
    public int def;
    public int mag;
    public int rep;
    public int sp;
    public int sp2;
    public int hp;
    public int hp_Max;
    public int weapon_Id;
    public int armor_Id;
    public int equip_Atk;
    public int equip_Def;
    public int equip_Mag;
    public int equip_Rep;
    public int equip_Sp;
    public int equip_Sp2;
    public int equip_Hp_Max;
    public string sd_Character_Model;
    public int party_Attack_Num;
    public int party_Attack1;
    public int party_Attack2;
    public int party_Attack3;

    public Party_Status(int _save_Num, int _party_Num, int _party_Id,string _party_Face_Icon,string _party_Name,
        int _dismissibility_Type , string _party_Grade,int _fame,int _atk,int _def,int _mag ,int _rep,int _sp, int _sp2
        ,int _hp,int _hp_Max,int _weapon_Id,int _armor_Id,int _equip_Atk,int _equip_Def,int _equip_Mag,int _equip_Rep,
        int _equip_Sp,int _equip_Sp2,int _equip_Hp_Max,string _sd_Character_Model,int _party_Attack_Num,int _party_Attack1,
        int _party_Attack2,int _party_Attack3)
    {
        this.save_Num = _save_Num;
        this.party_Num = _party_Num;
        this.party_Id = _party_Id;
        this.party_Face_Icon = _party_Face_Icon;
        this.party_Name = _party_Name;
        this.dismissibility_Type = _dismissibility_Type;
        this.party_Grade = _party_Grade;
        this.fame = _fame;
        this.atk = _atk;
        this.def = _def;
        this.mag = _mag;
        this.rep = _rep;
        this.sp = _sp;
        this.sp2 = _sp2;
        this.hp = _hp;
        this.hp_Max = _hp_Max;
        this.weapon_Id = _weapon_Id;
        this.armor_Id = _armor_Id;
        this.equip_Atk = _equip_Atk;
        this.equip_Def = _equip_Def;
        this.equip_Mag = _equip_Mag;
        this.equip_Rep = _equip_Rep;
        this.equip_Sp = _equip_Sp;
        this.equip_Sp2 = _equip_Sp2;
        this.equip_Hp_Max = _equip_Hp_Max;
        this.sd_Character_Model = _sd_Character_Model;
        this.party_Attack_Num = _party_Attack_Num;
        this.party_Attack1 = _party_Attack1;
        this.party_Attack2 = _party_Attack2;
        this.party_Attack3 = _party_Attack3;
    }
    // 초기화
    public Party_Status(int _save_Num,int _party_Num)
    {
        this.save_Num = _save_Num;
        this.party_Num = _party_Num;
        this.party_Face_Icon = " ";
        this.party_Name = " ";
        this.party_Grade = " ";
        this.sd_Character_Model = " ";
    }

    
} // class















