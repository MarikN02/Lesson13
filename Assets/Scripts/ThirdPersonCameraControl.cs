using UnityEngine;

public class ThirdPersonCameraControl : MonoBehaviour
{
    public Transform playerTransform;  // ��������� ������
    public float distanceFromPlayer = 5f; // ���������� �� ������
    public float heightAbovePlayer = 2f;  // ������ ��� ������� ������
    public float mouseSensitivity = 3f;   // ���������������� ����
    public float minAngle = -45f;         // ����������� ������ ������ ����
    public float maxAngle = 45f;          // ������������ ������ ������ �����

    private float cameraRotationX = 0f;  // ������ ������ �����-����
    private float cameraRotationY = 0f;  // ������� ������ �����-�������

    void LateUpdate()
    {
        // �������� �������� �� ����
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // ��������� ������� ������
        cameraRotationY += mouseX;
        cameraRotationX -= mouseY;

        // ������������ ������� ������ ������ � �����
        cameraRotationX = Mathf.Clamp(cameraRotationX, minAngle, maxAngle);

        // ��������� ����� ���������� ������
        Quaternion rotation = Quaternion.Euler(cameraRotationX, cameraRotationY, 0f);
        Vector3 position = playerTransform.position - rotation * Vector3.forward * distanceFromPlayer;
        position += Vector3.up * heightAbovePlayer;

        // ������������ ������
        transform.position = position;
        transform.rotation = rotation;
    }
}