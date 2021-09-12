using Photon.Pun;
using UnityEngine;

namespace StylizedMultiplayer
{
    public class Bullet : MonoBehaviour 
    {
        private Rigidbody _rigidbody;
        PhotonView _photonView;
        
        private void OnEnable() 
        {
           if(_photonView == null)
            _photonView = GetComponent<PhotonView>();

            if(_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
            
             if(!_photonView.IsMine)
            {
                _rigidbody.Sleep();
            }else
            {
                _rigidbody.WakeUp();
            }

            Invoke("DisableGameObject",5); 
        }
        public void SetTrajectory(Vector3 direction,float force)
        {
            if(!_photonView.IsMine) return;

            if(_rigidbody == null){
                _rigidbody = GetComponent<Rigidbody>();
            }
            print("_rigidbody");
            _rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
        private void OnCollisionEnter(Collision collisionInfo)
        {
           gameObject.SetActive(false);
        }

        private void DisableGameObject()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable() 
        {
            if(_photonView == null)
            _photonView = GetComponent<PhotonView>();

            if(_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();


             if(_photonView.IsMine)
            {
                _rigidbody.Sleep();
            }
        }    
    }

}