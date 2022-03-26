using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Pachinko
{
    /// <summary>
    /// ���̋�����ݒ肷��r���[
    /// </summary>
    public class BallMovementGraphView : GraphView
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public BallMovementGraphView() : base()
        {
            // �w�i
            //Insert( 0, new GridBackground() );

            // �Y�[����
            SetupZoom( ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale );

            // �h���b�O��
            this.AddManipulator( new SelectionDragger() );

            // �m�[�h�쐬��
            nodeCreationRequest += context =>
            {
                AddElement( new BallBranchNode() );
            };

            // �����m�[�h
            AddElement( new BallInjectionNode() );
        }

        /// <summary>
        /// �ڑ��\�ȃ|�[�g���擾����
        /// </summary>
        public override List<Port> GetCompatiblePorts( Port startAnchor, NodeAdapter nodeAdapter )
        {
            return ports.ToList();
        }
    }
}

