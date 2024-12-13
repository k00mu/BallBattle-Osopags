//==================================================
//
//  Created by [NAME]
//
//==================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallBattle.BattleField
{
    /// <summary>
    /// 
    /// </summary>
    public class BallVisual : MonoBehaviour
    {
        [Header("VFXs")]
        [SerializeField] private GameObject caughtEffect;
        
        //==================================================
        // Methods
        //==================================================
        
        public void PlayCaughtEffect()
        {
            if (caughtEffect != null)
            {
                caughtEffect.SetActive(true);
            }
        }
    }
}
