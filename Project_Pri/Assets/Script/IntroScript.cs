using UnityEngine;


public class IntroScript : FadeScript {

    [SerializeField] private GameObject TitleWindow;
    [SerializeField] private GameObject IntroWindow;

    void Start () {
        IntroWindow.SetActive(true);
        TitleWindow.SetActive(false);
        _FadeIn();
    }
    private void Update()
    {
        if (fadeoutEnd)
            OpenTitleWindow();
    }

    private void OpenTitleWindow()
    {
        this.gameObject.GetComponent<IntroScript>().enabled = false;
        IntroWindow.SetActive(false);
        TitleWindow.SetActive(true);
    }
}
