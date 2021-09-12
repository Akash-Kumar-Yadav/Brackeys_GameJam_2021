using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

namespace StylizedMultiplayer
{
    public class ObjectPooling : MonoBehaviour
    {
        private PhotonView _photonView;
        [SerializeField] private GameObject _poolPrefab;
        [SerializeField] private List<GameObject> _poolList;
        [SerializeField] private int _poolListAmount;
        [SerializeField] private Transform _bulletSpawnPoint;

        private void Awake() 
        {
            _photonView = GetComponent<PhotonView>();
            _poolList = new List<GameObject>();    
        }

        private void Start() 
        {
            for (int i = 0; i < _poolListAmount; i++)
            {
                GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("Bullet"),
                _bulletSpawnPoint.position,Quaternion.identity);

                bullet.SetActive(false);
                _poolList.Add(bullet);
            }    
        }
        public GameObject GetPooledObject()
        {
           for (int i = 0; i < _poolList.Count; i++)
           {
               if(!_poolList[i].activeInHierarchy)
               {
                   _poolList[i].transform.position = _bulletSpawnPoint.position;
                   _poolList[i].transform.rotation = Quaternion.identity;

                    return _poolList[i].gameObject;
               }
           }

            return null;
        }
    }
}
