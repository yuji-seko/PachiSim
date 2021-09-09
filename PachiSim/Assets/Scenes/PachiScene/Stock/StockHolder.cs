using PachiSim.Lottery;
using System.Collections.Generic;
using UnityEngine;
using Framework.Core;

namespace PachiSim.Scenes.PachiScene
{
    /// <summary>
    /// �ۗ�
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
        /// ������
        /// </summary>
        public void Init()
        {
            // �ϓ��}�V��
            m_fluctuateMachine.Init();
            m_fluctuateMachine.OnBegin.Subcrive( OnFluctuateBegin ).AddTo( this );
            m_fluctuateMachine.OnEnd.Subcrive( OnFluctuateEnd ).AddTo( this );
        }

        /// <summary>
        /// �ۗ��̐���
        /// </summary>
        /// <param name="results"> ���I���� </param>
        /// <returns> �ۗ� </returns>
        public Stock CreateStock( LotteryResult[] results )
        {
            return new Stock( results );
        }

        /// <summary>
        /// �ۗ���ǉ�����
        /// </summary>
        /// <param name="stock"> �ۗ� </param>
        /// <returns> true=����, false=���s </returns>
        public bool Enqueue( Stock stock )
        {
            if ( m_stocks.Count >= m_stockMax )
            {
                return false;
            }

            // �L���[�ɒǉ�
            m_stocks.Enqueue( stock );
            // �C�x���g����
            SendUpdateStocks();

            return true;
        }

        /// <summary>
        /// �ۗ����擾����
        /// </summary>
        /// <returns> �ۗ� </returns>
        public Stock Dequeue()
        {
            if ( m_stocks.Count == 0 )
            {
                return null;
            }

            var stock = m_stocks.Dequeue();
            // �C�x���g����
            SendUpdateStocks();
            return stock;
        }

        /// <summary>
        /// �ۗ�����
        /// </summary>
        /// <param name="stock"> �ۗ� </param>
        public void Digest( Stock stock )
        {
            m_digestStock = stock;
            m_fluctuateMachine.Begin( stock );
            // �C�x���g����
            SendUpdateStocks();
        }

        private void SendUpdateStocks()
        {
            // �C�x���g����
            m_receivers.ForEach( r => r.OnUpdateStock( m_digestStock, m_stocks.ToArray() ) );
        }

        //=================================================
        // Methods ( public static )
        //=================================================

        /// <summary>
        /// ���V�[�o�[�̒ǉ�
        /// </summary>
        /// <param name="receiver"> ���V�[�o�[ </param>
        public static void AddReceiver( IStockHolderReseiver receiver )
        {
            Instace.m_receivers.Add( receiver );
        }

        /// <summary>
        /// ���V�[�o�[�̍폜
        /// </summary>
        /// <param name="receiver"> ���V�[�o�[ </param>
        public static void RemoveReceiver( IStockHolderReseiver receiver )
        {
            Instace.m_receivers.Remove( receiver );
        }

        //=================================================
        // Methods ( FluctuateMachine Event )
        //=================================================

        /// <summary>
        /// �ϓ��J�n
        /// </summary>
        void OnFluctuateBegin()
        {
            m_receivers.ForEach( r => r.OnBeginDigest() );
        }

        /// <summary>
        /// �ϓ��I��
        /// </summary>
        void OnFluctuateEnd()
        {
            m_digestStock = null;
            m_receivers.ForEach( r => r.OnEndDigest() );
            SendUpdateStocks();
        }
    }
}
