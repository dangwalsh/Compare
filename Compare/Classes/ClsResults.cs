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
    public class ClsResults
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

        public int InstanceId
        {
            get { return _InstanceId; }
        }
        public int HostId
        {
            get { return _HostId; }
        }
        public ClsParameter HostParameter
        {
            get { return _HostParameter; }
        }
        public ClsParameter InstanceParameter
        {
            get { return _InstanceParameter; }
        }
        public String HostName
        {
            get { return _HostName; }
        }
        public String InstanceName
        {
            get { return _InstanceName; }
        }
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
            }
        }
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
            }
        }

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
    }
}
