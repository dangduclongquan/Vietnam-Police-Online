using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace VietnamPoliceOnline
{
    /// <summary>
    /// View showed at the end of the match.
    /// </summary>
    public class UIGameOverView : MonoBehaviour
    {
        public TextMeshProUGUI Winner;
        public AudioSource GameOverMusic;

        private GameUI _gameUI;
        private EGameplayState _lastState;

        // Called from button OnClick event.
        public void GoToMenu()
        {
            _gameUI.GoToMenu();
        }

        private void Awake()
        {
            _gameUI = GetComponentInParent<GameUI>();
        }

        private void Update()
        {
            if (_gameUI.Runner == null)
                return;

            // Unlock cursor.
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (_gameUI.Gameplay.Object == null || _gameUI.Gameplay.Object.IsValid == false)
                return;

            if (_lastState == _gameUI.Gameplay.State)
                return;

            GameOverMusic.PlayDelayed(1f);

            _lastState = _gameUI.Gameplay.State;

            Winner.text = string.Empty;



            Winner.text = "POLICE VICTORY";
            foreach (var playerPair in _gameUI.Gameplay.PlayerData)
            {
                if (playerPair.Value.Team == ETeam.Thief && !playerPair.Value.IsCatched)
                    Winner.text = "THIEF VICTORY";
            }
        }
    }
}
