using SimpleFPS;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace VietnamPoliceOnline
{
    public class UIGameplayView : MonoBehaviour
    {
        public TextMeshProUGUI gameState;
        public TextMeshProUGUI gameTime;

        private GameUI _gameUI;

        private void Awake()
        {
            _gameUI = GetComponentInParent<GameUI>();
        }

        private void Update()
        {
            gameState.text = _gameUI.Gameplay.State.ToString();
            if(_gameUI.Gameplay.State==EGameplayState.Running)
                ShowGameplayTime((int)_gameUI.Gameplay.RemainingTime.RemainingTime(_gameUI.Runner).GetValueOrDefault());
            if(_gameUI.Gameplay.State==EGameplayState.Skirmish)
                ShowGameplayTime((int)_gameUI.Gameplay.ReadyTimer.RemainingTime(_gameUI.Runner).GetValueOrDefault());
            if (_gameUI.Gameplay.State == EGameplayState.Finished)
                ShowGameplayTime((int)_gameUI.Gameplay.RestartingTimer.RemainingTime(_gameUI.Runner).GetValueOrDefault());
        }

        private void ShowGameplayTime(int remainingTime)
        {
            int minutes = (remainingTime / 60);
            int seconds = (remainingTime % 60);
            gameTime.text = $"{minutes}:{seconds:00}";
        }
    }
}
