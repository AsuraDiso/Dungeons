using Dungeons.Game.MapGeneration;
using Dungeons.Game.Rooms;
using UnityEngine;
using UnityEngine.UI;

namespace Dungeons.Game.UI.MiniMap
{
    public class UIMiniMapRoom : MonoBehaviour
    {
        [field: SerializeField] public RectTransform RectTransform { get; private set; }
        [field: SerializeField] public Image Image { get; private set; }
        
        [SerializeField] private Image _roomTypeIcon;
        [SerializeField] private Sprite _bossSprite;
        [SerializeField] private Sprite _shopSprite;
        [SerializeField] private Sprite _rewardSprite;
        [SerializeField] private Sprite _secretSprite;

        private Room _room;

        public void SetRoomTypeIcon(RoomType roomType)
        {
            if (roomType == RoomType.Boss)
            {
                _roomTypeIcon.sprite = _bossSprite;
                _roomTypeIcon.enabled = true;
                return;
            }

            if (roomType == RoomType.Secret)
            {
                _roomTypeIcon.sprite = _secretSprite;
                gameObject.SetActive(false);
                return;
            }
            
            if (roomType == RoomType.Shop)
            {
                _roomTypeIcon.sprite = _shopSprite;
                _roomTypeIcon.enabled = true;
                return;
            }
            if (roomType == RoomType.Reward)
            {
                _roomTypeIcon.sprite = _rewardSprite;
                _roomTypeIcon.enabled = true;
                return;
            } 
            _roomTypeIcon.sprite = null;
            _roomTypeIcon.enabled = false;
        }

        public void SetRoomSize(float roomWidth, float roomDepth)
        {
            var newSize = Mathf.Min(roomWidth, roomDepth);
            RectTransform.sizeDelta = new Vector2(roomWidth, roomDepth);
            _roomTypeIcon.rectTransform.sizeDelta = new Vector2(newSize, newSize);
        }

        public void ConnectRoom(Room room)
        {
            _room = room;
            _room.Exit += OnExit;
            _room.Enter += OnEnter;
        }

        private void OnEnter()
        {
            if (!gameObject.activeInHierarchy) 
                gameObject.SetActive(true);
            Image.color = new Color32(150, 150, 150, 255);
        }

        private void OnExit()
        {
            Image.color = new Color32(150, 150, 150, 50);
        }
    }
}