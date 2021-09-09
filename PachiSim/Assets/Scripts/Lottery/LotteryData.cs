using System;
using UnityEngine;

namespace PachiSim.Lottery
{
    [Serializable]
    public sealed class LotteryData
    {
        [SerializeField] private LOTTERY_TYPE   m_lotteryType = LOTTERY_TYPE.Jackpot;
        [SerializeField] private int            m_priority = 0;
        [SerializeField] private float          m_denominator = 199.8f;
        [SerializeField] private ushort         m_hitRange = 0;
        [SerializeField] private Division[]     divisions = null;

        public ushort HitRange => m_hitRange;
        public LOTTERY_TYPE LotteryType => m_lotteryType;
        public int Priority => m_priority;

        public LotteryData( LOTTERY_TYPE lotteryType, float denominator, params float[] args )
        {
            m_lotteryType = lotteryType;
            m_denominator = denominator;
            m_hitRange = ( ushort )( 1f / m_denominator * ( ( int )ushort.MaxValue + 1 ) );
        }

        public LotteryResult CreateResult( ushort value )
        {
            return new LotteryResult()
            {
                IsJackpot = value < m_hitRange,
            };
        }
    }

    [Serializable]
    public sealed class Division
    {
        [SerializeField] private float  m_percentage;
        [SerializeField] private ushort m_rangeMin;
        [SerializeField] private ushort m_rangeMax;
        [SerializeField] private int    m_round;
    }

    public class LotteryResult
    {
        public bool IsJackpot;
    }
}
