using Submarine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject _player;
    public GameObject Player => _player;

    public bool IsGamePaused() => Time.timeScale == 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        LoadGame();
    }

    public void ShowGameOverScreen()
    {
        Invoke("GameOver", 3);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void ChangeSceneByName(string name)
    {
        if (!string.IsNullOrEmpty(name))
            SceneManager.LoadScene(name);
    }

    public void ChangeSceneByIndex(int index)
    {
        if (index >= 0)
            SceneManager.LoadScene(index);
    }

    void LoadGame()
    {
        SaveData saveData = SaveManager.currentSaveData;
        if (saveData == null)
        {
            return;
        }
        Player.transform.position = new Vector3(saveData.playerPositionX, saveData.playerPositionY, 0);
        Player.GetComponentInChildren<HealthModule>().SetHealth(saveData.health);
    }
}
