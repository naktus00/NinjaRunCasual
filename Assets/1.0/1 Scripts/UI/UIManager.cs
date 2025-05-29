using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIManager : MonoBehaviour
{
    [Header("MENUS")]
    [SerializeField] protected UIMenu[] _menus;

    public void OpenMenu(string targetMenuName)
    {
        foreach (UIMenu menu in _menus)
        {
            if (menu.menuName == targetMenuName)
            {
                menu.Open();
            }
            else
            {
                if (menu.open == true)
                    menu.Close();
            }
        }
    }
    public void OpenMenu(UIMenu targetMenu)
    {
        foreach (UIMenu menu in _menus)
        {
            if (menu == targetMenu)
            {
                menu.Open();
            }
            else
            {
                if (menu.open == true)
                    menu.Close();
            }
        }
    }
    public void CloseMenu(string targetMenuName)
    {
        foreach (UIMenu menu in _menus)
        {
            if (menu.menuName == targetMenuName)
            {
                if (menu.open == true)
                    menu.Close();

                break;
            }
        }
    }
    public void CloseMenu(UIMenu targetMenu)
    {
        if (targetMenu.open == true)
            targetMenu.Close();
    }
    public void CloseMenu()
    {
        foreach (UIMenu menu in _menus)
            menu.Close();
    }
    public bool IsMenuOpen(string targetMenuName)
    {
        bool open = false;

        foreach (UIMenu menu in _menus)
        {
            if (menu.menuName == targetMenuName)
            {
                open = menu.open;
                break;
            }
        }

        return open;
    }
}
