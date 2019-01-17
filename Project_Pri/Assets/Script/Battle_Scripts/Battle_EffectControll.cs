using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_EffectControll : MonoBehaviour {

	
    public void CloseEffect()
    {
        this.gameObject.SetActive(false);
    }
    public void DestroyEffect()
    {
        Destroy(this.gameObject);
    }
}
