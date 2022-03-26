using PachiSim.Lottery;
using UnityEngine;

namespace PachiSim.Scenes.PachiScene
{
    /// <summary>
    /// �R���g���[���[
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
        /// ������
        /// </summary>
        public void Init()
        {
            // �`���b�J�[
            m_chucker.Init();
            m_chucker.OnChuck.Subcrive( OnChuck ).AddTo( this );

            // ���I�}�V��
            m_lotteryMachine.Init();
            m_lotteryMachine.AddLotteryData( new LotteryData( LOTTERY_TYPE.Jackpot, 199.8f ) );

            // �ۗ�
            m_stockHolder.Init();
            m_stockHolderView.Init();

            // �f�[�^�J�E���^�[
            m_dataCounter.Init();
        }

        /// <summary>
        /// �J�n
        /// </summary>
        public void Begin()
        {
            m_chucker.Begin();
        }

        /// <summary>
        /// ��~
        /// </summary>
        public void Stop()
        {
            m_chucker.Stop();
        }

        /// <summary>
        /// ����
        /// </summary>
        void OnChuck()
        {
            var results = m_lotteryMachine.GetResults();
            var stock = m_stockHolder.CreateStock( results );

            // �������Ȃ�L���[�ɒǉ�
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
        /// �ۗ��̏����J�n���̏���
        /// </summary>
        void IStockHolderReseiver.OnBeginDigest()
        {

        }

        /// <summary>
        /// �ۗ��̏����I�����̏���
        /// </summary>
        void IStockHolderReseiver.OnEndDigest()
        {
            // �J�E���g
            m_dataCounter.AddCount();

            // �ۗ������o���ď���
            var stock = m_stockHolder.Dequeue();
            if ( stock != null )
            {
                m_stockHolder.Digest( stock );
            }
        }

        /// <summary>
        /// �ۗ����ω����̏���
        /// </summary>
        /// <param name="digestStock"> �������ۗ̕� /param>
        /// <param name="reserveStocks"> �\�񒆂ۗ̕� </param>
        void IStockHolderReseiver.OnUpdateStock( Stock digestStock, Stock[] reserveStocks )
        {
        }
    }
}
