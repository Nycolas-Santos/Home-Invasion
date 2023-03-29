using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class File : MonoBehaviour
{
    [BoxGroup("Settings")]
    [SerializeField] private GameObject UI;
    [BoxGroup("Settings")]
    [SerializeField] private Image fileImage;
    [BoxGroup("Settings")]
    [SerializeField] private Text fileText;
    [BoxGroup("Settings")] 
    [SerializeField] private FileScriptableObject currentFile;
    

    public static File Instance { get; set; }

    private int contentIndex;
    
    public bool IsOpen()
    {
        return UI.gameObject.activeSelf;
    }

    public FileScriptableObject CurrentFile
    {
        get => currentFile;
        set => currentFile = value;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void OpenUI(FileScriptableObject file)
    {
        UI.gameObject.SetActive(true);
        CurrentFile = file;
        contentIndex = 0;
        UpdateContent();
    }

    public void CloseUI()
    {
        UI.gameObject.SetActive(false);
        CurrentFile = null;
        contentIndex = 0;
    }

    public void NextContent()
    {
        if (contentIndex != CurrentFile.content.Length - 1)contentIndex++;
        UpdateContent();
    }

    public void PreviousContent()
    {
        if (contentIndex != 0) contentIndex--;
        UpdateContent();
    }

    private void UpdateContent()
    {
        fileText.text = CurrentFile.content[contentIndex];
    }
}
