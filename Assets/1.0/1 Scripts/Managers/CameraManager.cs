using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance;
    public static CameraManager Instance { get { return _instance; } }

    public const float AimCamBlendTime = 0.5f;

    public Camera mainCam { get { return Camera.main; } }

    [SerializeField] private CinemachineVirtualCamera playerPlatform;
    [SerializeField] private CinemachineVirtualCamera playerAim;
    [SerializeField] private CinemachineVirtualCamera endingCam;
    [SerializeField] private CinemachineVirtualCamera endingPlayerCam;
    [SerializeField] private CinemachineVirtualCamera fightCam;

    public Transform fightCamLocTr;
    public Transform playerWinCamLocTr;
    public Transform fakePlayerWinCamLocTr;

    private void Awake()
    {
        #region Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        #endregion
    }

    private void Start()
    {
        SubscribeGameActions();
    }
    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }
    private void SubscribeGameActions()
    {
        GameManager.Instance.onArrivedBoss += () => { EndingCam(5f); };
        GameManager.Instance.onBossFightStarted += () => { AimCam(AimCamBlendTime); };
        GameManager.Instance.onBossFightOver += (playerWin) => 
        {
            if(playerWin)
                EndingPlayerCam(2f); 
            //else
            //    EndingPlayerCam(5f);
        };

        GameManager.Instance.onArrivedFightArea += () => { FightCam(1f); };
        GameManager.Instance.onFightEnded += (b) => { FightEndCam(b, 0.75f); }; 
    }

    public void EndingCam(float blendTime)
    {
        Vector3 position = endingCam.gameObject.transform.position;
        position.z = GameManager.Instance.player.gameObject.transform.position.z - 6.5f;
        endingCam.gameObject.transform.position = position;

        playerPlatform.Priority = 0;
        playerAim.Priority = 0;
        endingCam.Priority = 99;
        endingPlayerCam.Priority = 0;
        fightCam.Priority = 0;
        mainCam.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = blendTime;
    }
    public void AimCam(float blendTime)
    {
        playerPlatform.Priority = 0;
        playerAim.Priority = 99;
        endingCam.Priority = 0;
        endingPlayerCam.Priority = 0;
        fightCam.Priority = 0;
        mainCam.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = blendTime;
    }
    public void EndingPlayerCam(float blendTime)
    {
        Vector3 position = endingPlayerCam.gameObject.transform.position;
        position.z = GameManager.Instance.player.gameObject.transform.position.z - 5.5f;
        endingPlayerCam.gameObject.transform.position = position;

        playerPlatform.Priority = 0;
        playerAim.Priority = 0;
        endingCam.Priority = 0;
        endingPlayerCam.Priority = 99;
        fightCam.Priority = 0;
        mainCam.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = blendTime;
    }
    public void PlayerFallCam()
    {
        playerPlatform.Follow = null;
    }
    public void FightCam(float blendTime)
    {
        fightCam.transform.position = fightCamLocTr.position;
        fightCam.transform.rotation = fightCamLocTr.rotation;

        playerPlatform.Priority = 0;
        playerAim.Priority = 0;
        endingCam.Priority = 0;
        endingPlayerCam.Priority = 0;
        fightCam.Priority = 99;

        mainCam.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = blendTime;
    }
    public void FightEndCam(bool playerWin, float blendTime)
    {
        Vector3 pos = Vector3.zero;
        Quaternion rot = Quaternion.identity;

        if (playerWin == true)
        {
            pos = playerWinCamLocTr.position;
            rot = playerWinCamLocTr.rotation;
        }
            
        else
        {
            pos = fakePlayerWinCamLocTr.position;
            rot = fakePlayerWinCamLocTr.rotation;
        }

        endingPlayerCam.transform.position = pos;
        endingPlayerCam.transform.rotation = rot;

        playerPlatform.Priority = 0;
        playerAim.Priority = 0;
        endingCam.Priority = 0;
        endingPlayerCam.Priority = 99;
        fightCam.Priority = 0;

        mainCam.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = blendTime;
    }
}
