using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ZIPTest : MonoBehaviour
{
    private string sourePath;
    private string outPath;
    void Start()
    {
        sourePath = Application.dataPath + @"/Tools/ZIPTool/ziptest/zip.txt";
        outPath=Application.persistentDataPath+@"/txttest.zip";
    }
    void OnGUI()
    {
        sourePath = GUILayout.TextField(sourePath, GUILayout.Width(1000), GUILayout.Height(50));
        if (GUILayout.Button("压缩zip"))
        {
            ZIPTool.CompressDirectory(sourePath, outPath);
        }
        if (GUILayout.Button("解压zip"))
        {
            ZIPTool.DecompressToDirectory(sourePath, outPath);
        }
    }

    [MenuItem("Tool/ZipTest")]
    static void CompressDirectoryTest(){
         ZIPTool.CompressDirectory(Application.dataPath + @"/Tools/ZIPTool/ziptest/zip.txt", Application.persistentDataPath+@"/txttest.zip");

    }
     [MenuItem("Tool/CreatePackZip")]
    static void CreatePackZip(){
        ZIPTool.PackFiles("tt.zip",Application.dataPath + @"/Tools/ZIPTool/ziptest/zip.txt","");
    }

     [MenuItem("Tool/CompressFiles")]
    static void CompressFiles(){
        var files= new List<string>();
        files.Add(Application.dataPath +@"/Tools/ZIPTool/ziptest/zip.txt");
        ZIPTool.CompressFiles(files,"",Application.dataPath + @"/Tools/ZIPTool/ziptest",0);
    }
}
