﻿using SchoolProject.Scripts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolProject
{
    public partial class ProfesorForm : Form
    {
        public Profesor profesorObj = null;

        private string userName = "Undefined";
        private List<Elev> elevi = new List<Elev>();


        public ProfesorForm(string UserName)
        {
            InitializeComponent();
            this.userName = UserName;
        }

        private void ProfesorForm_Load(object sender, EventArgs e)
        {
            MainProgram.SetActiveForm(this);

            LoadInfo();
        }



        // Methods

        private void LoadInfo()
        {
            profesorObj = Profesor.CreateProfesor(userName);

            this.Text = "Logged in as TEACHER: " + profesorObj.GetName();

            GetEleviToLista();

        }

        public void UpdateData()
        {
            // Clear listbox to avoid duplicates
            listBox1.Items.Clear();

            foreach (Elev e in elevi)
            {
                string elevInfo = "Nume: " + e.GetName() + "; Clasa: " + e.GetClasa().ToString() + "; Medie: " + e.GetMediaAritmetica().ToString("0.00");
                listBox1.Items.Add(elevInfo);

                this.Controls.Add(listBox1);
            }
        }


        private void GetEleviToLista()
        {
            foreach(Elev e in MainProgram.listaElevi)
            {
                string elevInfo = "Nume: " + e.GetName() + "; Clasa: " + e.GetClasa().ToString() + "; Medie: " + e.GetMediaAritmetica().ToString("0.00");
                listBox1.Items.Add(elevInfo);

                this.Controls.Add(listBox1);

                elevi.Add(e);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            OpenElevInfoForm(elevi[listBox1.SelectedIndex]);
        }


        
        // Methods

        private void OpenElevInfoForm(Elev e)
        {
            MainProgram.ShowForm(new ElevInfo(e, this));
        }
    }
}
