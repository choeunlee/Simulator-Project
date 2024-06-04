using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ���󰡰��� �ϴ� ���
    public Vector3 offset;   // ī�޶�� ��� ������ �Ÿ�

    void LateUpdate()
    {
        if (target == null)
            return;

        // ����� ��ġ�� offset�� ���� ��ġ�� ī�޶� �̵���ŵ�ϴ�.
        transform.position = target.position + offset;
    }
}
