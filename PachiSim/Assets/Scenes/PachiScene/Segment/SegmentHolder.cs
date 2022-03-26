using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PachiSim.Scenes.PachiScene
{
    [DisallowMultipleComponent]
    public sealed class SegmentHolder : MonoBehaviour
    {
        //=================================================
        // SerializeFields
        //=================================================
        [SerializeField] private Segment[] m_segments = new Segment[ 3 ];


    }
}
