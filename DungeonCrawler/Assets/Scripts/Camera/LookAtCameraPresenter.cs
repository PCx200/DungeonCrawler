using UnityEngine;

public class LookAtCameraPresenter : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180f, 0);
    }
}
