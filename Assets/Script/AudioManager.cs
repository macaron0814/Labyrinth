using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public AudioClip[] se;

    //SE再生
    public void SoundPlaySE(int playse)//配列番号
    {
        audioSource.PlayOneShot(se[playse]);//再生
    }
}
