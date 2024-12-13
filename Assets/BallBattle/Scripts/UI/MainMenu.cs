//==================================================
//
//  Created by Khalish
//
//==================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Osopags.Core;

namespace BallBattle.UI
{
    /// <summary>
    ///
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button startButton;

#if UNITY_STANDALONE
        [Space(5), Header("Screen Resolution. For Standalone only")]
        [SerializeField] private int screenWidth = 720;
        [SerializeField] private int screenHeight = 1920;
#endif


        //==================================================
        // Methods
        //==================================================
        private void Awake()
        {
            startButton.onClick.AddListener(OnStartButtonClicked);

            OsopagsSDK.Instance.IAM.AuthDevice(
                result =>
                {
                    Debug.Log($"Device auth: {result.deviceToken}");

                    OsopagsSDK.Instance.Analytic.Track("game_started");
                }
            );

#if UNITY_STANDALONE
            Screen.SetResolution(screenWidth, screenHeight, true);
#endif
        }



        /// <summary>
        /// Change the scene to the next scene after the start button is clicked
        /// </summary>
        private void OnStartButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            OsopagsSDK.Instance.Analytic.Track("match_play");
        }
    }
}
