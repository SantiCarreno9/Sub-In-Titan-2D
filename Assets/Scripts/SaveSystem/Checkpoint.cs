using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    static int lasGeneratedCheckPointId = -1;
    static int lastSavedCheckpointId = -1;
    SaveManager saveManager;
    int checkPointId;
    private void Awake()
    {
        checkPointId = ++lasGeneratedCheckPointId;
        saveManager = FindObjectOfType<SaveManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(lastSavedCheckpointId == checkPointId)
        {
            return;
        }
        lastSavedCheckpointId = checkPointId;
        saveManager.SaveGameData();
        Debug.Log("Saved Data");
    }
}
