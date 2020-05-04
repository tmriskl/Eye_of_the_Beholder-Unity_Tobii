using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LocationSaveScript : MonoBehaviour {

    string fileName = "Location.txt";
    bool firstRead = true;
    public float y = 0;
    public Transform player;

    public void ReadFromFile()
    {
        string filePath = MemoryScript.GetCurrentPath();
        if (File.Exists(filePath + SceneManager.GetActiveScene().buildIndex + fileName))
        {
            using (StreamReader sr = File.OpenText(filePath + SceneManager.GetActiveScene().buildIndex + fileName))
            {
                string s;
                float x, y, z;
                s = sr.ReadLine();
                x = float.Parse(s);
                s = sr.ReadLine();
                y = float.Parse(s);
                s = sr.ReadLine();
                z = float.Parse(s);
                player.position = new Vector3(x, y+this.y, z);

                //float w;
                //s = sr.ReadLine();
                //x = float.Parse(s);
                //s = sr.ReadLine();
                //y = float.Parse(s);
                //s = sr.ReadLine();
                //z = float.Parse(s);
                //s = sr.ReadLine();
                //w = float.Parse(s);
                //player.rotation = new Quaternion(x, y, z, w);
            }
        }
    }

    public void saveToFile()
    {
        string filePath = MemoryScript.GetCurrentPath();
        using (StreamWriter sw = File.CreateText(filePath + SceneManager.GetActiveScene().buildIndex + fileName))
        {
            sw.WriteLine(player.position.x);
            sw.WriteLine(player.position.y);
            sw.WriteLine(player.position.z);
            //sw.WriteLine(player.rotation.x);
            //sw.WriteLine(player.rotation.y);
            //sw.WriteLine(player.rotation.z);
            //sw.WriteLine(player.rotation.w);
        }
    }

    private void Update()
    {
        if (firstRead)
        {
            if (MemoryScript.isReady())
            {
                firstRead = false;
                ReadFromFile();
            }
        }
        else
        {
            saveToFile();
        }
    }
}
