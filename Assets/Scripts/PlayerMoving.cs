using UnityEngine;

public class PlayerMovementWithPhysics : MonoBehaviour
{
    public Camera mainCamera;              // Основная камера
    public float moveSpeed = 5f;           // Скорость движения
    public Rigidbody rigidbody;            // Физическая модель игрока

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Получаем ввод с клавиатуры
        float horizontal = Input.GetAxisRaw("Horizontal"); // Лево-право
        float vertical = Input.GetAxisRaw("Vertical");     // Вперед-назад

        // Определяем вектор направления исходя из ввода
        Vector3 direction = new Vector3(horizontal, 0, vertical);

        // Преобразуем направление в пространство камеры
        direction = mainCamera.transform.TransformDirection(direction);

        // Нормализуем вектор направления
        direction.Normalize();

        // Применяем импульс к физической модели игрока
        rigidbody.AddForce(direction * moveSpeed, ForceMode.Acceleration);
    }
}