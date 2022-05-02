using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pachinko.Ball
{
    /// <summary>
    /// 球の挙動
    /// </summary>
    [CreateAssetMenu(menuName = "Pachinko/" + nameof( BallMovementAsset ), fileName = nameof( BallMovementAsset ) )]
    public class BallMovementAsset : ScriptableObject
    {
        //=====================================================================
        // Fields( private )
        //=====================================================================
        [SerializeReference] private string m_name = null;
        [SerializeReference] private Movement m_movement = null;

        //=====================================================================
        // Properties( public )
        //=====================================================================
        public Movement Movement => m_movement;
        public string Name { get => m_name; set => m_name = value; }

        //=====================================================================
        // Methods( public )
        //=====================================================================

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BallMovementAsset() { }

        /// <summary>
        /// 動作設定
        /// </summary>
        public void SetMovement( Movement movement )
        {
            m_movement = movement;
        }
    }
}

