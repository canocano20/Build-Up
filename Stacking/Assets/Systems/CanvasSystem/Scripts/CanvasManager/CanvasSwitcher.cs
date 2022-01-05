using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CanvasSwitcher : MonoBehaviour
{
    public CanvasType desiredCanvas;
    private Button menuButton;

    private CanvasManager canvasManager;
    private void Start()
    {
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(OnButtonClicked);
        canvasManager = CanvasManager.GetInstance();
    }

    private void OnButtonClicked()
    {
        
        canvasManager.SwitchCanvas(desiredCanvas);
    }
}
