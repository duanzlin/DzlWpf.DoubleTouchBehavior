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
            DoubleTouchCommand = new DelegateCommand(OnDoubleTouched);
            SharedDoubleTouchCommand = new DelegateCommand<string>(OnSharedDoubleTouched);
        }

        private void OnDoubleTouched()
        {
            System.Windows.MessageBox.Show("这是双击效果");
        }

        private void OnSharedDoubleTouched(string args)
        {
            System.Windows.MessageBox.Show("双击结果：" + args);
        }

        public DelegateCommand DoubleTouchCommand { get; private set; }

        public DelegateCommand<string> SharedDoubleTouchCommand { get; private set; }
    }
}
