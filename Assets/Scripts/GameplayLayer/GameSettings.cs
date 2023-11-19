using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "SnakeGame/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    public int initialSnakeLength = 3;
    public Vector2Int boardDimensions = new Vector2Int(10, 10);
    public float cellXYSize = 1f;
    public float initialSnakeSpeed = 5f;
    public float speedUpMultiplier = 1.5f;
    public float speedUpDuration = 5f;
    public float slowDownMultiplier = 0.5f;
    public float slowDownDuration = 5f;
    public int maxEdibles = 10;
}