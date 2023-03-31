using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(UpdateTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator UpdateTime()
    {
        while(!GameStateManager.Instance.isGameOver)
        {
            timerText.text = $"{GameStateManager.Instance.timer / 60:00} : {GameStateManager.Instance.timer % 60:00}";
            yield return new WaitForSeconds(1f);
        }        
    }
}
