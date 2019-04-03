namespace Xzy.EmbeddedApp.WinForm.Forms
{
    /// <summary>
    /// UpdateConfirmView.xaml 的交互逻辑
    /// </summary>
    public partial class UpdatingView
    {
        //public UpdatingView(string msg) : this()
        //{
        //    TbMsg.Text = msg;
        //}

        public UpdatingView()
        {
            InitializeComponent();
            DataContext = new UpdatingViewModel(this);
        }
    }
}
