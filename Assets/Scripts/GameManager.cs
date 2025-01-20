using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject startgametObject;
    [SerializeField] private GameObject gameplayObject;
    [SerializeField] private GameObject errorContinueText;
    public int currentRow = 4;
    public int currentColumn = 4;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void Start()
    {
        gameplayObject.SetActive(false);
    }
    public void ChangeNumberOfRow(int row)
    {
        this.currentRow = row;
    }
    public void ChangeNumberOfColumn(int column)
    {
        this.currentColumn = column;
    }
    public void Continue()
    {
        currentRow = PlayerPrefs.GetInt("Row", -1);
        currentColumn = PlayerPrefs.GetInt("Column", -1);
        PlayGame();
        if (currentColumn == -1 || currentColumn == -1)
        {
            errorContinueText.SetActive(true);
            return;
        }
        TileBoard.Instance.Continue();
    }
    public void NewGame()
    {
        PlayGame();

        TileBoard.Instance.NewGame();
        
    }
    public void PlayGame()
    {
        TileBoard.Instance.SetUpBoardSize(currentRow, currentColumn);
        gameplayObject.SetActive(true);
        startgametObject.SetActive(false);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit(); 
#endif
        Debug.Log("Game exited!");
    }

}
