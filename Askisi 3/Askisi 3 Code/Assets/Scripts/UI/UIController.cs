using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text healthLabel;
    [SerializeField] TMP_Text enemyCountLabel;
    [SerializeField] InventoryPopup popup;
    [SerializeField] TMP_Text levelEnding;

    void OnEnable()
    {
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.AddListener(GameEvent.ENEMY_COUNT_UPDATED, OnEnemyCountUpdated);
    }
    void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.RemoveListener(GameEvent.ENEMY_COUNT_UPDATED, OnEnemyCountUpdated);
    }
    void Start()
    {
        OnHealthUpdated();
        OnEnemyCountUpdated();
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

    private void OnEnemyCountUpdated()
    {
        EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
        if (enemyManager != null)
        {
            int total = enemyManager.TotalEnemyCount;
            int killed = enemyManager.EnemiesKilledCount;
            enemyCountLabel.text = $"Enemies Killed: {killed}/{total}";
        }
        else
        {
            // If there is no EnemyManager, display 0 enemies.
            enemyCountLabel.text = "Enemies Killed: 0/0";
        }
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
        Managers.Inventory.RemoveKeys();
        Managers.Inventory.RemoveEnergyAndOres();
        levelEnding.gameObject.SetActive(true);

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
            Managers.Mission.RestartCurrent();
        }
    }
}