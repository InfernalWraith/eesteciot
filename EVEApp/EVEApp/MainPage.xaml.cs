using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EVEApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>




    public sealed partial class MainPage : Page
    {
        static DeviceClient deviceClient;
        static ServiceClient serviceClient;


        static string iotHubUri = "elvircrn.azure-devices.net";
        static string deviceKey = "RlW5aIVRcmlesaxDNNsBrOcWt38BktwmnSAm44lw344=";
        static string connectionString = "HostName=elvircrn.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=i5/ZCqxvkpUWPY62jbPZr1hD6cyfSaNzOsRvzicn15Y=";

        public MainPage()
        {
            this.InitializeComponent();
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("yugo45", deviceKey));
            ReceiveC2dAsync();
        }


        private async void ReceiveC2dAsync()
        {
            while (true)
            {
                Microsoft.Azure.Devices.Client.Message receivedMessage = await deviceClient.ReceiveAsync();
                if (receivedMessage == null) continue;
                float[] receivedData = Shared.ConvertByteArrayToFloat(receivedMessage.GetBytes());

                humidityText.Text = receivedData[0].ToString();
                tempText.Text = receivedData[1].ToString();
                lightText.Text = receivedData[2].ToString();

                await deviceClient.CompleteAsync(receivedMessage);
            }

        }

    }
}
