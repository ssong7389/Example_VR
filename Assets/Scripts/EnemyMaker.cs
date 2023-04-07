using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker : MonoBehaviour
{
    public GameObject enemyPrefab;

    float genTime;
    public float coolTime = 3f;

    private Transform maker;
    private Vector3 originPos;
    void Start()
    {
        maker = transform;
        originPos = maker.position;
    }

    // Update is called once per frame
    void Update()
    {
        genTime += Time.deltaTime;
        if (genTime > coolTime)
        {
            genTime = 0;
            float randX = Random.Range(originPos.x - 3, originPos.x + 3);
            float randZ = Random.Range(originPos.z - 5, originPos.z);
            maker.position = new Vector3(randX, originPos.y, randZ);
            GameObject zombie = Instantiate(enemyPrefab, maker.position, maker.rotation);
            EnemyController enemy = zombie.GetComponent<EnemyController>();
            int rand = Random.Range(0, 10);
            if (rand < 2)
            {
                enemy.zombieSpeed = Random.Range(3f, 4.0f);
            }
            else
            {
                enemy.zombieSpeed = Random.Range(2f, 2.5f);
            }
        }
    }
}
