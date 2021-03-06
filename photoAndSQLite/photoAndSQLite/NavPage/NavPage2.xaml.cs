﻿using Plugin.Media.Abstractions;
using Realms;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace photoAndSQLite.NavPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavPage2 : ContentPage
    {
        MediaFile sourceFile = null;

        public NavPage2(MediaFile file) : this()
        {
            image.Source = ImageSource.FromFile(file.Path);
            sourceFile = file;
        }


        public NavPage2()
        {
            InitializeComponent();
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {

            Navigation.PopAsync(true);

        }

        public static byte[] GetByteArrayFromStream(Stream sm)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                sm.CopyTo(ms);
                return ms.ToArray();
            }
        }

        private void nextButton_Clicked(object sender, EventArgs e)
        {
            var time = DateTime.UtcNow.ToString("HH:mm:ss");

            // RealmにItemオブジェクトを追加する
            var realm = Realm.GetInstance();

            realm.Write(() =>
            {

                byte[] iBytes = GetByteArrayFromStream(sourceFile.GetStream());
                realm.Add(new Item { TimeString = time, imageBytes = iBytes/*, UrlString = sourceFile.Path*/ });
                DisplayAlert("NavPage2", "length : " + iBytes.Length, "OK");
            });
            // Navigation.PopToRootAsync(true);
            Application.Current.MainPage = new MainPage();

        }
    }
}