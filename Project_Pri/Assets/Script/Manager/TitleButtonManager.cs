using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleButtonManager : MonoBehaviour {

    // 싱글톤 패턴
    private static TitleButtonManager instance = null;
    public static TitleButtonManager Instance
    {
        get
        {
            if (instance)
                return instance;
            else
                return instance = new GameObject("*Manager").AddComponent<TitleButtonManager>();
        }
    }
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] private GameObject selectableButtons;
    [SerializeField] private GameObject touchToStartButton;

    [SerializeField] private float playingTime = 1f;


    private Image touchToStartImg;
    private float time = 0f;
    private bool isTitle = false;
    void Start() {
        touchToStartButton.SetActive(true);
        selectableButtons.SetActive(false);

        touchToStartImg = touchToStartButton.GetComponent<Image>();
        StartCoroutine(TouchButtonInvisible());
    }


    void Update() {

    }

    public void TouchToStartButtonOn()
    {
        isTitle = true;
        touchToStartButton.SetActive(false);
        selectableButtons.SetActive(true);
    }
    public void NewGameButtonOn()
    {
        // 새게임 버튼
        //초기화 진행
        SceneManager.LoadScene("WorldMap");
    }
    public void LoadGameButtonOn()
    {
        // 이어하기 버튼
        SceneManager.LoadScene("");
    }
    public void CgModeButtonOn()
    {
        // cg모드 버튼
    }
    public void OptionButtonOn()
    {
        // 옵션 버튼
    }
    public void ExitButtonOn()
    {
        // 끝내기 버튼
        Application.Quit();
    }
 
    IEnumerator TouchButtonInvisible()
    {
        Color color = touchToStartImg.color;
        time = 0f;
        color.a = Mathf.Lerp(1f, 0.5f, time);
        while (color.a > 0.5f)
        {
            time += Time.deltaTime / playingTime;
            color.a = Mathf.Lerp(1f, 0.5f, time);
            touchToStartImg.color = color;
            yield return null;
        }
        yield return null;
        if (!isTitle)
            StartCoroutine(TouchButtonVisible());
    }
    IEnumerator TouchButtonVisible()
    {
        Color color = touchToStartImg.color;
        time = 0f;
        color.a = Mathf.Lerp(0.5f, 1f, time);
        while (color.a < 1f)
        {
            time += Time.deltaTime / playingTime;
            color.a = Mathf.Lerp(0.5f, 1f, time);
            touchToStartImg.color = color;
            yield return null;
        }
        yield return null;
        if(!isTitle)
            StartCoroutine(TouchButtonInvisible());
    }
}
