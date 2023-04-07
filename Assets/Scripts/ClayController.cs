using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ClayController : MonoBehaviour
{
    private Rigidbody rigd;
    public float pwd = 100;
    private bool isLockedOn = false;
    public float curTime;
    public float coolTime = 0.5f;
    public GameObject explosionEffect;
    public GameMgr gameMgr;

    void Start()
    {
        rigd = GetComponent<Rigidbody>();
        rigd.velocity = transform.forward * pwd;
        gameMgr = GameObject.Find("GameMgr").GetComponent<GameMgr>();
        Destroy(gameObject, 10f);
    }

    public void AimEnter()
    {
        //Debug.Log("조준됨!!!!!!!!!!!!!!");
        isLockedOn = true;
    }

    public void AimExit()
    {
        //Debug.Log("조준 아웃!!!!!!!!!!!!!!");
        isLockedOn = false;
        curTime = 0;
    }

    void Update()
    {
        if(isLockedOn)
        {
            curTime += Time.deltaTime;
            if(curTime > coolTime)
            {
                curTime = 0;
                Instantiate(explosionEffect, transform.position, transform.rotation);
                gameMgr.ScoreCounter();
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
