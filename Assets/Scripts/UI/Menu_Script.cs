using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{
    [SerializeField] private AudioSource clickSound;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject Slot1;
    [SerializeField] private GameObject Slot2;
    [SerializeField] private GameObject Slot3;
    [SerializeField] private GameObject back_btn;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        Slot1.SetActive(false);
        Slot2.SetActive(false);
        Slot3.SetActive(false);
        back_btn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartGame()
    {
        SceneManager.LoadScene("SmallerMap");
    }
   

    public void PLayClickSound()
    {
        clickSound.Play();
    }

    public void ExitGame()
    {
        Debug.Log("Exiting program...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void LoadGame()
    {
        mainMenu.SetActive(false);
        Slot1.SetActive(true);
        Slot2.SetActive(true);
        Slot3.SetActive(true);
        back_btn.SetActive(true);
    }
    public void BackBtn()
    {
        mainMenu.SetActive(true);
        Slot1.SetActive(false);
        Slot2.SetActive(false);
        Slot3.SetActive(false);
        back_btn.SetActive(false);
    }
}
