using System.Collections;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

namespace StylizedMultiplayer
{
    public class Raycasting : MonoBehaviour,IRaycasting
    {
        [SerializeField] private Camera _main;
        [SerializeField] private PhotonView _photonView;

        [SerializeField] private Vector3 _cameraPosition;
        [SerializeField] private Vector3 _cameraRotation;

        [SerializeField] private Transform _followTarget;
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

        private void Awake()
        {
            if(_photonView == null)
            {
                _photonView = GetComponent<PhotonView>();
            }
            if (_photonView.IsMine == false)
            {
                // Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
        public Ray GetRay(Vector2 lookPosition) => _main.ScreenPointToRay(lookPosition);

        public Transform UnParentCameraFromPlayer(Transform target)
        {
            _cinemachineVirtualCamera.Follow = target;
            _cinemachineVirtualCamera.LookAt = target;
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(-12, 22, 14);
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineComposer>();
            return transform.parent = null;
        }

        
    }
}