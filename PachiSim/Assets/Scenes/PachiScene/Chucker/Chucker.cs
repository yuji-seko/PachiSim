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
        /// ������
        /// </summary>
        public void Init()
        {
        }

        /// <summary>
        /// �J�n
        /// </summary>
        public void Begin()
        {
            Debug.Log( "<color=green>Chucker Begin</color>" );

            m_disposable = Observable.Interval( TimeSpan.FromSeconds( 1f ) )
                .Subscribe( _Chuck )
                .AddTo( this );
        }

        /// <summary>
        /// ��~
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
        /// ���V�[�o�[�ǉ�
        /// </summary>
        /// <param name="receiver"> ���V�[�o�[ </param>
        public static void AddReceiver( IChuckReceiver receiver )
        {
            if ( ms_instance != null )
            {
                ms_instance._AddReceiver( receiver );
            }
        }

        /// <summary>
        /// ���V�[�o�[�폜
        /// </summary>
        /// <param name="receiver"> ���V�[�o�[ </param>
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
        /// ����
        /// </summary>
        private void _Chuck( long value )
        {
            m_onChuck.Invoke();
            m_receivers.ForEach( r => r.OnChuck() );
        }

        /// <summary>
        /// ���V�[�o�[�ǉ�
        /// </summary>
        /// <param name="receiver"> ���V�[�o�[ </param>
        private void _AddReceiver( IChuckReceiver receiver )
        {
            m_receivers.Add( receiver );
        }

        /// <summary>
        /// ���V�[�o�[�폜
        /// </summary>
        /// <param name="receiver"> ���V�[�o�[ </param>
        private void _RemoveReceiver( IChuckReceiver receiver )
        {
            m_receivers.Remove( receiver );
        }
    }
}
