#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// SVN工具
/// </summary>
public class SVNTool
{
    private static List<string> drives = new List<string>() { "c:", "d:", "e:", "f:" };
    private static string svnPath = @"\Program Files\TortoiseSVN\bin\";
    private static string svnProc = @"TortoiseProc.exe";
    private static string svnProcPath = "";

    [MenuItem("SVNTool/SVN Update %&e")]
    public static void UpdateFromSVN()
    {
        if (string.IsNullOrEmpty(svnProcPath))
            svnProcPath = GetSvnProcPath();
        var dir = new DirectoryInfo(Application.dataPath);
        var path = dir.Parent.FullName.Replace('/', '\\');
        var para = "/command:update /path:\"" + path + "\" /closeonend:0";
        System.Diagnostics.Process.Start(svnProcPath, para);
    }

    [MenuItem("SVNTool/SVN Commit %&r")]
    public static void CommitToSVN()
    {
        if (string.IsNullOrEmpty(svnProcPath))
            svnProcPath = GetSvnProcPath();
        var path = Application.dataPath.Replace('/', '\\');
        var para = "/command:commit /path:\"" + path + "\"";
        System.Diagnostics.Process.Start(svnProcPath, para);
    }

    [MenuItem("SVNTool/SVN Revert %&t")]
    public static void RevertFromSVN()
    {
        if (string.IsNullOrEmpty(svnProcPath))
            svnProcPath = GetSvnProcPath();
        var path = Application.dataPath.Replace('/', '\\');
        var para = "/command:revert /path:\"" + path + "\"";
        System.Diagnostics.Process.Start(svnProcPath, para);
    }


    [MenuItem("SVNTool/SVN Add %&u")]
    public static void AddToSVN()
    {
        if (string.IsNullOrEmpty(svnProcPath))
            svnProcPath = GetSvnProcPath();
        var path = Application.dataPath.Replace('/', '\\');
        var para = "/command:add /path:\"" + path + "\"";
        System.Diagnostics.Process.Start(svnProcPath, para);
    }

    [MenuItem("SVNTool/CleanUp SVN %&y")]
    public static void CleanUpFromSVN()
    {
        if (string.IsNullOrEmpty(svnProcPath))
            svnProcPath = GetSvnProcPath();
        var path = Application.dataPath.Replace('/', '\\');
        var para = "/command:cleanup /path:\"" + path + "\"";
        System.Diagnostics.Process.Start(svnProcPath, para);
    }

    private static string GetSvnProcPath()
    {
        foreach (var item in drives)
        {
            var path = string.Concat(item, svnPath, svnProc);
            if (File.Exists(path))
                return path;
        }
        return EditorUtility.OpenFilePanel("Select TortoiseProc.exe", "c:\\", "exe");
    }
}
# endif