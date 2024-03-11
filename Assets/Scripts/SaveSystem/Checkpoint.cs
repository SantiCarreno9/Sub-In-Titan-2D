using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] int checkPointIndex = -1;
    bool isEnabled = true;
    public static int lastCheckpointTriggered = -1;
    SaveManager saveManager;
    int checkPointId;
    [SerializeField] CheckpointEffect effect;
    private void Start()
    {
        if(checkPointIndex <= lastCheckpointTriggered)
        {
            isEnabled = false;
            return;
        }
        saveManager = FindObjectOfType<SaveManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isEnabled)
        {
            return;
        }
        isEnabled = false;
        lastCheckpointTriggered = checkPointId;
        saveManager.SaveGameData();
        effect.PlaySFX();
        Debug.Log("Saved Data");
    }
}
