using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        int numOfMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        print("Number of music players " + numOfMusicPlayers);
        if (numOfMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
