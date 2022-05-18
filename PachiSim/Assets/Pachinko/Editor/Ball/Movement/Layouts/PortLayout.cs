using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Pachinko.Ball
{
    /// <summary>
    /// ポートレイアウト
    /// </summary>
    public abstract class PortLayout : VisualElement
    {
        public abstract IReadOnlyList<Port> Ports { get; }
        public abstract IReadOnlyList<MovementNode> Nodes { get; }
    }
}
