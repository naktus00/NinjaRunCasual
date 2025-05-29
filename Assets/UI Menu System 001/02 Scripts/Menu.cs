using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoroCode
{
    public abstract class Menu : MonoBehaviour
    {
        public enum MenuType { Main, Gameplay, Settings, Market }

        public string menuName;
        public MenuType menuType;
        public bool open;

        private void Awake()
        {
            this.gameObject.name = menuName;
        }

        public virtual void Open()
        {
            open = true;
            this.gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            open = false;
            this.gameObject.SetActive(false);
        }

    }
}

