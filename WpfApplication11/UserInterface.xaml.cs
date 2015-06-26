﻿using InstaSharp.Models;
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
        Thread Refreshing;
        public UserInterface()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Refreshing = new Thread(() =>
            {
                Thread.Sleep(10000);
                while (true)
                {
                    FeedItem fi = Connection.LoadItem();
                    if(fi!=null)
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
            });
            Refreshing.Start();
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
            Refreshing.Abort();
        }


    }
}
