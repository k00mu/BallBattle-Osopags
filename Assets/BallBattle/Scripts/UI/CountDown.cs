//==================================================
//
//  Created by Khalish
//
//==================================================

using System.Collections;
using System.Collections.Generic;
using BallBattle.Core;
using BallBattle.EventSystem;
using TMPro;
using UnityEngine;

namespace BallBattle.UI
{
    /// <summary>
    /// Display the countdown before the game start
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    [DisallowMultipleComponent]
    public class CountDown : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private TextMeshProUGUI matchCountText;
        [SerializeField] private TextMeshProUGUI countDownText;
        [SerializeField] private int startingCountDown = 3;
        [SerializeField] private float countDownInterval = 1f;



        //==================================================
        // Methods
        //==================================================
        private void Start()
        {
            UpdateMatchCount();

            StartCoroutine(CountDownRoutine());
        }



        private void OnEnable()
        {
            EventManager.AddListener<OnMatchEnd>(OnMatchEnd);
        }



        private void OnDisable()
        {
            EventManager.RemoveListener<OnMatchEnd>(OnMatchEnd);
        }



        /// <summary>
        /// Handle match end event
        /// </summary>
        /// <param name="_evt"></param>
        private void OnMatchEnd(OnMatchEnd _evt)
        {
            if (GameManager.Instance.IsGameEnded())
            {
                return;
            }

            Show(true);

            UpdateMatchCount();

            StartCoroutine(CountDownRoutine());
        }



        /// <summary>
        /// Play the countdown
        /// </summary>
        /// <returns></returns>
        private IEnumerator CountDownRoutine()
        {
            var count = startingCountDown;

            while (count > 0)
            {
                countDownText.text = count.ToString();
                count--;

                yield return new WaitForSeconds(countDownInterval);
            }

            countDownText.text = "GO!";

            yield return new WaitForSeconds(countDownInterval);

            GameManager.Instance.StartGame();

            Show(false);
        }



        /// <summary>
        /// Update the match count
        /// </summary>
        private void UpdateMatchCount()
        {
            matchCountText.text = $"Match {GameManager.Instance.MatchCount + 1}";
        }



        /// <summary>
        /// Show the canvas group with the given visibility
        /// </summary>
        /// <param name="isShow"></param>
        private void Show(bool isShow)
        {
            canvasGroup.alpha = isShow ? 1f : 0f;
            canvasGroup.interactable = isShow;
            canvasGroup.blocksRaycasts = isShow;
        }
    }
}
