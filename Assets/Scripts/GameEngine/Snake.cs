using System.Collections.Generic;

public class Snake
{
    private readonly List<Cell> _body;
    private readonly int _initialLength;
    private float _speed; 
    private float _timeSinceLastMove;
    private float _originalSpeed;
    private float _speedModificationDuration;
    private float _speedModificationElapsed;
    public Direction CurrentDirection { get; private set; }

    public Snake(int startX, int startY, Direction startDirection, float initialSpeed, int initialLength)
    {
        _body = new List<Cell>();
        _initialLength = initialLength;
        for (int i = 0; i < _initialLength; i++)
        {
            _body.Add(new Cell(startX, startY - i));
        }
        CurrentDirection = startDirection;
        _speed = initialSpeed;
        _timeSinceLastMove = 0f;
    }

    public void ChangeDirection(Direction newDirection)
    {
        if (IsValidDirectionChange(newDirection))
        {
            CurrentDirection = newDirection;
        }
    }

    public void Move(Cell newHeadPosition)
    {
        _body.Insert(0, newHeadPosition);
        _body.RemoveAt(_body.Count - 1);
    }

    public Cell GetHeadPosition()
    {
        return _body[0];
    }

    private bool IsValidDirectionChange(Direction newDirection)
    {
        return newDirection switch
        {
            Direction.Up => CurrentDirection != Direction.Down,
            Direction.Down => CurrentDirection != Direction.Up,
            Direction.Left => CurrentDirection != Direction.Right,
            Direction.Right => CurrentDirection != Direction.Left,
            _ => false,
        };
    }

    public void Grow()
    {
        Cell lastSegment = _body[_body.Count - 1];
        _body.Add(lastSegment);
    }

    public void Shrink()
    {
        if (_body.Count > 1)
        {
            _body.RemoveAt(_body.Count - 1);
        }
    }

    public void Reverse()
    {
        _body.Reverse();
        switch (CurrentDirection)
        {
            case Direction.Up: CurrentDirection = Direction.Down; break;
            case Direction.Down: CurrentDirection = Direction.Up; break;
            case Direction.Left: CurrentDirection = Direction.Right; break;
            case Direction.Right: CurrentDirection = Direction.Left; break;
        }
    }

    public IEnumerable<Cell> GetBodyPositions()
    {
        return _body;
    }
    
    public void ModifySpeed(float speedMultiplier, float duration)
    {
        if (_speedModificationDuration <= 0)
        {
            _originalSpeed = _speed;
        }

        _speed *= speedMultiplier;
        _speedModificationDuration = duration;
        _speedModificationElapsed = 0;
    }

    public bool ShouldMove(float deltaTime)
    {
        if (_speedModificationDuration > 0)
        {
            _speedModificationElapsed += deltaTime;
            if (_speedModificationElapsed >= _speedModificationDuration)
            {
                _speed = _originalSpeed;
                _speedModificationDuration = 0;
            }
        }

        _timeSinceLastMove += deltaTime;
        if (_timeSinceLastMove >= 1f / _speed)
        {
            _timeSinceLastMove -= 1f / _speed;
            return true;
        }
        return false;
    }
}
