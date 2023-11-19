public class GameConfiguration
{
    public int InitialSnakeLength { get; private set; }
    public int BoardDimensionX { get; private set; }
    public int BoardDimensionY { get; private set; }
    public float CellSize { get; private set; }
    public float InitialSnakeSpeed { get; private set; }
    public int MaxEdibles { get; private set; }
    public EdibleEffectsConfiguration EdibleEffectsConfig { get; private set; }

    public GameConfiguration(int initialSnakeLength, int boardDimensionX, int boardDimensionY, float cellSize,
        float initialSnakeSpeed, float speedUpMultiplier, float speedUpDuration,
        float slowDownMultiplier, float slowDownDuration, int maxEdibles)
    {
        
        InitialSnakeLength = initialSnakeLength;
        BoardDimensionX = boardDimensionX;
        BoardDimensionY = boardDimensionY;
        CellSize = cellSize;
        InitialSnakeSpeed = initialSnakeSpeed;
        MaxEdibles = maxEdibles;
        EdibleEffectsConfig =
            new EdibleEffectsConfiguration(speedUpMultiplier, speedUpDuration, slowDownMultiplier, slowDownDuration);
    }
}