using MindTwisterXRSharp.MVVM;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MindTwisterXRSharp
{
    public partial class MainPage : Page
    {

        static ViewModel viewModel;

        public static Func<bool> UIUpdater;
        public static Func<bool> AnsUpdater;

        public MainPage()
        {
            InitializeComponent();
            viewModel = new ViewModel();
            //this.DataContext = viewModel;

            UIUpdater = UpdateUI;
            AnsUpdater = UpdateAns;

            viewModel.PopulateData();

        }


        private void ButtonBase3D_Click(object sender, RoutedEventArgs e)
        {
            if(viewModel.canClickAgain)
                viewModel.Button_Click_Processor(sender);
             
        }
        
        public bool UpdateUI()
        {

            QText.Text = viewModel.QText;
            ScoreText.Text = viewModel.ScoreText;
            UpdateAns();

            ButtonImage1.Content = viewModel.image3Ds[0];
            ButtonImage2.Content = viewModel.image3Ds[1];
            ButtonImage3.Content = viewModel.image3Ds[2];

            return true;
        }  

        public bool UpdateAns()
        {
            AText.Text = viewModel.AnsText;
            AText.Foreground = viewModel.solidColor;
            return true;
        }
         

    }
}
