using AUDIO;
using CAMERA;
using System.Collections;
using SYSTEM;
using SYSTEM.CHECKPOINT;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    // Inicializa o sistema de respawn, posiciona o jogador no checkpoint e inicia a transição de menu.
    private void Start() {
        respawnSystem = new RespawnSystem(player);
        checkpointSystem.Initialize();

        player.transform.position = checkpointSystem.GetCheckpointPosition();

        StartCoroutine(TransitionManager.Instance.MenuEndTransition());
    }

    public IEnumerator MenuStartTransition() { yield return TransitionManager.Instance.MenuStartTransition(); } // Inicia a transição de saída do menu.
    public IEnumerator MenuEndTransition() { yield return TransitionManager.Instance.MenuEndTransition(); } // Inicia a transição de entrada do menu.

    public void ChangeGrapplersDistance(float distance) => grapplerSettings.ChangeDistance(distance); // Altera a distância dos grapplers.

    public int GetLifeSize() => lifeHUDSystem.GetLifeSize(); // Retorna a quantidade total de vidas no HUD.
    public void ChangeLifeHUD(int currentLife) => lifeHUDSystem.ChangeLifeHUD(currentLife); // Atualiza a exibição das vidas no HUD conforme a vida atual.

    public void TeleportToCheckpoint(int target) => checkpointSystem.TeleportToCheckpoint(target, player);
    public void ChangeCheckpoint(GameObject checkpoint) => checkpointSystem.ChangeCheckpoint(checkpoint); // Altera o checkpoint atual para o fornecido.
    public void UpdateCheckpointFromCamera(int cameraIndex) => checkpointSystem.ChangeCheckpointByIndex(cameraIndex);  // Atualiza o checkpoint com base no índice da câmera.

    // Respawna o jogador na posição do checkpoint atual.
    public void RespawnPlayer() {
        Vector3 rawPos = checkpointSystem.GetCheckpointPosition();
        Vector3 pos = new Vector3(rawPos.x, rawPos.y, 0f);

        respawnSystem.RespawnPlayer(CameraManager.Instance.CurrentCheckpointIndex, pos);
    }
    public void UpdateCurrentStageAndMusic() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        switch (currentSceneName) {
            case "Fase_1": SetStageMusic("Audio/Music/fase1"); break;
            case "Fase_2": SetStageMusic("Audio/Music/fase2"); break;
            case "Fase_3": SetStageMusic("Audio/Music/fase3"); break;
            case "MainMenu": SetStageMusic("Audio/Music/menu"); break;
        }
    }

    private void SetStageMusic(string musicName) {
        AudioManager.Instance.StopAllTracks();
        AudioManager.Instance.PlayTrack(musicName, loop: true, startingVolume: 0.5f);
    }
    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => UpdateCurrentStageAndMusic();
}
