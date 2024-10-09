using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class SvnOperater :Editor
{
    [MenuItem("SVN/Update")]
    public static void Update()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        Process.Start("TortoiseProc.exe", "/command:update /path:" + Application.dataPath + " /closeonend:0");
#endif
    }

    [MenuItem("SVN/Commit")]
    public static void Commit()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        Process.Start("TortoiseProc.exe", "/command:commit /path:" + Application.dataPath + " /closeonend:0");
#endif
    }

}
