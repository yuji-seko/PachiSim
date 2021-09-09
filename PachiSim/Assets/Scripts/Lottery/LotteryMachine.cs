using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PachiSim.Lottery
{
    /// <summary>
    /// 抽選マシン
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class LotteryMachine : MonoBehaviour
    {
        //=================================================
        // SerializeFields
        //=================================================
        [SerializeField] private ushort             m_counter = 0;
        [SerializeField] private List<LotteryData>  m_lotteries = null;

        //=================================================
        // Methods ( public )
        //=================================================

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            m_lotteries = new List<LotteryData>();
        }

        /// <summary>
        /// 抽選データ追加
        /// </summary>
        /// <param name="data"> データ </param>
        public void AddLotteryData( LotteryData data )
        {
            m_lotteries.Add( data );
        }

        /// <summary>
        /// 抽選結果取得
        /// </summary>
        /// <returns> 抽選結果 </returns>
        public LotteryResult[] GetResults()
        {
            return m_lotteries
                .Where( c => c.LotteryType == LOTTERY_TYPE.Jackpot )
                .OrderBy( c => c.Priority )
                .Select( c => c.CreateResult( m_counter ) )
                .ToArray();
        }

        //=================================================
        // Methods ( MonoBehaviour )
        //=================================================

        void Update()
        {
            m_counter = ( ushort )UnityEngine.Random.Range( 0, ushort.MaxValue );
        }
    }
}
