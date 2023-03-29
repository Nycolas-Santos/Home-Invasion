using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New File",menuName = "Data/File")]
public class FileScriptableObject : ScriptableObject
{
    [BoxGroup("Settings"), Header("File Content"),TextArea]
    public string[] content;
}
