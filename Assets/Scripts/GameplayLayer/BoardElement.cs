using UnityEngine;

public class BoardElement : MonoBehaviour
{
    public Cell Position { get; private set; }
    public BoardElementType boardElementType;

    public void SetPosition(Cell newPosition, float cellXYSize, Vector2 boardDimensions)
    {
        Position = newPosition;
        float halfBoardWidth = boardDimensions.x / 2 * cellXYSize;
        float halfBoardHeight = boardDimensions.y / 2 * cellXYSize;
        float xPosition = newPosition.X * cellXYSize - halfBoardWidth + cellXYSize / 2;
        float yPosition = newPosition.Y * cellXYSize - halfBoardHeight + cellXYSize / 2;
        transform.position = new Vector3(xPosition, yPosition, 0);
        transform.localScale = new Vector2(cellXYSize, cellXYSize);
    }
}


public enum BoardElementType
{
    Grow = 0,
    Shrink = 1,
    SpeedUp = 2, 
    SlowDown = 3,
    Reverse = 4,
    SnakeSegment = 99
}