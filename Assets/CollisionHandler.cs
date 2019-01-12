using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")][SerializeField] float levelLoadDelay = 3f;
    [Tooltip("FX prefab Explosion")] [SerializeField] GameObject deathEffect;
    
    private void OnTriggerEnter(Collider other)
    {
        startPlayerDeath();
    }
    void startPlayerDeath()
    {
        SendMessage("OnPlayerDeath");//sends message to player handler class to disable controls
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;//removes ship renderer
        deathEffect.SetActive(true);//plays explosion
        Invoke("ReloadLevel", levelLoadDelay);
    }
    void ReloadLevel()//string referenced
    {
        SceneManager.LoadScene(1);
    }
}
