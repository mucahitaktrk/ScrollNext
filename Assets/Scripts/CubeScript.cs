using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{

    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;

    [SerializeField] [Range(0f, 1f)] float _lerpTime;
    private bool _playerCollision = false;

    [SerializeField] private Color _color;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_playerCollision)
        {
            _meshRenderer.material.color = Color.Lerp(_meshRenderer.material.color, _color, _lerpTime * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instance.isGameStart)
        {
            if (other.gameObject.tag == "Player")
            {
                _playerCollision = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (GameManager.instance.isGameStart)
        {
            if (other.gameObject.tag == "Player")
            {
                _rigidbody.isKinematic = false;
                _rigidbody.useGravity = true;
            }
        }
    }
}
