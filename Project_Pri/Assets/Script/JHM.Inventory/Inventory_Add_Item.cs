using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Add_Item : MonoBehaviour {



    public int current_Index;


    private void Start()
    {
        // 2부터 시작 이유는 맨처음 초기값있기때문에 헬멧 1 포션 5개
        current_Index = 2;
    }
    // 아이템 추가 할수있는 함수
    // 규태님 하고 희찬님 씬에서 아이템 구매시 이 함수 사용해주세요~~!!
    // param에 30001은 헬멧 30002은 포션 30003은 방패 입니다  
    public void Add_Item_Value(int _id)
    {
        Check_For_Loop_If_Index_Is_Null();
        if (current_Index < 20)
        { 
            if (_id == 30002)
            {
                for(int i = 0; i < Inventory_Add_Item_Json.instance.inventory_Item_List.Length; i++)
                {
                    if(Inventory_Add_Item_Json.instance.inventory_Item_List[i].id == 30002)
                    {
                        Inventory_Add_Item_Json.instance.inventory_Item_List[i].amount += 2;
                    }
                }
            }
            else
            {
                Inventory_Add_Item_Json.instance.inventory_Item_List[current_Index].id = _id;
                current_Index++;
            }
        }


        Inventory_Add_Item_Json.instance.SAVE_NEW_DATA_JSON_Inventory();
    }
    // 아이템 추가 시 제일 앞쪽 빈자리 찾아서 추가 할수있는 기능 
    private void Check_For_Loop_If_Index_Is_Null()
    {
        for(int i = 0; i < Inventory_Add_Item_Json.instance.inventory_Item_List.Length; i++)
        {
            if(Inventory_Add_Item_Json.instance.inventory_Item_List[i].id == -1)
            {
                current_Index = i;
                break;
            }
        }
    }

} // class










