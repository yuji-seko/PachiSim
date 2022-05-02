using Framework.UI;
using UnityEngine;

namespace PachiSim.Scenes.PachiScene
{
    [DisallowMultipleComponent]
    public sealed class PachiScene : MonoBehaviour
    {
        //=================================================
        // SerializeFields
        //=================================================
        [SerializeField] private PachiController    m_pachiController = null;
        [SerializeField] private ActionButton       m_startButton = null;
        [SerializeField] private ActionButton       m_stopButton = null;

        //=================================================
        // Fields ( private )
        //=================================================

        //=================================================
        // Methods ( MonoBehaviour )
        //=================================================

        public void Start()
        {
            m_pachiController.Init();

            m_startButton.OnClick = () => m_pachiController.Begin();
            m_stopButton.OnClick = () => m_pachiController.Stop();
        }

        private void Update()
        {

            foreach( var keycode in System.Enum.GetValues( typeof( KeyCode ) ) as KeyCode[] )
            {
                if ( Input.GetKey( keycode ) )
                {
                    Debug.Log( $"{keycode}" );
                }
            }
        }
    }
}
