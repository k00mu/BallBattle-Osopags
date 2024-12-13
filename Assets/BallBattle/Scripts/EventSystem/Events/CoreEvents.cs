//==================================================
//
//  Created by Khalish
//
//==================================================

using BallBattle.Data;

namespace BallBattle.EventSystem
{
    public class OnMatchStart : CustomEvent { }



    public class OnMatchStop : CustomEvent { }



    public class OnMatchEnd : CustomEvent { }



    public class OnGameEnd : CustomEvent { }



    public class OnSeeResult : CustomEvent
    {
        public int PlayerScore;
        public int EnemyScore;
    }



    public class OnGameTimeChanged : CustomEvent
    {
        public float CurrentTime;
    }
}
