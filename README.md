# Transcript4CSV
"Transcript4CSV" is a class that converts Teams Transcript to CSV.

## Overview
- Group Teams transcripts by speaker.
- Remove filler from text.
- Output processed data as CSV.

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
4. Creating a List internally
    ```
    transcript.MakeCSVList();
    ```
5. Manipulate data
    - Output as CSV
    ```
    transcript.WriteCSVFile("outputPath");
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