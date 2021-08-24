using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace StylizedMultiplayer
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PhotonView _photonView;
        [SerializeField] private string _playerCharacterPrefabName;
        [SerializeField] private string _camera;
        [SerializeField] private Transform _cameraSpawnPosition;
        

        private void Start()
        {
            _photonView = GetComponent<PhotonView>();

            if (_photonView.IsMine)
            {
                _cameraSpawnPosition = GameObject.Find("Camera Spawn Position").transform;
                Vector3 spawnPosition = new Vector3(7,11.69f,Random.Range(11,16));

                PhotonNetwork.Instantiate(Path.Combine(_playerCharacterPrefabName), spawnPosition,Quaternion.Euler(0,-90,0));
                PhotonNetwork.Instantiate(Path.Combine(_camera), _cameraSpawnPosition.position, _cameraSpawnPosition.rotation);
            }
        }
    }
}
