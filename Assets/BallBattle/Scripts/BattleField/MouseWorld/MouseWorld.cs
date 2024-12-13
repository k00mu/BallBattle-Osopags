//==================================================
//
//  Created by Atqa
//
//==================================================

using System;
using BallBattle.Utilities.Singleton;
using UnityEngine;

namespace BallBattle.BattleField
{
    /// <summary>
    /// </summary>
    public class MouseWorld : MonoBehaviourSingleton<MouseWorld>
    {
        private Camera targetCamera;


        //==================================================
        // Methods
        //==================================================
        protected override void Awake()
        {
            base.Awake();

            targetCamera = Camera.main;
        }

        
        public static Vector3 GetPosition()
        {
            var ray = Instance.targetCamera.ScreenPointToRay(Input.mousePosition);

            return Physics.Raycast(ray, out var raycastHitInfo, float.MaxValue, BattleFieldResources.Instance.GroundLayerMask)
                ? raycastHitInfo.point
                : Vector3.zero;
        }
        
        
        public static Vector3 GetPosition(int _touchIndex)
        {
            var touch = Input.GetTouch(_touchIndex);
            var ray = Instance.targetCamera.ScreenPointToRay(touch.position);

            return Physics.Raycast(ray, out var raycastHitInfo, float.MaxValue, BattleFieldResources.Instance.GroundLayerMask)
                ? raycastHitInfo.point
                : Vector3.zero;
        }
    }
}