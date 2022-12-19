using UnityEngine;
using PLATFORMER.Combat;
using System;

namespace PLATFORMER.Control
{
    public class PlayerController : MonoBehaviour
    {
        public CharacterController2D controller;
        public Animator animator;
        [SerializeField] Transform player;
        Weapon weapon;
        Health health;
        float horizontalMove = 0f;
        bool jump = false;
        bool crouch = false;
        public float runSpeed = 40f;
        float timeSinceLastPunch = Mathf.Infinity;
        float timeSinceLastShot = Mathf.Infinity;
        float punchCoolDown = 1f;
        float bulletCooldown = 0.35f;

        private void Start()
        {
            weapon = GetComponent<Weapon>();
            health = GetComponent<Health>();
        }

        // player pick up weapon
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Weapon") && player.gameObject.CompareTag("Player"))
            {
                Destroy(other.gameObject);
                animator.SetBool("weaponOn", true);
            }
        }

        private void Update()
        {
            timeSinceLastPunch += Time.deltaTime;
            timeSinceLastShot += Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && animator.GetBool("weaponOn") && timeSinceLastShot > bulletCooldown)
            {
                timeSinceLastShot = 0;
                weapon.Attack();
            }

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("speed", Mathf.Abs(horizontalMove));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump = true;
                animator.SetBool("isJumping", true);
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                crouch = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                crouch = false;
            }

            // spawn with a weapon of ur choice for later implementations
            if (weapon.GetComponent<Weapon>().equippedPrefab != null)
            {
                weapon.Spawn(animator);
            }

            PunchAttack();

        }

        private void PunchAttack()
        {
            if (Input.GetMouseButtonDown(1) && timeSinceLastPunch > punchCoolDown && !animator.GetBool("weaponOn"))
            {
                animator.SetTrigger("isPunching");
                timeSinceLastPunch = 0;
            }
        }

        public void OnLanding()
        {
            animator.SetBool("isJumping", false);
        }

        public void OnCrouching(bool isCrouching)
        {
            animator.SetBool("isCrouching", isCrouching);
        }

        void FixedUpdate()
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
    }
}
