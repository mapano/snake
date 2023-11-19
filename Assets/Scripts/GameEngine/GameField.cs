using System;
using System.Collections.Generic;
using System.Linq;

public class GameField
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    private Snake _snake;
    private EdibleElement[,] _fieldElements;

    public GameField(int width, int height, Snake snake)
    {
        Width = width;
        Height = height;
        this._snake = snake;
        _fieldElements = new EdibleElement[width, height];
    }

    public bool WillCollideWithSnake(Direction direction)
    {
        Cell nextHead = GetNextHeadPosition(_snake.GetHeadPosition(), direction);
        return IsSnakeBodyPosition(nextHead);
    }

    public Cell GetNextHeadPosition(Cell head, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return new Cell(head.X, (head.Y + 1 + Height) % Height);
            case Direction.Down:
                return new Cell(head.X, (head.Y - 1 + Height) % Height);  // Zaktualizowane
            case Direction.Left:
                return new Cell((head.X - 1 + Width) % Width, head.Y);
            case Direction.Right:
                return new Cell((head.X + 1) % Width, head.Y);
            default:
                return head;
        }
    }

    
    public int GetCurrentEdiblesCount()
    {
        int count = 0;
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (_fieldElements[x, y] is EdibleElement)
                {
                    count++;
                }
            }
        }
        return count;
    }

    public EdibleElement GetElementAtPosition(Cell position)
    {
        return _fieldElements[position.X, position.Y];
    }

    public void ClearElementAtPosition(Cell position)
    {
        _fieldElements[position.X, position.Y] = null;
    }

    public void AddEdibleElement(EdibleElement element)
    {
        if (IsPositionEmpty(element.Position))
        {
            _fieldElements[element.Position.X, element.Position.Y] = element;
        }
    }

    public Cell GetRandomEmptyPosition()
    {
        Random random = new Random();
        int x, y;
        do
        {
            x = random.Next(Width);
            y = random.Next(Height);
        }
        while (!IsPositionEmpty(new Cell(x, y)));

        return new Cell(x, y);
    }

    private bool IsPositionEmpty(Cell position)
    {
        return !IsSnakeBodyPosition(position) && _fieldElements[position.X, position.Y] == null;
    }

    private bool IsSnakeBodyPosition(Cell position)
    {
        return _snake.GetBodyPositions().Contains(position);
    }

    public IEnumerable<EdibleElement> GetAllEdibles()
    {
        List<EdibleElement> edibles = new List<EdibleElement>();

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (_fieldElements[x, y] is EdibleElement edible)
                {
                    edibles.Add(edible);
                }
            }
        }

        return edibles;
    }
}