using System;

namespace UnityEngine.UIElements
{
    public static class VisualElementExtensions
    {
        /// <summary>
        /// 最初に一致するものを取得する
        /// </summary>
        public static T First<T>( this VisualElement self, string name ) where T : VisualElement
        {
            return self.Query<T>( name ).First();
        }

        /// <summary>
        /// 最初に一致するものを取得する
        /// </summary>
        public static VisualElement First( this VisualElement self, string name )
        {
            return self.Query<VisualElement>( name ).First();
        }

        /// <summary>
        /// ボタンイベントの追加
        /// </summary>
        public static void AddButtonEvent( this VisualElement self, string buttonName, Action onClick )
        {
            var button = self.First<Button>( buttonName );
            if ( button != null )
            {
                button.clicked += onClick;
            }
        }

        /// <summary>
        /// ボタンイベントの削除
        /// </summary>
        public static void RemoveButtonEvent( this VisualElement self, string buttonName, Action onClick )
        {
            var button = self.First<Button>( buttonName );
            if ( button != null )
            {
                button.clicked -= onClick;
            }
        }

        public static void SetEnabledButton( this VisualElement self, string buttonName, bool enabled )
        {
            var button = self.First<Button>( buttonName );
            button.SetEnabled( enabled );
        }
    }
}
