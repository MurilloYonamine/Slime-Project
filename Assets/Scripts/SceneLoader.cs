using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string nextScene;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(NextSceneAnimation());
        }
    }
    private IEnumerator NextSceneAnimation() {
        StartCoroutine(TransitionManager.Instance.MenuStartTransition());
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextScene);
    }
}
