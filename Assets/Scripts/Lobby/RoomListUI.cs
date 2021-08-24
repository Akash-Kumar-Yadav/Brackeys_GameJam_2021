using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StylizedMultiplayer
{
    public class RoomListUI : Menu
    {
        [SerializeField] private Button _createRoom;
        [SerializeField] private Button _back;
        [SerializeField] private TMP_InputField _roomNameInputfield;

        private void Start()
        {
            _createRoom.onClick.AddListener(delegate { CreateRoomButton(_roomNameInputfield.text); });
            _back.onClick.AddListener(delegate { Backbutton(); });
        }


        private void CreateRoomButton(string text)
        {
            if (string.IsNullOrEmpty(_roomNameInputfield.text))
            {
                ErrorText.Instance.DisplayText("Room Name Can't be empty");
                return;
            }
            NetworkManager.Instance.CreateRoom(text);
            
        }
        private void Backbutton()
        {
            NetworkManager.Instance.LeaveRoomList();
        }
    }
}