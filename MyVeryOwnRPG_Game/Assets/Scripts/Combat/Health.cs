using UnityEngine;


namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        //shows up in the unity editor
        [SerializeField] float healthpoints = 100f; 

        bool isDead = false;

        //just a public way of checking if bad guy is dead
        public bool IsDead()
        {
            return isDead;
        }

        //this method makes objects take damage
        public void TakeDamage(float damage)
        {
            healthpoints = Mathf.Max(healthpoints - damage , 0);
            print(healthpoints);

            //ayy we did this by ourself bad guy will die if thier health is 0
            if(healthpoints == 0)
            {
                Die();
            }
        }

        //'has exit time' means that the transition will play after the animation is done
        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
        }

    }  
}
