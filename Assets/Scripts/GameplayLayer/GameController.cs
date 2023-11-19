using UnityEngine;
public class GameController : MonoBehaviour
{
    private GameEngine _gameEngine;
    [SerializeField]
    private GridManager gridManager;
    [SerializeField] 
    private GameObject borderPrefab;
    [SerializeField] 
    private GameSettings gameSettings;
    [SerializeField] 
    private UiController uiController;
    private GameConfiguration _gameConfiguration;
    private float _inputCooldown = 0.1f;
    private float _timeSinceLastInput = 0f;
    private int _currentScore = 0;
    private bool _isPaused = false;



    void Start()
    {
        _gameConfiguration = new GameConfiguration(gameSettings.initialSnakeLength, gameSettings.boardDimensions.x,
            gameSettings.boardDimensions.y, gameSettings.cellXYSize, gameSettings.initialSnakeSpeed,
            gameSettings.speedUpMultiplier,
            gameSettings.speedUpDuration, gameSettings.slowDownMultiplier, gameSettings.slowDownDuration, gameSettings.maxEdibles);
        _gameEngine = new GameEngine(_gameConfiguration);
        uiController.Init(RestartGame, TogglePauseGame);
        gridManager.Initialize(_gameEngine, _gameConfiguration.CellSize, 
            new Vector2Int(_gameConfiguration.BoardDimensionX, _gameConfiguration.BoardDimensionY));
        InitializeBorder();
        AdjustCam();
    }

    void Update()
    {
        if (!_isPaused)
        {
            _timeSinceLastInput += Time.deltaTime;

            if (_timeSinceLastInput >= _inputCooldown)
            {
                CheckInput();
            }
            _gameEngine.UpdateGame(Time.deltaTime);
            HandleScore();
            UpdateGameObjects();
        }
    }
    private void CheckInput()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            _gameEngine.ChangeDirection(Direction.Up);
            _timeSinceLastInput = 0;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            _gameEngine.ChangeDirection(Direction.Down);
            _timeSinceLastInput = 0;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            _gameEngine.ChangeDirection(Direction.Left);
            _timeSinceLastInput = 0;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            _gameEngine.ChangeDirection(Direction.Right);
            _timeSinceLastInput = 0;
        }
    }
    private void HandleScore()
    {
        int engineScore = _gameEngine.GetScore();
        if (_currentScore != engineScore)
        {
            uiController.SetScore(engineScore);
            _currentScore = engineScore;
        }
    }

    private void UpdateGameObjects()
    {
        gridManager.UpdateEdibles();
        gridManager.UpdateSnake();
    }

    private void RestartGame()
    {
        _gameEngine.RestartGame();
    }
    
    private void TogglePauseGame()
    {
        _isPaused = !_isPaused;
    }
    
    private void InitializeBorder()
    {
        InstantiateBorderSegment(new Vector2(0, (_gameConfiguration.BoardDimensionY / 2 * _gameConfiguration.CellSize) + _gameConfiguration.CellSize / 2), 
            new Vector2(_gameConfiguration.BoardDimensionX * _gameConfiguration.CellSize + _gameConfiguration.CellSize, _gameConfiguration.CellSize));
        InstantiateBorderSegment(new Vector2(0, -(_gameConfiguration.BoardDimensionY / 2 * _gameConfiguration.CellSize) - _gameConfiguration.CellSize / 2), 
            new Vector2(_gameConfiguration.BoardDimensionX * _gameConfiguration.CellSize + _gameConfiguration.CellSize, _gameConfiguration.CellSize));
        InstantiateBorderSegment(new Vector2((_gameConfiguration.BoardDimensionX / 2 * _gameConfiguration.CellSize) + _gameConfiguration.CellSize / 2, 0), 
            new Vector2(_gameConfiguration.CellSize, _gameConfiguration.BoardDimensionY * _gameConfiguration.CellSize + _gameConfiguration.CellSize));
        InstantiateBorderSegment(new Vector2(-(_gameConfiguration.BoardDimensionX / 2 * _gameConfiguration.CellSize) - _gameConfiguration.CellSize / 2, 0), 
            new Vector2(_gameConfiguration.CellSize, _gameConfiguration.BoardDimensionY * _gameConfiguration.CellSize + _gameConfiguration.CellSize));
    }



    private void InstantiateBorderSegment(Vector2 position, Vector2 scale)
    {
        GameObject borderSegment = Instantiate(borderPrefab, position, Quaternion.identity);
        borderSegment.transform.localScale = scale;
    }
    
    void OnDrawGizmos()
    {
        if (_gameEngine != null)
        {
            Gizmos.color = Color.gray;
            float halfBoardWidth = _gameConfiguration.BoardDimensionX / 2 * _gameConfiguration.CellSize;
            float halfBoardHeight = _gameConfiguration.BoardDimensionY / 2 * _gameConfiguration.CellSize;

            for (int x = 0; x < _gameEngine.FieldWidth; x++)
            {
                for (int y = 0; y < _gameEngine.FieldHeight; y++)
                {
                    float xPosition = x * _gameConfiguration.CellSize - halfBoardWidth + _gameConfiguration.CellSize / 2;
                    float yPosition = y * _gameConfiguration.CellSize - halfBoardHeight + _gameConfiguration.CellSize / 2;
                    Vector3 cellCenter = new Vector3(xPosition, yPosition, 0);
                    Gizmos.DrawWireCube(cellCenter, new Vector3(_gameConfiguration.CellSize, _gameConfiguration.CellSize, 1));
                }
            }
        }
    }

    private void AdjustCam()
    {
        Camera mainCamera = Camera.main;
        float padding = 2f; 
        float verticalSize = (_gameConfiguration.BoardDimensionY * _gameConfiguration.CellSize + padding * 2) / 2;
        float horizontalSize = (_gameConfiguration.BoardDimensionX * _gameConfiguration.CellSize + padding * 2) / 2 / mainCamera.aspect;

        mainCamera.orthographicSize = Mathf.Max(verticalSize, horizontalSize);
        mainCamera.transform.position = new Vector3(0, 0, -10); 
    }

}