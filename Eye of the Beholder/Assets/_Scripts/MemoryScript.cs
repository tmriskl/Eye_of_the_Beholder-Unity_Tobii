using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MemoryScript : MonoBehaviour {

    private static readonly string memoryFolder = "saves";
    private static readonly int defaultFolderNumber = 0;
    private static readonly string defaultFolderName = "default";
    private static string savesFileName = "saves_Names.txt";
    private static string levelFileName = "Level.txt";
    private static string[] saveFilesNames;
    private static int currentFolderNumber = defaultFolderNumber;
    private static bool ready = false;

    // Use this for initialization
    void Start () {
        ReadSavesFile();
        SaveToSavesFile();
        ready = true;
    }

    public static void nextFile()
    {
        int num = currentFolderNumber;
        num++;
        num %= saveFilesNames.Length;
        currentFolderNumber = num;
    }

    internal static void DeleteCurrentFile()
    {
        string[] newSaveFilesNames = new string[saveFilesNames.Length - 1];
        for (int i = 0, j = 0; i < saveFilesNames.Length; i++)
        {
            if (i != currentFolderNumber)
            {
                newSaveFilesNames[j] = saveFilesNames[i];
                j++;
            }
        }
        string currentFolder = memoryFolder + "\\" + saveFilesNames[currentFolderNumber];
        if(Directory.Exists(currentFolder))
            Directory.Delete(currentFolder, true);
        saveFilesNames = newSaveFilesNames;
        if (saveFilesNames.Length == 0)
        {
            saveFilesNames = new string[1];
            saveFilesNames[0] = defaultFolderName;
            currentFolderNumber = defaultFolderNumber;
            GetCurrentPath();
        }
        else
        {
            int num = currentFolderNumber + saveFilesNames.Length;
            num %= saveFilesNames.Length;
            currentFolderNumber = num;
        }
        SaveToSavesFile();
    }

    public static void previousFile()
    {
        int num = currentFolderNumber;
        num--;
        num += saveFilesNames.Length;
        num %= saveFilesNames.Length;
        currentFolderNumber = num;
    }

    public static bool addNewSaveFile(string filename)
    {
        bool exist = false;
        for (int i = 0; (!exist)&&(i < saveFilesNames.Length); i++)
        {
            if (filename.Equals(saveFilesNames[i]))
                exist = true;
        }
        if (!exist)
        {
            string[] newSaveFilesNames = new string[saveFilesNames.Length + 1];
            for (int i = 0; i < saveFilesNames.Length; i++)
            {
                newSaveFilesNames[i] = saveFilesNames[i];
            }
            newSaveFilesNames[saveFilesNames.Length] = filename;
            saveFilesNames = newSaveFilesNames;
            currentFolderNumber = saveFilesNames.Length-1;
            GetCurrentPath();
            SaveToSavesFile();
        }
        return !exist;
    }

    public static string getCurrentFileName()
    {
        return saveFilesNames[currentFolderNumber];
    }
    public static bool isReady()
    {
        return ready;
    }
    void ReadSavesFile()
    {
        if (File.Exists(memoryFolder + "\\" + savesFileName))
        {
            using (StreamReader sr = File.OpenText(memoryFolder + "\\" + savesFileName))
            {
                string s = sr.ReadLine();
                currentFolderNumber = int.Parse(s);
                s = sr.ReadLine();
                int length = int.Parse(s);
                saveFilesNames = new string[length];
                s = sr.ReadLine();
                for (int i = 0; (s != null)&&(i<length); i++)
                {
                    saveFilesNames[i] = s;
                    s = sr.ReadLine();
                }
            }
        }
        else
        {
            currentFolderNumber = defaultFolderNumber;
            saveFilesNames = new string[1];
            saveFilesNames[0] = defaultFolderName;
            SaveToSavesFile();
        }
    }
    public static void SaveToSavesFile()
    {
        Directory.CreateDirectory(memoryFolder);
        using (StreamWriter sw = File.CreateText(memoryFolder + "\\" + savesFileName))
        {
            sw.WriteLine(currentFolderNumber);
            sw.WriteLine(saveFilesNames.Length);
            for (int i = 0; i < saveFilesNames.Length; i++)
                sw.WriteLine(saveFilesNames[i]);
        }
    }

    public static string GetCurrentPath()
    {
        string currentFolder = memoryFolder + "\\" + saveFilesNames[currentFolderNumber];
        Directory.CreateDirectory(currentFolder);
        return currentFolder + "\\";
    }

    public static string GetCurrentLevelFilePath()
    {
        return GetCurrentPath() + levelFileName;
    }


    public static int GetCurrentLevel()
    {
        int currentLevel = 0;
        if (File.Exists(GetCurrentLevelFilePath()))
        using (StreamReader sr = File.OpenText(GetCurrentLevelFilePath()))
        {
            string s = sr.ReadLine();
            currentLevel = int.Parse(s);
        }
        else
        {
            currentLevel = 1;
        }
        return currentLevel;
    }
}
