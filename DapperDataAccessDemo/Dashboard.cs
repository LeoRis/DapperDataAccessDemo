using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DapperDataAccessDemo
{
    public partial class Dashboard : Form
    {
        List<Person> people = new List<Person>();
        public Dashboard()
        {
            InitializeComponent();
            UpdateBinding();
        }

        private void UpdateBinding()
        {
            peopleFoundListBox.DataSource = people;
            peopleFoundListBox.DisplayMember = "FullInfo";
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();

            people = db.GetPeople(lastNameText.Text);

            UpdateBinding();
        }

        private void InsertRecordButton_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();

            db.InsertPerson(firstNameInsText.Text, lastNameInsText.Text, emailAddressInsText.Text, phoneNumberInsText.Text);
            
            UpdateBinding();

            firstNameInsText.Text = "";
            lastNameInsText.Text = "";
            emailAddressInsText.Text = "";
            phoneNumberInsText.Text = "";
        }

        private void RemovePersonButton_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();

            db.DeletePerson(firstNameInsText.Text, lastNameInsText.Text, emailAddressInsText.Text, phoneNumberInsText.Text);

            UpdateBinding();

            firstNameInsText.Text = "";
            lastNameInsText.Text = "";
            emailAddressInsText.Text = "";
            phoneNumberInsText.Text = "";
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();

            // TODO Update only desired parameters of the person.

            db.UpdatePerson(firstNameInsText.Text, lastNameInsText.Text, emailAddressInsText.Text, phoneNumberInsText.Text);

            UpdateBinding();

            firstNameInsText.Text = "";
            lastNameInsText.Text = "";
            emailAddressInsText.Text = "";
            phoneNumberInsText.Text = "";
        }

        private void PeopleFoundListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            Person selector = (Person)peopleFoundListBox.SelectedItem;
            
            foreach(var person in people)
            {
                if(person.FirstName == selector.FirstName && person.LastName == selector.LastName && person.EmailAddress == selector.EmailAddress && person.PhoneNumber == selector.PhoneNumber)
                {
                    firstNameInsText.Text = selector.FirstName;
                    lastNameInsText.Text = selector.LastName;
                    emailAddressInsText.Text = selector.EmailAddress;
                    phoneNumberInsText.Text = selector.PhoneNumber;
                }
            }
        }
    }
}