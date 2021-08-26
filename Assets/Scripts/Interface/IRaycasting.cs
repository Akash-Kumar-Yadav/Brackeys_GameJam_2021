using System.Collections;
using UnityEngine;

namespace StylizedMultiplayer
{
    public interface IRaycasting 
    {
        Ray GetRay(Vector2 lookPosition);
        Transform UnParentCameraFromPlayer(Transform target);
      
    }
}