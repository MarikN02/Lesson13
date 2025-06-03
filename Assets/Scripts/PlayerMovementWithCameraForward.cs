using UnityEngine;

public class PlayerMovementWithCameraForward : MonoBehaviour
{
    public Camera mainCamera;               // Основная камера
    public float moveSpeed = 5f;            // Скорость движения
    public Rigidbody rigidbody;             // Физическая модель игрока

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Получаем ввод с клавиатуры
        float horizontal = Input.GetAxisRaw("Horizontal"); // Лево-право
        float vertical = Input.GetAxisRaw("Vertical");     // Вперед-назад

        // Формируем базовый вектор направления, учитывающий камеру
        Vector3 forwardDir = mainCamera.transform.forward; // Взгляд камеры
        Vector3 rightDir = mainCamera.transform.right;     // Право камеры

        // Формула направления движения игрока:
        // forwardDir умножаем на вертикальный ввод (W/S)
        // rightDir умножаем на горизонтальный ввод (A/D)
        Vector3 direction = forwardDir * vertical + rightDir * horizontal;

        // Нулевое ускорение при отсутствии ввода
        if (direction != Vector3.zero)
        {
            direction.Normalize(); // Нормализуем вектор направления
        }

        // Прикладываем усилие к физической модели игрока
        rigidbody.AddForce(direction * moveSpeed, ForceMode.Acceleration);
    }
}