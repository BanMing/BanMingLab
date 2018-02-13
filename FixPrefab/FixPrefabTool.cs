using System;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Text;
using System.Collections.Generic;

public class FixPrefabTool {
    static StringBuilder sb;
    static PrefabInfo prefabInfo;
    static int prefabItemIndex=0;
    static string jsonFilePath=Application.dataPath+@"\Editor\PrefabJson\";

    [MenuItem ("Tools/FixPrefab/FixSelectPrefab")]
    static void FixSelectPrefab () {       
        if (Selection.gameObjects.Length==0)
            Debug.LogError("No Select GameObject!");
            return;
        var transform = Selection.gameObjects[0].transform;
        var path = EditorUtility.OpenFilePanel ("Open "+transform.name+"PrefabJson",jsonFilePath, "txt");
        // Debug.Log("path:"+path);
        if (string.IsNullOrEmpty(path))
            return;
        var text = File.ReadAllText (path);
        // Debug.Log ("prefab text:" + text);
        prefabItemIndex=0;
        prefabInfo= JsonUtility.FromJson<PrefabInfo>(text);

        GetAll(transform,false);
        
    }

    [MenuItem ("Tools/FixPrefab/SaveSelectPrefabInfo")]
    static void SaveSelectPrefabInfo () {
        var transform = Selection.gameObjects[0].transform;
        sb=new StringBuilder();
        sb.AppendLine("{");
        sb.AppendLine("\"PrefabItems\":");
        sb.AppendLine("[");
        GetAll (transform,true);
        sb.Remove(sb.Length-3,2);
        sb.AppendLine("]");
        sb.AppendLine("}");
        File.WriteAllText(jsonFilePath+transform.name+"PrefabJson.txt",sb.ToString());
        AssetDatabase.Refresh();
        Debug.Log("生成成功！");
        //  var prefabInfo= JsonUtility.FromJson<PrefabInfo>(sb.ToString());
        // Debug.Log("prefabInfo.PrefabItems.Count:"+prefabInfo.PrefabItems.Count);
        // foreach (var item in prefabInfo.PrefabItems)
        // {
        //     Debug.Log("Item.name:"+item.name);
        // }
        // Debug.Log(sb.ToString());
    }
    static void GetAll (Transform transform,bool isSave) {
        foreach (Transform item in transform) {
            // Debug.Log (item);
            if (isSave){
                SaveToText (item.name, item.gameObject.activeSelf);
            }else{
                SetDataToPrefab(item);
            }
            
            GetAll (item,isSave);
        }
    }
    static void SaveToText (string name, bool isActive) {
        var prefabInfo = new PrefabItem (name, isActive);
        var jsonText = JsonUtility.ToJson (prefabInfo);
        sb.AppendLine(jsonText+",");
        // Debug.Log ("jsonText:" + jsonText);
    }

    static void SetDataToPrefab(Transform item){
        if(prefabInfo.PrefabItems !=null){
            item.name=prefabInfo.PrefabItems[prefabItemIndex].name;
            item.gameObject.SetActive(prefabInfo.PrefabItems[prefabItemIndex].isActive);
            prefabItemIndex++;
        }
    }

    [Serializable]
    struct PrefabItem {
        public string name;
        public bool isActive;
        public PrefabItem (string name, bool isActive) {
            this.name = name;
            this.isActive = isActive;
        }
    }
    [Serializable]
    struct PrefabInfo {
        public List<PrefabItem> PrefabItems;
    }
}