using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAlertBoxDemo.Controls
{
    public class ObservableProperty<T>
    {
        T value;

        public delegate void ChangeEvent(T data);
        public event ChangeEvent changed;

        public ObservableProperty(T initialValue)
        {
            value = initialValue;
        }

        public void Set(T v)
        {
            if (!v.Equals(value))
            {
                value = v;
                if (changed != null)
                {
                    changed(value);
                }
            }
        }

        public T Get()
        {
            return value;
        }

        public static implicit operator T(ObservableProperty<T> p)
        {
            return p.value;
        }
    }
}
