using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using Compare.UI;
using Compare.Classes;

namespace Compare
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        /// <summary>
        /// Command entry point
        /// </summary>
        /// <param name="commandData">Revit command data</param>
        /// <param name="message">Reference to message returned by command</param>
        /// <param name="elements">Potantial set of elements being passed in by Revit</param>
        /// <returns>message</returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                // test change
                //FormCompare formCompare = new FormCompare(new ClsSettings(commandData, elements));
                //formCompare.ShowDialog();

                return Result.Succeeded;
            }

            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}
