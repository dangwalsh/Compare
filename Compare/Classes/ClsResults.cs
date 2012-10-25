using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

using Autodesk.Revit.DB;

using Compare.Classes;

namespace Compare
{
    public class ClsResults : INotifyPropertyChanged
    {
        private Document _Doc;
        private int _InstanceId;
        private int _HostId;
        private ClsParameter _HostParameter;
        private ClsParameter _InstanceParameter;
        private String _HostName;
        private String _InstanceName;
        private String _HostValue;
        private String _InstanceValue;

        /// <summary>
        /// Event handler for property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// ElementId of instance object
        /// </summary>
        public int InstanceId
        {
            get { return _InstanceId; }
        }

        /// <summary>
        /// ElementId of host object
        /// </summary>
        public int HostId
        {
            get { return _HostId; }
        }

        /// <summary>
        /// Parameter of host 
        /// </summary>
        public ClsParameter HostParameter
        {
            get { return _HostParameter; }
        }

        /// <summary>
        /// Parameter of instance
        /// </summary>
        public ClsParameter InstanceParameter
        {
            get { return _InstanceParameter; }
        }

        /// <summary>
        /// Name of host
        /// </summary>
        public String HostName
        {
            get { return _HostName; }
        }

        /// <summary>
        /// Name of instance
        /// </summary>
        public String InstanceName
        {
            get { return _InstanceName; }
        }

        /// <summary>
        /// Value of parameter of host
        /// </summary>
        public String HostValue
        {
            get { return _HostValue; }
            set
            {
                Transaction t = new Transaction(_Doc, "Update Prop");
                if (t.Start() == TransactionStatus.Started)
                {
                    try
                    {
                        _HostParameter.ValueString = value;
                        t.Commit();
                        _HostValue = value;
                    }
                    catch (Exception ex)
                    {
                        t.RollBack();
                        MessageBox.Show(ex.Message);
                    }
                }
                this.NotifyPropertyChanged("HostValue");
            }
        }

        /// <summary>
        /// Value of parameter of instance
        /// </summary>
        public String InstanceValue
        {
            get { return _InstanceValue; }
            set
            {
                Transaction t = new Transaction(_Doc, "Update Prop");
                if (t.Start() == TransactionStatus.Started)
                {
                    try
                    {
                        _InstanceParameter.ValueString = value;
                        t.Commit();
                        _InstanceValue = value;
                    }
                    catch (Exception ex)
                    {
                        t.RollBack();
                        MessageBox.Show(ex.Message);
                    }
                }
                this.NotifyPropertyChanged("InstanceValue");
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="instId"></param>
        /// <param name="hostId"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <param name="hName"></param>
        /// <param name="iName"></param>
        public ClsResults(Document doc,
                          int instId,
                          int hostId,
                          ClsParameter h,
                          ClsParameter i,
                          String hName,
                          String iName)
        {
            _Doc = doc;
            _InstanceId = instId;
            _HostId = hostId;
            _HostParameter = h;
            _InstanceParameter = i;
            _HostValue = h.Value;
            _InstanceValue = i.Value;
            _HostName = hName;
            _InstanceName = iName;
        }

        /// <summary>
        /// Raises PropertyChanged event
        /// </summary>
        /// <param name="name"></param>
        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
