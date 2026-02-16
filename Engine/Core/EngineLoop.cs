using System.Diagnostics;

var isRunning = true;
var accumulator = 0.0;
const double fixedDelta = 1.0 / 50.0;

var stopwatch = new Stopwatch();
stopwatch.Start();

double previousTime = stopwatch.Elapsed.TotalMilliseconds;

while (isRunning)
{
    double currentTime = stopwatch.Elapsed.TotalMilliseconds;
    var deltaTime = currentTime - previousTime;
    previousTime = currentTime;
    
    accumulator += deltaTime;

    while (accumulator >= fixedDelta)
    {
        Console.WriteLine($"Current time: {currentTime} \n Frame time: {deltaTime}");
        accumulator -= fixedDelta;
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