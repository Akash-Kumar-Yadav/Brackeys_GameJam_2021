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
        [SerializeField] private Vector3 _cameraSpawnPoint;
        [SerializeField] private Vector2 _inputs;
        [SerializeField] private float _speed;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private PlayerInputs _inputActions;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;

        private PhotonView _photonView;
        private Ray _ray;
        private Vector2 _lookPosition;
        private Vector3 _move;
        private int _animatorWalk;

        private IRaycasting _raycasting;

        private void Awake()
        {
            if (_photonView == null)
                _photonView = GetComponent<PhotonView>();

            if (!_photonView.IsMine) 
            {
                GetComponent<Rigidbody>().Sleep();
                return;
            }

            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _animatorWalk = Animator.StringToHash("Walk");

            SpawnCamera();

            _raycasting.UnParentCameraFromPlayer(transform);
            _inputActions = new PlayerInputs();
            _inputActions.Enable();

            _inputActions.Player.Mouse.performed += MouseMovement;
            _inputActions.Player.Move.performed += Movement;
            _inputActions.Player.Move.canceled += Movement;
            _inputActions.Player.LeftClick.started += Shoot;
            
        }

        private void Shoot(InputAction.CallbackContext obj) => Fire();

        private void Movement(InputAction.CallbackContext obj) => _inputs = obj.ReadValue<Vector2>();
        private void MouseMovement(InputAction.CallbackContext obj) => _lookPosition = obj.ReadValue<Vector2>();

        private void Update()
        {
            if (!_photonView.IsMine) { return; }

            _ray = _raycasting.GetRay(_lookPosition);
            _move = new Vector3(_inputs.x * _speed, 0, _inputs.y * _speed);

            _animator.SetFloat(_animatorWalk, _move.normalized.magnitude);
        }
        private void FixedUpdate()
        {
            if (!_photonView.IsMine) { return; }

            if (Physics.Raycast(_ray, out RaycastHit hit))
            {
                var direction = hit.point - transform.position;
                float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg - 90;
                CharacterRotation(angle);
            }
            CharacterMovement();
        }
        private void CharacterMovement() => _rigidbody.MovePosition(transform.position + transform.TransformDirection(_move) * Time.deltaTime);
        private void CharacterRotation(float angle) => _rigidbody.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
        private void SpawnCamera()
        {
            var obj = PhotonNetwork.Instantiate(Path.Combine("Camera"),_cameraSpawnPoint,Quaternion.Euler(45,140,0));
            _raycasting = obj.GetComponent<IRaycasting>();
        }

        private void Fire()
        {
            GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("Bullet"),
            _bulletSpawnPoint.position,Quaternion.identity);
           
            bullet.GetComponent<Bullet>().SetTrajectory(transform.forward,1000);
          
        }
        private void OnDisable()
        {
            if (!_photonView.IsMine) { return; }
            _inputActions.Disable();
        }
    }
}
