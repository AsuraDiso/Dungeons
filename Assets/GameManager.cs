using System.Threading.Tasks;
using Rooms;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RoomSpawner _roomSpawner;  
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private int _currentFloor = 1;
    [SerializeField] private string _seed;

    public static GameManager Instance { get; private set; }
    public GameObject Player { get; set; }
    public int CurrentFloor { get => _currentFloor; set => _currentFloor = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);  
        }

        StartGame();
    }

    private async void StartGame()
    {
        await WaitForLevelManagerInitialization();
        Player = Instantiate(_playerPrefab, transform.position, Quaternion.identity);
        _roomSpawner.GenerateMap();
    }
    private async Awaitable WaitForLevelManagerInitialization()
    {
        while (LevelManager.Instance == null) await Awaitable.NextFrameAsync();
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}