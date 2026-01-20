using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Shared data container (Blackboard) used by FSM states to access and store relevant information.
/// The region "For dogs only" contains logic and data specific to dog AI behavior.
/// </summary>
public abstract class Blackboard : MonoBehaviour
{
    public Animator animator;

    public NavMeshAgent agent;

    public Vector3 targetPosition;           // Target position for alignment or movement
    public float moveSpeed;                  // Movement speed of the entity
    public float rotateSpeed = 180f;         // Rotation speed in degrees per second
    public Transform stateOwnerTransform;    // The transform of the FSM owner
    public float attackInterval = 0.5f;      // Time between attacks
    public float distanceThreshold = 0.2f;   // Distance tolerance for reaching a destination

    public bool isDamaged;
    public bool isDead;

}