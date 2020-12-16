using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyManagementSystem
{
    public partial class Form1 : Form
    {
        //Creating context
        readonly CompanyDatabaseEntities context = new CompanyDatabaseEntities();
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            //Data loading/binding
            try
            {
                context.Employees.Load();
                employeeBindingSource.DataSource = context.Employees.Local;
            }
            catch (Exception e)
            {

                MessageBox.Show("Exception " + e);
            }
            

            //Populate DataGridView
            FillDataSource();

            //Adding some design
            DesignDataGridView();

            employeeslistBox.DisplayMember = "FirstName";
            
        }

        //Data binding
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'companyDatabaseDataSet.Employee' table. You can move, or remove it, as needed.
            this.employeeTableAdapter.Fill(this.companyDatabaseDataSet.Employee);

        }

        //Populate DataGridView
        private void FillDataSource()
        {
            employeeslistBox.DataSource = (from i in context.Employees
                                   where i.FirstName.Contains(searchTextBox.Text)
                                   select i).ToList();
        }

        //Design functions
        private void DesignDataGridView()
        {
            foreach (DataGridViewColumn col in employeeDataGridView.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            employeeDataGridView.BorderStyle = BorderStyle.None;
            employeeDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            employeeDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            employeeDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(128, 128, 255);
            employeeDataGridView.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            employeeDataGridView.BackgroundColor = Color.FromArgb(204, 204, 255);
            employeeDataGridView.EnableHeadersVisualStyles = false;
            employeeDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            employeeDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("MS Reference Sans Serif", 10);
            employeeDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(204, 204, 255);
            employeeDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        }

        //TextBox function
        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            FillDataSource();
        }

        //Listbox function
        private void EmployeesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            Employee employee = (Employee)employeeslistBox.SelectedItem;
            
            var selectedEmployee = from emp in context.Employees
                          where emp.FirstName == employee.FirstName
                          select new
                          {
                              emp.Id,
                              emp.LastName,
                              emp.Gender,
                              emp.Language,
                              emp.PhoneNumber,
                              emp.Email,
                              emp.LoginName,
                              emp.FirstName
                          };

            employeeDataGridView.DataSource = selectedEmployee.ToList();
        }


    }
}
