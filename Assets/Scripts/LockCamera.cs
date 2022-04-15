using UnityEngine;
using Cinemachine;

public class LockCamera : MonoBehaviour
{
    CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] Transform player;

    private void Start()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (GameManager.instance.isGameWin)
        {
            cinemachineVirtualCamera.Follow = null;
        }
        else
        {
            cinemachineVirtualCamera.Follow = player;
        }
    }
}