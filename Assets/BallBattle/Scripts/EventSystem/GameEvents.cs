//==================================================
//
//  Created by Khalish
//
//==================================================

namespace BallBattle.EventSystem
{
    /// <summary>
    /// To store all custom events
    /// * Register your event here
    /// </summary>
    public static class GameEvents
    {
        #region HUD Events
        public static OnPointChanged OnPointChanged = new();
        public static OnEnergyChanged OnEnergyChanged = new();
        public static OnEnergyConsumed OnEnergyConsumed = new();

        #endregion




        #region Core Events
        public static OnMatchStart OnMatchStart = new();
        public static OnMatchStop OnMatchStop = new();
        public static OnMatchEnd OnMatchEnd = new();
        public static OnGameEnd OnGameEnd = new();
        public static OnSeeResult OnSeeResult = new();
        public static OnGameTimeChanged OnGameTimeChanged = new();
        #endregion
    }
}