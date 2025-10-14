using CodeGenerator_BusinessLayer;
using DVLD.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Guna.UI2.HtmlRenderer.Core;
using Code_Generator.Properties;
using System.Diagnostics;
namespace Code_Generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private List<string> _DatabasesNames;
        private void _SetConnactionInfo()
        {

            clsGenerator.SetConnactionInfo(cbDataBaseName.Text, tbUserID.Text, tbPassword.Text);
        }

        private void FillDatabasesNames()
        {
            _DatabasesNames = clsTable.GetAllDatabasesNames();
            cbDataBaseName.Items.AddRange(_DatabasesNames.ToArray());

        }


        private static string _Path = "C:\\Users\\TOPTECH\\source\\C#\\";

        private void btnGenerat_Click(object sender, EventArgs e)
        {
            
            if (!this.ValidateChildren())
            {
                return;
            }

            _SetConnactionInfo();

            if (clsGenerator.IsCredetialsCorrect())
            {

                if (chkRememberMe.Checked)
                {
                    //store username and password in Regjistry
                  clsUtil.RememberUsernameAndPasswordByRegjistry(tbUserID.Text.Trim(), tbPassword.Text.Trim());
                  
                }
                else
                {
                    clsUtil.RememberUsernameAndPasswordByRegjistry("", "");
                }

            }
            else
            {
                tbPassword.Focus();
                MessageBox.Show("Invalid UserID/Password  Or Database Doesn't Existe..", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Stopwatch stopwatch1 = Stopwatch.StartNew();


            btnGenerat.Enabled = false;
            Task.Run(() => clsGenerator.Generate(tbProjectName.Text.Trim())).ContinueWith(t =>
            {

                MessageBox.Show("Created Successs in :" + stopwatch1.ElapsedMilliseconds + "ms,\n Saved in " + clsGenerator.Path);
                            btnGenerat.Enabled = true;
            });



            //if (clsGenerator.Generate(tbProjectName.Text.Trim()))
            //{
            //    stopwatch1.Stop();
            //    MessageBox.Show("Created Successs in :" + stopwatch1.ElapsedMilliseconds + "ms,\n Saved in " + clsGenerator.Path);
            //}

        }
    
      

        private void Form1_Load(object sender, EventArgs e)
        {
            FillDatabasesNames();
           cbDataBaseName.SelectedIndex = 0;


            string UserName = "", Password = "";

            if ( clsUtil.GetStoredCredentialByRegjistry(ref UserName, ref Password))
            {
                tbUserID.Text = UserName;
                tbPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cbDataBaseName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbProjectName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbProjectName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbProjectName, "Project Name cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(tbProjectName, null);
            };

        }

        private void tbUserID_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(tbUserID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbUserID, "UserID cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(tbUserID, null);
            };

        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbPassword, "Password cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(tbPassword, null);
            };

        }
    }
}
