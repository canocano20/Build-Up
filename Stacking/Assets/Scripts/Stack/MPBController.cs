using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPBController : MonoBehaviour
{
    #region Static Fields
    public static float r, g, b;
    public static int rMulti, gMulti, bMulti;
    #endregion

    #region Serialize Fields

    [SerializeField] private float _minRGB;
    [SerializeField] private float _maxRGB;
    [SerializeField] private int _multiMin;    
    [SerializeField] private int _multiMax;
    
    #endregion

    #region Private Fields
    private Color _myColor;
    private Renderer _myRenderer;
    private MaterialPropertyBlock _mpb;

    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _myRenderer = GetComponent<Renderer>();
        _mpb = new MaterialPropertyBlock();

        if(GameManager.GetInstance().CurrentScore % 10 == 0)
        {
            ChangeColor(_minRGB, _maxRGB, _multiMin, _multiMax );
        }

        _myColor =  new Color((rMulti * (GameManager.GetInstance().CurrentScore % 10)/255f) + r , (gMulti * (GameManager.GetInstance().CurrentScore % 10) /255f) + g , (bMulti * (GameManager.GetInstance().CurrentScore % 10) /255f) + b);
        //_myColor = Random.ColorHSV();
    }

    private void OnEnable()
    {
        _mpb.SetColor("_Color", _myColor);
        _myRenderer.SetPropertyBlock(_mpb);
    }

    #endregion

    #region Private Functions

    private void ChangeColor(float rgbMin, float rgbMax, int multiMin, int multiMax)
    {
        r = Random.Range(rgbMin, rgbMax) ;
        g = Random.Range(rgbMin, rgbMax) ;
        b = Random.Range(rgbMin, rgbMax) ;

        rMulti = Random.Range(multiMin, multiMax);
        gMulti = Random.Range(multiMin, multiMax);
        bMulti = Random.Range(multiMin, multiMax);
    }   

    #endregion
}
