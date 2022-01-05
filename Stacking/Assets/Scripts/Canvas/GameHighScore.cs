using TMPro;
using UnityEngine;

public class GameHighScore : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _textMeshPro.text = "Record" + "\n" + GameManager.GetInstance().HighestScore.ToString();
    }

    public void ArrangeTheHighScore()
    {
        _textMeshPro.text = "Record" + "\n" + GameManager.GetInstance().HighestScore.ToString();
    }
}
