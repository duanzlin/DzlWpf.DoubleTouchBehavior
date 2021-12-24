using DzlWpf.DoubleTouchBehavior.Extensions;
using Prism.Interactivity;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DzlWpf.DoubleTouchBehavior
{
    public class DoubleTouchCommandBehavior : CommandBehaviorBase<UIElement>
    {
        private static TaskCompletionSource<string> _tcs;
        public DoubleTouchCommandBehavior(UIElement element)
            : base(element)
        {
            element.MouseDown += Element_MouseDown; // 鼠标点击
            //element.TouchDown += Element_TouchDown; // 触屏点击
        }
        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Element_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OnClick();
        }

        /// <summary>
        /// 触摸点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Element_TouchDown(object sender, TouchEventArgs e)
        {
            OnClick();
        }

        private void OnClick()
        {
            // 第二次点击到来，给任务赋值
            if (_tcs != null && !_tcs.Task.IsCompleted && !_tcs.Task.IsCanceled)
            {
                _tcs.TrySetResult(CommandParameter.ToString());
            }
            // 第一次点击到来，则等待第二次点击
            else
            {
                _ = WaitAndClickOther(CommandParameter.ToString());
            }
        }

        /// <summary>
        /// 等待第二次按下
        /// </summary>
        /// <param name="first">第一次按键</param>
        /// <returns></returns>
        private async Task WaitAndClickOther(string first)
        {
            _tcs = new TaskCompletionSource<string>();

            // 登记第二次点击任务结果，三秒内未点击第二次，则双击任务取消结束
            var second = await _tcs.WithCancellation(new CancellationTokenSource(3000).Token);
            // 保存命令参数
            var origin = CommandParameter.ToString();
            CommandParameter = $"{first},{second}";
            ExecuteCommand(CommandParameter);

            // 还原命令参数
            CommandParameter = origin;

        }
        
    }

}
