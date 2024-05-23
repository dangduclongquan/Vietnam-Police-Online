using SimpleFPS;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace VietnamPoliceOnline
{
    public class UIPlayerView : MonoBehaviour
    {
        public TextMeshProUGUI Nickname;
        public UICrosshair Crosshair;
        public TextMeshProUGUI Team;
        public TextMeshProUGUI PointerAction;
        public GameObject CatchedMessage;

        public void UpdatePlayer(Player player, PlayerData playerData)
        {
            Nickname.text = playerData.Nickname;
            CatchedMessage.SetActive(playerData.IsCatched);
            Team.text = playerData.Team.ToString();

            Crosshair.gameObject.SetActive(player.movementEnabled);
        }
    }
}
