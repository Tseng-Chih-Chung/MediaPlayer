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
using System.Windows.Threading;//同時跑兩個程式

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
            var fd = new Microsoft.Win32.OpenFileDialog();// 外部檔案開啟物件
            //如果只要OpenFileDialog();那在最前面要多加using Microsoft.Win32;
            fd.Filter = "音訊檔案(*.mp3,*.3gp,*.wma)|*.mp3; *.3gp; *.wma|影片檔案(*.mp4, *.avi, *.mpeg, *.wmv)|*.mp4; *.avi; *.mpeg; *.wmv|所有檔案(*.*)|*.*";
            //過濾副檔名(格式)
            fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);// 設定預設開啟檔案位置，設定為桌面
            fd.ShowDialog();// 開啟對話框
            string filename = fd.FileName;// 如果使用者有選取檔案，就把檔案位置與檔名儲存到filename中
            if (filename != "")
            {
                txt_filePath.Text = filename;// 將檔案位置與檔名顯示在輸入文字框裡面
                Uri u = new Uri(filename);// 將檔案位置與檔名轉化成URI(一種用來設定檔案資源定位的位置資料)
                Med_show.Source = u;// 將URI(檔案位置)放進影音元件中
                Med_show.Volume = 1f; // 設定這個影音的聲音大小（可有可無）(0~1)
                Med_show.LoadedBehavior = MediaState.Play;// 將影音進行播放
            }
        }

        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            Med_show.LoadedBehavior = MediaState.Play;//讓檔案的加載行為設為撥放
        }

        private void btn_pause_Click(object sender, RoutedEventArgs e)
        {
            Med_show.LoadedBehavior = MediaState.Pause;//讓檔案的加載行為設為暫停
        }

        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            Med_show.LoadedBehavior = MediaState.Stop;//讓檔案的加載行為設為停止並從頭撥放
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);//關閉視窗
        }

        private void sliVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Med_show.Volume = sliVolume.Value; // 讓影片的聲音跟大小聲拖動處連動
            //txt_filePath.Text = Med_show.Volume.ToString();// 讓音量數值具體顯示在上方的文字輸入框
        }

        TimeSpan TimePosition; // 宣告一個時間間格
        DispatcherTimer timer = null; // 宣告一個「空的」計時器
        private void Med_show_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimePosition = Med_show.NaturalDuration.TimeSpan; // 取得所開啟影片的時間長度
            sliProgress.Minimum = 0;// 讓影片播放滑桿從零開始
            sliProgress.Maximum = TimePosition.TotalMilliseconds; //最大值設定為影片的總毫秒數(該影音時長)

            timer = new DispatcherTimer();// 設定計時器
            timer.Interval = TimeSpan.FromSeconds(1); // 這個計時器設定每一個刻度為1秒(1秒跑1次)
            timer.Tick += new EventHandler(timer_tick); //每一個時間刻度設定一個小程序timer_tick
            timer.Start(); // 啟動這個計時器
        }

        private void timer_tick(object sender, EventArgs e)
        {
            sliProgress.Value = Med_show.Position.TotalMilliseconds;// 小程序，更新目前影片播放進度(顯示當前進度)
        }

        private void sliProgress_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int SliderValue = (int)sliProgress.Value; // 還記得轉型嗎？

            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue); //將滑桿的數值改變成時間間格的資料形式
            Med_show.Position = ts; // 調整影片播放進度到新的時間
        }

    }
}
