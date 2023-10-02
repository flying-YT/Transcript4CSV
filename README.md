# Transcript4CSV
"Transcript4CSV" is a class that converts Teams Transcript to CSV

## Usage
1. Add dll
    Add the dll to your project
2. Add using
    Add the following description
    ```
    using Transcript4CSV;
    ```
3. Make Instance
    Create an instance
    ```
    TranscriptProcess transcript = new TranscriptProcess("vttPath");
    ```
4. Manipulate data
    - Output as CSV
    ```
    transcript.WriteCSVFile("outputPath");
    ```
    - Get in List
    ```
    transcript.WriteCSVFile("xxx.csv");
    ```