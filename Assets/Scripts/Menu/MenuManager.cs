using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace MENU {
    public class MenuManager : MonoBehaviour {
        [Header("Canvas Menus")]
        [SerializeField] private CanvasGroup mainMenuCanvas;
        [SerializeField] private CanvasGroup optionsCanvas;
        [SerializeField] private CanvasGroup creditsCanvas;

        private Menu menu;

        [SerializeField] private GameObject transitionPrefab;

        [SerializeField] private string firstScene = "Fase_1";

        private void Awake() {
            menu = new Menu(transitionPrefab);
            Cursor.visible = true;
            StartCoroutine(menu.HandleTransition("End", moreTime: 0.5f));
        }
        private void Start() {
            HandleVisibility(mainMenuCanvas, hide: false);
            HandleVisibility(optionsCanvas, hide: true);
            HandleVisibility(creditsCanvas, hide: true);
        }
        public void Jogar() {
            StartCoroutine(StartGame());
        }
        private IEnumerator StartGame() {
            yield return menu.HandleTransition("Start", moreTime: 0.5f);
            SceneManager.LoadSceneAsync(firstScene, LoadSceneMode.Single);
        }
        public void Creditos() {
            HandleVisibility(mainMenuCanvas, hide: true);
            HandleVisibility(creditsCanvas, hide: false);
        }
        public void Opcoes() {
            HandleVisibility(mainMenuCanvas, hide: true);
            HandleVisibility(optionsCanvas, hide: false);
        }
        public void VoltarProMenuPeloCreditos() {
            HandleVisibility(mainMenuCanvas, hide: false);
            HandleVisibility(creditsCanvas, hide: true);
        }
        public void VoltarProMenuPeloOpcoes() {
            HandleVisibility(mainMenuCanvas, hide: false);
            HandleVisibility(optionsCanvas, hide: true);
        }
        public void Sair() {
            StartCoroutine(QuitAnimation());
        }
        private IEnumerator QuitAnimation() {
            yield return menu.HandleTransition("Start", moreTime: 0.5f);
            Application.Quit();
        }
        private void HandleVisibility(CanvasGroup canvas, bool hide) {
            canvas.alpha = hide ? 0 : 1;
            canvas.interactable = !hide;
            canvas.blocksRaycasts = !hide;
        }
    }
}