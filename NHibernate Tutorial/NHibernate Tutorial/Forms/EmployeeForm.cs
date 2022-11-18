using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NHibernate;
using NHibernate_Tutorial.Model;

namespace NHibernate_Tutorial
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadEmployeeData();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            loadEmployeeData();
        }

        private void loadEmployeeData()
        {
            ISession isSession = SessionFactory.OpenSession;
            using (isSession)
            {
                IQuery iQuery = isSession.CreateQuery("FROM Employee");
                IList<Employee> employees = iQuery.List<Employee>();
                dgViewEmployee.DataSource = employees;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Model.Employee empData = new Model.Employee();
            SetEmployeeInfo(empData);
            ISession session = SessionFactory.OpenSession;
            using (session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(empData);
                        transaction.Commit();
                        loadEmployeeData();

                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                    }
                    catch (Exception exception)
                    {
                        transaction.Rollback();
                        System.Windows.Forms.MessageBox.Show(exception.Message);
                        Console.WriteLine(exception);
                        throw;
                    }
                }
            }
        }

        private void SetEmployeeInfo(Employee empData)
        {
            empData.FirstName = textBox2.Text;
            empData.LastName = textBox3.Text;
            empData.Email = textBox4.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ISession iSession = SessionFactory.OpenSession;
            using (iSession)
            {
                using (ITransaction transaction = iSession.BeginTransaction())
                {
                    try
                    {
                        IQuery iQuery = iSession.CreateQuery("from Employee where id = '" + textBox1.Text + "'");
                        Employee employee = iQuery.List<Employee>()[0];
                        SetEmployeeInfo(employee);
                        iSession.Update(employee);
                        transaction.Commit();
                        loadEmployeeData();

                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                    }
                    catch (Exception exception)
                    {
                        transaction.Rollback();
                        Console.WriteLine(exception);
                        throw;
                    }
                }
            }
        }
    }
}
