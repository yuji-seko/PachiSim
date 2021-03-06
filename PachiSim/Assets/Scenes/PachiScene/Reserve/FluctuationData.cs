using PachiSim.Lottery;

namespace PachiSim.Scenes.PachiScene
{
    /// <summary>
    /// 変動データ
    /// </summary>
    public class FluctuationData
    {
        readonly private LotteryResult[] m_results = null;

        public FluctuationData( LotteryResult[] results )
        {
            m_results = results;
        }
    }
}
