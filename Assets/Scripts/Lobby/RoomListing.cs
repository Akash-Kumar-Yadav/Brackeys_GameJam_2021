using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StylizedMultiplayer
{
    public class RoomListing : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject _roomPrefab;
        [SerializeField] private GameObject _roomPrefabParent;

        [SerializeField] private List<GameObject> _roomLists = new List<GameObject>();

        public static Action onLeftRoom;

        private void Awake()
        {
            onLeftRoom += ClearRoomLists;
        }

        private void ClearRoomLists()
        {
            foreach (var item in _roomLists)
            {
                Destroy(item);

            }
            _roomLists.Clear();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            base.OnRoomListUpdate(roomList);
            foreach (var item in roomList)
            {
                if (item.RemovedFromList)
                {
                    int index = _roomLists.FindIndex(x => x.GetComponentInChildren<TMP_Text>().text == item.Name);
                    if (index != -1)
                    {
                        Destroy(_roomLists[index]);
                        _roomLists.RemoveAt(index);

                    }
                }
                else
                {
                    int index = _roomLists.FindIndex(x => x.GetComponentInChildren<TMP_Text>().text == item.Name);
                    if(index == -1)
                    {
                        UpdateRoomList(item);
                    }
                   
                }
            }
            
        }

        private void UpdateRoomList(RoomInfo roomInfo)
        {
            var obj = Instantiate(_roomPrefab, _roomPrefabParent.transform);
            var button = obj.GetComponent<Button>();
            button.GetComponentInChildren<TMP_Text>().text = roomInfo.Name;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => NetworkManager.Instance.JoinRoom(roomInfo.Name));

            _roomLists.Add(button.gameObject);
        }

        public override void OnDisable()
        {
            onLeftRoom += ClearRoomLists;
        }
    }
}