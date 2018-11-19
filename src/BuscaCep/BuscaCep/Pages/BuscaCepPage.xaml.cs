﻿using BuscaCep.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuscaCep.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuscaCepPage : ContentPage
    {
        public BuscaCepPage()
        {
            InitializeComponent();

            BindingContext = new BuscaCepViewModel()
            {
                CEP = "06807470"
            };
        }
    }
}