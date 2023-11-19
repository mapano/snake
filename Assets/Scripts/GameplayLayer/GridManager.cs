using System.Collections.Generic;
using System.Linq; 
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private GameEngine _gameEngine;
    private List<BoardElement> _snakeSegments = new List<BoardElement>(); 
    private List<BoardElement> _edibles = new List<BoardElement>();
    private float _cellXYSize;
    private Vector2 _boardDimensions;
    [SerializeField]
    private ObjectPool objectPool;

    public void Initialize(GameEngine engine, float cellXYSize,  Vector2 boardDimensions)
    {
        _gameEngine = engine;
        _cellXYSize = cellXYSize;
        _boardDimensions = boardDimensions;
    }

    public void UpdateSnake()
    {
        var currentBodyPositions = _gameEngine.GetSnakeBody().ToList();
        
        while (_snakeSegments.Count < currentBodyPositions.Count)
        {
            var position = currentBodyPositions[_snakeSegments.Count];
            var segment = objectPool.GetObject(BoardElementType.SnakeSegment); 
            segment.GetComponent<BoardElement>().SetPosition(position,_cellXYSize, _boardDimensions);
            _snakeSegments.Add(segment.GetComponent<BoardElement>());
        }
        
        while (_snakeSegments.Count > currentBodyPositions.Count)
        {
            var lastSegment = _snakeSegments.Last();
            objectPool.ReturnObject(BoardElementType.SnakeSegment, lastSegment.gameObject); 
            _snakeSegments.RemoveAt(_snakeSegments.Count - 1);
        }
        
        for (int i = 0; i < currentBodyPositions.Count; i++)
        {
            _snakeSegments[i].SetPosition(currentBodyPositions[i], _cellXYSize, _boardDimensions);
        }
    }

    public void UpdateEdibles()
    {
        var newEdibles = _gameEngine.GetEdibles().ToList();
        
        for (int i = _edibles.Count - 1; i >= 0; i--)
        {
            var edible = _edibles[i];
            if (!newEdibles.Any(e => e.Position.X == edible.Position.X && e.Position.Y == edible.Position.Y))
            {
                objectPool.ReturnObject(edible.boardElementType, edible.gameObject); 
                _edibles.RemoveAt(i);
            }
        }
        
        foreach (var edibleElement in newEdibles)
        {
            var existingEdible = _edibles.FirstOrDefault(e => e.Position.X == edibleElement.Position.X && e.Position.Y == edibleElement.Position.Y);
            if (existingEdible == null)
            {
                BoardElementType edibleType = GetEdibleType(edibleElement);
                var newEdible = objectPool.GetObject(edibleType);
                newEdible.GetComponent<BoardElement>().SetPosition(new Cell(edibleElement.Position.X, edibleElement.Position.Y), _cellXYSize, _boardDimensions);
                _edibles.Add(newEdible.GetComponent<BoardElement>());
            }
        }
    }
    
    private BoardElementType GetEdibleType(EdibleElement edibleElement)
    {
        return (BoardElementType)edibleElement.EdibleTypeIndex; 
    }
}
