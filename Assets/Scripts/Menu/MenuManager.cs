using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace MENU {
    public class MenuManager : MonoBehaviour {
        [Header("Buttons")]
        [SerializeField] private Button jogarButton;
        [SerializeField] private Button creditosButton;
        [SerializeField] private Button opcoesButton;
        [SerializeField] private Button sairButton;

        private Menu menu;

        [SerializeField] private GameObject transitionPrefab;

        [SerializeField] private string firstScene = "Fase_1";

        private void Awake() {
            menu = new Menu(transitionPrefab);
            StartCoroutine(menu.HandleTransition("End", moreTime: 0.5f));
        }

        private void Start() {
            jogarButton.onClick.AddListener(() => Jogar());
            creditosButton.onClick.AddListener(() => Creditos());
            opcoesButton.onClick.AddListener(() => Opcoes());
            sairButton.onClick.AddListener(() => Sair());
        }

        public void Jogar() {
            StartCoroutine(TransitionManager.Instance.MenuStartTransition());
            SceneManager.LoadScene(firstScene);
        }

        public void Creditos() {

        }
        public void Opcoes() {

        }
        public void Sair() {

        }
    }
}