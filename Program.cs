using System.Text.Json;

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
