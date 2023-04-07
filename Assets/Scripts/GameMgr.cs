using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    public ClaySpawner claySpawner;
    public Text scoreTxt;
    public int score;
    public bool isClicked = false;
    public float curTime;
    public float coolTime = 2f;
    public GameObject UICanvas;

    public void ScoreCounter()
    {
        score++;
        scoreTxt.text = $"| SCORE | {score}";
    }

    public void StartGame()
    {
        claySpawner.spawnerSW = true;
        UICanvas.SetActive(false);
        scoreTxt.text = $"| SCORE | {score}";
    }

    public void StartBtnEnter()
    {
        isClicked = true;
    }

    public void StartBtnExit()
    {
        isClicked = false;
        curTime = 0;
    }

    void Update()
    {
        if(isClicked)
        {
            curTime += Time.deltaTime;
            if(curTime > coolTime)
            {
                StartGame();
                curTime = 0;
                isClicked = false;
            }
        }
    }
}
