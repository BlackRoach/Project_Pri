using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Event : MonoBehaviour {
    private GameObject face_Emotion_01, face_Emotion_02, face_Emotion_03;

  
    private void Awake()
    {
        face_Emotion_01 = transform.Find("Face_Type01").gameObject;
        face_Emotion_02 = transform.Find("Face_Type02").gameObject;
        face_Emotion_03 = transform.Find("Face_Type03").gameObject;
               
    }
    private void Start()
    {
        face_Emotion_01.SetActive(false);
        face_Emotion_02.SetActive(false);
        face_Emotion_03.SetActive(false);      
    }

    public void Trigger_Eight_Character_Emotion_To_Face_Control()
    {
        if (EventController.instance.face_Current_State_Count == 1)
        {
            face_Emotion_01.SetActive(true);
            face_Emotion_02.SetActive(false);
            face_Emotion_03.SetActive(false);
        }
        if (EventController.instance.face_Current_State_Count == 2)
        {
            face_Emotion_01.SetActive(false);
            face_Emotion_02.SetActive(true);
            face_Emotion_03.SetActive(false);
        }
        if (EventController.instance.face_Current_State_Count == 3)
        {
            face_Emotion_01.SetActive(false);
            face_Emotion_02.SetActive(false);
            face_Emotion_03.SetActive(true);
        }
    }

} // class







