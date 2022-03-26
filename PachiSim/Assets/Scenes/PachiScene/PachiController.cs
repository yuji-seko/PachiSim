using PachiSim.Lottery;
using UnityEngine;

namespace PachiSim.Scenes.PachiScene
{
    /// <summary>
    /// コントローラー
    /// </summary>
    public class PachiController : MonoBehaviour, IStockHolderReseiver
    {
        //=================================================
        // SerializeFields
        //=================================================
        [SerializeField] private Chucker            m_chucker           = null;
        [SerializeField] private LotteryMachine     m_lotteryMachine    = null;
        [SerializeField] private DataCounterUI      m_dataCounter       = null;
        [SerializeField] private StockHolder        m_stockHolder       = null;
        [SerializeField] private StockHolderView    m_stockHolderView   = null;

        //=================================================
        // Fields ( private static )
        //=================================================
        private static PachiController ms_instance = null;

        //=================================================
        // Methods ( MonoBehaviour )
        //=================================================

        private void Start()
        {
            StockHolder.AddReceiver( this );
        }

        private void OnDestroy()
        {
            StockHolder.RemoveReceiver( this );
        }

        //=================================================
        // Methods ( public )
        //=================================================

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            // チャッカー
            m_chucker.Init();
            m_chucker.OnChuck.Subcrive( OnChuck ).AddTo( this );

            // 抽選マシン
            m_lotteryMachine.Init();
            m_lotteryMachine.AddLotteryData( new LotteryData( LOTTERY_TYPE.Jackpot, 199.8f ) );

            // 保留
            m_stockHolder.Init();
            m_stockHolderView.Init();

            // データカウンター
            m_dataCounter.Init();
        }

        /// <summary>
        /// 開始
        /// </summary>
        public void Begin()
        {
            m_chucker.Begin();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            m_chucker.Stop();
        }

        /// <summary>
        /// 入賞
        /// </summary>
        void OnChuck()
        {
            var results = m_lotteryMachine.GetResults();
            var stock = m_stockHolder.CreateStock( results );

            // 消化中ならキューに追加
            if ( m_stockHolder.IsDigest )
            {
                m_stockHolder.Enqueue( stock );
                return;
            }

            m_stockHolder.Digest( stock );
        }

        //=================================================
        // Methods ( IStockHolderReseiver )
        //=================================================

        /// <summary>
        /// 保留の消化開始時の処理
        /// </summary>
        void IStockHolderReseiver.OnBeginDigest()
        {

        }

        /// <summary>
        /// 保留の消化終了時の処理
        /// </summary>
        void IStockHolderReseiver.OnEndDigest()
        {
            // カウント
            m_dataCounter.AddCount();

            // 保留を取り出して消化
            var stock = m_stockHolder.Dequeue();
            if ( stock != null )
            {
                m_stockHolder.Digest( stock );
            }
        }

        /// <summary>
        /// 保留数変化時の処理
        /// </summary>
        /// <param name="digestStock"> 消化中の保留 /param>
        /// <param name="reserveStocks"> 予約中の保留 </param>
        void IStockHolderReseiver.OnUpdateStock( Stock digestStock, Stock[] reserveStocks )
        {
        }
    }
}
