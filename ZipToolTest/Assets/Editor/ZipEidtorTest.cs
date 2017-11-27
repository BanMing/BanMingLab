using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class ZipToolEidtor {
    [MenuItem ("Tool/TestCompressFiles")]
    static void CompressFiles () {
        string[] fileName = new string[] {
            Application.dataPath + @"/ZipTest/zip01.txt",
            Application.dataPath + @"/ZipTest/zip02.txt"
        };
        string outputFilePath = Application.dataPath + @"/ZipTest/ZipTest.zip";
        ZipTool.TestZipFile (fileName, outputFilePath, 9);
        AssetDatabase.Refresh ();
    }

    [MenuItem ("Tool/TestUnCompressFiles")]
    static void UnCompressFiles () {
        string zipPath = Application.dataPath + @"/ZipTest/ZipTest.zip";
        string outPath = Application.dataPath + @"/ZipTest/UncCompress/";
        ZipTool.TestUnZipFile (zipPath, outPath);
        AssetDatabase.Refresh ();
    }
}

