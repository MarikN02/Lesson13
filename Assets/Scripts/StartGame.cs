
















using UnityEngine;
using UnityEngine.SceneManagement;

// ��������� �����, ������������� �� �����
//
// ��� ����� ��� ����� �� ������
public class StartGame : MonoBehaviour
{
    // ������� ���������� ������������� ��� ������� OnClick() ������
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
}