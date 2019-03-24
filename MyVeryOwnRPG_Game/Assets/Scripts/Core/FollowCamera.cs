using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this namespace lets us use follow camera when in another namespace (using "...') 
namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        //transform is the position of the player in the world
        [SerializeField] Transform target;

        //prevents camera from moving first nad it follows the player
        void LateUpdate()
        {
            //this is adding the 3 munbers of position to 3 number of vector
            transform.position = target.position;
        }

    }
}
