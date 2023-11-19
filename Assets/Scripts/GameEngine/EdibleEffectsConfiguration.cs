public class EdibleEffectsConfiguration
{
    public float SpeedUpMultiplier { get; private set; }
    public float SpeedUpDuration { get; private set; }
    public float SlowDownMultiplier { get; private set; }
    public float SlowDownDuration { get; private set; }
    public EdibleEffectsConfiguration(float speedUpMultiplier, float speedUpDuration, float slowDownMultiplier, float slowDownDuration)
    {
        SpeedUpMultiplier = speedUpMultiplier;
        SpeedUpDuration = speedUpDuration;
        SlowDownMultiplier = slowDownMultiplier;
        SlowDownDuration = slowDownDuration;
    }
}