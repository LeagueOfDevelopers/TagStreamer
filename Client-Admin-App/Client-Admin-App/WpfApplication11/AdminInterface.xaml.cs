using InstaSharp.Models;
using System;
using System.Collections.Generic;
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
using TagStreamer.Models;

namespace WpfApplication11
{
    /// <summary>
    /// Логика взаимодействия для AdminInterface.xaml
    /// </summary>
    public partial class AdminInterface : Window
    {
        string Key, Id;
        public AdminInterface(string key)
        {
            Key = key;
            InitializeComponent();
        }

        private void bNext_Click(object sender, RoutedEventArgs e)
        {
            FeedItem fi;
            bool q;
            do
            {
                q = false;
                fi = Connection.LoadItemAsAdmin(Key);
                if (fi == null)
                    q = true;
            }
            while (q);
            //Id = (String) fi.ItemId;
            switch (fi.ItemType)
            {
                case FeedItemType.Instagram:
                    Media item = fi.InstagramItem;
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Image.Source = new BitmapImage(new Uri(item.Images.StandardResolution.Url));
                        UserImage.Source = new BitmapImage(new Uri(item.User.ProfilePicture));
                        Name.Text = item.User.Username;
                        FullName.Text = item.User.FullName;
                        Tags.Text = TagsMaker(item);
                        likes.Text = item.Likes.Count.ToString();
                        comments.Text = item.Comments.Count.ToString();
                        SetInstaGrid();
                    }));
                    break;
                case FeedItemType.Twitter:
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        //TwitPicture.Source = new BitmapImage(new Uri(     ));
                        //TwitFullName.Content = 
                        //TwitName.Content = 
                        //TwitText.Text = 
                        //Retweets.Text = 
                        //Favorites.Text = 
                        SetTwitGrid();
                    }));
                    break;
            }
        }

        private string TagsMaker(Media item)
        {
            string q = "";
            foreach (string x in item.Tags)
                q = String.Concat(q, x);
            return q;
        }

        private void SetBackgroundGrid()
        {
            Background.Visibility = System.Windows.Visibility.Visible;
            TwitGrid.Visibility = System.Windows.Visibility.Hidden;
            InstaGrid.Visibility = System.Windows.Visibility.Hidden;
        }
        private void SetTwitGrid()
        {
            Background.Visibility = System.Windows.Visibility.Hidden;
            TwitGrid.Visibility = System.Windows.Visibility.Visible;
            InstaGrid.Visibility = System.Windows.Visibility.Hidden;
        }
        private void SetInstaGrid()
        {
            Background.Visibility = System.Windows.Visibility.Hidden;
            TwitGrid.Visibility = System.Windows.Visibility.Hidden;
            InstaGrid.Visibility = System.Windows.Visibility.Visible;
        }

        private void bGood_Click(object sender, RoutedEventArgs e)
        {
            if (!Connection.SendGoodId(Key, Id))
                MessageBox.Show("Неполадки в соединении. Не удалось отправить сообщение.");
            else
                bNext_Click(null, null);
        }
    }
}
