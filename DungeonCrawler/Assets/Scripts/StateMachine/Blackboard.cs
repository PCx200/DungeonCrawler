using UnityEngine;
using UnityEngine.AI;

public abstract class Blackboard : MonoBehaviour
{
    public Animator animator;

    public NavMeshAgent agent;

    public Vector3 targetPosition;
    public float moveSpeed;              
    public float rotateSpeed = 180f;
    public Transform stateOwnerTransform;
    public float attackInterval = 0.5f;

    public bool isDamaged;
    public bool isDead;

}