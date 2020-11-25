using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DialogHostWpf;
using SevenZ.Calculator;
using OxyPlot;
using OxyPlot.Series;
using MaterialDesignThemes.Wpf;


namespace RungeKutta
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Solution solution = new Solution();
        private BackgroundWorker worker ;
       
        public MainWindow()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = false;
            worker.DoWork += drawResults;
          
            
        }
     

        private void drawResults(object sender, DoWorkEventArgs e)
         {

             Dispatcher.Invoke(new Action(() =>
             {
                 var dataTable = solution.GetTable(MathExpression.Text, InitialValueX.Text, InitialValueY.Text, Increment.Text);
                 resutlTable.ItemsSource = dataTable;
                 toDrawGraphic(solution.GetCoordinates(dataTable));
             }));
       


         }
      
         private void runMessage()
         {
             if (!worker.IsBusy)
                 worker.RunWorkerAsync();
         }

         private void MouseWindow(object sender, MouseButtonEventArgs e)
         {
             this.DragMove();
         }
         private void btnCloseWindow(object sender, RoutedEventArgs e)
         {
             this.Close();
         }
         private void btnMinimizeWindow(object sender, RoutedEventArgs e)
         {
             this.WindowState = WindowState.Minimized;
         }

         private bool AreEmptyTextBoxes()
         {

             for (int i = 0; i < VisualTreeHelper.GetChildrenCount(Form); i++)
             {

                 var control = VisualTreeHelper.GetChild(Form, i);
                 if(control is TextBox)
                 {

                     if (String.IsNullOrEmpty(((TextBox)(control)).Text))
                         return false;
                 }

             }
             return true;
         }

         private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
         {
               var regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
               if (regex.IsMatch(e.Text) && !(e.Text == "." && ((TextBox)sender).Text.Contains(e.Text)))
                     e.Handled = false;
               else
                 e.Handled = true;
         }
        private void toDrawGraphic(Coordinates coordinates)
         {
             var model = new PlotModel();
             var series = new LineSeries();
             series.Title = "Grafica de " + MathExpression.Text;
             for(int i=0; i<coordinates.X.Length; i++)
             {
                 series.Points.Add(new DataPoint(coordinates.X[i], coordinates.Y[i]));
             }
             series.Color = OxyColor.FromRgb(122, 108, 222);

             series.BrokenLineColor = OxyColor.FromRgb(122, 108, 222);
             model.PlotAreaBorderColor = OxyColor.FromRgb(26, 32, 42);


             model.TextColor= OxyColor.FromRgb(255,255,255);
             nameFunction.Text = "La gráfica de la función: "+MathExpression.Text;

             model.Series.Add(series);
             drawGraphic.Model = model;


         }
        public void showDialog(PackIconKind iconName, string msg)
        {
            iconMessage.Kind = iconName;
            messageContent.Text = msg;
            MyDialogHost.IsOpen = true;
        }
        private void ComputeRungeKuttaMethod(object sender, RoutedEventArgs e)
        {
             if(!AreEmptyTextBoxes())
             {

                showDialog(PackIconKind.AlertOutline, "¡Alguno de sus campos esta vacios!");
                return;
             }

             if(!Expression.IsValideMathExpression(MathExpression.Text,
                 InitialValueX.Text, InitialValueY.Text))
             {
                showDialog(PackIconKind.Allergy, "¡Tu expresión es incorrecta! ");
                return;
             }
            showDialog(PackIconKind.HeadCheck, "¡Se ha calculado correctamente! Presiona clic en la opción: gráfica o tabla. ");
            worker.RunWorkerAsync();

          


         }


    }
}
