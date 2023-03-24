using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace screenRecoder
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Définit l'événement Click pour le bouton CaptureScreenButton
            CaptureScreenButton.Click += CaptureScreenButton_Click;
        }

        private void CaptureScreenButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Prend une capture d'écran de l'ensemble de l'écran
                var bmpScreenCapture = new Bitmap((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight);
                var gfxScreenCapture = Graphics.FromImage(bmpScreenCapture);
                gfxScreenCapture.CopyFromScreen(0, 0, 0, 0, bmpScreenCapture.Size);

                // Affiche la boîte de dialogue pour choisir le nom et l'emplacement du fichier
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PNG Image (*.png)|*.png";
                if (saveFileDialog.ShowDialog() == true)
                {
                    // Enregistre le bitmap dans le fichier choisi par l'utilisateur
                    bmpScreenCapture.Save(saveFileDialog.FileName, ImageFormat.Png);

                    // Affiche l'image capturée
                    CapturedImage.Source = BitmapFrame.Create(new MemoryStream(File.ReadAllBytes(saveFileDialog.FileName)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de la capture d'écran : {ex.Message}");
            }
        }
    }
}