using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MediaPlayer
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_open_Click(object sender, RoutedEventArgs e)
        {
            var fd = new Microsoft.Win32.OpenFileDialog();// 檔案開啟物件
            fd.Filter = "音訊檔案(*.mp3,*.3gp,*.wma)|*.mp3; *.3gp; *.wma|影片檔案(*.mp4, *.avi, *.mpeg, *.wmv)|*.mp4; *.avi; *.mpeg; *.wmv|所有檔案(*.*)|*.*";
            //選擇副標題的格式
            
            fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);// 設定預設開啟檔案位置，設定為桌面
            fd.ShowDialog();// 開啟對話框
            string filename = fd.FileName;// 如果使用者有選取檔案，就把檔案位置與檔名儲存到filename中
            if (filename != "")
            {
                txt_filePath.Text = filename;// 將檔案位置與檔名顯示在輸入文字框裡面
                Uri u = new Uri(filename);// 將檔案位置與檔名轉化成URI，一種用來設定檔案資源定位的位置資料
                Med_show.Source = u;// 將URI放進影音元件中
                Med_show.Volume = 0.5f; // 設定這個影音的聲音大小（可有可無）
                Med_show.LoadedBehavior = MediaState.Play;// 將影音進行播放
            }
        }
    }
}
