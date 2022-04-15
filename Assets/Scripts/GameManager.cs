using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameStart;
    public bool isGameOver;
    public bool isGameWin;

    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private ParticleSystem _particleSystem;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        _particleSystem = _particleSystem.GetComponent<ParticleSystem>();
        _startPanel.SetActive(true);
    }

    public void StartGame(bool gameStart)
    {
        isGameStart = gameStart;
        if (isGameStart)
        {
            _startPanel.SetActive(false);
        }
    }

    public void WinGame(bool gameWin)
    {
        isGameWin = gameWin;
        if (isGameWin)
        {
            _particleSystem.Play();
            //playerObject.transform.DOMove(helicopterTransform.transform.position, 2f);
            Invoke(nameof(WinPanel), 2f);
        }
    }

    public void GameOver(bool gameOver)
    {
        isGameOver = gameOver;
        if (isGameOver)
        {
            Invoke(nameof(LosePanel), 2f);
        }
    }

    private void WinPanel()
    {
        _panels[0].SetActive(true);
    }

    private void LosePanel()
    {
        _panels[1].SetActive(true);
    }
}
