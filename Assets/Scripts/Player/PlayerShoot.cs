using AUDIO;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace PLAYER {
    [Serializable]
    public class PlayerShoot {
        private PlayerController player;
        private PlayerHealth playerHealth;

        [SerializeField] private float bulletSpeed = 50f;

        [SerializeField] private RectTransform aimPrefab;
        [SerializeField] private GameObject bulletPrefab;

        private Camera mainCamera;
        private Canvas canvas;

        [SerializeField] private float bulletDamage = 1f;
        [SerializeField] private GameObject hitEffect;

        [SerializeField] private GameObject playerEyes;
        [SerializeField] private float eyesHeight;

        public void Initialize(PlayerController player, PlayerHealth playerHealth) {
            this.player = player;
            this.playerHealth = playerHealth;

            mainCamera = Camera.main;
            canvas = aimPrefab.GetComponentInParent<Canvas>();
        }

        public void OnUpdate() {
            if (!CheatManager.Instance.canMouseControl) return;

            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 anchoredPos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                mousePosition,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
                out anchoredPos
            );

            aimPrefab.anchoredPosition = anchoredPos;
            aimPrefab.gameObject.SetActive(!GameManager.Instance.isPaused);

            HandlePlayerEyes(mousePosition.x, mousePosition.y);
        }
        private void HandlePlayerEyes(float x, float y) {
            Vector3 eyesOrigin = new Vector3(0f, eyesHeight, 0f);
            Vector3 worldMouse = mainCamera.ScreenToWorldPoint(new Vector3(x, y, mainCamera.nearClipPlane));
            worldMouse.z = player.transform.position.z;
            Vector3 dir = (worldMouse - player.transform.position).normalized;

            float maxOffset = 0.1f;
            Vector3 offset = dir * maxOffset;

            playerEyes.transform.localPosition = eyesOrigin + offset;
        }

        public void Shoot(InputAction.CallbackContext context) {
            if (context.started && playerHealth.currentHealth > 0 && CheatManager.Instance.canMouseControl) {
                if (!CheatManager.Instance.invunerable) playerHealth.currentHealth -= 1;
                playerHealth.SizeChange();

                Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                mousePosition.z = player.transform.position.z;

                Vector3 shootDirection = (mousePosition - player.transform.position).normalized;

                GameObject bullet = GameObject.Instantiate(bulletPrefab, player.transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);

                bullet.AddComponent<PlayerBullet>();
                bullet.GetComponent<PlayerBullet>().bulletDamage = bulletDamage;
                bullet.GetComponent<PlayerBullet>().hitEffect = hitEffect;
                bullet.GetComponent<PlayerBullet>().player = player.gameObject;

                bullet.GetComponent<Rigidbody2D>().linearVelocity = shootDirection * bulletSpeed;

                AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_shot", volume: 1f, pitch: 1.5f);
            }
        }
    }
}