using UnityEngine;

public class AdvancedParticleTrail : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;                 // ��������� ������
    public Vector3 positionOffset = new Vector3(0, -0.3f, 0); // �������� ������� ������ ������������ ������

    [Header("Emission Settings")]
    [Range(0, 100)] public float minEmission = 5f;     // ����������� ������������� ������� ������
    [Range(0, 200)] public float maxEmission = 50f;    // ������������ ������������� ������� ������
    public float emissionResponseSpeed = 5f;            // �������� ��������� �������������

    [Header("Velocity Settings")]
    [Range(0.1f, 5f)] public float velocityMultiplier = 1.5f; // ��������� �������� �������� ������
    public float directionSmoothness = 5f;              // ������� ������������ �����������
    public float randomVelocityStrength = 0.3f;         // ������������� ���������� ���������� ��������

    private ParticleSystem ps;                          // ��� ����� ������
    private ParticleSystem.EmissionModule emissionModule; // ������ �������
    private Vector3 lastPosition;                       // ��������� ������� ������
    private Vector3 smoothedDirection;                  // ���������� �����������
    private float currentSpeed;                         // ������� �������� ������

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not assigned!", this);
            enabled = false;
            return;
        }

        ps = GetComponent<ParticleSystem>(); // �������� ����� ������
        emissionModule = ps.emission; // �������� ������ �������
        lastPosition = player.position;
        smoothedDirection = Vector3.zero;

        // ������ �� ���������� ������
        player.gameObject.AddComponent<ObjectTracker>().TrackedObjectDestroyed += OnPlayerDestroyed;

        // ��������� ��������� ������ ��������
        var velocityModule = ps.velocityOverLifetime;
        velocityModule.enabled = true;
        emissionModule.rateOverTime = minEmission;
    }

    void Update()
    {
        if (player == null || ps == null) return;

        // ��������� ������� ������ ������ ������������ ������
        transform.position = player.position + positionOffset;

        // ������������ ���������� �������� ������
        Vector3 currentMovement = player.position - lastPosition;
        currentSpeed = currentMovement.magnitude / Time.deltaTime;
        lastPosition = player.position;

        if (currentSpeed > 0.01f)
        {
            // ������������ ����������� ��������������� �������� ������
            Vector3 oppositeDirection = -currentMovement.normalized;
            smoothedDirection = Vector3.Lerp(smoothedDirection, oppositeDirection,
                                            directionSmoothness * Time.deltaTime);

            // ������������� �������� ������ � �����������, �������� �������� ������
            var velocityModule = ps.velocityOverLifetime;
            velocityModule.x = new ParticleSystem.MinMaxCurve(
                velocityMultiplier * smoothedDirection.x,
                randomVelocityStrength
            );
            velocityModule.y = new ParticleSystem.MinMaxCurve(
                velocityMultiplier * smoothedDirection.y,
                randomVelocityStrength
            );
            velocityModule.z = new ParticleSystem.MinMaxCurve(
                velocityMultiplier * smoothedDirection.z,
                randomVelocityStrength
            );

            // ���������� ������� ���������� ������ � ����������� �� �������� ������
            float emissionRate = Mathf.Lerp(minEmission, maxEmission, currentSpeed * 0.1f);
            emissionModule.rateOverTime = emissionRate;
        }
        else
        {
            // ����������� �������� ������� ���������� ��� ������ ��������
            emissionModule.rateOverTime = Mathf.Lerp(emissionModule.rateOverTime.constant,
                                                    minEmission,
                                                    emissionResponseSpeed * Time.deltaTime);
        }
    }

    // ������, ��������� ��� ����������� ������
    void OnPlayerDestroyed(GameObject destroyedObj)
    {
        if (destroyedObj == player.gameObject)
        {
            // ������������� �������� ����� ������ � ������� ������������
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            gameObject.SetActive(false); // ������������ ������ ������ ������
        }
    }

    void OnDisable()
    {
        if (ps != null)
        {
            var em = ps.emission;
            em.rateOverTime = 0;
        }
    }
}

// ����� ObjectTracker ������ ��� �������������� ������ �������� � ����� ������
class ObjectTracker : MonoBehaviour
{
    public event System.Action<GameObject> TrackedObjectDestroyed;

    void OnDestroy()
    {
        TrackedObjectDestroyed?.Invoke(gameObject);
    }
}