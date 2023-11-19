using System.Collections.Generic;
using Random = System.Random;

public class GameEngine
{
    private Snake _snake;
    private GameField _field;
    private EdibleFactory _edibleFactory;
    private Random _random = new Random();
    private ScoreManager _scoreManager;
    private GameConfiguration _config;

    public int FieldWidth => _config.BoardDimensionX;
    public int FieldHeight => _config.BoardDimensionY;

    public GameEngine(GameConfiguration gameConfiguration)
    {
        _config = gameConfiguration;

        _snake = new Snake(_config.BoardDimensionX / 2, _config.BoardDimensionY / 2, Direction.Right, 
            _config.InitialSnakeSpeed, _config.InitialSnakeLength);
        _field = new GameField(_config.BoardDimensionX, _config.BoardDimensionY, _snake);
        _edibleFactory = new EdibleFactory(_config.EdibleEffectsConfig);
        _scoreManager = new ScoreManager();
    }

    public void UpdateGame(float deltaTime)
    {
        if (_snake.ShouldMove(deltaTime))
        {
            if (_field.WillCollideWithSnake(_snake.CurrentDirection))
            {
                RestartGame();
                return;
            }

            Cell nextHead = _field.GetNextHeadPosition(_snake.GetHeadPosition(), _snake.CurrentDirection);
            _snake.Move(nextHead);

            EdibleElement edible = _field.GetElementAtPosition(nextHead);
            if (edible != null)
            {
                edible.ApplyEffect(_snake);
                if (edible is IncreaseLengthElement)
                {
                    _scoreManager.IncreaseScore();
                }
                else if (edible is DecreaseLengthElement)
                {
                    _scoreManager.DecreaseScore();
                }
                _field.ClearElementAtPosition(nextHead);
            }
        }
        if (ShouldGenerateNewEdible())
        {
            GenerateNewEdible();
        }
    }
    
    public void ChangeDirection(Direction newDirection)
    {
        _snake.ChangeDirection(newDirection);
    }

    private bool ShouldGenerateNewEdible()
    {
        int currentEdiblesCount = _field.GetCurrentEdiblesCount(); 
        return currentEdiblesCount < _config.MaxEdibles && _random.NextDouble() < 0.1;
    }

    private void GenerateNewEdible()
    {
        Cell position = _field.GetRandomEmptyPosition();
        EdibleElement newEdible = _edibleFactory.CreateRandomEdible(position);
        _field.AddEdibleElement(newEdible);
    }
    
    public IEnumerable<Cell> GetSnakeBody()
    {
        return _snake.GetBodyPositions();
    }

    public IEnumerable<EdibleElement> GetEdibles()
    {
        return _field.GetAllEdibles();
    }

    public int GetScore()
    {
        return _scoreManager.Score;
    }

    public void RestartGame()
    {
        _scoreManager.ResetScore();
        _snake = new Snake(_config.BoardDimensionX / 2, _config.BoardDimensionY / 2, Direction.Right, 
            _config.InitialSnakeSpeed, _config.InitialSnakeLength);
        _field = new GameField(_config.BoardDimensionX, _config.BoardDimensionY, _snake);
    }
}