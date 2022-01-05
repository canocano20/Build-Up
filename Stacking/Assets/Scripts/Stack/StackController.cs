using UnityEngine;
using System.Collections;

public class StackController : Stack
{
    #region Static Fields
    public static StackController CurrentStack{get; private set;}
    public static StackController PreviousStack{get; private set;}
    private static int _combo = 0;

    #endregion

    #region Serialize Fields

    [Header("Attributes")]
    [SerializeField] private float _speed;
    [SerializeField] private float _positionCorrectionThreshold;
    [SerializeField] private int _comboLimit;
    
    [Header("Event")]
    [SerializeField] private GameEvent _onGameEnded;

    #endregion
    
    #region Private Fields
    private bool _canMove = false;

    #endregion
    
    #region Unity Callbacks
    private void OnEnable()
    {
        if(PreviousStack == null)
        {
            PreviousStack = GameObject.Find("StartStack").GetComponent<StackController>();
        }

        CurrentStack = this;

        _canMove = true;
    }

    private void Update()
    {
        if(_canMove)
        {
            if(GameManager.GetInstance().CanGameStart)
                MoveAxis(base.direction ,_speed);
        }
    }
    
    #endregion

    #region Public Funtions
    public void OnMouseClick()
    {
        if(_canMove == true)
        {
            _canMove = false;

            float hangover = GetHangover();

            float hangoverDirection = base.moveDirection == MoveDirection.Right ? PreviousStack.transform.localScale.x : PreviousStack.transform.localScale.z;

            if(Mathf.Abs(hangover) >= hangoverDirection)
            {
                OnGameEnded();

                EventManager.GetInstance().RaiseEvent(_onGameEnded);

                return; 
            }

            GameManager.GetInstance().CurrentScore += 1;

            if(Mathf.Abs(hangover) < _positionCorrectionThreshold)
            {
                AudioManager.GetInstance().Play("Perfect");
                _combo += 1;

                if(_combo > _comboLimit)
                {
                    float randomness = Random.Range(0f, 1f);
                    
                    if(randomness < 0.5f)
                        transform.localScale = new Vector3(transform.localScale.x , transform.localScale.y, transform.localScale.z * 1.1f);
                    else
                        transform.localScale = new Vector3(transform.localScale.x * 1.1f , transform.localScale.y, transform.localScale.z );

                }

                transform.position = new Vector3(PreviousStack.transform.position.x, transform.position.y, PreviousStack.transform.position.z);
                PreviousStack = this;
                return;
            }

            float direction = hangover > 0 ? 1 : -1;

            if(base.moveDirection == MoveDirection.Right)
            {
                
                SplitCubeOnX(hangover, direction);
            }
                

            else if(base.moveDirection == MoveDirection.Back)
            {
                
                SplitCubeOnZ(hangover, direction);
            }

            SplitEffect();
        }
    }

    #endregion

    #region Private Functions

    private void MoveAxis(Vector3 direction,float speed)
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.Self);
    }

    private void SplitCubeOnX(float hangover, float direction)
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x * 10) / 10, transform.position.y, transform.position.z);

        float newXSize = PreviousStack.transform.localScale.x - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.x - newXSize;

        float newXPosition = PreviousStack.transform.position.x + (hangover / 2); 

        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXSize / 2 * direction);
        float fallingBlockXPosition = cubeEdge + (fallingBlockSize / 2 * direction);

        SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
    }


    private void SplitCubeOnZ(float hangover, float direction)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z * 10) / 10);

        float newZSize = PreviousStack.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;

        float newZXPosition = PreviousStack.transform.position.z + (hangover / 2); 

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZXPosition);

        float cubeEdge = transform.position.z + (newZSize / 2 * direction);
        float fallingBlockZPosition = cubeEdge + (fallingBlockSize / 2 * direction);

        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockPosition, float fallingBlockSize)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if(base.moveDirection == MoveDirection.Right)
        {
            cube.transform.localScale = new Vector3(fallingBlockSize , transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockPosition , transform.position.y, transform.position.z);
        }
                
        else if(base.moveDirection == MoveDirection.Back)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x , transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x , transform.position.y, fallingBlockPosition);
        }

        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = new Color(.2f, .2f, .2f, 1);
        Destroy(cube, 1f);
    }

    private float GetHangover()
    {
        if(base.moveDirection == MoveDirection.Right)
            return transform.position.x - PreviousStack.transform.position.x;

        else
            return transform.position.z - PreviousStack.transform.position.z;
    }

    private void OnGameEnded()
    {        
        CurrentStack = null;
        PreviousStack = null;

        _combo = 0;

        AudioManager.GetInstance().Play("Dead");

        CanvasManager.GetInstance().SwitchCanvas(CanvasType.EndMenu);

        if(GameManager.GetInstance().CurrentScore > GameManager.GetInstance().HighestScore)
            PlayerPrefs.SetInt("HighScore", GameManager.GetInstance().CurrentScore);

        Destroy(gameObject, .1f);
    }

    private void SplitEffect()
    {
            AudioManager.GetInstance().Play("Damaged");
            _combo = 0;
                
            PreviousStack = this;
            _canMove = false;
    }

    #endregion
}
