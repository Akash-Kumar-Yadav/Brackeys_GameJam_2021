using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StylizedMultiplayer
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        private static NetworkManager instance;

        public static NetworkManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<NetworkManager>();
                    if (instance == null)
                    {
                        var obj = new GameObject("NetworkManager");
                        obj.AddComponent<NetworkManager>();
                        instance = obj.GetComponent<NetworkManager>();

                    }
                }
                return instance;
            }
        }
       
        public override void OnConnected()
        {
            base.OnConnected();
            print("Connected");
        }
        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            print("Connected To master");
            PhotonNetwork.JoinLobby();

        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            print("Joined Lobby");
            UIManager.Instance.Open("Room List");
          
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            print("Room Name = " + PhotonNetwork.CurrentRoom.Name);
            UIManager.Instance.Open("Player List");
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            print("Joined Room" + PhotonNetwork.NickName);
            UIManager.Instance.Open("Player List");

            Invoke("UpdatePlayerListInRoom", 1f);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            print("Player Entered");
            PlayerListing.OnRemotePlayerJoined?.Invoke();
           
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            print("Player left");
            PlayerListing.OnRemotePlayerLeft?.Invoke();
        }

        private void UpdatePlayerListInRoom()
        {
            PlayerListing.OnPlayerJoined?.Invoke();
        }
        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            RoomListing.onLeftRoom?.Invoke();
            UIManager.Instance.Open("Room List");
            PlayerListing.OnPlayerLeft?.Invoke();
        }
        
        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            print("Disconnected");
            UIManager.Instance.Open("User Name");
        }

        public void ConnectToMaster(string playerName)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.NickName = playerName;
        }

        public void CreateRoom(string roomName)
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 4;
            options.IsOpen = true;
            options.IsVisible = true;
            PhotonNetwork.CreateRoom(roomName, options);
        }

        public void JoinRoom(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
        }

        public void LeaveRoomList()
        {
            PhotonNetwork.Disconnect();
        }

        public void StartGame(string levelName)
        {
            if (!PhotonNetwork.IsMasterClient) 
            {
                ErrorText.Instance.DisplayText("Sorry, Only host can start the game.");
                return;
            }
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(levelName);
        }
        public void LeavePlayerList()
        {
            PhotonNetwork.LeaveRoom();
        }


    }
}
