using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        //shows up in the unity editor
        [SerializeField] Transform target;

        NavMeshAgent navMeshAgent;

        //makes code easier to read by only calling it once
        private void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        //makes sure we can cancel attacking the player with movement
        public void StartMoveAction(Vector3 destination)
        {
            //calls the script for cancelling the action
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        //this is the mover method
        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        //stops player within range
        public void Cancel() 
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            //the player will be animated based on the animations (will run, walk etc)
            Vector3 velocity = navMeshAgent.velocity;

            //Transforms a direction from world space to local space
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            //makes the character move forward
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }

    }
}
