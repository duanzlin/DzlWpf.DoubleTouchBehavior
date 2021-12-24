using Prism.Commands;
using Prism.Mvvm;

namespace Sample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {
            DoubleTouchCommand = new DelegateCommand<string>(OnDoubleTouch);
        }

        private void OnDoubleTouch(string args)
        {
            System.Windows.MessageBox.Show("点击结果：" + args);
        }

        public DelegateCommand<string> DoubleTouchCommand { get; private set; }
    }
}
