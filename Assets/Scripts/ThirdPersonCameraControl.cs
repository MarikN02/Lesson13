using UnityEngine;

public class ThirdPersonCameraControl : MonoBehaviour
{
    public Transform playerTransform;  // Трансформ игрока
    public float distanceFromPlayer = 5f; // Расстояние от игрока
    public float heightAbovePlayer = 2f;  // Высота над головой игрока
    public float mouseSensitivity = 3f;   // Чувствительность мыши
    public float minAngle = -45f;         // Минимальный наклон камеры вниз
    public float maxAngle = 45f;          // Максимальный наклон камеры вверх

    private float cameraRotationX = 0f;  // Наклон камеры вверх-вниз
    private float cameraRotationY = 0f;  // Поворот камеры слева-направо

    void LateUpdate()
    {
        // Получить вращение от мыши
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Обновляем поворот камеры
        cameraRotationY += mouseX;
        cameraRotationX -= mouseY;

        // Ограничиваем наклоны камеры сверху и снизу
        cameraRotationX = Mathf.Clamp(cameraRotationX, minAngle, maxAngle);

        // Вычисляем точку назначения камеры
        Quaternion rotation = Quaternion.Euler(cameraRotationX, cameraRotationY, 0f);
        Vector3 position = playerTransform.position - rotation * Vector3.forward * distanceFromPlayer;
        position += Vector3.up * heightAbovePlayer;

        // Перемещаемся плавно
        transform.position = position;
        transform.rotation = rotation;
    }
}