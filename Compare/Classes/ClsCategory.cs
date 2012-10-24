using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.Revit.DB;

namespace Compare.Classes
{
    public class ClsCategory
    {
        private Category _Cat;
        private List<Element> _InstanceElements;
        private List<ClsParameter> _AllParameters = new List<ClsParameter>();
        private Document _Doc;

        public String CatName
        {
            get { return _Cat.Name; }
        }

        public List<Element> InstanceElements
        {
            get { return _InstanceElements; }
        }

        public List<ClsParameter> AllParameters
        {
            get { return _AllParameters; }
        }

        public ClsCategory(Category cat, Document doc)
        {
            _Cat = cat;
            _Doc = doc;

            FilteredElementCollector col = new FilteredElementCollector(doc);
            col.OfCategoryId(cat.Id).WhereElementIsNotElementType();

            _InstanceElements = col.ToElements().ToList();
        }

        public void GetAllParameters()
        {
            foreach (Element elem in _InstanceElements)
            {
                Element elemType = _Doc.GetElement(elem.GetTypeId());

                ParamItor(elem);
                ParamItor(elemType);
            }
        }

        void ParamItor(Element elem)
        {
            foreach (Parameter param in elem.Parameters)
            {
                switch (param.StorageType)
                {
                    case StorageType.ElementId:
                        break;
                    case StorageType.Double:
                        _AllParameters.Add(new ClsParameter(param));
                        break;
                    case StorageType.String:
                        _AllParameters.Add(new ClsParameter(param));
                        break;
                    case StorageType.Integer:
                        if (param.Definition.ParameterType != ParameterType.YesNo)
                            _AllParameters.Add(new ClsParameter(param));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
