using Submarine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] Camera screenshotCamera;
    public static int currentGameSlot = 0;
    static string directoryPath = $"{Application.dataPath}/Saves";
    public void SaveGameData()
    {
        screenshotCamera.Render();
        var rt = screenshotCamera.targetTexture;
        // Remember currently active render texture
        RenderTexture currentActiveRT = RenderTexture.active;

        // Set the supplied RenderTexture as the active one
        RenderTexture.active = rt;

        // Create a new Texture2D and read the RenderTexture image into it
        Texture2D tex = new Texture2D(rt.width, rt.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        tex.Apply();

        // Restore previously active render texture
        RenderTexture.active = currentActiveRT;
        
        var saveData = new SaveData()
        {
            screenshotData = tex.EncodeToPNG(),
            playerPositionX = GameManager.Instance.Player.transform.position.x,
            playerPositionY = GameManager.Instance.Player.transform.position.y,
            health = GameManager.Instance.Player.GetComponentInChildren<HealthModule>().HealthPoints,
            depth = 0, //Will get from the script Juan creates
            screenshotHeight = tex.height,
            screenshotWidth = tex.width,
        };

        Destroy(tex);
        EnsureDirectoryExists();
        using (var filestream = new FileStream($"{directoryPath}/Save{currentGameSlot}", FileMode.Create))
        {
            new BinaryFormatter().Serialize(filestream, saveData);
        }
    }

    static void EnsureDirectoryExists()
    {
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    public static SaveData ReadSaveData(int slotNumber)
    {
        EnsureDirectoryExists();
        string path = $"{directoryPath}/Save{slotNumber}";
        if (!File.Exists(path))
        {
            return null;
        }
        using (var filestream = new FileStream(path, FileMode.Open))
        {
            SaveData saveData = (SaveData)new BinaryFormatter().Deserialize(filestream);
            return saveData;
        }
    }

}
[System.Serializable]
public class SaveData
{
    public byte[] screenshotData;
    public float playerPositionX;
    public float playerPositionY;
    public float health;
    public float depth;
    public int screenshotWidth;
    public int screenshotHeight;
    public Texture2D CreateTexture2DFromScreenshotData()
    {
        Texture2D tex = new Texture2D(screenshotWidth, screenshotHeight);
        tex.LoadImage(screenshotData);
        return tex;
    }
}
