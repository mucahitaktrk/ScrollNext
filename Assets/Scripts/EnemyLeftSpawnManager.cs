using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeftSpawnManager : MonoBehaviour
{
    [SerializeField] private int secondEnemy;
    [SerializeField] private List<GameObject> leftEnemy;


    private void Update()
    {
        for (int i = 0; i < leftEnemy.Count; i++)
        {
            if (leftEnemy[i].gameObject.layer != 9)
            {
                leftEnemy.RemoveAt(i);
            }
        }
    }


    private void Start()
    {
        InvokeRepeating(nameof(EnemySpawn), 4f, 1f);
    }

    void EnemySpawn()
    {

        for (int i = 0; i < secondEnemy; i++)
        {
            Vector3 vec = transform.position;
            if (GameManager.instance.isGameStart && !GameManager.instance.isGameOver && !GameManager.instance.isGameWin)
            {
                leftEnemy.Add(ObjectPool.Instance.SpawnFromPool("EnemyLeft", vec, Quaternion.Euler(Vector3.up * -90)));
            }
            foreach (GameObject gameObject in leftEnemy)
            {
                gameObject.layer = 9;
            }
        }


    }
}
