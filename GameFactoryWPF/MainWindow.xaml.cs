﻿using ClassLibrary;
using System.Collections.Generic;
using System.Windows;


namespace GameFactoryWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Login LoginScreen = new Login();
        Leaderboard LeaderBoardScreen = new Leaderboard();
        TTT TTTScreen = new TTT();
        Stats StatsScreen;
        History HistoryScreen;

        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = LoginScreen;
            LoginScreen.PlayerLoggedIn += LoginScreen_PlayerLoggedIn;

        }
        #region UI Event Handlers
        private void ToMainScreen(object sender, RoutedEventArgs e)
        {
            MainContent.Content = LoginScreen;
        }
        private void ToLeaderboard(object sender, RoutedEventArgs e)
        {
            MainContent.Content = LeaderBoardScreen;
        }
        private void ToStats(object sender, RoutedEventArgs e)
        {
            if (StatsScreen == null)
            {
                Login.TextBox("Please log in to view stats.");
                return;
            }

            StatsScreen.Visibility = StatsScreen.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }
        private void ToTTT(object sender, RoutedEventArgs e)
        {
            MainContent.Content = TTTScreen;
        }
        #endregion
        private void LoginScreen_PlayerLoggedIn(Player p_Player)
        {
            Login.TextBox("Logged in as " + p_Player.Ident + " !");


            if (StatsScreen == null)
            {
                StatsScreen = new Stats(p_Player);
                StatsPanel.Children.Add(StatsScreen);
            }
            if (HistoryScreen == null)
            {
                HistoryScreen = new History(p_Player);
                HistoryScreen.LoadHistory(p_Player);
            }
            StatsScreen.Visibility = StatsScreen.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            MainContent.Content = HistoryScreen;
        }
    }



}
