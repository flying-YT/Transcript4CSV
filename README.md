# Transcript4CSV
"Transcript4CSV" is a class that converts Teams Transcript to CSV.

## Overview
- Group Teams transcripts by speaker.
- Remove filler from text.
- Output processed data as CSV.

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
    var list = transcript.GetCSVData();
    ```

## Lisence
This project is licensed under the MIT License, see the LICENSE.txt file for details.