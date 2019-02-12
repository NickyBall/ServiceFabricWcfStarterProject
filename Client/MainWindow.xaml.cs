using Client.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }

    public class MainViewModel : BindableBase
    {
        private string endpointUrl;
        private string a;
        private string b;
        private string logContent = string.Empty;
        private bool isConnected = false;

        ICalculatorService Calculator { get; set; }

        public string EndpointUrl { get => endpointUrl; set => SetProperty(ref endpointUrl, value, nameof(EndpointUrl)); }
        public string A { get => a; set => SetProperty(ref a, value, nameof(A)); }
        public string B { get => b; set => SetProperty(ref b, value, nameof(B)); }
        public string LogContent { get => logContent; set => SetProperty(ref logContent, value, nameof(LogContent)); }
        public bool IsConnected { get => isConnected; set => SetProperty(ref isConnected, value, nameof(IsConnected)); }

        public AsyncDelegateCommand ConnectClick => new AsyncDelegateCommand(() =>
        {
            try
            {
                ChannelFactory<ICalculatorService> Factory = new ChannelFactory<ICalculatorService>(new NetTcpBinding(), new EndpointAddress(EndpointUrl));
                Calculator = Factory.CreateChannel();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        });
        public AsyncDelegateCommand AddClick => new AsyncDelegateCommand( async () =>
        {
            try
            {
                var Result = await Calculator.Add(double.Parse(A), double.Parse(B));
                LogContent += $"{A} + {B} = {Result}\n";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        });
    }
}
