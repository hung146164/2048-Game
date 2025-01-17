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

    }
    private void Start()
    {
        TileBoard.Instance.OnBestScoreChanged += ScoreChange;
    }
    public void ScoreChange(int score)
    {
        textMeshProUGUI.text = score.ToString();
    }
}
