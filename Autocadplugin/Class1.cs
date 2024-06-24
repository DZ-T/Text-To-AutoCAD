using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

namespace Autocadplugin
{
    public class Class1
    {
        

        [CommandMethod("txt2ac")]
        public void txt2ac() {
            try
            {
                // Create and show MainForm
                Form1 mainForm = new Form1();
                mainForm.ShowDialog();
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("\n Loaded: ");
            }
            catch (System.Exception ex)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage($"\nError: {ex.Message}");
            }
        }
    }
}
