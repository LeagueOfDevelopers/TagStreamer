using InstaSharp.Models;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfApplication11
{
    /// <summary>
    /// Логика взаимодействия для AdminInterface.xaml
    /// </summary>
    public partial class AdminInterface : Window
    {
	    private readonly string _key;
	    private string _id;

	    public AdminInterface(string key)
        {
            _key = key;
            InitializeComponent();
        }

        private void bNext_Click(object sender, RoutedEventArgs e)
        {
            FeedItem fi;
            bool q;
	        if (!string.IsNullOrEmpty(_id))
	        {
		        Connection.ProcessedPhoto(_key, _id, false);
	        }

            do
            {
                q = false;
                fi = Connection.LoadItemAsAdmin(_key);
                if (fi == null)
                    q = true;
            }
            while (q);
            _id = fi.ItemId.ToString();
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
	        if (string.IsNullOrEmpty(_id))
	        {
		        return;
	        }

            if (!Connection.ProcessedPhoto(_key, _id, true))
                MessageBox.Show("Неполадки в соединении. Не удалось отправить сообщение.");
            else
                bNext_Click(null, null);
        }
    }
}
