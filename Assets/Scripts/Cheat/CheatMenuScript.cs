using UnityEngine;
using UnityEngine.UI;

namespace CHEAT {
    public class CheatMenuScript : MonoBehaviour {
        [SerializeField] private Button check1;
        [SerializeField] private Button check2;
        [SerializeField] private Button check3;

        private void Start() {
            ButtonThemButtons();
        }
        private void ButtonThemButtons() {
            check1.onClick.AddListener(() => telepotato(0));
            check2.onClick.AddListener(() => telepotato(1));
            check3.onClick.AddListener(() => telepotato(2));
        }
        // Update is called once per frame

        private void telepotato(int piss) {
            CheatManager.Instance.Teleportation(piss);
        }

    }
}