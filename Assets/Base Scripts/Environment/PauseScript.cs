using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour{

    private CanvasGroup canvas;
    public string mainMenu;
    public bool isPaused;
    public GameObject pauseCanvas;


    /*public void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
        var rect = GetComponent<RectTransform>();
        rect.offsetMax = rect.offsetMin = new Vector2(0, 0);

    }*/

    void Update()
    {
        if(isPaused)
        {
            pauseCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
    }

    public void Resume()
    {
        isPaused = false;
    }

    public void Quit()
    {
        Application.LoadLevel("Main");
    }



}
