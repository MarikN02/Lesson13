
















using UnityEngine;
using UnityEngine.SceneManagement;

// Назначаем класс, ответственный за загру
//
// зку сцены при клике на кнопку
public class StartGame : MonoBehaviour
{
    // Функция вызывается автоматически при событии OnClick() кнопки
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
}