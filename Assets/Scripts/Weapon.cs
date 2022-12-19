using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PLATFORMER.Combat
{
    public class Weapon : MonoBehaviour
    {

        // gun
        // shoots +
        // pick up gun +
        // drop gun -
        // switch gun -
        [SerializeField] Animator animator;

        public GameObject equippedPrefab = null;

        [SerializeField] float weaponRange = 1.15f;

        [SerializeField] float weaponDamage = 5f;

        public Transform firePoint;
        public GameObject bulletPrefab;

        public void Spawn(Animator animator)
        {
            animator.SetBool("weaponOn", true);
        }

        public void Attack()
        {
            if (bulletPrefab != null)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
        }

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetRange()
        {
            return weaponRange;
        }

    }
}

