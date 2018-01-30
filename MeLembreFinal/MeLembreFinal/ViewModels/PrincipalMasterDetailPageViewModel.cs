using MeLembreFinal.MenuItens;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MeLembreFinal.ViewModels
{
    public class PrincipalMasterDetailPageViewModel : BindableBase
    {
        #region Properties

        public ICommand ItemSelectedCommand { get; set; }
        private readonly INavigationService _navigationService;

        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<MasterPageItem> _listItems;
        public ObservableCollection<MasterPageItem> ListItems
        {
            get { return _listItems; }
            set { _listItems = value; RaisePropertyChanged(); }
        }

        #endregion

        public PrincipalMasterDetailPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ItemSelectedCommand = new Command(OnMenuItemSelected);
            PreencherListaItems();
        }

        private async void OnMenuItemSelected()
        {
            var page = ((MasterPageItem)SelectedItem).TargetType;
            await _navigationService.NavigateAsync("NavigationPage/" + page);
        }

        private void PreencherListaItems()
        {
            ListItems = new ObservableCollection<MasterPageItem>();
            var homePage = new MasterPageItem() { Title = "Principal", Icon = "home.png", TargetType = "MainPage" };
            var niverPage = new MasterPageItem() { Title = "Aniversários", Icon = "niver.png", TargetType = "CadAniversarioPage" };
            var saudePage = new MasterPageItem() { Title = "Saúde", Icon = "saude.png", TargetType = "CadSaudePage" };
            var educacaoPage = new MasterPageItem() { Title = "Educação", Icon = "educacao.png" };


            ListItems.Add(homePage);
            ListItems.Add(niverPage);
            ListItems.Add(saudePage);
            ListItems.Add(educacaoPage);
        }
    }
}
