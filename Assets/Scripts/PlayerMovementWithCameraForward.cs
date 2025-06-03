using UnityEngine;

public class PlayerMovementWithCameraForward : MonoBehaviour
{
    public Camera mainCamera;               // �������� ������
    public float moveSpeed = 5f;            // �������� ��������
    public Rigidbody rigidbody;             // ���������� ������ ������

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // �������� ���� � ����������
        float horizontal = Input.GetAxisRaw("Horizontal"); // ����-�����
        float vertical = Input.GetAxisRaw("Vertical");     // ������-�����

        // ��������� ������� ������ �����������, ����������� ������
        Vector3 forwardDir = mainCamera.transform.forward; // ������ ������
        Vector3 rightDir = mainCamera.transform.right;     // ����� ������

        // ������� ����������� �������� ������:
        // forwardDir �������� �� ������������ ���� (W/S)
        // rightDir �������� �� �������������� ���� (A/D)
        Vector3 direction = forwardDir * vertical + rightDir * horizontal;

        // ������� ��������� ��� ���������� �����
        if (direction != Vector3.zero)
        {
            direction.Normalize(); // ����������� ������ �����������
        }

        // ������������ ������ � ���������� ������ ������
        rigidbody.AddForce(direction * moveSpeed, ForceMode.Acceleration);
    }
}