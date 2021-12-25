using DzlWpf.DoubleTouchBehavior.Extensions;
using Prism.Interactivity;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DzlWpf.DoubleTouchBehavior
{
    public class DoubleTouchCommandBehavior : CommandBehaviorBase<UIElement>
    {
        private static TaskCompletionSource<TouchResult> _tcs;
        public DoubleTouchCommandBehavior(UIElement element)
            : base(element)
        {
            //element.MouseDown += Element_MouseDown; // 鼠标点击
            element.TouchDown += Element_TouchDown; // 触屏点击
        }
        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Element_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OnClick(sender);
        }

        /// <summary>
        /// 触摸点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Element_TouchDown(object sender, TouchEventArgs e)
        {
            OnClick(sender);
        }

        private void OnClick(object sender)
        {
            // 第二次点击到来，给任务赋值
            if (_tcs != null && !_tcs.Task.IsCompleted && !_tcs.Task.IsCanceled)
            {
                _tcs.TrySetResult(new TouchResult { CommandParameter = CommandParameter?.ToString(), Source = sender });
            }
            // 第一次点击到来，则等待第二次点击
            else
            {
                _ = WaitAndClickOther(sender, CommandParameter?.ToString());
            }
        }

        /// <summary>
        /// 等待第二次按下
        /// </summary>
        /// <param name="first">第一次按键</param>
        /// <returns></returns>
        private async Task WaitAndClickOther(object sender, string first)
        {
            _tcs = new TaskCompletionSource<TouchResult>();

            // 登记第二次点击任务结果，三秒内未点击第二次，则双击任务取消结束
            var second = await _tcs.WithCancellation(new CancellationTokenSource(Math.Max(500, TouchInerval)).Token);

            // 单个元素双击事件时，两次点击不是同一控件
            if (!TouchShared && sender != second.Source)
                return;

            // 保存命令参数
            var origin = CommandParameter?.ToString();
            CommandParameter = $"{first},{second.CommandParameter}";
            ExecuteCommand(CommandParameter);

            // 还原命令参数
            CommandParameter = origin;

        }

        public bool TouchShared { get; set; }

        public int TouchInerval { get; set; } = 2000;

    }

    public class TouchResult
    {
        public object Source { get; set; }

        public string CommandParameter { get; set; }
    }

}
