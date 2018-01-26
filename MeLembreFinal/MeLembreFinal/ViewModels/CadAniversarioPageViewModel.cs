using MeLembreFinal.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MeLembreFinal.ViewModels
{
	public class CadAniversarioPageViewModel : BindableBase
	{

        private IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;

        #region Commands

        public ICommand SaveItemCommand { get; set; }

        #endregion

        #region Properties

        private ObservableCollection<string> _listTiposAniver;
        public ObservableCollection<string> ListTiposAniver
        {
            get { return _listTiposAniver; }
            set { _listTiposAniver = value; RaisePropertyChanged(); }
        }

        private string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; RaisePropertyChanged(); }
        }

        private string _tipoAniver;
        public string TipoAniver
        {
            get { return _tipoAniver; }
            set { _tipoAniver = value; RaisePropertyChanged(); }
        }

        private string _nomePessoa;
        public string NomePessoa
        {
            get { return _nomePessoa; }
            set { _nomePessoa = value; RaisePropertyChanged(); }
        }

        private DateTimeOffset? _dataAviver;
        public DateTimeOffset? DataAniver
        {
            get { return _dataAviver; }
            set { _dataAviver = value; RaisePropertyChanged(); }
        }

        private string _detalhes;
        public string Detalhes
        {
            get { return _detalhes; }
            set { _detalhes = value; RaisePropertyChanged(); }
        }

        private bool _seteDias = false;
        public bool SeteDias
        {
            get { return _seteDias; }
            set { _seteDias = value; RaisePropertyChanged(); }
        }

        private bool _doisDias = false;
        public bool DoisDias
        {
            get { return _doisDias; }
            set { _doisDias = value; RaisePropertyChanged(); }
        }

        private bool _umDia = false;
        public bool UmDia
        {
            get { return _umDia; }
            set { _umDia = value; RaisePropertyChanged(); }
        }

        private bool _noDia = true;
        public bool NoDia
        {
            get { return _noDia; }
            set { _noDia = value; RaisePropertyChanged(); }
        }

        #endregion

        public CadAniversarioPageViewModel(IPageDialogService dialogService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;

            ListTiposAniver = new ObservableCollection<string>();
            ListTiposAniver.Add("Pessoa");
            ListTiposAniver.Add("Namoro");
            ListTiposAniver.Add("Casado");

            SaveItemCommand = new Command(Save);
        }

        private void Save()
        {
            if (VerificarCamposObrigatorios())
            {
                var realmDB = Helpers.Util.GetInstanceRealm();
                var count = realmDB.All<Aniversario>().Count();
                long funciId = 0;
                //var realmDB = Realm.GetInstance();
                if (count == 0)
                    funciId = realmDB.All<Aniversario>().Count() + 1;
                else
                    funciId = realmDB.All<Aniversario>().LastOrDefault().Id + 1;

                var aniversario = new Aniversario()
                {
                    Id = funciId,
                    TipoAniver = TipoAniver,
                    Titulo = Titulo,
                    NomePessoa = NomePessoa,
                    Detalhes = Detalhes,
                    DataAniver = DataAniver,
                    SeteDias = SeteDias,
                    UmDia = UmDia,
                    NoDia = NoDia,
                    DataCriacao = DateTimeOffset.UtcNow,
                    Foto = "niver.png"
                };

                realmDB.Write(() =>
                {
                    aniversario = realmDB.Add(aniversario);
                });

                _dialogService.DisplayAlertAsync("Dados salvos", "Os dados foram salvos com sucesso", "OK");

                LimparCampos();
                //_navigationService.NavigateAsync("/PrincipalMasterDetailPage/NavigationPage/MainPage");


            }

        }

        private void LimparCampos()
        {
            ListTiposAniver = new ObservableCollection<string>();
            ListTiposAniver.Add("Pessoa");
            ListTiposAniver.Add("Namoro");
            ListTiposAniver.Add("Casado");
            TipoAniver = null;
            Titulo = String.Empty;
            NomePessoa = String.Empty;
            Detalhes = String.Empty;
            DataAniver = DateTimeOffset.UtcNow;
            SeteDias = false;
            UmDia = false;
            NoDia = true;

        }

        private bool VerificarCamposObrigatorios()
        {
            if (TipoAniver == null)
            {
                _dialogService.DisplayAlertAsync("Campo obrigatório", "Selecione o tipo de aniversário", "OK");
                return false;
            }

            else if (NomePessoa == null)
            {
                _dialogService.DisplayAlertAsync("Campo obrigatório", "Forneça o nome da pessoa", "OK");
                return false;
            }

            ColocarMenssagemTitulo();

            return true;
        }

        private void ColocarMenssagemTitulo()
        {
            switch (TipoAniver)
            {
                case "Pessoa":
                    {
                        Titulo = String.Concat("Aniversário de ", NomePessoa);
                        break;
                    }

                case "Namoro":
                    {
                        Titulo = String.Concat("Aniversário de namoro com ", NomePessoa);
                        break;
                    }

                case "Casado":
                    {
                        Titulo = String.Concat("Aniversário de casado com ", NomePessoa);
                        break;
                    }

                default:
                    break;
            }
        }

        

    }
}
