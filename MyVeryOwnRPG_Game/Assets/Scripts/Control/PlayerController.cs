using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using System;

//this namespace lets us use player controller when in another namespace (using "...') 
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            //if these methods are true retun them
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        //returns all of the things that it hits
        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            //execute block for each hit of the targets that were hit
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                //if it can attack then go to the next thing in the for loop
                if(!GetComponent<Fighter>().CanAttack(target))
                {
                    continue;
                }

                //attacks the target
                if(Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                return true;

            }
            //return false if no targets were located bascially charcter doesnt move if target is hit
            return false;
        }

        //[Raycasting] 
        private bool InteractWithMovement()
        {
            RaycastHit hit;
            //allows us to return information about the location that a raycast has hit
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    //this gets the mover component from the other file
                    //we are going to move to the point the mouse button hits
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        //the camera will detect and object even if target is not in sight
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}