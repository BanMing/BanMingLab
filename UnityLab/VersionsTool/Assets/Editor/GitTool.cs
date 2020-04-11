using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
public class GitTool {

    private static List<string> drives = new List<string> () { "c:", "d:", "e:", "f:" };
    private static string gitPath = @"\Program Files\TortoiseGit\bin\";
    private static string gitProc = @"TortoiseGitProc.exe ";
    private static string gitProcPath = "";

    [MenuItem ("GitTool/Git Update ")]
    static void UpdateFromGit () {
        if (string.IsNullOrEmpty(gitProcPath))
            gitProcPath = GetGitProcPath();
        var dir = new DirectoryInfo(Application.dataPath);
        var path = dir.Parent.FullName.Replace('/', '\\');
        var para = "/command:pull /path:\"" + path + "\" /closeonend:0";
        System.Diagnostics.Process.Start(gitProcPath, para);
}

[MenuItem ("GitTool/Git Commit")]
static void CommitToGit () {
    if (string.IsNullOrEmpty (gitProcPath))
        gitProcPath = GetGitProcPath ();
    var path = Application.dataPath.Replace ('/', '\\');
    var para = "/command:commit /path:\"" + path + "\"";
    System.Diagnostics.Process.Start (gitProcPath, para);
}

[MenuItem ("GitTool/Git Revert")]
static void RevertFromGit () {
    if (string.IsNullOrEmpty (gitProcPath))
        gitProcPath = GetGitProcPath ();
    var path = Application.dataPath.Replace ('/', '\\');
    var para = "/command:revert /path:\"" + path + "\"";
    System.Diagnostics.Process.Start (gitProcPath, para);
}

[MenuItem ("GitTool/Git Add")]
static void AddToGit () {
    if (string.IsNullOrEmpty (gitProcPath))
        gitProcPath = GetGitProcPath ();
    var path = Application.dataPath.Replace ('/', '\\');
    var para = "/command:add /path:\"" + path + "\"";
    System.Diagnostics.Process.Start (gitProcPath, para);
}

[MenuItem ("GitTool/Git ClearUp")]
static void ClearUpFromGit () {
    if (string.IsNullOrEmpty (gitProcPath))
        gitProcPath = GetGitProcPath ();
    var path = Application.dataPath.Replace ('/', '\\');
    var para = "/command:cleanup /path:\"" + path + "\"";
    System.Diagnostics.Process.Start (gitProcPath, para);
}
private static string GetGitProcPath () {
    foreach (var item in drives) {
        var path = string.Concat (item, gitPath, gitProc);
        if (File.Exists (path))
            return path;
    }
    return EditorUtility.OpenFilePanel ("Select TortoiseProc.exe", "c:\\", "exe");
}
}