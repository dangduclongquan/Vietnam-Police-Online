using SimpleFPS;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace VietnamPoliceOnline
{
    public class UIScoreboard : MonoBehaviour
    {
        public ETeam team;
        public UIScoreboardRow Row;
        public float DisconnectedPlayerAlpha = 0.4f;

        List<UIScoreboardRow> _rows = new(32);
        List<PlayerData> _players = new(32);

        private GameUI _gameUI;

        private void Awake()
        {
            Row.gameObject.SetActive(false);
            _rows.Add(Row);

            _gameUI = GetComponentInParent<GameUI>();
        }

        private void OnEnable()
        {
            InvokeRepeating(nameof(UpdateScoreboard), 0f, 0.5f);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        private void UpdateScoreboard()
        {
            if (_gameUI.Runner == null)
                return;

            _players.Clear();

            foreach (var record in _gameUI.Gameplay.PlayerData)
            {
                if(record.Value.Team==team)
                    _players.Add(record.Value);
            }

            if (team == ETeam.Police)
                _players.Sort((a, b) => -a.CatchedCount.CompareTo(b.CatchedCount));
            if(team == ETeam.Thief)
                _players.Sort((a, b) => -a.RescuedCount.CompareTo(b.RescuedCount));

            PrepareRows(_players.Count);
            UpdateRows();
        }

        private void PrepareRows(int playerCount)
        {
            // Add missing rows
            for (int i = _rows.Count; i < playerCount; i++)
            {
                var row = Instantiate(Row, Row.transform.parent);
                row.gameObject.SetActive(true);

                _rows.Add(row);
            }

            // Activate correct count of rows
            for (int i = 0; i < _rows.Count; i++)
            {
                _rows[i].gameObject.SetActive(i < playerCount);
            }
        }

        private void UpdateRows()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                var row = _rows[i];
                var data = _players[i];

                row.Name.text = data.Nickname;
                row.Kills.text = data.CatchedCount.ToString();
                row.Deaths.text = data.RescuedCount.ToString();
                if (team == ETeam.Police) row.Deaths.text = "";

                row.DeadGroup.SetActive(data.IsAlive == false || data.IsConnected == false || data.IsCatched==true);
                row.LocalPlayerGroup.SetActive(_gameUI.Runner.LocalPlayer == data.PlayerRef);

                row.CanvasGroup.alpha = data.IsConnected ? 1f : DisconnectedPlayerAlpha;
            }
        }
    }
}
