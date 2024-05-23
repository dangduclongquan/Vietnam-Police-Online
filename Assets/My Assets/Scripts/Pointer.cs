using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

namespace VietnamPoliceOnline
{
    public class Pointer : NetworkBehaviour
    {
        public Transform pointerTransform;
        public float range;

        public LayerMask HitMask;

        public Player getPointedPlayer()
        {

            var hitOptions = HitOptions.IncludePhysX | HitOptions.IgnoreInputAuthority;

            // Whole projectile path and effects are immediately processed (= hitscan projectile).
            if (Runner.LagCompensation.Raycast(pointerTransform.position, pointerTransform.forward, range,
                    Object.InputAuthority, out var hit, HitMask, hitOptions))
            {
                if (hit.Hitbox != null)
                {
                    return hit.Hitbox.Root.GetComponent<Player>();
                }
            }

            return null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pointerTransform.position,pointerTransform.position+pointerTransform.forward*range);
        }
    }
}
