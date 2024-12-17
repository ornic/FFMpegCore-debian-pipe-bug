using System.Text.Json;

var test = Directory.GetCurrentDirectory();

var test2 = new DirectoryInfo(test);

var test3 = test2.GetFiles();

using var fileStream = File.OpenRead("BAD.mp4");
//using var fileCopyStream = new MemoryStream();

//fileStream.CopyTo(fileCopyStream);
//fileCopyStream.Position = 0;

var analyzerOptions = new FFMpegCore.FFOptions
{
    LogLevel = FFMpegCore.Enums.FFMpegLogLevel.Verbose
};

var jsonSerializationOptions = new JsonSerializerOptions { WriteIndented = true };

var fileAnalysisResult = await FFMpegCore.FFProbe.AnalyseAsync(fileStream, analyzerOptions);
Console.WriteLine($"Analysis STREAM:\n{JsonSerializer.Serialize(fileAnalysisResult, jsonSerializationOptions)}");

fileAnalysisResult = await FFMpegCore.FFProbe.AnalyseAsync("BAD.mp4", analyzerOptions);
Console.WriteLine($"Analysis FILE:\n{JsonSerializer.Serialize(fileAnalysisResult, jsonSerializationOptions)}");
