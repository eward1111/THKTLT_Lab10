using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace THKTLT_Lab10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        TextBlock lastTextBlockClicked;
        bool findingMatch = false;
        DispatcherTimer timer = new DispatcherTimer(); //Tạo biến timer
        DispatcherTimer timer1 = new DispatcherTimer();
        int tenthsOfSecondsElapsed; //Tạo biến đếm thời gian 1/10 giây
        int matchesFound; //Số cặp được trùng khớp
        bool firstTime = true; // Lần chơi đầu tiên.
        int temp2;          // Lưu thời gian dùng để so sánh giữa 2 lần chơi.
        public string[,] randomAnimalArray = new string[4, 4];
        private TextBlock textBlock;
        abc Good = new abc();
        public MainPage()
        {
            this.InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            SetUpGame();

            timer1.Interval = TimeSpan.FromSeconds(1);
            timer1.Tick += Timer1_Tick;

            Good.ReturnArray();
            randomAnimalArray = Good.myArray;
        }

        private void Timer1_Tick(object sender, object e)
        {
            textBlock.Text = "?";
            //textBlock.Visibility = Visibility.Collapsed;
            timer1.Stop();
        }
        private void Timer_Tick(object sender, object e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                int temp1 = tenthsOfSecondsElapsed; // current time
                if (firstTime) // lần chơi đầu tiên mặc định lưu high score
                {
                    highTextBlock.Text = timeTextBlock.Text;
                    firstTime = false;
                    temp2 = temp1;
                }
                else if (temp1 < temp2) // lưu high score mới
                {
                    highTextBlock.Text = timeTextBlock.Text;
                    temp2 = temp1;
                }
                else // giữ nguyên high score
                {
                    highTextBlock.Text = highTextBlock.Text;
                }
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        public void SetUpGame()
        {
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }
        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        { 
            textBlock = sender as TextBlock;
            int rowText = Grid.GetRow(textBlock);   
            int colText = Grid.GetColumn(textBlock);
            highTextBlock.Text = "" + matchesFound;
            textBlock.Text = randomAnimalArray[rowText, colText];

            Debug.WriteLine($"textBlock.Text: {textBlock.Text}");
            
            
            if (findingMatch == false)
            {
                timer1.Start();
                //textBlock.Visibility = Visibility.Collapsed;
                lastTextBlockClicked = textBlock;
                if (object.ReferenceEquals(textBlock, lastTextBlockClicked))
                {
                    Debug.WriteLine("abc");
                }
                Debug.WriteLine($"lastTextBlockClicked.Text: {lastTextBlockClicked.Text}");
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text )//&& textBlock != lastTextBlockClicked)
            {
                //lastTextBlockClicked.Visibility = Visibility.Collapsed;
                Debug.WriteLine($"lastTextBlockClicked.Text: {lastTextBlockClicked.Text}");
                textBlock.Visibility = Visibility.Collapsed;
                findingMatch = false;
                matchesFound++; // tăng biến matchesFound lên khi hai hình giống nhau.
            }
            else
            {
                timer1.Start();
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }
        private void timeTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            timer.Start();
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
