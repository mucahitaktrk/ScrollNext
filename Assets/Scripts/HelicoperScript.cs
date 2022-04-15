using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicoperScript : MonoBehaviour
{
    [SerializeField] private Transform _helicopterPos;

    void Update()
    {
        if (GameManager.instance.isGameWin)
        {
            Invoke(nameof(HelicopterSystem), 5f);
        }
    }

    private void HelicopterSystem()
    {
        transform.position = Vector3.MoveTowards(transform.position, _helicopterPos.position, 10 * Time.deltaTime);
    }
}
