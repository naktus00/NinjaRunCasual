using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private static GameSceneManager _instance;
    public static GameSceneManager Instance { get { return _instance; } }

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
    private void Start()
    {
        StartCoroutine(IEStart());
    }
    private IEnumerator IEStart()
    {
        yield return new WaitUntil(() => DataManager.Instance.user != null);
        LoadSceneAsync(GAMESCENES.MAIN);
    }
    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }

    public void LoadSceneAsync(GAMESCENES pGameScene)
    {
        StartCoroutine(IELoadSceneAsync(pGameScene));
    }
    IEnumerator IELoadSceneAsync(GAMESCENES pGameScene)
    {
        string sceneName = string.Empty;

        switch (pGameScene)
        {
            case GAMESCENES.APPSTARTING:
                sceneName = "App Starting";
                break;
            case GAMESCENES.MAIN:
                sceneName = "Menu Scene";
                break;
            case GAMESCENES.GAME:
                sceneName = "Game Scene";
                break;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = true;

        //if (pGameScene == GAMESCENES.MAIN)
        //{
        //    UIManagerAdditional.Instance.Open(UIManagerAdditional.IMAGES.RETURNINGMENU);
        //}
        //else if (pGameScene == GAMESCENES.GAME)
        //{
        //    UIManagerAdditional.Instance.Open(UIManagerAdditional.IMAGES.MATCHSTARTING);
        //}

        yield return new WaitUntil(() => asyncLoad.isDone == true);

        //UIManagerAdditional.Instance.Close();
    }
}

public enum GAMESCENES
{
    APPSTARTING,
    MAIN,
    GAME
}
