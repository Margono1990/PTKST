using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TestKST
{
    public partial class Form1 : Form
    {
        private SubClass _SubClass = new SubClass();
        private string _Path;
        private string _UnsortedName= "unsorted-names-list.txt";
        private string _SortedName= "sorted-names-list.txt";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string __ColumnName = "Name";
            DataTable __TableSort = new DataTable();
            __TableSort.Columns.Add(__ColumnName, typeof(string));

            OpenFileDialog __OpenDialog = new OpenFileDialog();
            __OpenDialog.Filter = "Text | "+_UnsortedName;
            if (__OpenDialog.ShowDialog() == DialogResult.OK)
            {
                _Path = __OpenDialog.FileName.Replace(_UnsortedName,_SortedName);
                __TableSort.Clear();
                listBox1.Items.Clear();

                List<string> lines = new List<string>();
                using (StreamReader _StreamReader = new StreamReader(__OpenDialog.OpenFile()))
                {
                    string __Source;
                    while ((__Source = _StreamReader.ReadLine()) != null)
                    {
                        listBox1.Items.Add(__Source);
                        __TableSort.NewRow();
                        __TableSort.Rows.Add(_SubClass.RollBackName(__Source));
                    }
                }
                listBox2.DataSource = _SubClass.FileSort(__TableSort, __ColumnName);
                listBox2.DisplayMember = __ColumnName;
                listBox2.ValueMember = __ColumnName;                
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.Delete(_Path);
            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(_Path);
            SaveFile.WriteLine(listBox1.Items);
            SaveFile.ToString();
            SaveFile.Close();
            if (File.Exists(_Path))
            {
                MessageBox.Show("Programs saved!");
            }
        }
    }
}
