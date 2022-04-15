using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private Transform _playerObject;
    [SerializeField] private Transform _enemyFinishPos;
    [SerializeField] private Transform _enemyRightTransform;
    [SerializeField] private Transform _enemyLeftTransform;
    private Animator _enemyAnimator;
    private Rigidbody _enemyRigidbody;
    private float step = 7.5f;
    

    private void Start()
    {
        _enemyRigidbody = GetComponent<Rigidbody>();
        _enemyAnimator = GetComponent<Animator>();
        if (LevelManager.Instance._level != 0)
        {
            _enemyRightTransform = GameObject.FindGameObjectWithTag("RightTransform").GetComponent<Transform>();
            _enemyLeftTransform = GameObject.FindGameObjectWithTag("LeftTransform").GetComponent<Transform>();
        }
        _playerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _enemyFinishPos = GameObject.FindGameObjectWithTag("EnemyFinishPos").GetComponent<Transform>();
    }

    private void Update()
    {
        EnemyMove(step);
    }

    private void EnemyMove(float step)
    {

            if (GameManager.instance.isGameStart && !GameManager.instance.isGameWin)
            {
            if (gameObject.layer == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_playerObject.position.x,
                transform.position.y, _playerObject.position.z), step * Time.deltaTime);
                transform.rotation = Quaternion.Euler(Vector3.up * 180);
            }
            else if (gameObject.layer == 8)
            {
                Vector3 vector = _enemyRightTransform.position;
                transform.position = Vector3.MoveTowards(transform.position, vector, step * Time.deltaTime);
                if (transform.position == vector)
                {
                    gameObject.layer = 0;
                }
            }
            if (gameObject.layer == 9)
            {
                Vector3 vector = _enemyLeftTransform.position;
                transform.position = Vector3.MoveTowards(transform.position, vector, step * Time.deltaTime);
                if (transform.position == vector)
                {
                    gameObject.layer = 0;
                }
            }

        }
            else if (GameManager.instance.isGameStart && GameManager.instance.isGameWin)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_enemyFinishPos.position.x,
                transform.position.y, _enemyFinishPos.transform.position.z), step * Time.deltaTime);
            }
        
        EnemyAnimation();
    }

    private void EnemyAnimation()
    {
        _enemyAnimator.SetBool("Run", GameManager.instance.isGameStart);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.GameOver(true);
            _enemyAnimator.SetBool("Dead", true);
            GetComponent<CapsuleCollider>().isTrigger = true;
        }
        else if (other.gameObject.tag == "DeadLine")
        {
            gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "Obstacle")
        {
            Vector3 vector = transform.position - other.transform.position;
            _enemyRigidbody.AddForce(vector * 250, ForceMode.Impulse);
        }
    }
}
