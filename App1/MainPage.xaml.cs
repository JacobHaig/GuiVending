using System;
using System.Collections.Generic;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<Items> containsOfItemFile;
        public List<int> listOfIndexsSelectedFromFileList = new List<int>();

        public class Items
        {
            public Items(string n, double d, int i)
            {
                name = n;
                price = d;
                qty = i;
            }
            public Items() { }

            public string getName() { return name; }
            public void setName(string n) { name = n; }

            public double getPrice() { return price; }
            public void setPrice(double n) { price = n; }

            public int getQty() { return qty; }
            public void setQty(int n) { qty = n; }

            private string name;
            private double price;
            private int qty;
        }

        public void createList(List<Items> containsOfItemFile)
        {
            foreach (Items item in containsOfItemFile)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = item.getName();

                checkBox.Checked += updateHandler;
                checkBox.Unchecked += updateHandler;

                itemList.Items.Add(checkBox);
            }
        }

        private void updateHandler(object sender, RoutedEventArgs e)
        {
            createUserSelectedlist();
        }

        public List<Items> readFile(string fileName = "Items.txt")
        {
            List<Items> listOfItemsFromFile = new List<Items>();
            var file = File.OpenText(fileName);
            string line = "";

            while ((line = file.ReadLine()) != null)
            {
                String[] listOfInformation = line.Split(' ');
                Items item = new Items();

                item.setName(listOfInformation[0].Replace('_', ' '));
                item.setPrice(Double.Parse(listOfInformation[1]));
                item.setQty(Int16.Parse(listOfInformation[2]));
                listOfItemsFromFile.Add(item);
            }
            return listOfItemsFromFile;
        }

        public void createUserSelectedlist()
        {
            int index = 0;
            List<int> templistOfIndexsSelectedFromFileList = new List<int>();
            foreach (CheckBox item in itemList.Items)
            {
                if (item.IsChecked == true)
                    templistOfIndexsSelectedFromFileList.Add(index);
                index++;
            }
            listOfIndexsSelectedFromFileList = templistOfIndexsSelectedFromFileList;
            mytext.Text = listOfIndexsSelectedFromFileList.Count.ToString();
        }

        public MainPage()
        {
            this.InitializeComponent();
            containsOfItemFile = readFile("Items.txt");

            createList(containsOfItemFile);
        }
    }
}