using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public static GameGrid Instance;
    public int rows;
    public int cols;
    private GridPoint[,] inactiveGridPoints;
    [SerializeField] private GameObject defaultGridParent;

    public float resolution;
    public GameObject gameGridPlaceholder;

    [SerializeField] private GameObject gridPointPrefab;

    [SerializeField] private bool generateGridSystemically = false;
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
        if(generateGridSystemically)
        {
            SpawnGrid(rows, cols, resolution);
        }
        else
        {

            rows = defaultGridParent.transform.childCount;
            cols = defaultGridParent.transform.GetChild(0).transform.childCount;
            inactiveGridPoints = new GridPoint[cols, rows];

            List<Transform> goRows = new List<Transform>();

            for (int i = 0; i < rows; i++)
            {
                goRows.Add(defaultGridParent.transform.GetChild(i));
            }

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    inactiveGridPoints[col, row] = goRows[row].GetChild(col).GetComponent<GridPoint>();
                }
            }
        }
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
