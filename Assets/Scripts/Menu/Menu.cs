using System.Collections;
using UnityEngine;

namespace MENU {
    public class Menu {
        private GameObject gameObject;
        private CanvasGroup canvas;
        private Animator animator;
        private float timer;

        public Menu(GameObject transitionPrefab) {
            this.gameObject = transitionPrefab;

            canvas = transitionPrefab.GetComponentInChildren<CanvasGroup>();
            animator = transitionPrefab.GetComponent<Animator>();
            timer = animator.GetCurrentAnimatorStateInfo(0).length;

            canvas.alpha = 0f;
            canvas.interactable = false;
            canvas.blocksRaycasts = false;
        }
        public IEnumerator HandleTransition(string animationName, float moreTime) {
            animator.SetTrigger(animationName);
            yield return new WaitForSeconds(timer + moreTime);
        }
        public IEnumerator HandleTransition(string animationName) {
            animator.SetTrigger(animationName);
            yield return new WaitForSeconds(timer);
        }
        public void SetVisibility(CanvasGroup section, bool isVisible) {
            section.alpha = isVisible ? 1 : 0;
            section.blocksRaycasts = isVisible;
            section.interactable = isVisible;
            //section.gameObject.SetActive(isVisible);
        }
    }
}