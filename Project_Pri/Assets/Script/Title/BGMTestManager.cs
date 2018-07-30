using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMTestManager : MonoBehaviour {

    public List<AudioClip> clipLists;

    public void GoToTitle()
    {
        SceneManager.LoadScene("Title 1");
    }

    public void SoundEffectTest(AudioClip clip)
    {
        AudioManager AM = FindObjectOfType<AudioManager>();
        AM.SFPlay(clip);
    }

    public void PlayNextBGM()
    {
        AudioManager AM = FindObjectOfType<AudioManager>();

        // 현재 재생되고 있는 노래를 리스트에서 찾는다.
        // 그 다음에 있는 노래를 재생 한다.
        // 근데 현재 재생되고 있는 노래가 리스트 맨 마지막이라면
        // 처음에 있는 노래을 재생한다.
        for (int i =0; i < clipLists.Count; i++)
        {
            if(clipLists[i].name == AM.BGM.clip.name)
            {
                
                if (i == clipLists.Count - 1)  
                {
                    AM.BGMPlay(clipLists[0]);
                }
                else
                {
                    AM.BGMPlay(clipLists[i + 1]);
                }
                return;
            }
        }
    }
}
