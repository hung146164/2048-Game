using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScoreUpdate : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        TileBoard.Instance.OnBestScoreChanged.AddListener(ScoreChange);

    }

    public void ScoreChange(int score)
    {
        textMeshProUGUI.text = score.ToString();
    }
}
