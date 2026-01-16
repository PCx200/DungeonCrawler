using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;

    private void Awake()
    {
        //offset = transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
