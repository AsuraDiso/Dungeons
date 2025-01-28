using Dungeons.Infrastructure;
using Unity.Cinemachine;
using UnityEngine;

namespace Dungeons.Services
{
    public class MainMenuService : MonoBehaviour
    {
        [field: SerializeField] public CinemachineCamera MainCamera { get; private set; }

        public void ChangePriority(CinemachineCamera vcam)
        {
            MainCamera.Priority--;
            MainCamera = vcam;
            MainCamera.Priority++;
        }
        
        public void StartGame()
        {
            Locator<LevelSystem>.Instance.StartGame();
        }
    }
}
