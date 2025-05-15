using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_menu : MonoBehaviour {

     [SerializeField] private GameObject transitionPrefab;
     private CanvasGroup transitionCanvas;
     private Animator transitionAnimator;
     private float transitionTime;
     private void Start() {
          transitionCanvas = transitionPrefab.GetComponentInChildren<CanvasGroup>();
          transitionAnimator = transitionPrefab.GetComponent<Animator>();
          transitionTime = transitionAnimator.GetCurrentAnimatorStateInfo(0).length;

          transitionCanvas.alpha = 0f;
          transitionCanvas.interactable = false;
          transitionCanvas.blocksRaycasts = false;
     }
     public void Play() {
          StartCoroutine(LoadSceneTransition(1));
     }
     private IEnumerator LoadSceneTransition(int sceneIndex) {
          transitionCanvas.alpha = 1f;
          transitionCanvas.interactable = true;
          transitionCanvas.blocksRaycasts = true;

          transitionAnimator.SetTrigger("Start");

          yield return new WaitForSeconds(transitionTime + 0.5f);

          SceneManager.LoadSceneAsync(sceneIndex);
     }

     public void FUCKYOU() {
          Application.Quit();
     }
}
