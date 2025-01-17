
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    public static TileBoard Instance;

    private const int winNumber = 2048;
    private const int tileSize = 125;
    private bool isPlay=true;
    [Header("Row Prefab")]
    public Transform rowPrefab;
    [Header("Tile Prefab")]
    public Tile tilePrefab;
    [Header("Cell Tile Configure")]
    public TileCell cellPrefab;
    [Header("Grid Configure")]
    public Transform gridTransform;
    public int rows;
    public int columns;
    [Header("Data Tile State")]
    public TileState[] tileData;
    [Header("UI")]
    public GameObject gameOverUI;
    public GameObject gameWinUI;
    private int currentScore=0;
    private int bestScore=0;

    private Tile[,] tiles;
    private TileCell[,] tileCells;
    private RectTransform rectTransform;
    private Dictionary<int, TileState> tileStates;

    public event Action<int> OnScoreChanged;
    public event Action<int> OnBestScoreChanged;

    public int CurrentScore
    {
        get => currentScore;
        set
        {
            currentScore = value;
            OnScoreChanged?.Invoke(currentScore); 
        }
    }
    public int BestScore
    {
        get => bestScore;
        set
        {
            bestScore = value;
            OnBestScoreChanged?.Invoke(bestScore);
        }
    }
    private void Awake()
    {
        Instance = this;

        rectTransform = GetComponent<RectTransform>();
        gridTransform = transform.Find("Grid");
        tiles = new Tile[rows, columns];
        tileCells = new TileCell[rows, columns];
        tileStates = new Dictionary<int, TileState>();
        ConvertDataToDictionary();
        CreateGrid();
    }
    public void ConvertDataToDictionary()
    {
        int start = 2;
        for(int i=0; i< tileData.Length; i++)
        {
            tileStates[start] = tileData[i];
            start *= 2; 
        }
    }
    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if(isPlay)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveTile(0, -1, 0, rows, 0, columns, 1, 1);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                MoveTile(-1, 0, 0, rows, 0, columns, 1, 1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                MoveTile(0, 1, 0, rows, columns - 1, -1, 1, -1);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                MoveTile(1, 0, rows - 1, -1, 0, columns, -1, 1);
            }
        }
        

    }
    public void NewGame()
    {
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
        isPlay = true;
        CurrentScore = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++) {
                if (tiles[i, j] != null)
                {
                    Destroy(tiles[i, j].gameObject);
                }
                tiles[i, j] = null;
                tileCells[i, j].occupied = false;
            }
        }
        CreateTile();
        CreateTile();
    }
    public void ContinueGame()
    {
        gameWinUI.SetActive(false);
        isPlay = true;

    }
    public void BoardReset()
    {

    }
    public void CreateGrid()
    {
        SetSizeGrid(this.rows* tileSize, this.columns* tileSize);
        for (int i = 0; i < rows; i++)
        {
            Transform row=Instantiate(rowPrefab, gridTransform).transform;
            for (int j = 0; j < columns; j++)
            {
                tileCells[i,j]= Instantiate(cellPrefab, row);
                tileCells[i,j].coordinates=new Vector2Int(i,j);
            }
        }
    }
    public void SetSizeGrid(int width,int height)
    {
        rectTransform.sizeDelta= new Vector2(width,height);
    }

    public void CreateTile()
    {
        Vector2Int spawnPos = GetRandomPos();
        if(spawnPos==-Vector2.one)
        {
            return;
        }
        int numState = UnityEngine.Random.Range(0, 10) < 9 ? 2 : 4;
        tiles[spawnPos.x, spawnPos.y] = Instantiate(tilePrefab, tileCells[spawnPos.x, spawnPos.y].transform);
        tileCells[spawnPos.x,spawnPos.y].occupied= true;
        tiles[spawnPos.x, spawnPos.y].SetState(tileStates[numState], numState); 
    }
    
    public Vector2Int GetRandomPos()
    {
        List<TileCell> tileValid = new List<TileCell>();
        foreach (var item in tileCells)
        {
            if(!item.occupied)
            {
                tileValid.Add(item);
            }
        }
        int randomIndex=UnityEngine.Random.Range(0, tileValid.Count);
        return tileValid.Count==0? -Vector2Int.one:tileValid[randomIndex].coordinates;
    }
    public void MoveTile(int incrementX, int incrementY, int startRow, int endRow, int startColumn, int endColumn, int rowStep, int columnStep)
    {
        for (int i = startRow; i != endRow; i += rowStep)
        {
            for (int j = startColumn; j != endColumn; j += columnStep)
            {
                if (tileCells[i, j].occupied)
                {
                    Tile newTile = tiles[i, j];
                    tiles[i, j] = null;
                    tileCells[i, j].occupied = false;

                    int x = i + incrementX, y = j + incrementY;
                    while (x >= 0 && y >= 0 && x < rows && y < columns && !tileCells[x, y].occupied)
                    {
                        x += incrementX;
                        y += incrementY;
                    }
                    if (x >= 0 && y >= 0 && x < rows && y < columns && tiles[x, y]?.number == newTile.number)
                    {
                        int newNumber = tiles[x, y].number * 2;
                        CurrentScore += newNumber;
                        tiles[x, y].SetState(tileStates[newNumber], newNumber);
                        Destroy(newTile.gameObject);
                    }
                    else
                    {
                        tiles[x - incrementX, y - incrementY] = newTile;
                        newTile.transform.SetParent(tileCells[x - incrementX, y - incrementY].transform);
                        newTile.transform.localPosition = Vector3.zero;
                        tileCells[x - incrementX, y - incrementY].occupied = true;
                    }
                }
            }

        }
        CreateTile();
        SetBestScore();
        if (CheckWin())
        {
            Debug.Log("You win!");
            Win();
            return;
        }

        // Kiểm tra thua
        if (CheckGameOver())
        {
            Debug.Log("Game Over!");
            GameOver();
        }

    }
    public void SetBestScore()
    {
        if(CurrentScore >BestScore)
        {
            BestScore = CurrentScore;
        }
    }
    //public void SetCurrentScoreText()
    //{
    //    currentScoreText.text= $"Your Score: {currentScore}";
    //}
    //public void SetBestScoreText()
    //{
    //    bestScoreText.text = $"Best Score: {bestScore}";
    //}
    public bool CheckWin()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (tiles[i, j] != null && tiles[i, j].number >= winNumber)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool CheckGameOver()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (tiles[i, j] == null)
                {
                    return false; 
                }

                if (j < columns - 1 && tiles[i, j].number == tiles[i, j + 1]?.number)
                {
                    return false; 
                }


                if (i < rows - 1 && tiles[i, j].number == tiles[i + 1, j]?.number)
                {
                    return false; 
                }
            }
        }
        return true;
    }
    public void GameOver()
    {
        gameOverUI.SetActive(true);
        isPlay = false;
    }
    public void Win()
    {
        gameWinUI.SetActive(true);
        isPlay = false;
    }


}
