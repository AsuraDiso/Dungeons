using Dungeons.Game.UI.HealthBar;
using Dungeons.Game.UI.MiniMap;
using Dungeons.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Dungeons.Game.PlayerSystem
{
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private UIHealthBar _healthBar;
        [SerializeField] private UIMiniMap _miniMap;
        [SerializeField] private UIPause _pause;
        private Player _player;

        private void Awake()
        {
            _player = Locator<Player>.Instance;
            _player.Health.HealthDelta += OnHealthDelta;
            _pause.gameObject.SetActive(false);
            OnHealthDelta();
        }

        private void OnHealthDelta()
        {
            _healthBar.OnChangeHealth(_player.Health.GetPercent());
        }
        
        private void Update()
        {
            if (_player.Controller.EscTriggered)
            {
                _pause.gameObject.SetActive(!_pause.gameObject.activeSelf);
            }
        }
    }
}