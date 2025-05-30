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

    // Inicializa o singleton, componentes de animação e CanvasGroup, e define durações das transições.
    private void Awake() {
        if (Instance == null) Instance = this;
        else DestroyImmediate(gameObject);

        deathAnimator = deathObject.GetComponent<Animator>();
        deathDuration = deathAnimator.GetCurrentAnimatorStateInfo(0).length;

        menuAnimator = menuObject.GetComponent<Animator>();
        menuDuration = menuAnimator.GetCurrentAnimatorStateInfo(0).length;
    }

    // Executa a transição de morte, chama uma ação no meio e controla o estado de transição.
    public IEnumerator PlayDeathTransition(System.Action onMidTransition) {
        IsTransitioning = true;
        deathAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(deathDuration / 2f);
        onMidTransition?.Invoke();

        yield return new WaitForSeconds(deathDuration / 2f);
        IsTransitioning = false;
    }

    // Inicia a animação de transição do menu e espera sua duração.
    public IEnumerator MenuStartTransition() {
        menuAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(menuDuration + 0.5f);
    }

    // Finaliza a animação de transição do menu e espera sua duração.
    public IEnumerator MenuEndTransition() {
        menuAnimator.SetTrigger("End");
        yield return new WaitForSeconds(menuDuration + 0.5f);
    }
}
