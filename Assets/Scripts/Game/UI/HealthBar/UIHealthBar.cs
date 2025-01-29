using System;
using UnityEngine;
using UnityEngine.UI;

namespace Dungeons.Game.UI.HealthBar
{
    public class UIHealthBar : MonoBehaviour
    {
        [SerializeField] protected Image _bgImageWhite; 
        [SerializeField] protected Image _bar;        

        private float _percentage = 1;     
        private float _newpercentage;  
        private float _decreaseSpeed; 
        private float _decreaseDuration = 0.5f;

        private void Update()
        {
            if (_percentage > _newpercentage)
            {
                _percentage -= _decreaseSpeed * Time.deltaTime;
                _bgImageWhite.fillAmount = Mathf.Clamp01(_percentage); 
            }
        }

        public void OnChangeHealth(float percentage)
        {
            _newpercentage = Mathf.Clamp01(percentage);
            _bar.fillAmount = _newpercentage;

            float difference = _percentage - _newpercentage;
            _decreaseSpeed = difference / _decreaseDuration;
        }
    }
}