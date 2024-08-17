using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FileSearch : MonoBehaviour
{
    public TMP_InputField searchField;
    
    public List<GameObject> files;
    public Transform viewTransform;
    // Start is called before the first frame update
    void Start()
    {
        searchField.onValueChanged.AddListener(delegate { SearchGameObject(); });
        
        // show all game objects
        ShowAllFiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SearchGameObject()
    {
        string searchValue = searchField.text;
        if(string.IsNullOrEmpty(searchValue))
        {
            ShowAllFiles();
        }
        else
        {
            List<GameObject> filteredFiles = files.Where(file => file.name.ToLower().Contains(searchValue.ToLower())).ToList();
            UpdateFilesView(filteredFiles);
        }
    }

    void ShowAllFiles()
    {
        UpdateFilesView(files);
    }

    void UpdateFilesView(List<GameObject> file)
    {
        foreach(GameObject go in files)
        {
            go.SetActive(false);
        }

        foreach(GameObject go in file)
        {
            go.SetActive(true);
        }
    }
}
