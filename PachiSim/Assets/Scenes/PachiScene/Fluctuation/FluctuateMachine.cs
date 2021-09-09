using Framework.Message;
using System;
using UniRx;
using UnityEngine;

namespace PachiSim.Scenes.PachiScene
{
    /// <summary>
    /// 変動マシン
    /// </summary>
    public sealed class FluctuateMachine : MonoBehaviour
    {
        //=================================================
        // Fields ( private )
        //=================================================
        private MessageEvent    m_onBegin       = new MessageEvent();
        private MessageEvent    m_onEnd         = new MessageEvent();
        private bool            m_isFluctuate   = false;
        private Stock           m_stock         = null;

        //=================================================
        // Propaerties ( public )
        //=================================================
        public MessageEvent OnBegin     => m_onBegin;
        public MessageEvent OnEnd       => m_onEnd;
        public bool         IsFluctuate => m_isFluctuate;

        //=================================================
        // Methods ( public )
        //=================================================

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
        }

        /// <summary>
        /// 変動開始
        /// </summary>
        /// <param name="stock"> 保留 </param>
        public void Begin( Stock stock )
        {
            m_stock = stock;

            _OnBegin();
            Observable.Timer( TimeSpan.FromSeconds( 5f ) )
                .Subscribe( _ => _OnEnd() )
                .AddTo( this );
        }

        //=================================================
        // Methods ( private )
        //=================================================

        /// <summary>
        /// 変動開始時の処理
        /// </summary>
        private void _OnBegin()
        {
            m_isFluctuate = true;
            m_onBegin.Invoke();
        }

        /// <summary>
        /// 変動終了時の処理
        /// </summary>
        private void _OnEnd()
        {
            m_isFluctuate = false;
            m_onEnd.Invoke();
        }
    }
}
