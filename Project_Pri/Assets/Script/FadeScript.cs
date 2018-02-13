 using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour {

    [SerializeField] private Image fade;
    [SerializeField] private GameObject TitleWindow;
    [SerializeField] private GameObject IntroWindow;
    public float playingTime = 0.5f;
    public float WaitTime = 1.0f;
    private float time = 0f;

	void Start () {
        IntroWindow.SetActive(true);
        TitleWindow.SetActive(false);
        StartCoroutine(FadeIn());
 
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
        yield return new WaitForSeconds(WaitTime);
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
        yield return new WaitForSeconds(WaitTime);
        this.gameObject.GetComponent<FadeScript>().enabled = false;
        IntroWindow.SetActive(false);
        TitleWindow.SetActive(true);
    }
   
}
