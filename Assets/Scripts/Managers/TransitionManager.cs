using System.Collections;
using UnityEngine;

public class TransitionManager : MonoBehaviour {
    public static TransitionManager Instance { get; private set; }

    [Header("Transitions")]
    [SerializeField] private GameObject deathObject;
    [SerializeField] private GameObject menuObject;
    private Animator deathAnimator;
    private Animator menuAnimator;

    private float deathDuration;
    private float menuDuration;

    public bool IsTransitioning { get; private set; } = false;

    // Inicializa o singleton, componentes de anima��o e CanvasGroup, e define dura��es das transi��es.
    private void Awake() {
        if (Instance == null) Instance = this;
        else DestroyImmediate(gameObject);

        deathAnimator = deathObject.GetComponent<Animator>();
        deathDuration = deathAnimator.GetCurrentAnimatorStateInfo(0).length;

        menuAnimator = menuObject.GetComponent<Animator>();
        menuDuration = menuAnimator.GetCurrentAnimatorStateInfo(0).length;
    }

    // Executa a transi��o de morte, chama uma a��o no meio e controla o estado de transi��o.
    public IEnumerator PlayDeathTransition(System.Action onMidTransition) {
        IsTransitioning = true;
        deathAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(deathDuration / 2f);
        onMidTransition?.Invoke();

        yield return new WaitForSeconds(deathDuration / 2f);
        IsTransitioning = false;
    }

    // Inicia a anima��o de transi��o do menu e espera sua dura��o.
    public IEnumerator MenuStartTransition() {
        menuAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(menuDuration + 0.5f);
    }

    // Finaliza a anima��o de transi��o do menu e espera sua dura��o.
    public IEnumerator MenuEndTransition() {
        menuAnimator.SetTrigger("End");
        yield return new WaitForSeconds(menuDuration + 0.5f);
    }
}
