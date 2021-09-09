using PachiSim.Lottery;
using System.Collections.Generic;
using UnityEngine;
using Framework.Core;

namespace PachiSim.Scenes.PachiScene
{
    /// <summary>
    /// 保留
    /// </summary>
    public class StockHolder : SingletonBehaviour<StockHolder>
    {
        //=================================================
        // SerializeFields
        //=================================================
        [SerializeField] private int m_stockMax = 4;
        [SerializeField] private FluctuateMachine m_fluctuateMachine = null;

        //=================================================
        // Fields ( private )
        //=================================================
        private Queue<Stock>                m_stocks        = new Queue<Stock>();
        private Stock                       m_digestStock   = null;
        private List<IStockHolderReseiver>  m_receivers     = new List<IStockHolderReseiver>();

        //=================================================
        // Properties
        //=================================================
        public int  StockCount  => m_stocks.Count;
        public bool IsDigest    => m_fluctuateMachine.IsFluctuate;

        //=================================================
        // Methods ( public )
        //=================================================

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            // 変動マシン
            m_fluctuateMachine.Init();
            m_fluctuateMachine.OnBegin.Subcrive( OnFluctuateBegin ).AddTo( this );
            m_fluctuateMachine.OnEnd.Subcrive( OnFluctuateEnd ).AddTo( this );
        }

        /// <summary>
        /// 保留の生成
        /// </summary>
        /// <param name="results"> 抽選結果 </param>
        /// <returns> 保留 </returns>
        public Stock CreateStock( LotteryResult[] results )
        {
            return new Stock( results );
        }

        /// <summary>
        /// 保留を追加する
        /// </summary>
        /// <param name="stock"> 保留 </param>
        /// <returns> true=成功, false=失敗 </returns>
        public bool Enqueue( Stock stock )
        {
            if ( m_stocks.Count >= m_stockMax )
            {
                return false;
            }

            // キューに追加
            m_stocks.Enqueue( stock );
            // イベント発火
            SendUpdateStocks();

            return true;
        }

        /// <summary>
        /// 保留を取得する
        /// </summary>
        /// <returns> 保留 </returns>
        public Stock Dequeue()
        {
            if ( m_stocks.Count == 0 )
            {
                return null;
            }

            var stock = m_stocks.Dequeue();
            // イベント発火
            SendUpdateStocks();
            return stock;
        }

        /// <summary>
        /// 保留消化
        /// </summary>
        /// <param name="stock"> 保留 </param>
        public void Digest( Stock stock )
        {
            m_digestStock = stock;
            m_fluctuateMachine.Begin( stock );
            // イベント発火
            SendUpdateStocks();
        }

        private void SendUpdateStocks()
        {
            // イベント発火
            m_receivers.ForEach( r => r.OnUpdateStock( m_digestStock, m_stocks.ToArray() ) );
        }

        //=================================================
        // Methods ( public static )
        //=================================================

        /// <summary>
        /// レシーバーの追加
        /// </summary>
        /// <param name="receiver"> レシーバー </param>
        public static void AddReceiver( IStockHolderReseiver receiver )
        {
            Instace.m_receivers.Add( receiver );
        }

        /// <summary>
        /// レシーバーの削除
        /// </summary>
        /// <param name="receiver"> レシーバー </param>
        public static void RemoveReceiver( IStockHolderReseiver receiver )
        {
            Instace.m_receivers.Remove( receiver );
        }

        //=================================================
        // Methods ( FluctuateMachine Event )
        //=================================================

        /// <summary>
        /// 変動開始
        /// </summary>
        void OnFluctuateBegin()
        {
            m_receivers.ForEach( r => r.OnBeginDigest() );
        }

        /// <summary>
        /// 変動終了
        /// </summary>
        void OnFluctuateEnd()
        {
            m_digestStock = null;
            m_receivers.ForEach( r => r.OnEndDigest() );
            SendUpdateStocks();
        }
    }
}
