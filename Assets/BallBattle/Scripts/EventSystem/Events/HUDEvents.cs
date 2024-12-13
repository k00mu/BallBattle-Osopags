//==================================================
//
//  Created by Khalish
//
//==================================================

using BallBattle.Data;

namespace BallBattle.EventSystem
{
    public class OnPointChanged : CustomEvent
    {
        public FractionData Fraction;
        public int Point;
    }



    public class OnEnergyChanged : CustomEvent
    {
        public FractionData Fraction;
        public float Energy;
    }



    public class OnEnergyConsumed : CustomEvent
    {
        public FractionData Fraction;
        public float EnergyConsumed;
    }
}
