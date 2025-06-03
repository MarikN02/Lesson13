using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine;
using UnityEngine.SceneManagement;

public class WinNextLevel : MonoBehaviour
{
    public Animator skullAnimator; // Ссылка на Animator объекта с анимациями
    public string happyTrigger = "Happy"; // Имя триггерного параметра в Animator

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            // Активируем анимацию перехода
            if (skullAnimator != null)
            {
                skullAnimator.SetTrigger(happyTrigger);
            }

            // Запускаем задержку перед загрузкой сцены
            StartCoroutine(WaitAndLoadGameOver());
        }
    }

    IEnumerator WaitAndLoadGameOver()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");
    }
}