using QuinaNadal.Properties;
using QuinaNadalClient;
using QuinaNadalServer.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Threading;

namespace QuinaNadal.ViewModels
{
    public class MainViewModel : BindableBase, INotifyPropertyChanged
    {
        const int MidaTaulell = 100;

        private enum Mode { Client, Server }
        private ApiClient _apiClient;
        private bool _syncEnabled;
        private Mode _mode;

        public ObservableCollection<Casella> Taulell { get; set; } = new ObservableCollection<Casella>();

        private string _logo;
        private string _titol1;
        private string _titol2;
        private bool _syncOk = true;
        private DispatcherTimer _dispatcherTimer;
        private int _refreshRate;
        private bool _demoMode;
        private int _indexDemo = 0;

        public string Logo
        {
            get => _logo;
            set => SetProperty(ref _logo, value);
        }

        public string Titol1
        {
            get => _titol1;
            set => SetProperty(ref _titol1, value);
        }

        public string Titol2
        {
            get => _titol2;
            set => SetProperty(ref _titol2, value);
        }

        public bool SyncOk
        {
            get => _syncOk;
            set => SetProperty(ref _syncOk, value);
        }

        public MainViewModel()
        {
            Titol1 = Settings.Default.Titol;
            Titol2 = Settings.Default.Entitat;

            for (int i = 0; i < MidaTaulell; i++)
            {
                var c = new Casella(i);
                c.PropertyChanged += OnCasellaChanged;
                Taulell.Add(c);
            }

            CheckLogos();
            InitSync();
        }

        private void CheckLogos()
        {
            try
            {
                string exePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string logoFile = Path.Combine(exePath, "logo.jpg");
                if (File.Exists(logoFile))
                {
                    Logo = logoFile;
                    return;
                }

                logoFile = Path.Combine(exePath, "logo.png");
                if (File.Exists(logoFile))
                {
                    Logo = logoFile;
                    return;
                }

                Logo = null;
            }
            catch
            {
                Logo = null;
            }
        }

        private void OnCasellaChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_syncEnabled)
                return;

            if (_mode != Mode.Server)
                return;

            Taulell taulell = new Taulell();
            taulell.Marcats.AddRange(Taulell.Where(x => x.Marcada).Select(x => x.Id));
            try
            {
                _apiClient.SetTaulell(taulell);
                SyncOk = true;
            }
            catch (Exception ex)
            {
                SyncOk = false;
                Console.WriteLine(ex.Message);
            }
        }

        private void InitSync()
        {
            _syncEnabled = Settings.Default.ServerSync;
            _mode = Settings.Default.ModeServer ? Mode.Server : Mode.Client;
            _refreshRate = Settings.Default.RefreshRate;
            _demoMode = Settings.Default.RunDemo;

            if (_refreshRate < 200)
                _refreshRate = 200;

            if (!_syncEnabled)
                return;

            if (_apiClient == null)
                _apiClient = new ApiClient(new Uri(Settings.Default.ServerUrl));

            if (_mode == Mode.Client)
            {
                _dispatcherTimer = new DispatcherTimer();
                _dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(_refreshRate);
                _dispatcherTimer.Start();
            }
            else if (_demoMode)
            {
                _dispatcherTimer = new DispatcherTimer();
                _dispatcherTimer.Tick += new EventHandler(demoTimer_Tick);
                _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000);
                _dispatcherTimer.Start();
            }
        }

        private void demoTimer_Tick(object sender, EventArgs e)
        {
            if (_indexDemo > 0 && _indexDemo <= 90)
                Taulell[_indexDemo].Marcada = false;

            _indexDemo = _indexDemo + 1;

            if (_indexDemo > 90)
                _indexDemo = 1;

            Taulell[_indexDemo].Marcada = true;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (!_syncEnabled)
                return;
            try
            {
                Taulell taulell = _apiClient.GetTaulell();

                for (int i = 0; i < MidaTaulell; i++)
                {
                    bool marcada = taulell.Marcats.Contains(i);
                    if (Taulell[i].Marcada != marcada)
                        Taulell[i].Marcada = marcada;
                }

                SyncOk = true;
            }
            catch (Exception ex)
            {
                SyncOk = false;
                Console.WriteLine(ex.Message);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < Taulell.Count; i++)
                Taulell[i].Marcada = false;
        }
    }
}
