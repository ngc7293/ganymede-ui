using CommunityToolkit.Mvvm.ComponentModel;
using Ganymede.Services.Device;

namespace Ganymede.UI.ViewModels
{
    internal partial class ConfigViewModel : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string uid;

        public static ConfigViewModel FromProtobuf(Config config)
        {
            var state = new ConfigViewModel();
            state.Name = config.DisplayName;
            state.Uid = config.Uid;
            return state;
        }
    }
}
