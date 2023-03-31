using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyKilled : MonoBehaviour
{
    public TextMeshProUGUI amountEnemyKilled;
    // Update is called once per frame
    void Update()
    {
        UpdateAmountEnemyKilledText();
    }

    private void UpdateAmountEnemyKilledText()
    {
        amountEnemyKilled.text = $" {GameStateManager.Instance.enemyKilled :000000}";
    }
}
