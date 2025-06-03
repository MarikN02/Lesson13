using UnityEngine;

public class AdvancedParticleTrail : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;                 // Трансформ игрока
    public Vector3 positionOffset = new Vector3(0, -0.3f, 0); // Смещение позиции частиц относительно игрока

    [Header("Emission Settings")]
    [Range(0, 100)] public float minEmission = 5f;     // Минимальная интенсивность выпуска частиц
    [Range(0, 200)] public float maxEmission = 50f;    // Максимальная интенсивность выпуска частиц
    public float emissionResponseSpeed = 5f;            // Скорость адаптации интенсивности

    [Header("Velocity Settings")]
    [Range(0.1f, 5f)] public float velocityMultiplier = 1.5f; // Множитель скорости движения частиц
    public float directionSmoothness = 5f;              // Степень сглаженности направления
    public float randomVelocityStrength = 0.3f;         // Интенсивность случайного отклонения скорости

    private ParticleSystem ps;                          // Наш поток частиц
    private ParticleSystem.EmissionModule emissionModule; // Модуль эмиссии
    private Vector3 lastPosition;                       // Последняя позиция игрока
    private Vector3 smoothedDirection;                  // Упрощённое направление
    private float currentSpeed;                         // Текущая скорость игрока

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not assigned!", this);
            enabled = false;
            return;
        }

        ps = GetComponent<ParticleSystem>(); // Получаем поток частиц
        emissionModule = ps.emission; // Получаем модуль эмиссии
        lastPosition = player.position;
        smoothedDirection = Vector3.zero;

        // Следим за состоянием игрока
        player.gameObject.AddComponent<ObjectTracker>().TrackedObjectDestroyed += OnPlayerDestroyed;

        // Начальные настройки модуля скорости
        var velocityModule = ps.velocityOverLifetime;
        velocityModule.enabled = true;
        emissionModule.rateOverTime = minEmission;
    }

    void Update()
    {
        if (player == null || ps == null) return;

        // Обновляем позицию потока частиц относительно игрока
        transform.position = player.position + positionOffset;

        // Рассчитываем мгновенную скорость игрока
        Vector3 currentMovement = player.position - lastPosition;
        currentSpeed = currentMovement.magnitude / Time.deltaTime;
        lastPosition = player.position;

        if (currentSpeed > 0.01f)
        {
            // Рассчитываем направление противоположное движению игрока
            Vector3 oppositeDirection = -currentMovement.normalized;
            smoothedDirection = Vector3.Lerp(smoothedDirection, oppositeDirection,
                                            directionSmoothness * Time.deltaTime);

            // Устанавливаем скорости частиц в направлении, обратном движению игрока
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

            // Регулируем частоту испускания частиц в зависимости от скорости игрока
            float emissionRate = Mathf.Lerp(minEmission, maxEmission, currentSpeed * 0.1f);
            emissionModule.rateOverTime = emissionRate;
        }
        else
        {
            // Постепенное снижение частоты испускания при низкой скорости
            emissionModule.rateOverTime = Mathf.Lerp(emissionModule.rateOverTime.constant,
                                                    minEmission,
                                                    emissionResponseSpeed * Time.deltaTime);
        }
    }

    // Методы, вызванные при уничтожении игрока
    void OnPlayerDestroyed(GameObject destroyedObj)
    {
        if (destroyedObj == player.gameObject)
        {
            // Останавливаем создание новых частиц и очищаем существующие
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            gameObject.SetActive(false); // Деактивируем объект потока частиц
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

// Класс ObjectTracker служит для информирования других объектов о своей судьбе
class ObjectTracker : MonoBehaviour
{
    public event System.Action<GameObject> TrackedObjectDestroyed;

    void OnDestroy()
    {
        TrackedObjectDestroyed?.Invoke(gameObject);
    }
}