using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float speed;

    private Transform _playerTransform;

    private ArrayList _pathArray;
    public Node2D StartNode { get; set; }
    public Node2D GoalNode { get; set; }


    private void Awake()
    {
        _pathArray = new ArrayList();
        _playerTransform = transform;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && TurnHandlerNew.instance.faze == TurnHandlerNew.TurnFasez.Normal)
        {
            GetGoalPosition(_playerTransform.position);
            MovePlayer();
        }
    }

    private Vector2 GetMouseInput() // nullable Vector2 to return null when needed
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void GetGoalPosition(Vector2 startPosition)
    {
        StartNode = new Node2D(
            GridManager2D.Instance.GetGridCellCenter(GridManager2D.Instance.GetGridIndex(startPosition)));

        var mousePos = GetMouseInput();
        GoalNode = new Node2D(
            GridManager2D.Instance.GetGridCellCenter(GridManager2D.Instance.GetGridIndex(mousePos)));

        if (GoalNode.position.y < 1f)
            GoalNode = StartNode;
        _pathArray = AStar2D.FindPath(StartNode, GoalNode);
    }



    private void MovePlayer()
    {
        if (_pathArray == null)
            return;

        if (_pathArray.Count > 1)
        {
            var node2d = (Node2D) _pathArray[1];
            var goal = node2d.position;
            while ((Vector2) _playerTransform.position != goal)
            {
                _playerTransform.position = Vector2.MoveTowards(_playerTransform.position,goal,speed * Time.deltaTime);
                
                TurnHandlerNew.instance.faze = TurnHandlerNew.TurnFasez.Waiting;
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        if (_pathArray == null) // if we didn't find path - return
            return;

        if (_pathArray.Count > 0) // if path is longer that 0
        {
            int index = 1;      // indexator
            foreach (Node2D node2d in _pathArray)
            {
                if (index < _pathArray.Count) // if path has more than 1 node
                {
                    Node2D nextNode = (Node2D)_pathArray[index]; // next node
                    Debug.DrawLine(node2d.position, nextNode.position, Color.red); // draw line from current node to next node
                    index++;
                }
            }
        }
    }
}
