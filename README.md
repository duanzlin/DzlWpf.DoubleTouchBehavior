# WpfDoubleTouchBehavior
在Wpf Prism应用程序中绑定屏幕双击事件到viewmodel命令.

## 依赖项
[Prism.Wpf](https://github.com/PrismLibrary/Prism)

## 使用方法
1、安装nuget包 
```Shell
dotnet add package DzlWpf.DoubleTouchBehavior
```
2、单元素双击事件

在xaml中添加触摸元素，并添加绑定
```xml
<UserControl ...
        xmlns:bh="clr-namespace:DzlWpf.DoubleTouchBehavior;assembly=DzlWpf.DoubleTouchBehavior">
        <Grid>
            ...
            <Border bh:DoubleTouched.Command="{Binding DoubleTouchCommand}"
                Width="80" Height="80" HorizontalAlignment="Center" VerticalAlignment="Center" Background="LightGreen"/>
        </Grid>
</UserControl>
```
在viewmodel中添加命令属性和处理方法
```CSharp
        public TestPageViewModel()
        {
            DoubleTouchCommand = new DelegateCommand(OnDoubleTouched);
        }

        private void OnDoubleTouched()
        {
            System.Windows.MessageBox.Show("这是双击效果");
        }

        public DelegateCommand DoubleTouchCommand { get; private set; }
```
4、多个元素相互的双击事件

Xaml
```xml
        <Border bh:DoubleTouched.Command="{Binding SharedDoubleTouchCommand}" bh:DoubleTouched.CommandParameter="左上" bh:DoubleTouched.TouchShared="True"
                Width="80" Height="80" HorizontalAlignment="Left" VerticalAlignment="Top" Background="LightBlue"/>
        <Border bh:DoubleTouched.Command="{Binding SharedDoubleTouchCommand}" bh:DoubleTouched.CommandParameter="右上"  bh:DoubleTouched.TouchShared="True"
                Width="80" Height="80" HorizontalAlignment="Right" VerticalAlignment="Top" Background="LightBlue"/>
```
ViewModel
```CSharp
        public TestPageViewModel()
        {
            ...
            SharedDoubleTouchCommand = new DelegateCommand<string>(OnSharedDoubleTouched);
        }

        ...

        private void OnSharedDoubleTouched(string args)
        {
            System.Windows.MessageBox.Show("双击结果：" + args);
        }

        ...

        public DelegateCommand<string> SharedDoubleTouchCommand { get; private set; }
```
5、自定义双击间隔
默认的双击间隔时间是2000毫秒，可通过如下方式设置双击间隔：
```xml
    <Border bh:DoubleTouched.TouchInerval="3000" ...>
```
