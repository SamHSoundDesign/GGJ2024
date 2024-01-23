using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public static GameGrid Instance;
    public int rows;
    public int cols;
    private GridPoint[,] inactiveGridPoints;

    public float resolution;
    public GameObject gameGridPlaceholder;

    [SerializeField] private GameObject gridPointPrefab;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        SpawnGrid(rows, cols, resolution);
    }
    public void SpawnGrid(int cols, int rows, float resolution)
    {
        this.rows = rows;
        this.cols = cols;
        this.resolution = resolution;

        DestroyGrid();

        inactiveGridPoints = new GridPoint[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                inactiveGridPoints[col, row] = SpawnGridPoint(col,row, resolution);
            }
        }
    }

    public void DestroyGrid()
    {

    }

   public GridPoint GetRandomGridPoint()
    {
        int attempt = 0;
        do
        {
            int randomRow = Random.Range(0, rows);
            int randomCol = Random.Range(0, cols);

            GridPoint gp = inactiveGridPoints[randomCol, randomRow];

            if (gp.isActive == false && gp != null && gp.isCoolingDown == false)
            {
                return gp;
            }

            attempt++;
        } while (attempt < rows * cols);

        return null;
    }

    public GridPoint SpawnGridPoint(int col, int row, float resolution)
    {
        GameObject go = Instantiate(gridPointPrefab, GetGridPointWorldPos(col, row, Vector3.zero, resolution), Quaternion.identity, transform);
        return go.GetComponent<GridPoint>();
    }

    private Vector3 GetGridPointWorldPos(int row, int col, Vector3 origin, float resolution)
    {
        return origin + new Vector3(col * resolution, row * resolution);
    }


}
