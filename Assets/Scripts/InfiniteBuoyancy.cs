using UnityEngine;

public class InfiniteBuoyancy : MonoBehaviour
{
    public float waterLevel = 0.0f; // ���� ����
    public float floatHeight = 1.0f; // �η��� ����
    public float bounceDamp = 0.1f; // �ⷷ�Ÿ� ����
    public float waterDrag = 0.99f; // ���� ����
    public float waterAngularDrag = 0.5f; // ���� ������
    public float waveFrequency = 1.0f; // �ĵ��� �ֱ�
    public float waveAmplitude = 0.5f; // �ĵ��� ����

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // �߷��� ���
    }

    void FixedUpdate()
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
}
