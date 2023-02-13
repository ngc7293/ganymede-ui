using CommunityToolkit.Mvvm.ComponentModel;
using Ganymede.Services.Device;

namespace Ganymede.UI.ViewModels
{
    internal partial class DeviceViewModel : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string uid;

        public static DeviceViewModel FromProtobuf(Device device)
        {
            var state = new DeviceViewModel();
            state.Name = device.DisplayName;
            state.Uid = device.Uid;
            return state;
        }
    }
}
