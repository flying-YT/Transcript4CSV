using Transcript4CSV;

TranscriptProcess transcript = new TranscriptProcess("../vtt/test1.vtt");

transcript.MakeCSVList();

transcript.WriteCSVFile("../csv/test1.csv");
transcript.WriteJSONFile("../json/test1.json");

