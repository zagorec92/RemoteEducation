using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteEducationThesis
{
    class TestPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        private string exampleText;

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public TestPropertyChanged(string value)
        {
            this.exampleText = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ExampleText 
        {
            get { return exampleText; }
            set 
            {
                exampleText = value;
                OnPropertyChanged("ExampleText"); 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        private void OnPropertyChanged(string p)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(p));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, e);
        }
    }
}
