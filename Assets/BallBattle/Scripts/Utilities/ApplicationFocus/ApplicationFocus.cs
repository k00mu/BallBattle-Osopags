//==================================================
//
//  Created by Atqa
//
//==================================================

using UnityEngine;

namespace BallBattle.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationFocus : MonoBehaviour
    {
        //==================================================
        // Methods
        //==================================================

#if !UNITY_EDITOR
        private void Update()
        {
            Time.timeScale = Application.isFocused ? 1 : 0;
        }
#endif
    }
}