using System.Diagnostics;
using Engine.Core;

var isRunning = true;
var accumulator = 0.0;

var stopwatch = new Stopwatch();
stopwatch.Start();

double previousTime = stopwatch.Elapsed.TotalMilliseconds;

while (isRunning)
{
    double currentTime = stopwatch.Elapsed.TotalMilliseconds;
    var deltaTime = currentTime - previousTime;
    
    Time.CurrentTime = currentTime;
    Time.PreviousFrameTime = previousTime;
    Time.DeltaTime = deltaTime;
    
    previousTime = currentTime;
    accumulator += deltaTime;

    while (accumulator >= Time.FixedDeltaTime)
    {
        accumulator -= Time.FixedDeltaTime;
    }

    if (Console.KeyAvailable)
    {
        var key = Console.ReadKey(true).Key;
        if (key == ConsoleKey.Escape)
            isRunning = false;
    }
}

stopwatch.Stop();
Console.WriteLine($"{stopwatch.ElapsedMilliseconds} ms");