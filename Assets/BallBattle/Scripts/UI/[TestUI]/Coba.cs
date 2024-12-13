//==================================================
//
//  Created by Khalish
//
//==================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BallBattle.Data;
using BallBattle.BattleField;

namespace BallBattle.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class Coba : MonoBehaviour
    {
        [SerializeField] private LandField landField;
        //==================================================
        // Methods
        //==================================================
        private void Start()
        {
            landField.Data.ResetPoint();
        }

        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Alpha2))
            // {
            //     landField.Data.AddPoint();
            // }
        }
    }
}
