using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Ganymede.UI.ViewModels
{
    internal partial class AppViewModel : ObservableObject
    {
        [ObservableProperty]
        private string accessToken = "";

        public ObservableCollection<DeviceViewModel> Devices = new ObservableCollection<DeviceViewModel>();
    }
}
