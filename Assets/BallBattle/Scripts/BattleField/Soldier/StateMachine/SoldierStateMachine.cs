//==================================================
//
//  Created by Atqa
//
//==================================================

namespace BallBattle.BattleField
{
    /// <summary>
    /// </summary>
    public class SoldierStateMachine
    {
        public SoldierState CurrentState { get; private set; }

        //==================================================
        // Methods
        //==================================================
        public void Initialize(SoldierState _startingState)
        {
            CurrentState = _startingState;
            CurrentState.Enter();
        }


        public void ChangeState(SoldierState _newState)
        {
            CurrentState.Exit();
            CurrentState = _newState;
            CurrentState.Enter();
        }
    }
}