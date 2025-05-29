using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DoroCode
{
    public class MenuManager : MonoBehaviour
    {
        private static MenuManager _instance;
        public static MenuManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<MenuManager>();

                return _instance;
            }
        }

        [SerializeField] private Menu[] menus;

        private void Awake()
        {
            menus = GetComponentsInChildren<Menu>();
        }

        private void Start()
        {
            //CloseMenu();
            //OpenMenu("Main Menu");
        }

        public void OpenMenu(string name)
        {
            for (int i = 0; i < menus.Length; i++)
            {
                if (menus[i].menuName == name && menus[i].open == false)
                    menus[i].Open();

                else if (menus[i].menuName != name)
                {
                    menus[i].Close();
                }
            }
        }

        public void OpenMenu(Menu menu)
        {
            for (int i = 0; i < menus.Length; i++)
            {
                if (menus[i] == menu && menus[i].open == false)
                    menus[i].Open();

                else if (menus[i] != menu)
                {
                    menus[i].Close();
                }
            }
        }

        public void CloseMenu(string name)
        {
            for (int i = 0; i < menus.Length; i++)
            {
                if (menus[i].name == name && menus[i].open == true)
                    menus[i].Close();

            }
        }

        public void CloseMenu(Menu menu)
        {
            for (int i = 0; i < menus.Length; i++)
            {
                if (menus[i] == menu && menus[i].open == true)
                    menus[i].Close();

            }
        }

        public void CloseMenu()
        {
            for (int i = 0; i < menus.Length; i++)
                menus[i].Close();

        }

    }
}

