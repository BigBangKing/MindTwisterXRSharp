using MindTwisterXRSharp.MVVM;
using System;
using System.Collections.Generic; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using XRSharp.Controls;
using XRSharp.Controls.Primitives;

namespace MindTwisterXRSharp
{
    public partial class MainPage : Page
    {
        ConstantsProvider constants;
        private DispatcherTimer dispatcherTimer;

        int chosenBrand = 0;

        int score = 0;
        int totalAsked = 0;

        bool canClickAgain = false;

        public MainPage()
        {
            InitializeComponent();

            constants = new ConstantsProvider();

            dispatcherTimer = new DispatcherTimer();

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2); // 2 seconds interval

            PopulateData();

        }


        private void ButtonBase3D_Click(object sender, RoutedEventArgs e)
        {
            if (!canClickAgain) return;

            string s = ((ButtonBase3D)sender).Tag.ToString();

            totalAsked++;
            canClickAgain = false;

            switch (s)
            {
                case "1":

                    if (chosenBrand == 0)
                    {
                        correctAnswer();

                    }
                    else
                    {
                        wrongAnswer();
                    }


                    startTimer();
                    break;
                case "2": 
                    if (chosenBrand == 1)
                    {
                        correctAnswer();

                    }
                    else
                    {
                        wrongAnswer();

                    }
                    startTimer();
                    break; 
                case "3":
                    if (chosenBrand == 2)
                    {
                        correctAnswer();
                    }
                    else
                    {
                        wrongAnswer();

                    }
                    startTimer();
                    break;
                default:
                    break;
            }

             

        }

        private void startTimer()
        {
            ScoreText.Text = "Score: " + score + " / " + totalAsked;
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Do something here every second
            PopulateData();

        }

        private void PopulateData()
        {

            dispatcherTimer.Stop();

            int MaxLength = 9;
            int MinLength = 0;
            List<int> ints = new List<int>();
            List<int> actualPictures = new List<int>();
            Random rnd = new Random();
            string ImagePath  = "";
            string ImagePath2 = "";
            string ImagePath3 = "";



            for (int i = 1; i <= 3; i++)
            {
                int randomNumber = rnd.Next(MinLength, MaxLength);

                while (ints.Count > 0 && ints.Contains(randomNumber)) {
                    randomNumber= rnd.Next(MinLength, MaxLength);

                }
                // avoid already taken array.
                ints.Add(randomNumber);


                string[] array = constants.ArraysCorresponder(randomNumber);

                //just another random number for actual pictures path:
                int randomNumberActual = rnd.Next(MinLength, MaxLength);
                while (actualPictures.Count > 0 && actualPictures.Contains(randomNumberActual))
                {
                    randomNumberActual = rnd.Next(MinLength, MaxLength);
                 
                
                }
                // avoid already taken array.
                actualPictures.Add(randomNumberActual);

                switch (i)
                {
                    case 1: ImagePath = array[randomNumberActual];
                        break; 
                    case 2: ImagePath2 = array[randomNumberActual];
                        break; 
                    case 3: 
                        ImagePath3 = array[randomNumberActual];
                        break;

                }

                //Console.WriteLine(ImagePath +" , "+ImagePath2 +" , "+ImagePath3);

            }

            // just choose any random number to ask the brand of.
            chosenBrand = rnd.Next(0, ints.Count);

            QText.Text = "Select "+ constants.ArraysCorrespondingNames(ints[chosenBrand]) +" branded car:";
            AText.Text = "";
             
            ButtonImage1.Content = getImageSource(ImagePath);
             
            ButtonImage2.Content = getImageSource(ImagePath2);
             
            ButtonImage3.Content = getImageSource(ImagePath3);

            canClickAgain = true;

        }

        private Image3D getImageSource(string ImagePath)
        {

            var image = new Image3D();
            image.Source = new BitmapImage(new Uri(ImagePath, UriKind.Relative));

            return image;
        }

        private void correctAnswer()
        {
            score++;
            AText.Foreground = new SolidColorBrush(Colors.DarkCyan);
            AText.Text = "Correct";
        }

        private void wrongAnswer()
        {
            AText.Foreground = new SolidColorBrush(Colors.Red);
            AText.Text = "Wrong";
        }

    }
}
