using System;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.UI
{
    [DisallowMultipleComponent]
    [RequireComponent( typeof( Button ) )]
    public sealed class ActionButton : MonoBehaviour
    {
        //=================================================
        // SerializeFields
        //=================================================
        [SerializeField] private Button m_button = null;

        //=================================================
        // Properties ( public )
        //=================================================
        public Action OnClick { set => m_button.onClick.AddListener( () => value() ); }

        //=================================================
        // Methods ( MonoBehaviour )
        //=================================================
        private void Reset()
        {
            m_button = GetComponent<Button>();
        }

        private void OnDestroy()
        {
            m_button.onClick.RemoveAllListeners();
        }
    }
}
