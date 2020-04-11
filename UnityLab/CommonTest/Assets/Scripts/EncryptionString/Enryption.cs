using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Enryption {

    //加密和解密采用相同的key,可以任意数字，但是必须为32位
    private const string strKeyValue = "12345678901234567890198915689039";
    // 加密
    public static string EnryptionStr (string text, string strKey = strKeyValue) {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes (strKey);
        RijndaelManaged encryption = new RijndaelManaged ();
        encryption.Key = keyArray;
        encryption.Mode = CipherMode.ECB;
        encryption.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = encryption.CreateEncryptor ();
        byte[] _EncryptArray = UTF8Encoding.UTF8.GetBytes (text);
        byte[] resultArray = cTransform.TransformFinalBlock (_EncryptArray, 0, _EncryptArray.Length);
        return Convert.ToBase64String (resultArray, 0, resultArray.Length);
    }
    //解密
    public static string DecipheringStr (string text, string strKey = strKeyValue) {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes (strKey);
        RijndaelManaged decipher = new RijndaelManaged ();
        decipher.Key = keyArray;
        decipher.Mode = CipherMode.ECB;
        decipher.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = decipher.CreateDecryptor ();
        byte[] _EncryptArray = Convert.FromBase64String (text);
        byte[] resultArray = cTransform.TransformFinalBlock (_EncryptArray, 0, _EncryptArray.Length);
        return UTF8Encoding.UTF8.GetString (resultArray);
    }
}