using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private int secondEnemy = 1;

    private void Update()
    {
        if (GameManager.instance.isGameStart)
        {
            transform.Translate(Vector3.forward * 3f * Time.deltaTime);
        }

    }

    private void Start()
    {
        InvokeRepeating(nameof(EnemySpawn), 0f, 1f);
    }

    void EnemySpawn()
    {

            for (int i = 0; i < secondEnemy; i++)
            {
                Vector3 vec = transform.position;
                vec.x = Random.Range(-1f, 1f);
                transform.position = vec;
                if (GameManager.instance.isGameStart && !GameManager.instance.isGameOver && !GameManager.instance.isGameWin)
                {
                    ObjectPool.Instance.SpawnFromPool("Enemy", vec, Quaternion.Euler(Vector3.up * 180));
                }
            }
        
    }
}