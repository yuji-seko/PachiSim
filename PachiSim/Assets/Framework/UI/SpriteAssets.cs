using System.Linq;
using UnityEngine;

namespace Framework.UI
{
    [CreateAssetMenu( fileName = "SpriteAssets", menuName = "Framework/UI/Assets/SpriteAssets" )]
    public class SpriteAssets : ScriptableObject
    {
        //=================================================
        // SerializeFields
        //=================================================
        [SerializeField] private Sprite[] m_sprites = null;

        //=================================================
        // Methods( public )
        //=================================================

        public Sprite Get( int index )
        {
            return m_sprites.ElementAtOrDefault( index );
        }
    }
}
