//==================================================
//
//  Created by Atqa
//
//==================================================

using BallBattle.EventSystem;

namespace BallBattle.BattleField
{
    public class OnBallCarried : CustomEvent
    {
        public Ball Ball;
    }


    public class OnAttackerPoint : CustomEvent
    {
    }


    public class OnDefenderPoint : CustomEvent
    {
    }


    public class OnSoldierCaught : CustomEvent
    {
        public Soldier Carrier;
        public Soldier Catcher;
    }


    public class OnBallPassed : CustomEvent
    {
    }
}