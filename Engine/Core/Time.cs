namespace Engine.Core;

public static class Time
{
    public static float PreviousFrameTime { get; internal set; }
    public static float CurrentTime { get; internal set; }
    public static float DeltaTime { get; internal set; }
    public static float FixedDeltaTime => 1f / 50f;
    public static int CurrentFrame { get; internal set; }
}