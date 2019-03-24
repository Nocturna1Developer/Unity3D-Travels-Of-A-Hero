using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Contol
{
    public class PatrolPath : MonoBehaviour 
    {
        const float waypointGizmosRadius = .3f;

        private void OnDrawGizmos() 
        {
            //loops through the children of patrol path
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(transform.GetChild(i).position, waypointGizmosRadius);

            }
        }
    }
}