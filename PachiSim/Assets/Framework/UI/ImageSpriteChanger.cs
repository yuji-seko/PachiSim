using UnityEngine;
using UnityEngine.UI;

namespace Framework.UI
{
    [DisallowMultipleComponent]
    public class ImageSpriteChanger : MonoBehaviour
    {
        //=================================================
        // SerializeFields
        //=================================================
        [SerializeField] private Image          m_image     = null;
        [SerializeField] private SpriteAssets   m_assets    = null;

        //=================================================
        // Methods( public )
        //=================================================

        public void Set( int index )
        {
            m_image.sprite = m_assets.Get( index );
        }
    }
}
