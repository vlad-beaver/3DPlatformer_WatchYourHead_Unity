
using System;

namespace Assets.Scripts.Infrastructure
{
    public class ObservableValue<T>
        where T: struct
    {
        public T Value
        {
            get => _value;
            set
            {
                if (_value.Equals(value))
                {
                    return;
                }

                OnValueChanged?.Invoke(_value, value);
                _value = value;
            }
        }
        private T _value;

        /// <summary>
        /// The first argument is old value, the second arg - newest value.
        /// </summary>
        public event Action<T, T> OnValueChanged;

        public ObservableValue(T value)
        {
            _value = value;
        }

        public static implicit operator T(ObservableValue<T> obj) => obj.Value;
    }
}
