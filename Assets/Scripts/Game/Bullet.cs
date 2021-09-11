using UnityEngine;

namespace StylizedMultiplayer
{
    public class Bullet : MonoBehaviour 
    {
        private Rigidbody _rigidbody;
        private void Start() 
        {
            _rigidbody = GetComponent<Rigidbody>();
            Destroy(gameObject,5f);    
        }

        public void SetTrajectory(Vector3 direction,float force)
        {
            if(_rigidbody == null){
                _rigidbody = GetComponent<Rigidbody>();
            }
            print("_rigidbody");
            _rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
        private void OnCollisionEnter(Collision collisionInfo)
        {
            Destroy(gameObject);
        }    
    }

}