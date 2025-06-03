using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public ParticleSystem deathParticles; // ��������� Particles, ������� ��������� � ������
    public MeshRenderer meshRenderer;     // ������ ����� ������
    public string tagOfDeathCollider = "Finish"; // ��� �������� ������� (��������, Deadly)

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ���������� �� ����� � ������� ��������
        if (other.CompareTag(tagOfDeathCollider))
        {
            // �������� ������ ����������
            meshRenderer.enabled = false;

            // �������� ������� ������ (�������)
            deathParticles.Play();

            // ���������� ������ ����� ��������� ��������, ����� ������� �����
            Destroy(gameObject, deathParticles.main.duration);
        }
    }
}