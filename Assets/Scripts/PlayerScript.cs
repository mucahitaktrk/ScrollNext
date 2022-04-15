using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerScript : MonoBehaviour
{
    private float _lastFingerPos;
    private float _moveX;
    [SerializeField] private float _speedX;
    [SerializeField] private float _speedZ;
    [SerializeField] private float _angle;

    [SerializeField] private Transform _finishTransform;
    private CharacterController _characterController;
    private Animator _playerAnimator;
    private Rigidbody _playerRigidbody;
    [SerializeField] private GameObject _helicoperObject;

    private void Start()
    {
        _finishTransform = GameObject.FindGameObjectWithTag("PlayerFinishPos").GetComponent<Transform>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _helicoperObject = GameObject.FindGameObjectWithTag("Helicopter");
        GameStart();
        AnimationSystem();
        if (GameManager.instance.isGameWin)
        {
            Vector3 vec = _finishTransform.position;
            if (transform.position.z >= vec.z)
            {
                gameObject.transform.parent = _helicoperObject.transform.parent;
                transform.DOMove(_helicoperObject.transform.position, 0.5f);
                _playerAnimator.SetBool("Win", GameManager.instance.isGameWin);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, vec, 5 * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        MoveX();
    }


    private void GameStart()
    {
        if (!GameManager.instance.isGameStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.instance.StartGame(true);
            }
        }
        else
        {
            _speedX = 1.5f;
        }
    }

    private void MoveX()
    {
        if (GameManager.instance.isGameStart && !GameManager.instance.isGameOver && !GameManager.instance.isGameWin)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lastFingerPos = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                _moveX = _lastFingerPos - Input.mousePosition.x;
                _lastFingerPos = Input.mousePosition.x;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _moveX = 0;
            }

            float speedX = -_moveX * _speedX * Time.deltaTime;
            float a = Mathf.Clamp(transform.position.x, -4, 4);
            transform.position = new Vector3(a, transform.position.y, transform.position.z);
            transform.Translate(-speedX, 0, -_speedZ * Time.deltaTime);

            if (_moveX < 0)
            {
                //transform.DORotate(Vector3.up * _angle, 0.05f).SetEase(Ease.Linear);
            }
            else if (_moveX > 0)
            {
                //transform.DORotate(Vector3.up * -_angle, 0.05f).SetEase(Ease.Linear);
            }
            else
            {
                //transform.DORotate(Vector3.zero, 0.2f).SetEase(Ease.Linear);
            }
        }
    }

    private void AnimationSystem()
    {
        _playerAnimator.SetBool("Run", GameManager.instance.isGameStart);
        _playerAnimator.SetBool("Dead", GameManager.instance.isGameOver);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FinishLine")
        {
            GameManager.instance.WinGame(true);
        }
        else if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "DeadLine")
        {
            _characterController.enabled = false;
            GameManager.instance.GameOver(true);
        }
        else if (other.gameObject.tag == "Obstacle")
        {
            Vector3 vector = transform.position - other.transform.position;
            _playerRigidbody.AddForce(vector * 250, ForceMode.Impulse);
        }
    }
}
