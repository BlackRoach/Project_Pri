using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextContoller : MonoBehaviour {
    private static DamageTextScript popup;
    private static GameObject canvas;

    void Start () {
        canvas = GameObject.Find("BattleHUDCanvas");
        popup = Resources.Load<DamageTextScript>("Prefabs/Battle/DamageText");
    }

    public static void CreateDamageText(float val, Transform location)
    {
        DamageTextScript instance = Instantiate(popup);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = location.position;
        instance.SetText(val);
    }
    
	
	
}
