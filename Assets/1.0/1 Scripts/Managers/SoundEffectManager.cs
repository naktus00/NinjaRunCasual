using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager _instance;
    public static SoundEffectManager Instance { get { return _instance; } }

    [SerializeField] private AudioClip[] audioClips;

    [Space(5), Header("For Test")]
    public int clipIndexTest;
    public float startTime;
    public float stopTime;

    private void Awake()
    {
        #region Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        _instance = this;
        #endregion
    }

    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }

    public void GetSound(int clipIndex)
    {
        Vector3 position = Camera.main.transform.position;

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
