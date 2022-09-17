using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Task_Graph.Base;
using Task_Graph.Base.Commands;
using Task_Graph.Models;

namespace Task_Graph.ViewModels
{
    public class MainViewModel : ViewModel
    {
        #region props

        public ObservableCollection<UserModel>? UserModels { get; set; }
        public ICollectionView UserView { get; set; }

        private UserModel _selectedUser;
        public UserModel SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                this._selectedUser = value;
                RefreshGraph();
                OnPropertyChanged();
            }
        }    

        public int _selectedFormat;
        public int SelectedFormat { 
            get 
            {
                return this._selectedFormat;
            }
            set 
            {
                _selectedFormat = value;
                OnPropertyChanged();
            } 
        }


        private string inputText = "";
        public string InputText
        {
            get
            {
                return this.inputText;
            }
            set
            {
                this.inputText = value;
                RefreshTable();
                OnPropertyChanged();

            }
        }


        #region graphProps

        public ChartValues<int> CountSteps { get; set; }
        public SeriesCollection GraphsSeries { get; set; }

        #endregion


        #endregion

        public MainViewModel()
        {
            PrepareFunc();
        }

        #region commands

        public ICommand PushIntoFile //Push user's data in file /json/xml/csv
        {
            get
            {
                return new DelegateCommand(async (x) =>
                {
                    switch (SelectedFormat)
                    {
                        case (int)Formats.JSON: 
                           await FilePusher.PushIntoJsonFile(SelectedUser);
                            break;
                        case (int)Formats.XML:
                             FilePusher.PushIntoXMLFile(SelectedUser);
                            break;
                        case (int)Formats.CSV:
                            FilePusher.PushIntoCsvFile(SelectedUser);
                            break;

                        default: break;
                    }                

                }, (x) => SelectedUser != null && SelectedFormat!=null);
            }

        } 

        public ICommand ChooseFiles //Choose Files to get data
        {
            get
            {
                return new DelegateCommand((x) =>
                {
                    FileGetter.SetRangeFiles();
                    BindOperations();
                    OnPropertyChanged(nameof(UserView));
                    
                    
                });
            }

        } 

        public ICommand DefaultSettings //Default users data
        {
            get
            {
                return new DelegateCommand( (x) =>
                {
                    PrepareFunc();
                    OnPropertyChanged(nameof(UserView));

                });
            }

        } 


        #endregion

        #region methods

        private void PrepareFunc()//
        {
            Day.FullDays();
            BindOperations();
        }

        private void BindOperations()
        {
            UserModels = new ObservableCollection<UserModel>(UserModel.All());
            BindingOperations.EnableCollectionSynchronization(UserModels, new object());
            UserView = CollectionViewSource.GetDefaultView(UserModels);

            if(UserModels.Count!=0)
                SelectedUser = UserModels.First();
        }
        
        private void RefreshTable()
        {
            UserView.Filter = (x) =>
            {
                if (x is UserModel um)
                {
                    if (um.Name.ToLower().Contains(InputText.ToLower(), StringComparison.Ordinal)) return true;

                }
                return false;
            };
            UserView.Refresh();
         }

        #region graphSettings

        private void RefreshGraph()
        {
            if (SelectedUser != null)
            {
                CountSteps = new ChartValues<int>(SelectedUser.GetDays().Select(x => x.Steps));
                SetSettingsForGraph();
                OnPropertyChanged(nameof(CountSteps));
                OnPropertyChanged(nameof(GraphsSeries));

            }
        }

        private void SetSettingsForGraph()
        {
            var mapper = new LiveCharts.Configurations.CartesianMapper<int>()
            .X((value, index) => index)
            .Y((value) => value)
            .Fill((v, i) =>
            {
                if (v == SelectedUser.MaxSteps) return Brushes.White;
                else if (v == SelectedUser.MinSteps) return Brushes.Black;
                else return Brushes.DarkViolet;
            });
          
            LiveCharts.Charting.For<int>(mapper, SeriesOrientation.Horizontal);

            GraphsSeries = new SeriesCollection();
            var columnSeries = new ColumnSeries() { Values = new ChartValues<int>(), Title = "Steps" };

            foreach (var val in CountSteps)
            {
                columnSeries.Values.Add(val);
            }

            this.GraphsSeries.Add(columnSeries);
        }

        #endregion


        #endregion

    }
}


