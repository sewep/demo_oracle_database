using OracleBase.Model;
using OracleBase.Model.DBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OracleBase.ViewModel
{
    public class InfoTablesVM : ObservedObject
    {
        private ObservableCollection<Model.InfoTablePos> infoList;
        public ObservableCollection<Model.InfoTablePos> InfoList
        {
            get { return infoList; }
            set { infoList = value; }
        }


        private ICommand loadDataCommand;
        public ICommand LoadDataCommand
        {
            get
            {
                if (loadDataCommand == null) loadDataCommand = new RelayCommand(
                    (object o) =>
                    {
                        var list = DataBase.Instance.getInfoTables();
                        infoList = new ObservableCollection<InfoTablePos>(list);
                        onPropertyChanged("InfoList");
                    },
                    (object o) =>
                    {
                        return SqlBase.loginData != null && SqlBase.loginData.TestConnectionSucces == true;
                    });
                return loadDataCommand;
            }
        }
    }
}
