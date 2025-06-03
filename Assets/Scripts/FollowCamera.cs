using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;       // ���� �������� (�����)
    public Vector3 offsetPosition; // �������� ������ ������������ ���� (������� � ������)
    public float smoothSpeed = 0.125f; // ����������� ����������� ��������

    void LateUpdate()
    {
        // ������� ������, ��������� ������������ ������
        Vector3 desiredPosition = target.position + offsetPosition;

        // ��������� ������� �������� ������ � ����� �������
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        // ���������� ������ ���������� �� ����
        transform.LookAt(target);
    }

    private Vector3 velocity = Vector3.zero; // ���������� ���������� ��� SmoothDamp
}