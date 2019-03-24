using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction 
    {
        //shows up in the unity editor
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        // this is of type health because it is eaier to access
        Health target;

        float timeSinceLastAtack = 0;

        //player basically moves to targets position
        private void Update()
        {
            //we do this becuase we might run away from an enemy and fight them again
            timeSinceLastAtack += Time.deltaTime;

            //if target doesnt exist forget about the if statements
            if(target == null) return;
            if(target.IsDead()) return;

            //if the target is not in range stop it
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        //this part of the code executes the "attack" animation and makes it more smooth
        //
        private void AttackBehaviour()
        {
            if (timeSinceLastAtack > timeBetweenAttacks)
            {
                transform.LookAt(target.transform);
                TriggerAttack();
                timeSinceLastAtack = 0;
            }
        }

        // this will trigger the hit event
        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        //Animation Event
        //this part of the code executes the animation
        void Hit()
        {
            if(target == null)
            {
                return;
            }
            //calls the health componenet to deal the damage
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            //check to see if target is in range by getting distance between two vectors
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        //ignores dead enimies such as their hit box (checks is they are dead)
        public bool CanAttack(CombatTarget combatTarget)
        {
            //if we click on the terrain instead of target return false
            if(combatTarget == null)
            {
                return false;
            }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(CombatTarget combatTarget)
        {
            //calls the script for cancelling the action
            GetComponent<ActionScheduler>().StartAction(this);
            //player will know weather to attack a target or not
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        //stops the attack as soon as you click away, makes it more smooth
        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}