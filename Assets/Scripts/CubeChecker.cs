using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeChecker : MonoBehaviour
{
    public bool isChecked = false;
    public float curTime;
    public float coolTime;

    public Slider bar;
    public GameObject cubePrefab;

    public void OnCubeClick()
    {
        Debug.Log("Cube Clicked!!!!!!!!!!!!!!!!!!!!!");
    }

    public void OnCubeEnter()
    {
        isChecked = true;
    }

    public void OnCubeExit()
    {
        isChecked = false;
        curTime = 0;
        OnChangeValue();
    }

    public void OnChangeValue()
    {
        bar.value = curTime / coolTime;
    }

    public void MakeNewCube()
    {
        int x = Random.Range(-8, 9);
        int y = Random.Range(-4, 5);

        Instantiate(cubePrefab, new Vector3(x, y, transform.position.z), transform.rotation);
    }

    void Update()
    {
        if(isChecked)
        {
            curTime += Time.deltaTime;
            OnChangeValue();
            if (curTime>coolTime)
            {
                curTime = 0;
                Debug.Log("클릭이라고 치자!!!!");
                MakeNewCube();
            }
        }
    }
}
