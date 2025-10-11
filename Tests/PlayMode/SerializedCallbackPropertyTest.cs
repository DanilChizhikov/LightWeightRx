using UnityEngine;

namespace DTech.LightWeightRx.Tests.PlayMode
{
    internal sealed class SerializedCallbackPropertyTest : MonoBehaviour
    {
        [SerializeField] private SerializedCallbackProperty<int> _property = new (int.MinValue);
        
        public IReadOnlyCallbackProperty<int> Property => _property;
        
        public void SetValue(int value)
        {
            _property.Value = value;
        }
    }
}