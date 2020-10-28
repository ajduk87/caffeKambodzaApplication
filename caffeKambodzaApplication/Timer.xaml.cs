using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace caffeKambodzaApplication
{

     
    
    /// <summary>
    /// Interaction logic for Timer.xaml
    /// </summary>
    public partial class Timer : Window
    {

        private string _text = "Kreiranje izveštaja je u toku !";

        public string Text 
        {
            get { return _text; }
            set 
            {
                if (value != null)
                {
                    _text = value;
                }
            }
        }
        
        public Timer()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            var da = new DoubleAnimation(360, 0, new Duration(TimeSpan.FromSeconds(10)));
            var rt = new RotateTransform();
            animation.RenderTransform = rt;
            animation.RenderTransformOrigin = new Point(0.5, 0.5);
            da.RepeatBehavior = RepeatBehavior.Forever;
            rt.BeginAnimation(RotateTransform.AngleProperty, da);

            animation.Content = _text;
        }

        public void setDeletionText()
        {
            _text = "Brisanje podataka u toku !";
            animation.Content = _text;
        }

      

   
    
        

    }
}
