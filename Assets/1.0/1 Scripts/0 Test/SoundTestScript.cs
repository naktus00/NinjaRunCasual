using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTestScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] testClips;

    [Space(5), Header("For Test")]
    public int clipIndexTest;
    public float startTime;
    public float stopTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Vector3 pos = CameraManager.Instance.mainCam.gameObject.transform.position;
            SoundTest(clipIndexTest, pos, startTime, stopTime);
        }

    }

    public void SoundTest(int clipIndex, Vector3 position, float startTime, float stopTime)
    {
        GameObject audioSourceObj = ObjectPool.Instance.InstantiatePooledObj(3, position, Quaternion.identity);

        AudioSource audioSource = audioSourceObj.GetComponent<AudioSource>();
        audioSource.Stop();

        audioSource.clip = testClips[clipIndex];
        audioSource.time = startTime;
        audioSource.Play();

        ObjectPool.Instance.DestroyPooledObj(audioSourceObj, stopTime);
    }
}
