using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 따라가고자 하는 대상
    public Vector3 offset;   // 카메라와 대상 사이의 거리

    void LateUpdate()
    {
        if (target == null)
            return;

        // 대상의 위치에 offset을 더한 위치로 카메라를 이동시킵니다.
        transform.position = target.position + offset;
    }
}
