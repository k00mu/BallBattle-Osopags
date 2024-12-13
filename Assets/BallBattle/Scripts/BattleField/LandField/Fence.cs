//==================================================
//
//  Created by Atqa
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
    public class Fence : MonoBehaviour
    {
        [SerializeField] private LandField fieldOwner;
        
        //==================================================
        // Methods
        //==================================================
        
        public bool IsAlly(Soldier soldier)
        {
            return fieldOwner.Data.IsAttacker == soldier.IsAttacker;
        }
    }
}
