using CAMERA;
using System.Collections;
using UnityEngine;
using SYSTEM;
using SYSTEM.CHECKPOINT;


public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    [Header("Player Components")]
    [SerializeField] private GameObject player;
    [SerializeField] public Transform PlayerOriginalLayer;

    [Header("Systems and Settings")]
    [SerializeField] private GrapplerSettings grapplerSettings;
    [SerializeField] private CheckpointSystem checkpointSystem;
    [SerializeField] private RespawnSystem respawnSystem;
    [SerializeField] private LifeHUDSystem lifeHUDSystem;

    [Header("Booleas")]
    public bool isPaused = false;
    [SerializeField] private bool isCursorVisible = false;

    // Inicializa o singleton e manipula o cursor do mouse
    private void Awake() {
        Cursor.visible = isCursorVisible;

        if (Instance == null) {
            Instance = this;
        } else {
            DestroyImmediate(gameObject);
            return;
        }
    }

    // Inicializa o sistema de respawn, posiciona o jogador no checkpoint e inicia a transi��o de menu.
    private void Start() {
        respawnSystem = new RespawnSystem(player);
        checkpointSystem.Initialize();

        player.transform.position = checkpointSystem.GetCheckpointPosition();

        StartCoroutine(TransitionManager.Instance.MenuEndTransition());
    }

    public IEnumerator MenuStartTransition() { yield return TransitionManager.Instance.MenuStartTransition(); } // Inicia a transi��o de sa�da do menu.
    public IEnumerator MenuEndTransition() { yield return TransitionManager.Instance.MenuEndTransition(); } // Inicia a transi��o de entrada do menu.

    public void ChangeGrapplersDistance(float distance) => grapplerSettings.ChangeDistance(distance); // Altera a dist�ncia dos grapplers.

    public int GetLifeSize() => lifeHUDSystem.GetLifeSize(); // Retorna a quantidade total de vidas no HUD.
    public void ChangeLifeHUD(int currentLife) => lifeHUDSystem.ChangeLifeHUD(currentLife); // Atualiza a exibi��o das vidas no HUD conforme a vida atual.

    public void TeleportToCheckpoint(int target) => checkpointSystem.TeleportToCheckpoint(target, player);
    public void ChangeCheckpoint(GameObject checkpoint) => checkpointSystem.ChangeCheckpoint(checkpoint); // Altera o checkpoint atual para o fornecido.
    public void UpdateCheckpointFromCamera(int cameraIndex) => checkpointSystem.ChangeCheckpointByIndex(cameraIndex);  // Atualiza o checkpoint com base no �ndice da c�mera.

    // Respawna o jogador na posi��o do checkpoint atual.
    public void RespawnPlayer() {
        Vector3 rawPos = checkpointSystem.GetCheckpointPosition();
        Vector3 pos = new Vector3(rawPos.x, rawPos.y, 0f);

        respawnSystem.RespawnPlayer(CameraManager.Instance.CurrentCheckpointIndex, pos);
    }

}
