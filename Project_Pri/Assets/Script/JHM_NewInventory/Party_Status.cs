using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Party_Status  {
    private int save_Num;
    private int party_Num;
    private int party_Id;
    private string party_Face_Icon;
    private string party_Name;
    private int dismissibility_Type;
    private string party_Grade;
    private int fame;
    private int atk;
    private int def;
    private int mag;
    private int rep;
    private int sp;
    private int sp2;
    private int hp;
    private int hp_Max;
    private int weapon_Id;
    private int armor_Id;
    private int equip_Atk;
    private int equip_Def;
    private int equip_Mag;
    private int equip_Rep;
    private int equip_Sp;
    private int equip_Sp2;
    private int equip_Hp_Max;
    private string sd_Character_Model;
    private int party_Attack_Num;
    private int party_Attack1;
    private int party_Attack2;
    private int party_Attack3;

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
    public int SAVE_NUM
    {
        get { return this.save_Num; }
        set { this.save_Num = value; }
    }
    public int PARTY_NUM
    {
        get { return this.party_Num; }
        set { this.party_Num = value; }
    }
    public int PARTY_ID
    {
        get { return this.party_Id; }
        set { this.party_Id = value; }
    }
    public string PARTY_FACE_ICON
    {
        get { return this.party_Face_Icon; }
        set { this.party_Face_Icon = value; }
    }
    public string PARTY_NAME
    {
        get { return this.party_Name; }
        set { this.party_Name = value; }
    }
    public int DISMISSIBILITY_TYPE
    {
        get { return this.dismissibility_Type; }
        set { this.dismissibility_Type = value; }
    }
    public string PARTY_GRADE
    {
        get { return this.party_Grade; }
        set { this.party_Grade = value; }
    }
    public int FAME
    {
        get { return this.fame; }
        set { this.fame = value; }
    }
    public int ATK
    {
        get { return this.atk; }
        set { this.atk = value; }
    }
    public int DEF
    {
        get { return this.def; }
        set { this.def = value; }
    }
    public int MAG
    {
        get { return this.mag; }
        set { this.mag = value; }
    }
    public int REP
    {
        get { return this.rep; }
        set { this.rep = value; }
    }
    public int SP
    {
        get { return this.sp; }
        set { this.sp = value; }
    }
    public int SP2
    {
        get { return this.sp2; }
        set { this.sp2 = value; }
    }
    public int HP
    {
        get { return this.hp; }
        set { this.hp = value; }
    }
    public int HP_MAX
    {
        get { return this.hp_Max; }
        set { this.hp_Max = value; }
    }
    public int WEAPON_ID
    {
        get { return this.weapon_Id; }
        set { this.weapon_Id = value; }
    }
    public int ARMOR_ID
    {
        get { return this.armor_Id; }
        set { this.armor_Id = value; }
    }
    public int EQUIP_ATK
    {
        get { return this.equip_Atk; }
        set { this.equip_Atk = value; }
    }
    public int EQUIP_DEF
    {
        get { return this.equip_Def; }
        set { this.equip_Def = value; }
    }
    public int EQUIP_MAG
    {
        get { return this.equip_Mag; }
        set { this.equip_Mag = value; }
    }
    public int EQUIP_REP
    {
        get { return this.equip_Rep; }
        set { this.equip_Rep = value; }
    }
    public int EQUIP_SP
    {
        get { return this.equip_Sp; }
        set { this.equip_Sp = value; }
    }
    public int EQUIP_SP2
    {
        get { return this.equip_Sp2; }
        set { this.equip_Sp2 = value; }
    }
    public int EQUIP_HP_MAX
    {
        get { return this.equip_Hp_Max; }
        set { this.equip_Hp_Max = value; }
    }
    public string SD_CHARACTER_MODEL
    {
        get { return this.sd_Character_Model; }
        set { this.sd_Character_Model = value; }
    }
    public int PARTY_ATTACK_NUM
    {
        get { return this.party_Attack_Num; }
        set { this.party_Attack_Num = value; }
    }
    public int PARTY_ATTACK1
    {
        get { return this.party_Attack1; }
        set { this.party_Attack1 = value; }
    }
    public int PARTY_ATTACK2
    {
        get { return this.party_Attack2; }
        set { this.party_Attack2 = value; }
    }
    public int PARTY_ATTACK3
    {
        get { return this.party_Attack3; }
        set { this.party_Attack3 = value; }
    }
} // class





