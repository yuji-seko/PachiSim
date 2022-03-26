using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Pachinko
{
    /// <summary>
    /// 球の挙動を設定するビュー
    /// </summary>
    public class BallMovementGraphView : GraphView
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BallMovementGraphView() : base()
        {
            // 背景
            //Insert( 0, new GridBackground() );

            // ズーム可
            SetupZoom( ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale );

            // ドラッグ可
            this.AddManipulator( new SelectionDragger() );

            // ノード作成可
            nodeCreationRequest += context =>
            {
                AddElement( new BallBranchNode() );
            };

            // 初期ノード
            AddElement( new BallInjectionNode() );
        }

        /// <summary>
        /// 接続可能なポートを取得する
        /// </summary>
        public override List<Port> GetCompatiblePorts( Port startAnchor, NodeAdapter nodeAdapter )
        {
            return ports.ToList();
        }
    }
}

