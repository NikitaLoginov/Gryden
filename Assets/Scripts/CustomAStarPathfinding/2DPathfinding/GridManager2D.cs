using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager2D : MonoBehaviour
{
    private static GridManager2D s_Instance = null;
    public static GridManager2D Instance
    {
        get
        {
            if (s_Instance == null)
                s_Instance = FindObjectOfType(typeof(GridManager2D)) as GridManager2D;

            if(s_Instance == null)
                Debug.Log("Could not locate GridManager " + " object. " +
                    "\n You have to have exactly " + "one GridManager in the scene");

            return s_Instance;
        }
    }

    public int numOfRows;
    public int numOfColumns;
    public float gridCellSize;
    public bool showGrid = true;
    public bool showObstacleBlocks = true;

    GameObject[] _obstacleList;
    Vector2 _origin = new Vector2();
    public Vector2 Origin
    {
        get { return _origin; }
    }

    public Node2D[,] nodes { get; set; }

    private void Awake()
    {
        _obstacleList = GameObject.FindGameObjectsWithTag("Obstacle");
        CalculateObstacles();
    }

    void CalculateObstacles()
    {
        nodes = new Node2D[numOfColumns, numOfRows];
        int index = 0;
        for (int i = 0; i < numOfColumns; i++)
        {
            for (int j = 0; j < numOfRows; j++)
            {
                Vector2 cellPos = GetGridCellCenter(index);
                Node2D node2d = new Node2D(cellPos);
                nodes[i, j] = node2d;
                index++;
            }
        }

        if (_obstacleList != null && _obstacleList.Length > 0)
        {
            //for each obstacle found in the map, record it to obstacle list
            foreach (GameObject data in _obstacleList)
            {
                int indexCell = GetGridIndex(data.transform.position);
                int col = GetColumn(indexCell);
                int row = GetRow(indexCell);

                nodes[row, col].MarkAsObstacle();

                Debug.Log("2D nodes array lenght " + nodes.Length);
            }
        }
    }

    public Vector2 GetGridCellCenter(int index)
    {
        Vector2 cellPosition = GetGridCellPosition(index);
        cellPosition.x += (gridCellSize / 2.0f);
        cellPosition.y += (gridCellSize / 2.0f);
        return cellPosition;
    }

    public Vector2 GetGridCellPosition(int index)
    {
        int col = GetColumn(index);
        int row = GetRow(index);
        float xPosInGrid = col * gridCellSize;
        float yPosInGrid = row * gridCellSize;
        return Origin + new Vector2(xPosInGrid, yPosInGrid);
    }
    //GetGridIndex returns the grid cell index in the grid from a given position
    public int GetGridIndex(Vector2 pos)
    {
        if (!IsInBounds(pos))
            return -1;

        pos -= Origin;
        int col = (int)(pos.x / gridCellSize);
        int row = (int)(pos.y / gridCellSize);
        return (row * numOfColumns + col);
    }

    public bool IsInBounds(Vector2 pos)
    {
        float width = numOfColumns * gridCellSize;
        float height = numOfRows * gridCellSize;
        return (pos.x >= Origin.x && pos.x <= Origin.x + width && 
                pos.x <= Origin.y + height && pos.y >= Origin.y);
    }
    //GetRow and GetColumn return row and column data of the grid cell from a given index
    public int GetRow(int index)
    {
        int row = index / numOfColumns;
        return row;
    }
    public int GetColumn(int index)
    {
        int col = index % numOfColumns;
        return col;
    }
    // GetNeighbours used by AStar to retrieve a neigbouring nodes of a particular node
    public void GetNeighbours(Node2D node2d, ArrayList neighbors)
    {
        Vector2 neighborPos = node2d.position;
        int neighborIndex = GetGridIndex(neighborPos);

        int row = GetRow(neighborIndex);
        int column = GetColumn(neighborIndex);

        // Bottom
        int leftNodeRow = row - 1;
        int leftNodeColumn = column;
        AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors);

        //Top
        leftNodeRow = row + 1;
        leftNodeColumn = column;
        AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors);

        //Right
        leftNodeRow = row;
        leftNodeColumn = column + 1;
        AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors);

        //Left
        leftNodeRow = row;
        leftNodeColumn = column - 1;
        AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors);
    }

    void AssignNeighbour(int row, int column, ArrayList neighbors)
    {
        if (row != -1 && column != -1 && row < numOfRows && column < numOfColumns)
        {
            //Debug.Log("Rows " + row + " Columns " + column);
            Node2D node2dToAdd = nodes[row, column];
            if (!node2dToAdd.bObstacle)
            {
                neighbors.Add(node2dToAdd);
            }
        }

    }

    private void OnDrawGizmos()
    {
        if (showGrid)
        {
            DebugDrawGrid(transform.position, numOfRows, numOfColumns, gridCellSize, Color.blue);
        }

        Gizmos.DrawSphere(transform.position, 0.5f);
        if (showObstacleBlocks)
        {
            // here was Vector3
            Vector2 cellSize = new Vector2(gridCellSize, gridCellSize);

            if (_obstacleList != null && _obstacleList.Length > 0)
            {
                foreach (GameObject data in _obstacleList)
                {
                    Gizmos.DrawCube(GetGridCellCenter(GetGridIndex(data.transform.position)), cellSize);
                }
            }
        }
    }

    public void DebugDrawGrid(Vector2 origin, int numRows, int numCols, float cellSize, Color color)
    {
        float width = (numCols * cellSize);
        float height = (numRows * cellSize);

        // Draw the horizontal grid lines
        for (int i = 0; i < numRows + 1; i++)
        {
            Vector2 startPos = origin + i * cellSize * new Vector2(0.0f, 1.0f);
            Vector2 endPos = startPos + width * new Vector2(1.0f, 0.0f);
            Debug.DrawLine(startPos, endPos, color);
        }

        //Draw vertical grid lines
        for (int i = 0; i < numCols + 1; i++)
        {
            Vector2 startPos = origin + i * cellSize * new Vector2(1.0f, 0.0f);
            Vector2 endPos = startPos + height * new Vector2(0.0f, 1.0f);
            Debug.DrawLine(startPos, endPos, color);
        }
    }
}
