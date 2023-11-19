using System;

public class EdibleFactory
{
    private Random _random = new Random();
    private EdibleEffectsConfiguration _effectsConfig;

    public EdibleFactory(EdibleEffectsConfiguration config)
    {
        _effectsConfig = config;
    }

    public EdibleElement CreateRandomEdible(Cell position)
    {
        switch (_random.Next(5)) 
        {
            case 0:
                return new IncreaseLengthElement(position);
            case 1:
                return new DecreaseLengthElement(position);
            case 2:
                return new SpeedUpElement(position, _effectsConfig.SpeedUpMultiplier, _effectsConfig.SpeedUpDuration);
            case 3:
                return new SlowDownElement(position, _effectsConfig.SlowDownMultiplier, _effectsConfig.SlowDownDuration);
            case 4:
                return new ReverseElement(position);
            default:
                throw new InvalidOperationException("Nieznany typ jadalnego elementu");
        }
    }
}


