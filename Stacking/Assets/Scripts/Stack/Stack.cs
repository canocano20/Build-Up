using System.Collections.Generic;
using UnityEngine;
public enum MoveDirection
{
    Right,
    Back
}

public abstract class Stack : MonoBehaviour
{   
    [Header("Direction")]
    public MoveDirection moveDirection;
    protected Vector3 direction;
    [SerializeField] private float startPositionIndex;
    List<Vector3> _directions = new List<Vector3> {Vector3.right, Vector3.back};

    private void Start()
    {
        moveDirection = (MoveDirection)Random.Range(0, 2);
        direction = _directions[(int)moveDirection];
        transform.position +=  direction * -startPositionIndex;
    }
}
