using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Threading;
using XRSharp.Controls;
using XRSharp.Controls.Primitives;

namespace MindTwisterXRSharp.MVVM
{
    public class ViewModel
    {
        ConstantsProvider constants;
        private DispatcherTimer dispatcherTimer;

        int chosenBrand = 0;

        int score = 0;
        int totalAsked = 0;

        public bool canClickAgain = false;

        public String QText = "";
        public String ScoreText = "";
        public String AnsText = "";

        public SolidColorBrush solidColor;

        public Image3D[] image3Ds;

        public ViewModel()
        {
            constants = new ConstantsProvider();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2); // 2 seconds interval
            
            image3Ds = new Image3D[3];
            solidColor = new SolidColorBrush(Colors.DarkCyan);

            PopulateData();
             

        }

        public void Button_Click_Processor(object sender)
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


            MainPage.AnsUpdater?.Invoke();



        }


        private void startTimer()
        {
            ScoreText = "Score: " + score + " / " + totalAsked;
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Do something here every second
            PopulateData();

        }

        public void PopulateData()
        {

            dispatcherTimer.Stop();

            int MaxLength = 9;
            int MinLength = 0;
            List<int> ints = new List<int>();
            List<int> actualPictures = new List<int>();
            Random rnd = new Random();
            string ImagePath = "";
            string ImagePath2 = "";
            string ImagePath3 = "";



            for (int i = 1; i <= 3; i++)
            {
                int randomNumber = rnd.Next(MinLength, MaxLength);

                while (ints.Count > 0 && ints.Contains(randomNumber))
                {
                    randomNumber = rnd.Next(MinLength, MaxLength);

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
                    case 1:
                        ImagePath = array[randomNumberActual];
                        break;
                    case 2:
                        ImagePath2 = array[randomNumberActual];
                        break;
                    case 3:
                        ImagePath3 = array[randomNumberActual];
                        break;

                }

                //Console.WriteLine(ImagePath +" , "+ImagePath2 +" , "+ImagePath3);

            }

            // just choose any random number to ask the brand of.
            chosenBrand = rnd.Next(0, ints.Count);

            QText = "Select " + constants.ArraysCorrespondingNames(ints[chosenBrand]) + " branded car:";
            AnsText = ""; 

            image3Ds[0] = getImageSource(ImagePath);
            image3Ds[1] = getImageSource(ImagePath2);
            image3Ds[2] = getImageSource(ImagePath3);

            canClickAgain = true;

            MainPage.UIUpdater?.Invoke();

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
            //AText.Foreground = new SolidColorBrush(Colors.DarkCyan);
            solidColor = new SolidColorBrush(Colors.DarkCyan);
            AnsText = "Correct";
        }

        private void wrongAnswer()
        {
            solidColor = new SolidColorBrush(Colors.Red);
            //AText.Foreground = new SolidColorBrush(Colors.Red);
            AnsText = "Wrong";
        }

    }
}
