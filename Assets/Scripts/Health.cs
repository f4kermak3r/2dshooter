using UnityEngine;

namespace PLATFORMER.Combat
{
    public class Health : MonoBehaviour
    {
        public int health = 100;
        public Animator animator;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            if (isDead) return;

            isDead = true;


            animator.SetTrigger("isDead");
            Debug.Log("enemy died!");

            // die animation;

            animator.SetBool("isDead", true);



            GetComponent<Collider2D>().enabled = true;


            this.enabled = false;

        }
    }
}

