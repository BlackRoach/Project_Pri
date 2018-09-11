using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour {

    public Text nameText;
    public Text priceText;
    public Text infoText;
    public Image itemImage;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeValues(string name, string price, string info, string image)
    {
        nameText.text = name;
        if(price == "")
        {
            priceText.text = "";
        }
        else
        {
            priceText.text = price + "G";
        }
        
        infoText.text = info;

        if (image == "")
        {
            itemImage.enabled = false;
            itemImage.sprite = null;   
        }
        else
        {
            itemImage.enabled = true;
            itemImage.sprite = Resources.Load<Sprite>("KKT_Resources/Shop/" + image);
        }
    }
}
