using UnityEngine;
using UnityEngine.UI;
namespace PLAYER {
    public class PlayerHealth : MonoBehaviour {
        public float MaxHealth;
        public float Health;

        public Image healthBar;
        void Start() {
            MaxHealth = Health;
        }

        void Update() {
            healthBar.fillAmount = Mathf.Clamp(Health / MaxHealth, 0, 1);
        }
    }
}