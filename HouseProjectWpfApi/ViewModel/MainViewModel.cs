﻿using BuisnesLogicLayer.DTO;
using BuisnesLogicLayer.Interfaces;
using HouseProjectWpfApi.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HouseProjectWpfApi.ViewModel
{
    // user logic
    public partial class MainViewModel : INotifyPropertyChanged
    {
        protected IUserServices? userServices;
        protected IAdServices? adServices;
        protected IFavoritesServices? favoritesServices;
        protected IForCompareServices? forCompareServices;
        protected ICommentServices? commentServices;
        protected IImageServices? imageServices;
        protected string userEmail;
        public MainViewModel(IUserServices? userServices, IAdServices? adServices, IFavoritesServices? favoritesServices, 
            IForCompareServices? forCompareServices, ICommentServices? commentServices, IImageServices? imageServices, string userEmail)
        {
            this.userServices = userServices;
            this.adServices = adServices;
            this.favoritesServices = favoritesServices;
            this.forCompareServices = forCompareServices;
            this.commentServices = commentServices;
            this.imageServices = imageServices;
            this.userEmail = userEmail;
            UpdateData();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void UpdateData()
        {
            UserProfile = Task.Run(() => userServices?.GetUserProfileByEmail(userEmail)).Result;
            MyAds = Task.Run(() => adServices?.GetAdsByUserId(UserProfile.Id)).Result;
            FavoritesAds = Task.Run(() => favoritesServices?.GetAllFavoritesByUserId(UserProfile.Id)).Result;
            ForCompares = Task.Run(() => forCompareServices?.GetAllComparesByUserId(UserProfile.Id)).Result;
        }

        private BaseCommand closeWindow;
        public BaseCommand CloseWindow
        {
            get
            {
                return closeWindow ?? new BaseCommand(obj =>
                {
                    Window? window = obj as Window;
                    window?.Close();
                });
            }
        }
    }
}
