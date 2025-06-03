using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public ParticleSystem deathParticles; // Компонент Particles, который подключён к игроку
    public MeshRenderer meshRenderer;     // Рендер сетка игрока
    public string tagOfDeathCollider = "Finish"; // Тег опасного объекта (например, Deadly)

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, столкнулся ли игрок с опасным объектом
        if (other.CompareTag(tagOfDeathCollider))
        {
            // Скрываем игрока немедленно
            meshRenderer.enabled = false;

            // Включаем эффекты взрыва (частицы)
            deathParticles.Play();

            // Уничтожаем игрока через небольшую задержку, чтобы увидеть взрыв
            Destroy(gameObject, deathParticles.main.duration);
        }
    }
}