using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PachiSim.Lottery;

namespace PachiSim.Scenes.PachiScene
{
    /// <summary>
    /// �ۗ�
    /// </summary>
    public class Stock
    {
        readonly private LotteryResult[] m_results = null;

        public Stock( LotteryResult[] results )
        {
            m_results = results;
        }
    }
}
