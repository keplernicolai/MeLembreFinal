using MeLembreFinal.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MeLembreFinal.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        public DelegateCommand NavigateCommand { get; set; }
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        private ObservableCollection<Aniversario> _listaAniversarios;
        public ObservableCollection<Aniversario> ListaAniversarios
        {
            get { return _listaAniversarios; }
            set { _listaAniversarios = value; RaisePropertyChanged(); }
        }

        private Aniversario _aniversarioSelecionado;
        public Aniversario AniversarioSelecionado
        {
            get { return _aniversarioSelecionado; }
            set { _aniversarioSelecionado = value; RaisePropertyChanged(); ToolbarItens = true; }
        }

        private bool _toolbarItens = false;
        public bool ToolbarItens
        {
            get { return _toolbarItens; }
            set { _toolbarItens = value; RaisePropertyChanged(); }
        }

        //public ICommand TouchImageButtonCommand
        //{
        //    get;
        //    set;
        //}

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            ToolbarItens = false;

            EditItemCommand = new Command(Editar);
            DeleteItemCommand = new Command(Delete);
            // SelectedItemList = new Command(AparecerToolbarItem);

            Load();
        }

        private void AparecerToolbarItem()
        {
            ToolbarItens = true;
        }

        private void Editar()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("model", AniversarioSelecionado);
            _navigationService.NavigateAsync("EditarAniversarioPage", navigationParams);
        }

        private async void Delete()
        {
            bool resposta = await _dialogService.DisplayAlertAsync("Deletar", "Você tem certeza que deseja apagar este item?", "Sim", "Não");

            if (resposta)
            {
                var realmDB = Realm.GetInstance();
                var aniversario = realmDB.All<Aniversario>().First(f => f.Id == AniversarioSelecionado.Id);

                realmDB.Write(() =>
                {
                    realmDB.Remove(aniversario);
                });

                Load();
            }
        }

        private void Load()
        {
            var realmDb = Realm.GetInstance();
            //ListaAniversarios = realmDb.All<Aniversario>().OrderByDescending(f => f.DataCriacao).ToList();
        }

        #region Commands

        public ICommand EditItemCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ICommand SelectedItemList { get; set; }

        #endregion

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }
    }
}

