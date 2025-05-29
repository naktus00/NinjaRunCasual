using UnityEngine;
using Cinemachine;

public class MoveCamera : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float moveDuration;

    private CinemachineVirtualCamera vcam;
    private CinemachineTransposer transposer;
    private float elapsedTime;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        elapsedTime = 0f;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            transposer.m_FollowOffset = Vector3.Lerp(startPoint.position, endPoint.position, t);
        }
    }
}
