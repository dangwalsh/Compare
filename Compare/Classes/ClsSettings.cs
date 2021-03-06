﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Compare.Classes
{
    public class ClsSettings
    {
        private ExternalCommandData _CommandData;
        private ElementSet _ElementSet;
        private List<ClsCategory> _InstCategoryList = new List<ClsCategory>();

        /// <summary>
        /// Property that stores the Revit document object
        /// </summary>
        public Document Doc
        {
            get { return _CommandData.Application.ActiveUIDocument.Document; }
        }

        /// <summary>
        /// Property that stores the list of categories
        /// </summary>
        public List<ClsCategory> InstCategoryList
        {
            get { return _InstCategoryList; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="com"></param>
        /// <param name="set"></param>
        public ClsSettings(ExternalCommandData com, ElementSet set)
        {
            _CommandData = com;
            _ElementSet = set;

            foreach (Category cat in Doc.Settings.Categories)
            {
                ClsCategory c = new ClsCategory(cat, this.Doc);
                if (c.InstanceElements.Count > 0) _InstCategoryList.Add(c);//catDict.Add(cat.Name, c);
            }
        }
    }
}
