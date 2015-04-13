﻿using AdminTools;
using DatabaseController;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Windows.Shapes;
using System.Windows.Data;
using System.Collections;

namespace ChildcareApplication.AdminTools
{
    /// <summary>
    /// Interaction logic for LinkExistingChild.xaml
    /// </summary>
    public partial class LinkExistingChild : Window
    {

        private ChildInfoDatabase db;
        DataSet DS = new DataSet();
        private string ID;
        private ArrayList connectedChildren; 
        public LinkExistingChild(string pID, ArrayList connectedChildren)
        {
            InitializeComponent();
            this.db = new ChildInfoDatabase();
            this.ID = pID;
            this.connectedChildren = connectedChildren; 
            setChildBox();
        }

        private void setChildBox()
        {
            string fID = getFamilyID(this.ID);
            string[,] childrenData = db.FindFamilyChildren(fID, this.ID);

            if (childrenData == null)
            {
                return;
            }

            if (childrenData != null)
            {
                for (int x = 0; x < childrenData.GetLength(0); x++)
                {
                    if (!connectedChildren.Contains(childrenData[x, 0]))//child already has a link to this parent
                    {
                        Image image = buildImage(childrenData[x, 6], 60);
                        lst_ChildBox.Items.Add(new Child(childrenData[x, 0], childrenData[x, 1], childrenData[x, 2],
                                                image, childrenData[x, 3], childrenData[x, 4], childrenData[x, 5], childrenData[x, 6]));
                    }
                }
            }
        }//end setUpCheckInBox

        private Image buildImage(string path, int size)
        {
            Image image = new Image();
            image.Width = size;

            try
            {
                BitmapImage bitmapImage = new BitmapImage();
                var fileInfo = new FileInfo(@"" + path);
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(fileInfo.FullName);
                bitmapImage.DecodePixelWidth = size;
                bitmapImage.EndInit();
                image.Source = bitmapImage;
            }
            catch
            {
                BitmapImage bitmapImage = new BitmapImage();
                var fileInfo = new FileInfo(@"../../Pictures/default.jpg");
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(fileInfo.FullName);
                bitmapImage.DecodePixelWidth = size;
                bitmapImage.EndInit();
                image.Source = bitmapImage;
            }
            return image;
        }//end buildImage	


        public string getFamilyID(string pID)
        {
            string familyID = "";

            for (int x = 0; x < pID.Length - 1; x++)
            {
                familyID += pID[x];
            }

            return familyID;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_LinkChild_Click(object sender, RoutedEventArgs e)
        {
            if (lst_ChildBox.SelectedItem != null)
            {
                string childID = ((Child)(lst_ChildBox.SelectedItem)).ID;
                int connID = 0;
                DataSet DS = new DataSet();
                DS = this.db.GetMaxConnectionID();
                if (DS != null)
                {
                    connID = Convert.ToInt32(DS.Tables[0].Rows[0][0]);
                    connID = connID + 1;
                }
                string connectionID = connID.ToString();
                string fID = getFamilyID(this.ID);
                this.db.UpdateExistingChilderen(connectionID, this.ID, childID, fID);
                this.connectedChildren.Add(childID);
            }
            lst_ChildBox.Items.Clear();
            setChildBox(); 

        }
    }
}