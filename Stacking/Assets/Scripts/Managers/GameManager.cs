using UnityEngine.SceneManagement;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    private int _currentScore;
    private int _highestScore;
    private bool _canGameStart;

    public int CurrentScore {get{ return _currentScore;} set{ _currentScore = value;}}
    public int HighestScore {get{ return _highestScore;} set{ _highestScore = value;}}
    public bool CanGameStart {get{ return _canGameStart;} set{ _canGameStart = value;}}

    private void Start()
    {
       Application.targetFrameRate = 60;
    }
    private void OnEnable()
    {
        HighestScore = PlayerPrefs.GetInt("HighScore");
    }

    public void RestartTheScene()
    {
        ArrangeScores();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ArrangeScores()
    {
        CurrentScore = 0;

        HighestScore = PlayerPrefs.GetInt("HighScore");
    }
}
