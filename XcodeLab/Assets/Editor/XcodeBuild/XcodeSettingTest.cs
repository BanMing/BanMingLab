using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using UnityEditor.iOS.Xcode;
using System.IO;

public class XcodeSettingTest
{
    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {

        if (buildTarget == BuildTarget.iOS)
        {

            //获得proj文件
            string projPath = PBXProject.GetPBXProjectPath(path);
            PBXProject xcodeProj = new PBXProject();
            xcodeProj.ReadFromString(File.ReadAllText(projPath));

            //获得当前项目名字
            var targetName = xcodeProj.TargetGuidByName(PBXProject.GetUnityTargetName());

            //添加依赖库
            xcodeProj.AddFrameworkToProject(targetName, "libz.dylib", true);
            xcodeProj.AddFrameworkToProject(targetName, "MessageUI.framework", true);

            //设置关闭bitcode
            xcodeProj.SetBuildProperty(targetName, "ENABLE_BITCODE", "NO");

            //添加苹果自带功能
            xcodeProj.AddCapability(targetName, PBXCapabilityType.GameCenter);

            //保存工程
            xcodeProj.WriteToFile(projPath);

            //修改plist
            var plistPath = Path.Combine(path, "Info.plist");
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));
            var rootDict = plist.root;

            // 语音权限
            rootDict.SetString("NSMicrophoneUsageDescription", "是否允许此游戏使用麦克风？");
            // 地址权限
            rootDict.SetString("NSLocationAlwaysUsageDescription", "");
            rootDict.SetString("NSLocationUsageDescription", "");
            rootDict.SetString("NSLocationWhenInUseUsageDescription", "");
            
			//保存plist
			plist.WriteToFile(plistPath);
			File.WriteAllText(projPath,xcodeProj.WriteToString());
        }
    }
}