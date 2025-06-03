using UnityEngine;

public class PlayerMovementWithPhysics : MonoBehaviour
{
    public Camera mainCamera;              // �������� ������
    public float moveSpeed = 5f;           // �������� ��������
    public Rigidbody rigidbody;            // ���������� ������ ������

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // �������� ���� � ����������
        float horizontal = Input.GetAxisRaw("Horizontal"); // ����-�����
        float vertical = Input.GetAxisRaw("Vertical");     // ������-�����

        // ���������� ������ ����������� ������ �� �����
        Vector3 direction = new Vector3(horizontal, 0, vertical);

        // ����������� ����������� � ������������ ������
        direction = mainCamera.transform.TransformDirection(direction);

        // ����������� ������ �����������
        direction.Normalize();

        // ��������� ������� � ���������� ������ ������
        rigidbody.AddForce(direction * moveSpeed, ForceMode.Acceleration);
    }
}