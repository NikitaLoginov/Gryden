using System;
using System.Collections;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static GridManager s_Instance = null;

    public static GridManager Instance
    {
        get
        {
            if (s_Instance == null)
                s_Instance = FindObjectOfType(typeof(GridManager)) as GridManager;

            if (s_Instance == null)
                Debug.Log("Could not locate GridManager " + " object. " +
                    "\n You have to have exactly " + "one GridManager in the scene" );

        return s_Instance;
        }
    }

    public int numOfRows;
    public int numOfColumns;
    public float gridCellSize;
    public bool showGrid = true;
    public bool showObstacleBlocks = true;

    private GameObject[] _obstacleList;
    private Vector3 _origin = new Vector3();

    public Vector3 Origin
    {
        get { return _origin; }
    }
    public Node[,] nodes { get; set; }

    private void Awake()
    {
        _obstacleList = GameObject.FindGameObjectsWithTag("Obstacle");
        CalculateObstacles();
    }

    //Find all the obstacles on the map
    void CalculateObstacles()
    {
        nodes = new Node[numOfColumns, numOfRows];
        int index = 0;

        for (int i = 0; i < numOfColumns; i++)
        {
            for (int j = 0; j < numOfRows; j++)
            {
                Vector3 cellPos = GetGridCellCenter(index);
                Node node = new Node(cellPos);
                nodes[i, j] = node;
                index++;
            }
        }

        if (_obstacleList != null && _obstacleList.Length > 0)
        {
            //For each obstacle found in the map, record it in our obstacle list
            foreach (GameObject data in _obstacleList)
            {
                int indexCell = GetGridIndex(data.transform.position);
                int col = GetColumn(indexCell);
                int row = GetRow(indexCell);

                //Debug.Log("ForEach Rows " + row + " and Columns " + col);
                nodes[row, col].MarkAsObstacle();

                Debug.Log("Nodes array lenght " + nodes.Length);
            }
        }
    }

    // GetGridCellCenter returns position of the grid cell in world coordinates from the cell index
    public Vector3 GetGridCellCenter(int index)
    {
        Vector3 cellPosition = GetGridCellPosition(index);
        cellPosition.x += (gridCellSize / 2.0f);
        cellPosition.z += (gridCellSize / 2.0f);
        return cellPosition;
    }

    public Vector3 GetGridCellPosition(int index)
    {
        int col = GetColumn(index);
        int row = GetRow(index);
        float xPosInGrid = col * gridCellSize;
        float zPosInGrid = row * gridCellSize;
        return Origin + new Vector3(xPosInGrid, 0 , zPosInGrid);
    }

    //GetGridIndex returns the grid cell index in the grid from a given position
    public int GetGridIndex(Vector3 pos)
    {
        if (!IsInBounds(pos))
            return -1;

        pos -= Origin;
        int col = (int)(pos.x / gridCellSize);
        int row = (int)(pos.z / gridCellSize);
        return (row * numOfColumns + col);
    }

    public bool IsInBounds(Vector3 pos)
    {
        float width = numOfColumns * gridCellSize;
        float height = numOfRows * gridCellSize;
        return (pos.x >= Origin.x && pos.x <= Origin.x + width && 
            pos.x <= Origin.z + height && pos.z >= Origin.z);
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
    public void GetNeighbours(Node node, ArrayList neighbors)
    {
        Vector3 neighborPos = node.position;
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
            Node nodeToAdd = nodes[row, column];
            if (!nodeToAdd.bObstacle)
            {
                neighbors.Add(nodeToAdd);
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
            Vector3 cellSize = new Vector3(gridCellSize, 1.0f, gridCellSize);

            if (_obstacleList != null && _obstacleList.Length > 0)
            {
                foreach (GameObject data in _obstacleList)
                {
                    Gizmos.DrawCube(GetGridCellCenter(GetGridIndex(data.transform.position)), cellSize);
                }
            }
        }
    }

    public void DebugDrawGrid(Vector3 origin, int numRows, int numCols, float cellSize, Color color)
    {
        float width = (numCols * cellSize);
        float height = (numRows * cellSize);

        // Draw the horizontal grid lines
        for (int i = 0; i < numRows + 1; i++)
        {
            Vector3 startPos = origin + i * cellSize * new Vector3(0.0f, 0.0f, 1.0f);
            Vector3 endPos = startPos + width * new Vector3(1.0f, 0.0f, 0.0f);
            Debug.DrawLine(startPos, endPos, color);
        }

        //Draw vertical grid lines
        for (int i = 0; i < numCols + 1; i++)
        {
            Vector3 startPos = origin + i * cellSize * new Vector3(1.0f, 0.0f, 0.0f);
            Vector3 endPos = startPos + height * new Vector3(0.0f, 0.0f, 1.0f);
            Debug.DrawLine(startPos, endPos, color);
        }
    }
}
