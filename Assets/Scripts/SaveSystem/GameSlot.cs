using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSlot : MonoBehaviour
{
    [SerializeField] int slotNumber;
    [SerializeField] Image screenshotDisplayImage;
    [SerializeField] TextMeshProUGUI depthDisplay;
    [SerializeField] Button slotButton;
    SaveData saveData;
    Texture2D screenshotTexture;
    private void Awake()
    {
        saveData = SaveManager.ReadSaveData(slotNumber);
        if(saveData != null)
        {
            screenshotTexture = saveData.CreateTexture2DFromScreenshotData();
            screenshotDisplayImage.sprite = Sprite.Create(screenshotTexture, new Rect(0, 0, screenshotTexture.width, screenshotTexture.height), Vector2.one * 0.5f);
            depthDisplay.text = $"Depth: {saveData.depth}m";
        }         
    }
    private void OnEnable()
    {
        slotButton.onClick.AddListener(LoadGame);
    }
    private void OnDisable()
    {
        slotButton.onClick.RemoveListener(LoadGame);
    }

    private void OnDestroy()
    {
        Destroy(screenshotTexture);
    }

    void LoadGame()
    {
        if(saveData == null)
        {
            //Load new game
            return;
        }

        //load saved game
    }
}
