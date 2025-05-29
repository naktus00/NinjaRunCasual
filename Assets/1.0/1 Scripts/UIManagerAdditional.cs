using System;
using UnityEngine;

public class UIManagerAdditional : MonoBehaviour
{
    private static UIManagerAdditional _instance;
    public static UIManagerAdditional Instance { get { return _instance; } }

    [SerializeField] private RectTransform[] _images;

    private void Awake()
    {
        #region SINGLETON
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion

        Open(IMAGES.APPSTARTING);
    }

    public void Open(IMAGES images)
    {
        int index = -99;

        switch (images)
        {
            case IMAGES.APPSTARTING:
                index = 0;
                break;
            case IMAGES.MATCHSTARTING:
                index = 1;
                break;
            case IMAGES.RETURNINGMENU:
                index = 2;
                break;
            case IMAGES.WAITININGONLINEMODE:
                index = 3;
                break;
        }

        for (int i = 0; i < _images.Length; i++)
        {
            if (i == index)
                _images[i].gameObject.SetActive(true);
            else
                _images[i].gameObject.SetActive(false);
        }

        //for (int i = 0; i < _images.Length; i++)
        //{
        //    if (i == index && _images[i].gameObject.activeInHierarchy == false)
        //        _images[i].gameObject.SetActive(true);
        //    else if (_images[i].gameObject.activeInHierarchy == true)
        //        _images[i].gameObject.SetActive(false);

        //}
    }
    public void Close(IMAGES images)
    {
        int index = -99;

        switch (images)
        {
            case IMAGES.APPSTARTING:
                index = 0;
                break;
            case IMAGES.MATCHSTARTING:
                index = 1;
                break;
            case IMAGES.RETURNINGMENU:
                index = 2;
                break;
            case IMAGES.WAITININGONLINEMODE:
                index = 3;
                break;
        }

        if (_images[index].gameObject.activeInHierarchy == true)
            _images[index].gameObject.SetActive(false);

        //for (int i = 0; i < _images.Length; i++)
        //{
        //    if (i == index && _images[i].gameObject.activeInHierarchy == true)
        //        _images[i].gameObject.SetActive(false);
        //}
    }
    public void Close()
    {
        for (int i = 0; i < _images.Length; i++)
            _images[i].gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }

    [Serializable]
    public enum IMAGES
    {
        APPSTARTING,
        MATCHSTARTING,
        RETURNINGMENU,
        WAITININGONLINEMODE
    }
}
