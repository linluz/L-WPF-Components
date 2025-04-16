using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Sys.Extensions;

namespace L_WPF_Components
{
    /// <summary>
    /// 占位符文本框
    /// </summary>
    public class PlaceholderTextBox : TextBox
    {
        public PlaceholderTextBox()
        {
            GotFocus += (_, _) => ClearPlaceholder();
            LostFocus += (_, _) => SetPlaceholder();
            Loaded += (_, _) => SetPlaceholder();
        }

        /// <summary>
        /// 占位符文本依赖属性
        /// </summary>
        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(nameof(PlaceholderText), typeof(string), typeof(PlaceholderTextBox), new PropertyMetadata(string.Empty));

        /// <summary>
        /// 占位符颜色依赖属性
        /// </summary>
        public static readonly DependencyProperty PlaceholderColorProperty =
            DependencyProperty.Register(nameof(PlaceholderColor), typeof(Brush), typeof(PlaceholderTextBox), new PropertyMetadata(Brushes.LightGray));
        /// <summary>
        /// 文本修改事件，增加失焦时被代码修改文本后自行调整颜色
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            if (!IsFocused //失焦
                && Text.NotNullOrEmpty() //不为空
                && Text != PlaceholderText)//不是占位文本
                Foreground = ForegroundBackground;//恢复颜色
            
        }

        /// <summary>
        /// 占位符文本
        /// </summary>
        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        /// <summary>
        /// 占位符颜色
        /// </summary>
        public Brush PlaceholderColor
        {
            get => (Brush)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        /// <summary>
        /// 设置占位符
        /// </summary>
        public void SetPlaceholder()
        {
            if (!Text.IsNullOrEmpty())
                return;
            ForegroundBackground = Foreground;
            Foreground = PlaceholderColor;
            Text = PlaceholderText;
        }

        /// <summary>
        /// 清理占位符
        /// </summary>
        public void ClearPlaceholder()
        {
            if (Text!=PlaceholderText)
                return;
            Foreground = ForegroundBackground;
            Text = "";
        }

        public Brush? ForegroundBackground;
    }
}
