using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace StylizedMultiplayer
{
    public class PlayerListUI : Menu
    {
        [SerializeField] private Button _start;
        [SerializeField] private Button _back;

        private void Start()
        {
            _start.onClick.AddListener(delegate { StartGame(); });
            _back.onClick.AddListener(delegate { Backbutton(); });
        }

        private void StartGame()
        {
            NetworkManager.Instance.StartGame("Game");
        }

        private void Backbutton()
        {
            NetworkManager.Instance.LeavePlayerList();
        }
    }
}