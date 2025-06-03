using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine;
using UnityEngine.SceneManagement;

public class WinNextLevel : MonoBehaviour
{
    public Animator skullAnimator; // ������ �� Animator ������� � ����������
    public string happyTrigger = "Happy"; // ��� ����������� ��������� � Animator

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            // ���������� �������� ��������
            if (skullAnimator != null)
            {
                skullAnimator.SetTrigger(happyTrigger);
            }

            // ��������� �������� ����� ��������� �����
            StartCoroutine(WaitAndLoadGameOver());
        }
    }

    IEnumerator WaitAndLoadGameOver()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");
    }
}