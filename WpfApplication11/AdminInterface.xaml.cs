using System.Threading.Tasks;
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

        private async void bNext_Click(object sender, RoutedEventArgs e)
        {
            FeedItem fi = null;
            
	        if (!string.IsNullOrEmpty(_id))
	        {
		        Connection.ProcessedPhoto(_key, _id, false);
	        }

	        NextButton.IsEnabled = false;
	        ApproveButton.IsEnabled = false;

	        await Task.Run(() =>
	        {
		        do
		        {
			        Task.Delay(1000);
			        fi = Connection.LoadItemAsAdmin(_key);
		        } while (fi == null);
	        });

			NextButton.IsEnabled = true;
			ApproveButton.IsEnabled = true;

            _id = fi.ItemId.ToString();
            switch (fi.ItemType)
            {
                case FeedItemType.Instagram:
                    var instagramItem = fi.InstagramItem;
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Image.Source = new BitmapImage(new Uri(instagramItem.Images.StandardResolution.Url));
                        UserImage.Source = new BitmapImage(new Uri(instagramItem.User.ProfilePicture));
                        Name.Text = instagramItem.User.Username;
                        FullName.Text = instagramItem.User.FullName;
                        Tags.Text = TagsMaker(instagramItem);
                        likes.Text = instagramItem.Likes.Count.ToString();
                        comments.Text = instagramItem.Comments.Count.ToString();
                        SetInstaGrid();
                    }));
                    break;
                case FeedItemType.Twitter:
		            var twitterItem = fi.TwitterItem;
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
						TwitPicture.Source = new BitmapImage(new Uri(twitterItem.AuthorAvatarUrl));
						TwitFullName.Content = twitterItem.AuthorName;
						TwitName.Content = twitterItem.AuthorNick;
						TwitText.Text = twitterItem.Text;
						Retweets.Text = twitterItem.RetweetCount.ToString();
						Favorites.Text = twitterItem.FavouriteCount.ToString();
                        SetTwitGrid();
                    }));
                    break;
            }
        }

        private string TagsMaker(Media item)
        {
            string q = "";
            foreach (string x in item.Tags)
                q = String.Concat(q, "#", x);
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
