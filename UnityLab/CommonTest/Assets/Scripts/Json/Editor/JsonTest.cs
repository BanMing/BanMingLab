#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class JsonTest : Editor
{
    [MenuItem("Tool/JsonTest")]
    private static void JsonSerializeTest()
    {
        new JsonDataTest().Save();
    }
}
#endif
