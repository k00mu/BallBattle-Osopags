//==================================================
//
//  Created by Atqa
//
//==================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace BallBattle.Utilities
{
    public class NoRotation : MonoBehaviour
    {
        [SerializeField] private Quaternion rotation = Quaternion.Euler(45f, 0f, 0f);
        
        //==================================================
        // Methods
        //==================================================
        
        void LateUpdate()
        {
            transform.rotation = rotation;
        }
    }
}
