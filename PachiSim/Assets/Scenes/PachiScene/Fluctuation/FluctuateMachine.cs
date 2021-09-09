using Framework.Message;
using System;
using UniRx;
using UnityEngine;

namespace PachiSim.Scenes.PachiScene
{
    /// <summary>
    /// �ϓ��}�V��
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
        /// ������
        /// </summary>
        public void Init()
        {
        }

        /// <summary>
        /// �ϓ��J�n
        /// </summary>
        /// <param name="stock"> �ۗ� </param>
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
        /// �ϓ��J�n���̏���
        /// </summary>
        private void _OnBegin()
        {
            m_isFluctuate = true;
            m_onBegin.Invoke();
        }

        /// <summary>
        /// �ϓ��I�����̏���
        /// </summary>
        private void _OnEnd()
        {
            m_isFluctuate = false;
            m_onEnd.Invoke();
        }
    }
}
