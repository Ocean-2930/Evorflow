using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVParser
{
    public string[] data;

    public CSVParser(string directory)
    {
        TextAsset csvFile = Resources.Load<TextAsset>(directory);
        if (csvFile == null)
        {
            throw new FileNotFoundException($"CSV file could not be loaded from Resources path: {directory}");
        }

        StringReader reader = new StringReader(csvFile.text);
        string csvText = reader.ReadToEnd();
        data = csvText.Split('\n');
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = data[i].Trim();
        }
    }
}
