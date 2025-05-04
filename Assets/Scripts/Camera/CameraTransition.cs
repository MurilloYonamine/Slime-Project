using UnityEngine;
namespace CAMERA
{
    public class CameraTransition : MonoBehaviour
    {
        [SerializeField] private bool playerHasPassed = false;
        private void OnTriggerEnter2D(Collider2D collision2D)
        {
            if (collision2D.CompareTag("Player")) {
                CameraManager.Instance.ChangeCurrentCamera(playerHasPassed);
                playerHasPassed = !playerHasPassed;
            }
        }
    }
}