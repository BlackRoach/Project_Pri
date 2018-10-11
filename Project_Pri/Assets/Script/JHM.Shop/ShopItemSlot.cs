using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour {

    private int id; // id가 -1이면 아이템이 없는 것을 의미한다.

    public Text nameText;
    public Text priceText;
    public Text infoText;
    public Image itemImage;

	public void SelectItem()
    {
        ShopManager.instance.SelectItem(id);
    }

    public void ChangeValues(int id, string name, string price, string info, string image)
    {
        this.id = id;
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
            itemImage.sprite = Resources.Load<Sprite>("JHM_Resources/Shop/" + image);
        }
    }
}
