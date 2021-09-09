using TMPro;
using UnityEngine;

namespace PachiSim.Scenes.PachiScene
{
    public sealed class DataCounterUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_text = null;

        private int m_count = 0;

        public void Init()
        {
            RefreshUI();
        }

        public void AddCount()
        {
            m_count++;
            RefreshUI();
        }

        private void RefreshUI()
        {
            m_text.text = $"{m_count}";
        }
    }
}
