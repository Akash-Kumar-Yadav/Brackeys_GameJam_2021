using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StylizedMultiplayer
{
    public class PlayerController : MonoBehaviour
    {
        private Vector2 _lookPosition;
        
        [SerializeField] private Vector3 _cameraSpawnPoint;
        [SerializeField] private Vector2 _inputs;
        [SerializeField] private float _speed;
        [SerializeField] private PlayerInputs _inputActions;

        private PhotonView _photonView;
        private IRaycasting _raycasting;
        private void Awake()
        {
            if (_photonView == null)
                _photonView = GetComponent<PhotonView>();

            if (!_photonView.IsMine) { return; }

            SpawnCamera();

            _raycasting.UnParentCameraFromPlayer(transform);
            _inputActions = new PlayerInputs();
            _inputActions.Enable();

            _inputActions.Player.Mouse.performed += MouseMovement;
            _inputActions.Player.Move.performed += Movement;
            _inputActions.Player.Move.canceled += Movement;
        }

        private void Movement(InputAction.CallbackContext obj) => _inputs = obj.ReadValue<Vector2>();
        private void MouseMovement(InputAction.CallbackContext obj) => _lookPosition = obj.ReadValue<Vector2>();

        private void Update()
        {
            if (!_photonView.IsMine) { return; }

            Ray ray = _raycasting.GetRay(_lookPosition);

            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                var direction = hit.point - transform.position;
                float angle = Mathf.Atan2(direction.z,direction.x)*Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.AngleAxis(-angle,Vector3.up); 
            }
            Vector3 move = new Vector3(_inputs.x * _speed, 0, _inputs.y * _speed);
            transform.Translate(move * Time.deltaTime);

        }

        private void SpawnCamera()
        {
            var obj = PhotonNetwork.Instantiate(Path.Combine("Camera"),_cameraSpawnPoint,Quaternion.Euler(45,140,0));
            _raycasting = obj.GetComponent<IRaycasting>();
        }
        private void OnDisable()
        {
            if (!_photonView.IsMine) { return; }
            _inputActions.Disable();
        }
    }
}
