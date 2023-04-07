using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class EnemyController : MonoBehaviour
{
    public enum ENEMYSTATE
    {
        NONE = -1,
        IDLE = 0,
        MOVE,
        ATTACK,
        DAMAGE,
        DEAD
    }
    [Header("에너미상태")]
    public ENEMYSTATE enemyState;

    private NavMeshAgent agent;
    private Animator anim;

    [Header("타켓 플레이어")]
    public Transform target;
    private PlayableDirector rifleTimeline;

    [Header("히트 이펙트 & 아이템")]
    public GameObject hitEffect;
    public GameObject item;

    [Header("Zobmie")]
    [Range(0,5)]
    public float zombieSpeed = 2f;
    public float attackRange = 2f;
    public float stateTime;
    public float idleStateTime= 2f;
    public float attackStateTime = 1.5f;
    public float damageStateTime = 1.5f;
    public int hp = 5;
    public CapsuleCollider head;

    [Header("LockOn")]
    public bool isLockedOn = false;
    public float lockTime;
    public float lockCoolTime = 1f;
    public bool isHead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        rifleTimeline = Camera.main.GetComponentInChildren<PlayableDirector>();
        head = transform.Find("Head").GetComponent<CapsuleCollider>();
        anim = GetComponentInChildren<Animator>();
        enemyState = ENEMYSTATE.IDLE;
        Debug.Log(rifleTimeline.duration);

    }

    void Update()
    {
        switch (enemyState)
        {
            case ENEMYSTATE.NONE:
                GetComponent<CapsuleCollider>().enabled = false;
                head.enabled = false;
                break;
            case ENEMYSTATE.IDLE:
                anim.SetInteger("ENEMYSTATE", (int)enemyState);
                agent.speed = 0;
                stateTime += Time.deltaTime;
                if(stateTime > idleStateTime)
                {
                    stateTime = 0;
                    enemyState = ENEMYSTATE.MOVE;
                }
                break;
            case ENEMYSTATE.MOVE:
                anim.SetInteger("ENEMYSTATE", (int)enemyState);
                anim.SetFloat("Speed", zombieSpeed);
                agent.SetDestination(target.position);
                float dist = Vector3.Distance(target.position, transform.position);
                if(dist < attackRange)
                {
                    enemyState = ENEMYSTATE.ATTACK;
                }
                else
                {
                    agent.speed = zombieSpeed;
                }
                break;
            case ENEMYSTATE.ATTACK:
                anim.SetInteger("ENEMYSTATE", (int)enemyState);
                agent.speed = 0;
                stateTime += Time.deltaTime;
                if(stateTime > attackStateTime)
                {
                    stateTime = 0;
                    Debug.Log("공격!!!!");
                }
                break;
            case ENEMYSTATE.DAMAGE:
                anim.SetInteger("ENEMYSTATE", (int)enemyState);
                agent.speed = 0;
                stateTime += Time.deltaTime;

                if (stateTime > damageStateTime)
                {
                    stateTime = 0;
                    //GetComponentInChildren<Transform>().LookAt(target);
                    enemyState = ENEMYSTATE.MOVE;
                }
                if (hp <= 0)
                {
                    enemyState = ENEMYSTATE.DEAD;
                }
                break;
            case ENEMYSTATE.DEAD:
                anim.SetTrigger("DEAD");
                enemyState = ENEMYSTATE.NONE;
                Destroy(gameObject, 3f);
                break;
            default:
                break;
        }
        if (isLockedOn && enemyState != ENEMYSTATE.DEAD)
        {
            lockTime += Time.deltaTime;
            if(  lockTime > lockCoolTime)
            {
                lockTime = 0;
                rifleTimeline.Play();
                if(!isHead)
                    Instantiate(hitEffect, transform.position, transform.rotation);
                else
                    Instantiate(hitEffect, head.transform.position, head.transform.rotation);
                DamageByPlayer();
                enemyState = ENEMYSTATE.DAMAGE;
            }
        }
    }

    void DamageByPlayer()
    {
        int addScore = 1;
        if (isHead)
        {
            int prob = Random.Range(0, 10);
            if(prob > 1)
            {
                //Debug.Log("HeadShot");
                addScore = 10;
                hp = 0;
            }
            else
            {
                hp -= 2;
                addScore = 5;
            }
        }
        else
        {
            --hp;

        }
        if (hp <= 0)
        {
            enemyState = ENEMYSTATE.DEAD;
            UIMgr.Instance.UpdateScore(addScore);
        }
    }

    public void AimEnter()
    {
        isLockedOn = true;
    }

    public void AimExit()
    {
        isLockedOn = false;
        lockTime = 0f;
    }
}
