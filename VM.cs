using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EvalUITest
{
    // https://ru.stackoverflow.com/a/632894/10105
    class VM : INotifyPropertyChanged
    {
        protected bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
