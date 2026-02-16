namespace Engine.Core;

public static class Time
{
    public static double PreviousFrameTime { get; internal set; }
    public static double CurrentTime { get; internal set; }
    public static double DeltaTime { get; internal set; }
    public static double FixedDeltaTime => 1.0 / 50.0;
    public static int CurrentFrame { get; internal set; }
}