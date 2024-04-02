using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{
    [SerializeField] private AudioSource clickSound;


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }


    public void PLayClickSound()
    {
        clickSound.Play();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
