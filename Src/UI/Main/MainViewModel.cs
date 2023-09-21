using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    internal class MainViewModel
    {
        private static readonly object _lock = new object();
        private static MainViewModel _instance = null;
        private MainViewModel() { }
        public static MainViewModel GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new MainViewModel();
                    }
                }
            }
            return _instance;
        }
    }
}
