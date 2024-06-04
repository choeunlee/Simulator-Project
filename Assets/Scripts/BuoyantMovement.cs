using UnityEngine;

public class BuoyantMovement : MonoBehaviour
{
    public Vector3 destination;  // 목적지 좌표
    public float speed;          // 이동 속도
    public float waterLevel = 0.0f; // 물의 높이
    public float floatHeight = 1.0f; // 부력의 높이
    public float bounceDamp = 0.1f; // 출렁거림 감쇠
    public float waterDrag = 0.99f; // 물의 저항
    public float waterAngularDrag = 0.5f; // 물의 각저항
    public float waveFrequency = 1.0f; // 파도의 주기
    public float waveAmplitude = 0.5f; // 파도의 진폭

    private Rigidbody rb;
    private float initialRotationX;
    private float initialPositionY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // 중력을 사용

        initialRotationX = transform.rotation.eulerAngles.x;
        initialPositionY = transform.position.y;
    }

    void FixedUpdate()
    {
        ApplyBuoyancy();
        MoveTowardsDestination();
    }

    void ApplyBuoyancy()
    {
        Vector3 actionPoint = transform.position;
        float wave = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
        float forceFactor = 1.0f - ((actionPoint.y - (waterLevel + wave)) / floatHeight);

        if (forceFactor > 0.0f)
        {
            Vector3 uplift = -Physics.gravity * (forceFactor - rb.velocity.y * bounceDamp);
            rb.AddForceAtPosition(uplift, actionPoint);

            // 물의 저항 적용
            rb.velocity *= waterDrag;
            rb.angularVelocity *= waterAngularDrag;
        }
    }

    void MoveTowardsDestination()
    {
        // 현재 위치와 목적지 사이의 방향 벡터 계산
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(destination.x, initialPositionY, destination.z);
        Vector3 direction = (targetPosition - currentPosition).normalized;

        // 이동 거리 계산
        float step = speed * Time.fixedDeltaTime;
        Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, step);

        // 새로운 위치로 객체 이동
        rb.MovePosition(newPosition);

        // 객체 회전 설정
        Quaternion rotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Euler(initialRotationX, rotation.eulerAngles.y, rotation.eulerAngles.z));

        // 목적지에 도달하면 이동 중지
        if (Vector3.Distance(newPosition, targetPosition) < 0.001f)
        {
            rb.MovePosition(targetPosition);
        }
    }
}
