﻿using StrasbourgTransport.Models;
using StrasbourgTransport.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace StrasbourgTransport.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public MainViewModel VM { get { return (MainViewModel)this.DataContext; } }
        public HomePage()
        {
            this.InitializeComponent();

            this.DataContext = new MainViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var favoriteResultSelected = e.Parameter as StopResult;

            if (favoriteResultSelected != null)
                await VM.GetJourneyResults(favoriteResultSelected.Code);

            base.OnNavigatedTo(e);
        }
    }
}