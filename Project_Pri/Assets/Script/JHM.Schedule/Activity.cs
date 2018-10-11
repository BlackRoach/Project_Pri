using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Activity : MonoBehaviour {

    public string title;
    public int days;
    public int price;

    public Text titleText;
    public Text daysText;
    public Text priceText;
    public bool special; // 특수가트/일반카드

    // Activity Button을 클릭한다.
    // 일정슬롯에 추가한다.
    // 리스트가 필요하다.
    // 크기가 3인 리스트를 만들어서 관리한다.
    // title, days, price 정보를 모두 전달한다.

    // 변수에 저장된 값들을 실제로 표시한다.
    public void ShowValues() 
    {
        titleText.text = title;
        daysText.text = days.ToString();
        priceText.text = price.ToString();
    }

    // 다른 활동정보를 받아서 저장한다.
    public void ChangeValues(Activity other)
    {
        title = other.title;
        days = other.days;
        price = other.price;
        ShowValues();
    }
}
