using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingScript : MonoBehaviour
{
    [SerializeField]
    private float minimumTime = 3f;

    [SerializeField]
    private SpriteRenderer[] backgrounds;

    [SerializeField]
    private Image fade;

    public bool fadeOuttrue = false;
    public bool fadeIntrue = false;
    private float changeTime;
    private float linearTime;
    private float fades = 1.0f;

    private int backgroundIndex;

    bool IsDone = false;
    float fTime = 0f;
    int m_chapternum;
    int m_istomain;
    AsyncOperation async_operation;

    void Start()
    {
        fadeOuttrue = true;
        //m_chapternum = PlayerPrefs.GetInt(Prefstype.ChapterNum);
        //m_istomain = PlayerPrefs.GetInt(Prefstype.IsToMain);
        //if (m_istomain == 0)
        //{
        //    if (m_chapternum == 0)
        //        StartCoroutine(StartLoad("Tutorial"));
        //    else
        //        StartCoroutine(StartLoad("Stage" + m_chapternum));
        //}
        //else if (m_istomain == 1)
        //    StartCoroutine(StartLoad(SceneType.Title));
        StartCoroutine(StartLoad("WorldMap"));
        changeTime = minimumTime / backgrounds.Length;
    }
    void Update()
    {
        fTime += Time.deltaTime;
        if (fTime >= 4f)
        {

            fadeIntrue = true;
        }
        if (fTime >= minimumTime)
        {

            async_operation.allowSceneActivation = true;
        }
    
    }
    //void Update()
    //{
    //    if (fadeIntrue) // fadein
    //    {
    //        if (fades < 1.0f)
    //        {
    //            fades += 0.02f;
    //            fade.color = new Color(0, 0, 0, fades);

    //        }
    //        else if (fades >= 1.0f)
    //        {
    //            fadeIntrue = false;
    //        }
    //    }
    //    else if (fadeOuttrue) // fadeout
    //    {

    //        if (fades >= 0)
    //        {
    //            fades -= 0.02f;
    //            fade.color = new Color(0, 0, 0, fades);

    //        }
    //        else if (fades <= 0)
    //        {

    //            fadeOuttrue = false;
    //        }
    //    }
    //    fTime += Time.deltaTime;
    //    if (fTime >= 4f)
    //    {

    //        fadeIntrue = true;
    //    }
    //    if (fTime >= minimumTime)
    //    {

    //        async_operation.allowSceneActivation = true;
    //    }

    //    SmoothChangeBackground();
    //}

 

    public IEnumerator StartLoad(string strSceneName)
    {
        async_operation = SceneManager.LoadSceneAsync(strSceneName);

        async_operation.allowSceneActivation = false;

        if (IsDone == false)
        {
            IsDone = true;

            while (async_operation.progress < 0.9f)
            {

                yield return true;
            }
        }
    }
}