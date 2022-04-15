using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private GameObject[] _levels;
    public int _level;
    private int _levelCount;
    [SerializeField] private TextMeshProUGUI _levelText;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _level = PlayerPrefs.GetInt("Level");
        _levelCount = PlayerPrefs.GetInt("LevelCount");
        _levelText.text = "LEVEL : " + (_levelCount + 1);
        Instantiate(_levels[_level]);
    }

    void Update()
    {
        
    }

    public void NextLevel()
    {
        _level++;
        if (_level > 2)
        {
            _level = 0;
        }
        _levelCount++;
        PlayerPrefs.SetInt("Level", _level);
        PlayerPrefs.SetInt("LevelCount", _levelCount);
        SceneManager.LoadScene(0);
    }

    public void LoseGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(0);
    }
}
