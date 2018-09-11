using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTalkList {

    private int id;
    private string talkGroupName;
    private List<string> talkList;

    public ShopTalkList(int id, string talkGroupName, List<string> talkList)
    {
        this.id = id;
        this.talkGroupName = talkGroupName;

        this.talkList = new List<string>();

        for(int i = 0; i < talkList.Count; i++)
        {
            this.talkList.Add(talkList[i]);
        }
    }

    public List<string> TALK_LIST
    {
        get
        {
            return talkList;
        }
    }
}
