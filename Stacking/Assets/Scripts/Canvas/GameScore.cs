using UnityEngine;
using TMPro;

public class GameScore : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        ArrangeTheScore();
    }
    public void ArrangeTheScore()
    {
        _textMeshPro.text = GameManager.GetInstance().CurrentScore.ToString();
    }
}
