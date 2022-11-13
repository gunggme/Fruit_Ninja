using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmLoop : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    public void SoundOFF_ON()
    {
        audioSource.loop = !audioSource.loop;
    }
}
