using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{
    
    TextMeshProUGUI textMeshProUGUI;
    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        TileBoard.Instance.OnScoreChanged.AddListener(ScoreChange);
        Debug.Log("add");

    }

    public void ScoreChange(int score)
    {
        textMeshProUGUI.text=score.ToString();
    }


}
