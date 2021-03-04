using OracleBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OracleBase.ViewModel
{
    public class InfoTablesVM : ObservedObject
    {




        private ICommand loadDataCommand;
        public ICommand LoadDataCommand
        {
            get
            {
                if (loadDataCommand == null) loadDataCommand = new RelayCommand(
                    (object o) =>
                    {
                        // TODO: Ładowanie danych
                    },
                    (object o) =>
                    {
                        return DataBase.Instance != null && DataBase.Instance.isConnected;
                    });
                return loadDataCommand;
            }
        }
    }
}
