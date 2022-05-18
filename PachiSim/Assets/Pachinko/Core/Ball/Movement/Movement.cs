using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pachinko.Ball
{
    public enum MovementType
    {
        Injection,
        Bounce,
        Chucker,
        OutPocket,
        WinPocket,
    }

    /// <summary>
    /// 玉の挙動
    /// </summary>
    [Serializable]
    public abstract class Movement
    {
        [SerializeReference] private List<Movement> m_upstreams = new List<Movement>();
        [SerializeReference] private List<Movement> m_downstreams = new List<Movement>();

        public abstract MovementType MovementType { get; }

        // 上流
        public IReadOnlyList<Movement> Upstreams => m_upstreams;
        public abstract bool IsPossessableUpstreams { get; } // 所持可能か
        public abstract int PossessableUpstreamsCount { get; } // 所持可能数

        // 下流
        public IReadOnlyList<Movement> Downstreams => m_downstreams;
        public abstract bool IsPossessableDownstreams { get; } // 所持可能か
        public abstract int PossessableDownstreamsCount { get; } // 所持可能数

        public void AddUpstream( Movement upstream )
        {
            m_upstreams.Add( upstream );
        }

        public void RemoveUpstream( Movement upstream )
        {
            m_upstreams.Remove( upstream );
        }

        public void AddDownstream( Movement downstream )
        {
            m_downstreams.Add( downstream );
        }

        public void RemoveDownstream( Movement downstream )
        {
            m_downstreams.Remove( downstream );
        }
    }

    /// <summary>
    /// 打ち出し
    /// </summary>
    [Serializable]
    public sealed class Injection : Movement
    {
        public override MovementType MovementType => MovementType.Injection;

        public override bool IsPossessableUpstreams => false;
        public override int PossessableUpstreamsCount => 1;

        public override bool IsPossessableDownstreams => true;
        public override int PossessableDownstreamsCount => 1;
    }

    /// <summary>
    /// 分岐
    /// </summary>
    [Serializable]
    public class Bounce : Movement
    {
        public override MovementType MovementType => MovementType.Bounce;

        public override bool IsPossessableUpstreams => true;
        public override int PossessableUpstreamsCount => 1;

        public override bool IsPossessableDownstreams => true;
        public override int PossessableDownstreamsCount => 999;
    }

    /// <summary>
    /// チャッカー
    /// </summary>
    [Serializable]
    public class Chucker : Movement
    {
        public override MovementType MovementType => MovementType.Chucker;

        public override bool IsPossessableUpstreams => true;
        public override int PossessableUpstreamsCount => 1;

        public override bool IsPossessableDownstreams => true;
        public override int PossessableDownstreamsCount => 1;
    }

    /// <summary>
    /// アウト
    /// </summary>
    [Serializable]
    public class OutPocket : Movement
    {
        public override MovementType MovementType => MovementType.OutPocket;

        public override bool IsPossessableUpstreams => true;
        public override int PossessableUpstreamsCount => 1;

        public override bool IsPossessableDownstreams => false;
        public override int PossessableDownstreamsCount => 1;
    }

    /// <summary>
    /// 入賞
    /// </summary>
    [Serializable]
    public class WinPocket : Movement
    {
        public override MovementType MovementType => MovementType.WinPocket;

        public override bool IsPossessableUpstreams => true;
        public override int PossessableUpstreamsCount => 1;

        public override bool IsPossessableDownstreams => true;
        public override int PossessableDownstreamsCount => 1;
    }
}
