using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace Autocadplugin
{
    public partial class Form1 : Form
    {
        public int XColumnPosition => (int)xNumericUpDown.Value;
        public int YColumnPosition => (int)yNumericUpDown.Value;
        public int ZColumnPosition => (int)zNumericUpDown.Value;
        private string filePath;
        public Form1()
        {
            InitializeComponent();
        }

        private void BrowserBtn_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.Filter = "Text files (*.txt)|*.txt";
            openFileDialog1.FileName = "";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            { 
                filePath = openFileDialog1.FileName;
                FileLocationTextBox.Text = filePath;
            }


        }
        private List<Point3d> ReadDataFromFile(string filePath, int xColumn, int yColumn, int zColumn)
        {
            List<Point3d> points = new List<Point3d>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] columns = line.Split(new char[] { ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        if (columns.Length > Math.Max(xColumn, Math.Max(yColumn, zColumn)))
                        {
                            double x = double.Parse(columns[xColumn].Trim());
                            double y = double.Parse(columns[yColumn].Trim());
                            double z = double.Parse(columns[zColumn].Trim());

                            Point3d point = new Point3d(x, y, z);
                            points.Add(point);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return points;
        }

        private int ImportPointsIntoAutoCAD(List<Point3d> points)
        {
            int successfullyImportedCount = 0;

            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                foreach (Point3d point in points)
                {
                    try
                    {
                        DBPoint dbPoint = new DBPoint(point);
                        btr.AppendEntity(dbPoint);
                        tr.AddNewlyCreatedDBObject(dbPoint, true);
                        successfullyImportedCount++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error importing point: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                tr.Commit();
            }

            doc.Editor.WriteMessage($"{successfullyImportedCount} points imported into AutoCAD.");

            return successfullyImportedCount;
        }
    


    private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                int xColumn = (int)xNumericUpDown.Value - 1; // Convert to zero-based index
                int yColumn = (int)yNumericUpDown.Value - 1; // Convert to zero-based index
                int zColumn = (int)zNumericUpDown.Value - 1; // Convert to zero-based index

                List<Point3d> points = ReadDataFromFile(filePath, xColumn, yColumn, zColumn);

                if (points.Count > 0)
                {
                    int successfullyImportedCount = ImportPointsIntoAutoCAD(points);

                    // Check if all points were imported successfully
                    if (successfullyImportedCount == points.Count)
                    {
                        MessageBox.Show($"{successfullyImportedCount} points imported into AutoCAD. Form will now close.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Close the form
                    }
                    else
                    {
                        MessageBox.Show($"Only {successfullyImportedCount} points out of {points.Count} were imported into AutoCAD.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No valid points found in the selected file.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a file first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FileLocationTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            string url = "https://github.com/DZ-T/";

            // Open the URL in the default browser
            Process.Start(url);
        }
    }
}
