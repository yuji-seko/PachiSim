using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PachiSim.Lottery
{
    /// <summary>
    /// ���I�}�V��
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
        /// ������
        /// </summary>
        public void Init()
        {
            m_lotteries = new List<LotteryData>();
        }

        /// <summary>
        /// ���I�f�[�^�ǉ�
        /// </summary>
        /// <param name="data"> �f�[�^ </param>
        public void AddLotteryData( LotteryData data )
        {
            m_lotteries.Add( data );
        }

        /// <summary>
        /// ���I���ʎ擾
        /// </summary>
        /// <returns> ���I���� </returns>
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
