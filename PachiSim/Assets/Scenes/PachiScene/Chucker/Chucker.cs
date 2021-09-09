using Framework.Message;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace PachiSim.Scenes.PachiScene
{
    [DisallowMultipleComponent]
    public sealed partial class Chucker : MonoBehaviour
    {
        //=================================================
        // Fields ( private )
        //=================================================
        private IDisposable             m_disposable = null;
        private MessageEvent            m_onChuck = new MessageEvent();
        private List<IChuckReceiver>    m_receivers = new List<IChuckReceiver>();

        private static Chucker ms_instance = null;

        //=================================================
        // Properties ( public )
        //=================================================
        public MessageEvent OnChuck => m_onChuck;

        //=================================================
        // Methods ( MonoBehaviour )
        //=================================================

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            if ( ms_instance != null )
            {
                DestroyImmediate( ms_instance.gameObject );
            }
            ms_instance = this;
        }

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
        /// 開始
        /// </summary>
        public void Begin()
        {
            Debug.Log( "<color=green>Chucker Begin</color>" );

            m_disposable = Observable.Interval( TimeSpan.FromSeconds( 1f ) )
                .Subscribe( _Chuck )
                .AddTo( this );
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            Debug.Log( "<color=green>Chucker Stop</color>" );

            m_disposable.Dispose();
            m_disposable = null;
        }

        //=================================================
        // Methods ( public static )
        //=================================================

        /// <summary>
        /// レシーバー追加
        /// </summary>
        /// <param name="receiver"> レシーバー </param>
        public static void AddReceiver( IChuckReceiver receiver )
        {
            if ( ms_instance != null )
            {
                ms_instance._AddReceiver( receiver );
            }
        }

        /// <summary>
        /// レシーバー削除
        /// </summary>
        /// <param name="receiver"> レシーバー </param>
        public static void RemoveReceiver( IChuckReceiver receiver )
        {
            if ( ms_instance != null )
            {
                ms_instance._RemoveReceiver( receiver );
            }
        }

        //=================================================
        // Methods ( private )
        //=================================================

        /// <summary>
        /// 入賞
        /// </summary>
        private void _Chuck( long value )
        {
            m_onChuck.Invoke();
            m_receivers.ForEach( r => r.OnChuck() );
        }

        /// <summary>
        /// レシーバー追加
        /// </summary>
        /// <param name="receiver"> レシーバー </param>
        private void _AddReceiver( IChuckReceiver receiver )
        {
            m_receivers.Add( receiver );
        }

        /// <summary>
        /// レシーバー削除
        /// </summary>
        /// <param name="receiver"> レシーバー </param>
        private void _RemoveReceiver( IChuckReceiver receiver )
        {
            m_receivers.Remove( receiver );
        }
    }
}
