using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentObject : MonoBehaviour {
    private static PermanentObject playerInstance;

    void Awake () {
        DontDestroyOnLoad(transform.gameObject);

        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }	
}