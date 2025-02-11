using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text healthLabel;
    [SerializeField] InventoryPopup popup;
    [SerializeField] TMP_Text levelEnding;
    
    void OnEnable()
    {
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
    }
    void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
    }
    void Start()
    {
        OnHealthUpdated();

        levelEnding.gameObject.SetActive(false);
        popup.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isShowing = popup.gameObject.activeSelf;
            popup.gameObject.SetActive(!isShowing);
            popup.Refresh();
        }
    }
    private void OnHealthUpdated()
    {
        string message = $"Health: {Managers.Player.health}/{Managers.Player.maxHealth}";
        healthLabel.text = message;
    }

    private void OnLevelFailed()
    {
        StartCoroutine(FailLevel());
    }
    private IEnumerator FailLevel()
    {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "Level Failed";
        yield return new WaitForSeconds(2);
        Managers.Inventory.ClearInventory();
        Managers.Player.Respawn();
        Managers.Mission.RestartCurrent();
    }

    private void OnLevelComplete()
    {
        StartCoroutine(CompleteLevel());
    }
    private IEnumerator CompleteLevel()
    {
        levelEnding.gameObject.SetActive(true);

        // Check if the current level is the final one.
        if (Managers.Mission.curLevel < Managers.Mission.maxLevel)
        {
            levelEnding.text = "Level Complete!";
            yield return new WaitForSeconds(2);
            Managers.Mission.GoToNext();
        }
        else
        {
            levelEnding.text = "You Won!";
            yield return new WaitForSeconds(2);
            // Restart the game or load a designated scene – adjust as needed.
            Managers.Mission.RestartCurrent();
        }
    }
}