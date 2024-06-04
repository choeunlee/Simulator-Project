using UnityEngine;

public class InfiniteBuoyancy : MonoBehaviour
{
    public float waterLevel = 0.0f; // 물의 높이
    public float floatHeight = 1.0f; // 부력의 높이
    public float bounceDamp = 0.1f; // 출렁거림 감쇠
    public float waterDrag = 0.99f; // 물의 저항
    public float waterAngularDrag = 0.5f; // 물의 각저항
    public float waveFrequency = 1.0f; // 파도의 주기
    public float waveAmplitude = 0.5f; // 파도의 진폭

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // 중력을 사용
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

            // 물의 저항 적용
            rb.velocity *= waterDrag;
            rb.angularVelocity *= waterAngularDrag;
        }
    }
}
