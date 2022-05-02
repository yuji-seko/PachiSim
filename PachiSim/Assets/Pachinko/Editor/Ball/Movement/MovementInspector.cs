using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Pachinko.Ball
{
    [CustomEditor(typeof(BallMovementAsset))]
    public class MovementInspector : Editor
    {
        private void OnEnable()
        {
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}
