public abstract class EdibleElement
{
    public Cell Position { get; private set; }
    public virtual int EdibleTypeIndex { get; }

    public abstract void ApplyEffect(Snake snake);
    protected EdibleElement(Cell position)
    {
        Position = position;
    }
}

public class IncreaseLengthElement : EdibleElement
{
    public override int EdibleTypeIndex => 0;
    public override void ApplyEffect(Snake snake)
    {
        snake.Grow();
    }

    public IncreaseLengthElement(Cell position) : base(position)
    {
    }
}

public class DecreaseLengthElement : EdibleElement
{
    public override int EdibleTypeIndex => 1;
    public override void ApplyEffect(Snake snake)
    {
        snake.Shrink();
    }

    public DecreaseLengthElement(Cell position) : base(position)
    {
    }
}

public class SpeedUpElement : EdibleElement
{
    public override int EdibleTypeIndex => 2;
    private readonly float _speedMultiplier;
    private readonly float _duration;

    public SpeedUpElement(Cell position, float multiplier, float duration) : base(position)
    {
        _speedMultiplier = multiplier;
        _duration = duration;
    }

    public override void ApplyEffect(Snake snake)
    {
        snake.ModifySpeed(_speedMultiplier, _duration);
    }
}

public class SlowDownElement : EdibleElement
{
    public override int EdibleTypeIndex => 3;
    private readonly float _speedMultiplier;
    private readonly float _duration;

    public SlowDownElement(Cell position, float multiplier, float duration) : base(position)
    {
        _speedMultiplier = multiplier;
        _duration = duration;
    }

    public override void ApplyEffect(Snake snake)
    {
        snake.ModifySpeed(_speedMultiplier, _duration);
    }
}


public class ReverseElement : EdibleElement
{
    public override int EdibleTypeIndex => 4;
    public override void ApplyEffect(Snake snake)
    {
        snake.Reverse();
    }

    public ReverseElement(Cell position) : base(position)
    {
    }
}