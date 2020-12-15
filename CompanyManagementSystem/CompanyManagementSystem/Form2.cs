using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyManagementSystem
{
    public partial class Form2 : Form
    {
        //Creating context and employees list
        readonly CompanyDatabaseEntities context = new CompanyDatabaseEntities();
        readonly BindingList<Employee> employees = new BindingList<Employee>();
        public Form2()
        {
            InitializeComponent();
            try
            {
                context.Employees.Load();
                
            }
            catch (Exception e)
            {
                MessageBox.Show("An exception occured when loading table data" + e);
            }
            //Populate DataGridView
            FillDataGridView();

            //Adding some design
            DesignDataGridView();
            FormDesign();
        }

        //Design functions
        private void FormDesign()
        {
            this.BackColor = Color.FromArgb(238, 239, 249);
            menuStrip1.BackColor = Color.FromArgb(238, 239, 249);
        }

        private void DesignDataGridView()
        {
            foreach (DataGridViewColumn col in employeesDataGridView.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            employeesDataGridView.BorderStyle = BorderStyle.None;
            employeesDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            employeesDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            employeesDataGridView.DefaultCellStyle.SelectionBackColor = Color.Black;
            employeesDataGridView.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            employeesDataGridView.BackgroundColor = Color.FromArgb(30, 30, 30);
            employeesDataGridView.EnableHeadersVisualStyles = false;
            employeesDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            employeesDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("MS Reference Sans Serif", 10);
            employeesDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 37, 38);
            employeesDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        //Populate DataGridView
        private void FillDataGridView()
        {
            foreach (var emp in context.Employees.Local)
            {
                employees.Add(emp);
            }
            employeesDataGridView.DataSource = context.Employees.Local;
        }

        //Data binding
        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'companyDatabaseDataSet.Employee' table. You can move, or remove it, as needed.
            this.employeeTableAdapter.Fill(this.companyDatabaseDataSet.Employee);

        }

        //Save, Load Files for Employee Table
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Comma Seperated Values (*.csv)|*.csv",
                DefaultExt = "csv",
                AddExtension = true
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            try
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                {
                    foreach (var s in employees)
                    {
                        sw.Write(s.Id);
                        sw.Write(";");
                        sw.Write(s.FirstName);
                        sw.Write(";");
                        sw.Write(s.LastName);
                        sw.Write(";");
                        sw.Write(s.Gender);
                        sw.Write(";");
                        sw.Write(s.Language);
                        sw.Write(";");
                        sw.Write(s.PhoneNumber);
                        sw.Write(";");
                        sw.Write(s.Email);
                        sw.Write(";");
                        sw.Write(s.LoginName);
                        sw.Write("\n");
                    }
                }
            }
            catch (IOException io)
            {
                MessageBox.Show("The file could not be opened:" + io);
            }

        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Comma Seperated Values (*.csv)|*.csv",
                DefaultExt = "csv",
                AddExtension = true
            };
            employees.Clear();

            if (ofd.ShowDialog() != DialogResult.OK) return;
            try
            {
                using (StreamReader sr = new StreamReader(ofd.FileName, Encoding.Default))
                {

                    while (!sr.EndOfStream)
                    {
                        string[] sor = sr.ReadLine().Split(';');

                        Employee emp = new Employee
                        {
                            Id = int.Parse(sor[0]),
                            FirstName = sor[1],
                            LastName = sor[2],
                            Gender = sor[3],
                            Language = sor[4],
                            PhoneNumber = sor[5],
                            Email = sor[6],
                            LoginName = sor[7]
                        };

                        employees.Add(emp);
                        employeesDataGridView.DataSource = employees;
                        employeesDataGridView.Refresh();
                    }

                }
            }
            catch (System.IndexOutOfRangeException ex)
            {
                MessageBox.Show("Can not open this type of file: " + ex);
            }
            catch (FileNotFoundException ex1)
            {
                MessageBox.Show("The file was not found: " + ex1);
            }
            catch (DirectoryNotFoundException ex2)
            {
                MessageBox.Show("The directory was not found: " + ex2);
            }
            catch (IOException ex3)
            {
                MessageBox.Show("The file could not be opened: " + ex3);
            }
        }

        //Show new Forms
        private void HierarchyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void SearchFirstNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        //Exit Button
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    if (MessageBox.Show("Biztosan ki akarsz lépni?",
                                        "Kilépés?",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                    break;
            }
        }
    }
}
