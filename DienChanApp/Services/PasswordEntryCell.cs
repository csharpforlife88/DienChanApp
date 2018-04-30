using System;
using Xamarin.Forms;

namespace DienChanApp.Services
{
    public class PasswordEntryCell : EntryCell
    {
        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                setStars();
            }
        }

        private string starFiller(int count)
        {
            var output = "";
            for (; count > 0; count--, output += "●")
                ;
            return output;
        }

        private void setStars()
        {
            this.Text = starFiller(this.Value.Length);
        }

        public PasswordEntryCell()
        {
            this.Value = "";

            this.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) => {

                //if (e.PropertyName == "Text")
                //{
                //    var txtLen = this.Text.Length;
                //    var txtVal = this.Text;
                //    var mdlLen = this.Value.Length;
                //    if (txtLen > mdlLen)
                //    {
                //        this.Value += txtVal.Substring(txtLen - 1);
                //    }
                //    else
                //    {
                //        this.Value = this.Value.Substring(0, txtLen);
                //    }
                //    setStars();
                //}
            };
        }
    }
}
