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
    //Credit: Jason FinTech for Drawing -> https://www.youtube.com/watch?v=5Y-Qq3_PWrQ
    //Credit: Nishan Chathuranga Wickramarathna for Screen Capturing -> https://medium.com/@nishancw/c-screenshot-utility-to-capture-a-portion-of-the-screen-489ddceeee49
    public partial class Form1 : Form
    {
        CompanyDatabaseEntities context = new CompanyDatabaseEntities();
        public Form1()
        {
            InitializeComponent();
            context.Employees.Load();

            employeeBindingSource.DataSource = context.Employees.Local;

            
            FillDataSource();
            DesignDataGridView();
            employeeslistBox.DisplayMember = "FirstName";
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'companyDatabaseDataSet.Employee' table. You can move, or remove it, as needed.
            this.employeeTableAdapter.Fill(this.companyDatabaseDataSet.Employee);

        }

        private void FillDataSource()
        {
            employeeslistBox.DataSource = (from i in context.Employees
                                   where i.FirstName.Contains(searchTextBox.Text)
                                   select i).ToList();
        }

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

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            FillDataSource();
        }

        private void employeesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            Employee employee = (Employee)employeeslistBox.SelectedItem;
            
            var selectedEmployee = from emp in context.Employees
                          where emp.FirstName == employee.FirstName
                          select new
                          {
                              Id = emp.Id,
                              LastName = emp.LastName,
                              Gender = emp.Gender,
                              Language = emp.Language,
                              PhoneNumber = emp.PhoneNumber,
                              Email = emp.Email,
                              LoginName = emp.LoginName,
                              FirstName = emp.FirstName
                          };

            employeeDataGridView.DataSource = selectedEmployee.ToList();
        }
    }
}
