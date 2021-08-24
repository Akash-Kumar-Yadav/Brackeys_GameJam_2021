using System.Collections;
using UnityEngine;

namespace StylizedMultiplayer
{
    public class UIManager : MonoBehaviour
    {
        #region Singleton
        private static UIManager instance;
        public static UIManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<UIManager>();
                    if (instance == null)
                    {
                        var obj = new GameObject("NetworkManager");
                        obj.AddComponent<UIManager>();
                        instance = obj.GetComponent<UIManager>();

                    }
                }
                return instance;
            }
        }
        #endregion

        [SerializeField] private Menu[] _menus;

        private void Start()
        {
            Open("User Name");
        }
        public void Open(string name)
        {
            foreach (var item in _menus)
            {
                if (item.MenuName == name)
                    item.gameObject.SetActive(true);
                else
                    item.gameObject.SetActive(false);
            }
        }
    }
}