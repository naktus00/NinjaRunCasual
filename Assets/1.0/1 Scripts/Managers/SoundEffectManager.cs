using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager instance;
    public static SoundEffectManager Instance { get { return instance; } }

    [SerializeField] private AudioClip[] audioClips;

    [Space(5), Header("For Test")]
    public int clipIndexTest;
    public float startTime;
    public float stopTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void GetSound(int clipIndex, Vector3 position)
    {
        GameObject audioSourceObj = ObjectPool.Instance.InstantiatePooledObj(3, position, Quaternion.identity);

        AudioSource audioSource = audioSourceObj.GetComponent<AudioSource>();
        audioSource.Stop();

        audioSource.clip = audioClips[clipIndex];
        audioSource.time = 0f;
        audioSource.Play();

        float clipLength = audioClips[clipIndex].length;
        Debug.Log("Clip Length is : " + clipLength.ToString());
        ObjectPool.Instance.DestroyPooledObj(audioSourceObj, clipLength);
    }
    public void GetSound(int clipIndex, Vector3 position, float startTime)
    {
        GameObject audioSourceObj = ObjectPool.Instance.InstantiatePooledObj(3, position, Quaternion.identity);

        AudioSource audioSource = audioSourceObj.GetComponent<AudioSource>();
        audioSource.Stop();

        audioSource.clip = audioClips[clipIndex];
        audioSource.time = startTime;
        audioSource.Play();

        float clipLength = audioClips[clipIndex].length;
        ObjectPool.Instance.DestroyPooledObj(audioSourceObj, clipLength);
    }
    public void GetSound(int clipIndex, Vector3 position, float startTime, float stopTime)
    {
        GameObject audioSourceObj = ObjectPool.Instance.InstantiatePooledObj(3, position, Quaternion.identity);

        AudioSource audioSource = audioSourceObj.GetComponent<AudioSource>();
        audioSource.Stop();

        audioSource.clip = audioClips[clipIndex];
        audioSource.time = startTime;
        audioSource.Play();

        ObjectPool.Instance.DestroyPooledObj(audioSourceObj, stopTime);
    }

    
}
