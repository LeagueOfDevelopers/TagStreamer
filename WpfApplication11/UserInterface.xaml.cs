using InstaSharp.Models;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfApplication11
{
    /// <summary>
    /// Логика взаимодействия для UserInterface.xaml
    /// </summary>
    enum TypeOfItem { Twit, Inst };
    public partial class UserInterface : Window
    {
        Thread _refreshing;
	    private readonly string _connectionKey;
        public UserInterface()
        {
            InitializeComponent();
	        _connectionKey = Connection.ConnectUser();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _refreshing = new Thread(() =>
            {
                while (true)
                {
					Thread.Sleep(10000);
                    FeedItem fi = Connection.LoadItemAsUser(_connectionKey);
                    if(fi!=null)
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
            });
            _refreshing.Start();
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _refreshing.Abort();
        }


    }
}
