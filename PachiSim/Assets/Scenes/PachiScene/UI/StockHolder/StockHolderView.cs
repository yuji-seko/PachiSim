using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PachiSim.Scenes.PachiScene
{
    [DisallowMultipleComponent]
    public sealed class StockHolderView : MonoBehaviour, IStockHolderReseiver
    {
        //=================================================
        // SerializeFields
        //=================================================
        [SerializeField] private StockView m_current = null;
        [SerializeField] private StockView[] m_reserves = null;

        //=================================================
        // Fields
        //=================================================


        //=================================================
        // Methods ( MonoBehaviour )
        //=================================================

        private void Reset()
        {
            m_current = GetComponentInChildren<StockView>();
            m_reserves = GetComponentsInChildren<StockView>().Skip( 1 ).ToArray();
        }

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
            m_current.SetActive( false );
            m_reserves.ForEach( reserve => reserve.SetActive( false ) );
        }


        //=================================================
        // Methods ( IStockHolderReseiver )
        //=================================================

        void IStockHolderReseiver.OnBeginDigest()
        {
        }

        void IStockHolderReseiver.OnEndDigest()
        {
        }

        /// <summary>
        /// 保留数変化時の処理
        /// </summary>
        /// <param name="digestStock"> 消化中の保留 /param>
        /// <param name="reserveStocks"> 予約中の保留 </param>
        void IStockHolderReseiver.OnUpdateStock( Stock digestStock, Stock[] reserveStocks )
        {
            m_current.SetActive( digestStock != null );

            for ( int i = 0; i < m_reserves.Length; i++ )
            {
                var ui = m_reserves[ i ];
                var stock = reserveStocks.ElementAtOrDefault( i );
                ui.SetActive( stock != null );
            }
        }
    }
}
