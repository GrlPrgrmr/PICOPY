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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Threading.Tasks;


namespace PICOPY
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static List<string> fileName;
        //this will hold the name of destination folder
        private string _dfolderName = "";
        private static int m_CurrentItemIndex = 0;
        public MainWindow()
        {
            fileName = new List<string>();
            InitializeComponent();

        }


        private void SrcButton_Click(object sender, RoutedEventArgs e)
        {
          
            //this will hold the name of source folder
            string s_folderName = "";


            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult res = dialog.ShowDialog();

            if (res == System.Windows.Forms.DialogResult.OK)
            {
                s_folderName = dialog.SelectedPath;

            }
            else
            {
                return;
            }

            SrcFolder.Text = s_folderName;

            string supportedExtensions = "*.jpg,*.gif,*.png,*.bmp,*.jpe,*.jpeg,*.wmf,*.emf";
            var files = System.IO.Directory.EnumerateFiles(s_folderName, "*.*", SearchOption.AllDirectories).Where(s => supportedExtensions.Contains(System.IO.Path.GetExtension(s).ToLower()));

            foreach (string filename in files)
            {
                lbFiles.Items.Add(filename);
                fileName.Add(filename);
            }

            
            /*try
            {
                Parallel.ForEach(files, filename =>
                    {

                        lbFiles.Items.Add(fileName);
                        fileName.Add(filename);
                    }
                    );
            }
            catch(AggregateException ex)
            {
                throw ex.Flatten();
            }*/

            images.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(fileName.ElementAt(m_CurrentItemIndex));
            CopyLogic.mp_CurrentFile = fileName.ElementAt(m_CurrentItemIndex);
        }


        //Button to select destination folder path
        private void DesButton_Click(object sender, RoutedEventArgs e)
        {



            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult res = dialog.ShowDialog();

            if (res == System.Windows.Forms.DialogResult.OK)
            {
                _dfolderName = dialog.SelectedPath;
            }


            DesFolderName.Text = _dfolderName;

        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxImage icon = MessageBoxImage.Stop;
            while (DesFolderName.Text == "")
            {
                System.Windows.MessageBox.Show("Enter Destination", "Error", MessageBoxButton.OK, icon);
                return;
            }
            CopyLogic obj = new CopyLogic(_dfolderName);
            CopyLogic.Copy();

        }

        private void Nextbutton_Click(object sender, RoutedEventArgs e)
        {
            string currentFile;
            MessageBoxImage icon = MessageBoxImage.Stop;
            if (SrcFolder.Text != "" && DesFolderName.Text!="")
            {
                if (m_CurrentItemIndex == (fileName.Count - 1))
                {
                    m_CurrentItemIndex = 0;
                }
                else
                {
                    ++m_CurrentItemIndex;
                }
                currentFile = fileName.ElementAt(m_CurrentItemIndex);

                images.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(currentFile);
                CopyLogic.Current(currentFile);
            }
            else
            {
                System.Windows.MessageBox.Show("Please Specify Source and Destination First","Error",MessageBoxButton.OK,icon);
            }
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            string currentFile;
            MessageBoxImage icon = MessageBoxImage.Stop;
            if (SrcFolder.Text != "" && DesFolderName.Text != "")
            {
                if (m_CurrentItemIndex == 0)
                {
                    m_CurrentItemIndex = fileName.Count - 1;
                }
                else
                {
                    --m_CurrentItemIndex;
                }
                currentFile = fileName.ElementAt(m_CurrentItemIndex);

                images.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(currentFile);
                CopyLogic.Current(currentFile);
            }
            else
            {
                System.Windows.MessageBox.Show("Please Specify Source and Destination First", "Error", MessageBoxButton.OK, icon);
            }

        }





    }
}
