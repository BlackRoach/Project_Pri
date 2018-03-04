using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour {

    [SerializeField] protected Image fade;

    [SerializeField] protected float playingTime = 0.5f;
    [SerializeField] protected float waitTime = 1.0f;
    [SerializeField] protected bool fadeout = false;
    protected float time = 0f;
    protected bool fadeoutEnd = false;
	void Start () {
      
    
 
    }
    public void _FadeIn()
    {
        StartCoroutine(FadeIn());
    }
    public void _FadeOut()
    {
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeIn()
    {
        Color color = fade.color;
        time = 0f;
        color.a = Mathf.Lerp(1f, 0f, time);
        while (color.a > 0f)
        {
            time += Time.deltaTime / playingTime;
            color.a = Mathf.Lerp(1f, 0f, time);
            fade.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(waitTime);
        if (fadeout)
            StartCoroutine(FadeOut());

    }
    IEnumerator FadeOut()
    {
        Color color = fade.color;
        time = 0f;
        color.a = Mathf.Lerp(0f, 1f, time);
        while (color.a < 1f)
        {
            time += Time.deltaTime / playingTime;
            color.a = Mathf.Lerp(0f, 1f, time);
            fade.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(waitTime);
        fadeoutEnd = true;
    }
   
}
