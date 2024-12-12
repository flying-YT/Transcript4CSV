# Transcript4CS:C# library to convert subtitle files to CSV or JSON
This library can easily convert subtitle files (.vtt format) to CSV or JSON files.
Outputs time codes, text, and metadata in CSV or JSON formate, useful for data analysis and text processing.

## Overview
- Loading and parsing .vtt files.
- Start/end time, text extraction.
- Remove filler from text.
- Conversion to CSV or JSON.
- If there is speaker information, separate it for each speaker.

## Usage
1. Add dll<br>
    Add the dll to your project
2. Add using<br>
    Add the following description
    ```
    using Transcript4CSV;
    ```
3. Make Instance<br>
    Create an instance
    ```
    TranscriptProcess transcript = new TranscriptProcess("vttPath");
    ```
    If you need console output
    ```
    TranscriptProcess transcript = new TranscriptProcess("vttPath", true);
    ```
4. Creating a List internally
    ```
    transcript.MakeCSVList();
    ```
5. Manipulate data
    - Output as CSV or JSON
    ```
    transcript.WriteCSVFile("outputPath");
    transcript.WriteJSONFile("outputPath");
    ```
    - Get in List
    ```
    var list = transcript.GetCSVData();
    ```
- If you want to add a conversion list<br>
    ```
    transcript.AddChangeWordList("csvPath");
    ```
    or
    ```
    transcript.AddChangeWordList(list);
    ```

## Lisence
This project is licensed under the MIT License, see the LICENSE.txt file for details.