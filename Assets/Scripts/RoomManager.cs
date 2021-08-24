using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StylizedMultiplayer
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        private static RoomManager instance;

        public static RoomManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<RoomManager>();
                    if (instance == null)
                    {
                        var obj = new GameObject("NetworkManager");
                        obj.AddComponent<RoomManager>();
                        instance = obj.GetComponent<RoomManager>();

                    }
                }
                return instance;
            }
        }
        
        [SerializeField] private PhotonView _photonView;
        [SerializeField] private string _lanucherPrefabName;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else if(instance != this)
            {
                DestroyImmediate(gameObject);
            }
            DontDestroyOnLoad(gameObject);


        }

        public override void OnEnable()
        {
            base.OnEnable();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if(scene.buildIndex == 1)
            {
                PhotonNetwork.Instantiate(Path.Combine(_lanucherPrefabName), Vector3.zero, Quaternion.identity);
            }
        }

        private void Start()
        {
            _photonView = GetComponent<PhotonView>();
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }
    }
}
