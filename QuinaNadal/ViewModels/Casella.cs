using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuinaNadal.ViewModels
{
    public class Casella : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int _id;
        private bool _marcada;

        public Casella(int id)
        {
            _id = id;
        }

        public bool Marcada
        {
            get => _marcada;
            set
            {
                if (_marcada != value)
                {
                    _marcada = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Id => _id;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
