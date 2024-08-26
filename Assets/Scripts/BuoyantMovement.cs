using UnityEngine;

public class BuoyantMovement : MonoBehaviour
{
    public Vector3 destination;  // ������ ��ǥ
    public float speed;          // �̵� �ӵ�
    public float waterLevel = 0.0f; // ���� ����
    public float floatHeight = 1.0f; // �η��� ����
    public float bounceDamp = 0.1f; // �ⷷ�Ÿ� ����
    public float waterDrag = 0.99f; // ���� ����
    public float waterAngularDrag = 0.5f; // ���� ������
    public float waveFrequency = 1.0f; // �ĵ��� �ֱ�
    public float waveAmplitude = 0.5f; // �ĵ��� ����

    private Rigidbody rb;
    private float initialRotationX;
    private float initialPositionY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // �߷��� ���

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

            // ���� ���� ����
            rb.velocity *= waterDrag;
            rb.angularVelocity *= waterAngularDrag;
        }
    }

    void MoveTowardsDestination()
    {
        // ���� ��ġ�� ������ ������ ���� ���� ���
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(destination.x, initialPositionY, destination.z);
        Vector3 direction = (targetPosition - currentPosition).normalized;

        // �̵� �Ÿ� ���
        float step = speed * Time.fixedDeltaTime;
        Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, step);

        // ���ο� ��ġ�� ��ü �̵�
        rb.MovePosition(newPosition);

        // ��ü ȸ�� ����
        Quaternion rotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Euler(initialRotationX, rotation.eulerAngles.y, rotation.eulerAngles.z));

        // �������� �����ϸ� �̵� ����
        if (Vector3.Distance(newPosition, targetPosition) < 0.001f)
        {
            rb.MovePosition(targetPosition);
        }
    }
}
