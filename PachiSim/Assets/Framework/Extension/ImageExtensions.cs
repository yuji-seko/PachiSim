namespace UnityEngine.UI
{
    public static class ImageExtensions
    {
        public static void SafeSetSprite( this Image self, Sprite sprite )
        {
            if ( self != null )
            {
                self.sprite = sprite;
            }
        }
    }
}
