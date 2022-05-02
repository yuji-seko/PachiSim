using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

namespace Pachinko.Ball
{
    public class SearchMenuWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        private MovementGraphView m_view = null;
        private EditorWindow m_window = null;

        public void Initialize( MovementGraphView graphView, EditorWindow editorWindow )
        {
            m_view = graphView;
            m_window = editorWindow;
        }

        List<SearchTreeEntry> ISearchWindowProvider.CreateSearchTree( SearchWindowContext context )
        {
            var entries = new List<SearchTreeEntry>();
            entries.Add( new SearchTreeGroupEntry( new GUIContent( "Create Node" ) ) );
            entries.Add( new SearchTreeGroupEntry( new GUIContent( "Node" ) ) { level = 1 } );

            var list = m_view.GetCreatableNodeList();
            list.ForEach( type =>
            {
                var gui = new GUIContent( type.ToString() );
                entries.Add( new SearchTreeEntry( gui ) { level = 2, userData = type } );
            } );

            return entries;
        }

        bool ISearchWindowProvider.OnSelectEntry( SearchTreeEntry searchTreeEntry, SearchWindowContext context )
        {
            // マウスの位置取得
            var worldMousePosition = m_window.rootVisualElement.ChangeCoordinatesTo( m_window.rootVisualElement.parent, context.screenMousePosition - m_window.position.position );
            var localMousePosition = m_view.contentViewContainer.WorldToLocal( worldMousePosition );

            var movementType = ( MovementType )searchTreeEntry.userData;
            return m_view.CreateNode( movementType, localMousePosition );
        }
    }
}
