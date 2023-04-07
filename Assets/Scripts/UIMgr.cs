using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIMgr : MonoBehaviour
{
    private static UIMgr _instance;
    public static UIMgr Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    private int score;
    public Text scoreText;
    public Button pause;
    public Button resume;

    public bool isClicked = false;
    private float timer;
    public float clickTime = 1f;
    void Start()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        //pause.onClick.AddListener(() => OnPuaseBtn());
        //resume.onClick.AddListener(() => OnResumeBtn());
    }
    private void Update()
    {
        if (isClicked)
        {
            timer += Time.unscaledDeltaTime;
            if (timer > clickTime)
            {
                timer = 0;
                if (pause.gameObject.activeSelf)
                {
                    OnPuaseEnter();
                }
                else
                {
                    OnResumeEnter();
                }
            }
        }

    }
    private void OnPuaseEnter()
    {
        Time.timeScale = 0f;
        pause.gameObject.SetActive(false);
        resume.gameObject.SetActive(true);
        isClicked = false;
    }

    private void OnResumeEnter()
    {
        Time.timeScale = 1f;
        resume.gameObject.SetActive(false);
        pause.gameObject.SetActive(true);
        isClicked = false;
    }

    public void OnAimEnter()
    {
        isClicked = true;
    }
    public void OnAimExit()
    {
        isClicked = false;
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString("0000");
    }
}
