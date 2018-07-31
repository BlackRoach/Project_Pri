using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    //public static AudioManager instance;

    public AudioSource BGM;
    public AudioSource SoundEffect;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType<AudioManager>().Length > 1)
            Destroy(gameObject);

        //if(instance != null)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
    }

    public void BGMPlay(AudioClip clip)
    {
        BGM.clip = clip;
        BGM.Play();
    }

    public void SFPlay(AudioClip clip)
    {
        SoundEffect.clip = clip;
        SoundEffect.Play();
    }

    public void SetBGMVolume(float value)
    {
        BGM.volume = value;
    }

    public void SetSoundEffectVolume(float value)
    {
        SoundEffect.volume = value;
    }

    public float GetBGMVolume()
    {
        return BGM.volume;
    }

    public float GetSoundEffectVolume()
    {
        return SoundEffect.volume;
    }
}

// AudioManager 프리팹을 만들어 두었습니다.
