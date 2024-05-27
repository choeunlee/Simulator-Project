using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Vector3 destination;  // 목적지 좌표
    public float speed;          // 이동 속도

    private float initialRotationX; 
    private float initialPositionY; 
    void Start()
    {
        // 초기 목적지와 속도를 설정할 수 있음
        // 예: destination = new Vector3(10, 2, 15);
        // 예: speed = 5f;
        initialRotationX = transform.rotation.eulerAngles.x;
        initialPositionY = transform.position.y;
    }

    void Update()
    {
        MoveTowardsDestination();
    }

    void MoveTowardsDestination()
    {
        // 현재 위치와 목적지 사이의 방향 벡터 계산
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(destination.x, initialPositionY, destination.z);
        Vector3 direction = (targetPosition - currentPosition).normalized;

        // 이동 거리 계산
        float step = speed * Time.deltaTime;
        Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, step);

        // 새로운 위치로 객체 이동
        transform.position = newPosition;

        // 객체 회전 설정
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Euler(initialRotationX, rotation.eulerAngles.y, rotation.eulerAngles.z);

        // 목적지에 도달하면 이동 중지
        if (Vector3.Distance(newPosition, targetPosition) < 0.001f)
        {
            transform.position = targetPosition;
        }
    }
}
