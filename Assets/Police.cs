using Fusion;
using SimpleFPS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VietnamPoliceOnline
{
    public class Police : NetworkBehaviour
    {
        public Pointer pointer;

        private SceneObjects _sceneObjects;

        public override void Spawned()
        {
            _sceneObjects = Runner.GetSingleton<SceneObjects>();
        }

        public override void FixedUpdateNetwork()
        {
            if (HasInputAuthority)
            {
                Player p = pointer.getPointedPlayer();
                Thief t=null;
                if (p != null)
                {
                    t = p.GetComponent<Thief>();
                }

                if (p!=null && t != null && !t.isCuffed)
                {
                    _sceneObjects.GameUI.PlayerView.PointerAction.text = "Catch";
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
                    if (t != null && !t.isCuffed)
                    {
                        t.isCuffed = true;
                        _sceneObjects.Gameplay.PlayerCatched(Object.InputAuthority, t.Object.InputAuthority);
                    }
                }
            }
        }
    }
}
