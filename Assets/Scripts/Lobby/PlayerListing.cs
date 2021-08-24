using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace StylizedMultiplayer
{
    public class PlayerListing : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject _PlayerPrefab;
        [SerializeField] private GameObject _playerPrefabParent;

        [SerializeField] private List<GameObject> _playerLists = new List<GameObject>();

        public static Action OnPlayerJoined;
        public static Action OnRemotePlayerJoined;
        public static Action OnRemotePlayerLeft;
        public static Action OnPlayerLeft;
      

        public override void OnEnable()
        {
            OnPlayerJoined += UpdatePlayerListing;
            OnPlayerLeft += ClearPlayerListing;
            OnRemotePlayerJoined += PlayerListingForRemotePlayers;
            OnRemotePlayerLeft += PlayerListingForRemotePlayers;
            
        }
        
        private void PlayerListingForRemotePlayers()
        {
            ClearPlayerListing();
            UpdatePlayerListing();
        }

        private void UpdatePlayerListing()
        {
            foreach (var item in PhotonNetwork.PlayerList)
            {
                var obj = Instantiate(_PlayerPrefab, _playerPrefabParent.transform);
                obj.GetComponentInChildren<TMP_Text>().text = item.NickName;
                _playerLists.Add(obj);
            }
            print("ggggggggggggggggggggggggggg");
        }

        private void ClearPlayerListing()
        {
            foreach (var item in _playerLists)
            {
                Destroy(item);
            }
            _playerLists.Clear();
        }
        public override void OnDisable()
        {
            ClearPlayerListing();
            OnPlayerJoined -= UpdatePlayerListing;
            OnRemotePlayerJoined -= PlayerListingForRemotePlayers;
            OnRemotePlayerLeft -= PlayerListingForRemotePlayers;
            OnPlayerLeft -= ClearPlayerListing;
        }

    }
}