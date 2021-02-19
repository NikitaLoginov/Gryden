using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour, IMoveable
{
    [SerializeField] private float enemySpeed;
    
    private Transform _enemyTransform;
    private ArrayList _pathArray;
    private PlayerController _player;
    private Vector2 _startPos;
    public Node2D StartNode { get; set; }
    public Node2D GoalNode { get; set; }

    private void Awake()
    {
        _pathArray = new ArrayList();
        _enemyTransform = transform;
        _startPos = _enemyTransform.position;
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void GetGoalPosition()
    {
        UpdateStartPosition();
        StartNode = new Node2D(
            GridManager2D.Instance.GetGridCellCenter(GridManager2D.Instance.GetGridIndex(_startPos)));

        GoalNode = new Node2D(
            GridManager2D.Instance.GetGridCellCenter(GridManager2D.Instance.GetGridIndex(_player.transform.position)));

        _pathArray = AStar2D.FindPath(StartNode, GoalNode);
    }

    //TurnHandler calls this to move enemies
    public void MoveToPlayer()
    {
        if (_pathArray == null)
            return;
        if (_pathArray.Count > 1)
        {
            var node2d = (Node2D) _pathArray[1];
            var goal = node2d.position;
            while ((Vector2) _enemyTransform.position != goal)
            {
                _enemyTransform.position =
                    Vector2.MoveTowards(_enemyTransform.position, goal, enemySpeed * Time.deltaTime);
                TurnHandlerNew.instance.faze = TurnHandlerNew.TurnFasez.Waiting;
            }
        }
    }

    void UpdateStartPosition()
    {
        _startPos = _enemyTransform.position;
    }
}
