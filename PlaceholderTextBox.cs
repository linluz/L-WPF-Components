using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Sys.Extensions;

namespace L_WPF_Components
{
    /// <summary>
    /// ռλ���ı���
    /// </summary>
    public class PlaceholderTextBox : TextBox
    {
        /// <summary>
        /// ռλ���ı���������
        /// </summary>
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(PlaceholderTextBox), new PropertyMetadata(string.Empty));

        /// <summary>
        /// ռλ����ɫ��������
        /// </summary>
        public static readonly DependencyProperty PlaceholderColorProperty =
            DependencyProperty.Register(nameof(PlaceholderColor), typeof(Brush), typeof(PlaceholderTextBox), new PropertyMetadata(Brushes.LightGray));

        /// <summary>
        /// ������Ϣ��������
        /// </summary>
        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register(nameof(ErrorMessage), typeof(string), typeof(PlaceholderTextBox), new PropertyMetadata(string.Empty, OnErrorMessageChanged));

        // ռλ���ı�
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        // ռλ����ɫ
        public Brush PlaceholderColor
        {
            get => (Brush)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        // ������Ϣ
        public string ErrorMessage
        {
            get => (string)GetValue(ErrorMessageProperty);
            set => SetValue(ErrorMessageProperty, value);
        }

        private static void OnErrorMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = d as PlaceholderTextBox;
            textBox?.UpdateErrorState();
        }

        private TextBlock? _errorTextBlock;
        private TextBlock? ErrorTextBlock
        {
            get => _errorTextBlock;
            set
            {
                if (value == null)
                {
                    if (Parent is Panel parent)
                    {
                        _errorTextBlock = new TextBlock
                        {
                            Foreground = Brushes.Red,
                            Visibility = Visibility.Collapsed
                        };
                        parent.Children.Add(_errorTextBlock);
                    }
                }
                else
                    _errorTextBlock = value;
            }
        }

        public PlaceholderTextBox() => Loaded += PlaceholderTextBox_Loaded;

        private void PlaceholderTextBox_Loaded(object sender, RoutedEventArgs e) => UpdateErrorState();
        
        private void UpdateErrorState()
        {
                ErrorTextBlock!.Text = ErrorMessage;
                ErrorTextBlock.Visibility = 
                    ErrorMessage.IsNullOrEmpty() 
                    ? Visibility.Collapsed 
                    : Visibility.Visible;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (Text.IsNullOrEmpty() && !IsFocused)
            {
                drawingContext.DrawText(
                    new FormattedText(
                        Placeholder,
                        System.Globalization.CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                        FontSize,
                        PlaceholderColor,
                        VisualTreeHelper.GetDpi(this).PixelsPerDip),
                    new Point(2, 2));
            }
        }
    }
}
