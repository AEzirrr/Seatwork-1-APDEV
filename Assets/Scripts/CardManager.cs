using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CardManager : MonoBehaviour
{
    private string RedFolder = "S:/Unity/Seatwork#1/Assets";
    private string BlackFolder = "S:/Unity/Seatwork#1/Assets";
    [SerializeField] private List<GameObject> cards;

    void Start()
    {

        int RedCount = GetItemCountInFolder(RedFolder);
        int BlackCount = GetItemCountInFolder(BlackFolder);
        Debug.Log("Red Cards Folder: " + RedCount / 2);
        Debug.Log("Black Cards Folder: " + BlackCount / 2);
    }

    int GetItemCountInFolder(string path)
    {

        // Get the number of files and subdirectories in the folder
        int fileCount = Directory.GetFiles(path).Length;
        int directoryCount = Directory.GetDirectories(path).Length;

        // Return the total count of items (files + directories)
        return fileCount + directoryCount;
    }
}
