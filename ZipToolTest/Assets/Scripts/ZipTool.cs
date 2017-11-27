using System;
using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using UnityEngine;

public class ZipTool {

	/// <summary>
	/// 简单创建压缩Zip文件
	/// </summary>
	/// <param name="fileNames">需要压缩的文件集合</param>
	/// <param name="outputFilePath">压缩文件生成的路径</param>
	/// <param name="compressLevel">压缩等级0-9</param>
	public static void TestZipFile (string[] fileNames, string outputFilePath, int compressLevel) {
		try {
			using (ZipOutputStream stream = new ZipOutputStream (File.Create (outputFilePath))) {
				stream.SetLevel (compressLevel); //设置压缩等级
				byte[] buffer = new byte[4096];
				foreach (string file in fileNames) {
					var entry = new ZipEntry (Path.GetFileName (file));
					entry.DateTime = DateTime.Now;
					stream.PutNextEntry (entry);

					using (FileStream fs = File.OpenRead (file)) {
						int sourceBytes;
						do {
							sourceBytes = fs.Read (buffer, 0, buffer.Length);
							stream.Write (buffer, 0, sourceBytes);

						} while (sourceBytes > 0);
					}
				}
				stream.Finish ();
				stream.Close ();
				Debug.Log ("压缩完成！");
			}
		} catch (Exception ex) {
			Debug.Log ("异常为：" + ex);
		}

	}
	/// <summary>
	/// 解压
	/// </summary>
	/// <param name="zipPath">压缩文件路径</param>
	/// <param name="outPath">解压出去路径</param>
	public static void TestUnZipFile (string zipPath, string outPath) {
		if (!File.Exists (zipPath)) {
			Debug.LogError ("没有此文件路径：" + zipPath);
			return;
		}
		using (ZipInputStream stream = new ZipInputStream (File.OpenRead (zipPath))) {
			ZipEntry theEntry;
			while ((theEntry = stream.GetNextEntry ()) != null) {

				// Debug.Log ("theEntry.Name：" + theEntry.Name);
				string fileName = Path.GetFileName (theEntry.Name);
				// Debug.Log ("fileName：" + fileName);
				string filePath = Path.Combine (outPath, theEntry.Name);
				// Debug.Log ("filePath:" + filePath);
				string directoryName = Path.GetDirectoryName (filePath);
				// Debug.Log ("directoryName：" + directoryName);

				// 创建压缩文件中文件的位置
				if (directoryName.Length > 0) {
					Directory.CreateDirectory (directoryName);
				}
				if (fileName != String.Empty) {
					using (FileStream streamWriter = File.Create (filePath)) {
						int size = 2048;
						byte[] data = new byte[2048];
						while (true) {
							size = stream.Read (data, 0, data.Length);
							if (size > 0) {
								streamWriter.Write (data, 0, size);
							} else {
								// Debug.Log (theEntry.Name+"解压完成！");
								break;
							}
						}
					}
				}
			}
			Debug.Log ("解压完成！");
		}
		
	}
}