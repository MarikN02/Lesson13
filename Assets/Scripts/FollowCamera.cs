using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;       // Цель слежения (игрок)
    public Vector3 offsetPosition; // Смещение камеры относительно цели (позиция и высота)
    public float smoothSpeed = 0.125f; // Коэффициент сглаживания движений

    void LateUpdate()
    {
        // Позиция камеры, смещённая относительно игрока
        Vector3 desiredPosition = target.position + offsetPosition;

        // Применяем плавное движение камеры к новой позиции
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        // Ориентация камеры направлена на цель
        transform.LookAt(target);
    }

    private Vector3 velocity = Vector3.zero; // Внутренняя переменная для SmoothDamp
}