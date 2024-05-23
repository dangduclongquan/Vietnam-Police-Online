using Fusion;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VietnamPoliceOnline
{
    [RequireComponent(typeof(Player))]
    public class Thief : NetworkBehaviour
    { 
        public Pointer pointer;
        public GameObject cuffedIndicator;

        [HideInInspector]
        [Networked, OnChangedRender(nameof(OnCuffedChanged))]
        public NetworkBool isCuffed { get; set; }

        public void OnCuffedChanged()
        {
            player.movementEnabled = !isCuffed;
            cuffedIndicator.SetActive(isCuffed);
        }
        

        private SceneObjects _sceneObjects;

        [HideInInspector]
        public Player player;
        public override void Spawned()
        {
            _sceneObjects = Runner.GetSingleton<SceneObjects>();
            player = GetComponent<Player>();
            isCuffed = false;
        }

        public override void FixedUpdateNetwork()
        {
            if (HasInputAuthority)
            {
                    Player p = pointer.getPointedPlayer();
                    Thief t = null;
                    if (p != null)
                    {
                        t = p.GetComponent<Thief>();
                    }

                    if (p != null && t != null && t.isCuffed)
                    {
                        _sceneObjects.GameUI.PlayerView.PointerAction.text = "Rescue";
                    }
                    else
                    {
                        _sceneObjects.GameUI.PlayerView.PointerAction.text = "";
                    }
            }

            if (GetInput(out NetworkedInput networkedInput))
            {
                ProccessInput(networkedInput);
            }
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            _sceneObjects.GameUI.PlayerView.PointerAction.text = "";
        }

        private void ProccessInput(NetworkedInput input)
        {
            if (input.Buttons.IsSet(EInputButton.Fire))
            {
                Player p = pointer.getPointedPlayer();
                if (p != null)
                {
                    Thief t = p.GetComponent<Thief>();
                    if (t != null && t.isCuffed)
                    {
                        t.isCuffed = false;
                        _sceneObjects.Gameplay.PlayerRescued(Object.InputAuthority, t.Object.InputAuthority);
                    }
                }
            }
        }
    }
}
