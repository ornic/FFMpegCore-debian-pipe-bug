Use `docker-compose up --build app` command to run the example on Debian 12 using .NET 8

or `docker-compose up --build app-net9` command to run the example on Debian 12 using .NET 9

or `docker-compose up --build app-ubuntu` command to run the example on Ubuntu 24.04 LTS using .NET 8

All variants have latest FFMpeg available: 5.1.6 for Debian, and 6.1.1 for Ubuntu. FFMpegCore version is 5.1.0.

Example code:
```c#
var analyzerOptions = new FFMpegCore.FFOptions
{
    LogLevel = FFMpegCore.Enums.FFMpegLogLevel.Verbose
};

var jsonSerializationOptions = new JsonSerializerOptions { WriteIndented = true };

using var fileStream = File.OpenRead("BAD.mp4");

var fileAnalysisResult = await FFMpegCore.FFProbe.AnalyseAsync(fileStream, analyzerOptions);
Console.WriteLine($"Analysis STREAM:\n{JsonSerializer.Serialize(fileAnalysisResult, jsonSerializationOptions)}");

fileAnalysisResult = await FFMpegCore.FFProbe.AnalyseAsync("BAD.mp4", analyzerOptions);
Console.WriteLine($"Analysis FILE:\n{JsonSerializer.Serialize(fileAnalysisResult, jsonSerializationOptions)}");
```

The first AnalyseAsync (using stream) produce error:
```json
{
  "ErrorData": [
    "[mov,mp4,m4a,3gp,3g2,mj2 @ 0x55d9b146d2c0] stream 0, offset 0x81c: partial file"
  ]
}
```

The second one (using filename) working without errors - as it should.
