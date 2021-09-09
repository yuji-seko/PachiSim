namespace UnityEngine
{
    public static class MonoBehaviourExtensions
    {
        public static void SetActive( this MonoBehaviour self, bool active )
        {
            self.gameObject.SetActive( active );
        }
    }
}
