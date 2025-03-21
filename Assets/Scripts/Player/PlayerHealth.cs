using System;
using UnityEngine;
using UnityEngine.UI;
namespace PLAYER {
    [Serializable]
    public class PlayerHealth {

        public float Health;
        [HideInInspector] public float MaxHealth;

        public Image healthBar;
        public void Initialize() {
            MaxHealth = Health;
        }
        public void OnUpdate() {
            healthBar.fillAmount = Mathf.Clamp(Health / MaxHealth, 0, 1);
        }
    }
}