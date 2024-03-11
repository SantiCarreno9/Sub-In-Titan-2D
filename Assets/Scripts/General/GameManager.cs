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

    void LoadGame()
    {
        SaveData saveData = SaveManager.currentSaveData;
        if(saveData == null)
        {
            return;
        }
        Checkpoint.lastCheckpointTriggered = saveData.checkPointIndex;
        Player.transform.position = new Vector3(saveData.playerPositionX, saveData.playerPositionY, 0);
        Player.GetComponentInChildren<HealthModule>().SetHealth(saveData.health);
    }
}
