using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevel : MonoBehaviour
{
    [SerializeField] float reloadTime = 2f;

    void Start()
    {    
        Invoke("LoadFirstLevel", reloadTime);
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            LoadFirstLevel();
        }
    }
    void LoadFirstLevel()
    { 
       SceneManager.LoadScene(1);
    }
    
}
