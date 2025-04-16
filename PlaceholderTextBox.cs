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
        public PlaceholderTextBox()
        {
            GotFocus += (_, _) => ClearPlaceholder();
            LostFocus += (_, _) => SetPlaceholder();
            Loaded += (_, _) => SetPlaceholder();
        }

        /// <summary>
        /// ռλ���ı���������
        /// </summary>
        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(nameof(PlaceholderText), typeof(string), typeof(PlaceholderTextBox), new PropertyMetadata(string.Empty));

        /// <summary>
        /// ռλ����ɫ��������
        /// </summary>
        public static readonly DependencyProperty PlaceholderColorProperty =
            DependencyProperty.Register(nameof(PlaceholderColor), typeof(Brush), typeof(PlaceholderTextBox), new PropertyMetadata(Brushes.LightGray));
        /// <summary>
        /// �ı��޸��¼�������ʧ��ʱ�������޸��ı������е�����ɫ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            if (!IsFocused //ʧ��
                && Text.NotNullOrEmpty() //��Ϊ��
                && Text != PlaceholderText)//����ռλ�ı�
                Foreground = ForegroundBackground;//�ָ���ɫ
            
        }

        /// <summary>
        /// ռλ���ı�
        /// </summary>
        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        /// <summary>
        /// ռλ����ɫ
        /// </summary>
        public Brush PlaceholderColor
        {
            get => (Brush)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        /// <summary>
        /// ����ռλ��
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
        /// ����ռλ��
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
