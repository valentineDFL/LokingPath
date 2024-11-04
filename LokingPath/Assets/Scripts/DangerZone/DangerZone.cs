using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.DangerZone;
using Assets.Scripts.HeroShield;
using Assets.Scripts.Move;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.DangerZone
{
    public class DangerZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            TryDestroyHero(other, nameof(OnTriggerEnter));
        }

        private void OnTriggerStay(Collider other)
        {
            TryDestroyHero(other, nameof(OnTriggerStay));
        }

        private void OnTriggerExit(Collider other)
        {
            TryDestroyHero(other, nameof(OnTriggerExit));
        }

        private void TryDestroyHero(Collider other, string whereCall)
        {
            bool otherIsShield = other.TryGetComponent<HeroShield.HeroShield>(out HeroShield.HeroShield heroShield);
            bool heroHaveHealth = heroShield.TryGetComponent<HeroHealth.HeroHealth>(out HeroHealth.HeroHealth heroHealth);

            if (otherIsShield && !heroShield.ShieldEnabled && heroHaveHealth && heroHealth.IsAlive == true)
            {
                print($"CalledGrom: {whereCall}");
                heroHealth.TakeDamage();
            }
        }
    }
}
