using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class FloorPreset
{
    public string floorName; 
    public int minDifficulty; 
    public int maxDifficulty; 

    public List<GameObject> mobs;  
    public List<GameObject> rewards; 
    public List<GameObject> bosses; 
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<FloorPreset> _floorPresets; 

    public static LevelManager Instance { get; private set; }
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
    }

    public FloorPreset GetFloorPreset(int floorNumber)
    {
        if (floorNumber >= 0 && floorNumber < _floorPresets.Count)
        {
            return _floorPresets[floorNumber-1];
        }

        return null;  
    }
}