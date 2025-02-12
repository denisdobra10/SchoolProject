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
    public partial class ElevInfo : Form
    {
        private ProfesorForm profesorForm;
        private Elev elevObj;

        public ElevInfo(Elev e, ProfesorForm _profesorForm)
        {
            InitializeComponent();

            elevObj = e;
            this.profesorForm = _profesorForm;
        }

        private void ElevInfo_Load(object sender, EventArgs e)
        {
            LoadInfo();
        }



        // Methods

        private void LoadInfo()
        {
            this.Text = "Interactiune cu elevul: " + elevObj.GetName();

            // Adaugare note in lista de nota
            noteElevListBox.Items.Clear();
            foreach (float n in elevObj.GetNote())
                noteElevListBox.Items.Add(n.ToString());
            this.Controls.Add(noteElevListBox);

            // Adaugare medie generala elev
            medieGeneralaLabel.Text = elevObj.GetMediaAritmetica().ToString("0.000");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NoteazaElev();
        }

        private void NoteazaElev()
        {
            // POSIBILITATE ADAUGARE EROARE!!!!!!!!!!!!!!!!!!
            float nota = float.Parse(notaTextBox.Text);
            Profesor.GiveGrade(elevObj, nota);

            // Adaugare in lista de nota
            noteElevListBox.Items.Add(nota.ToString());
            this.Controls.Add(noteElevListBox);

            //Modificare medie generala
            medieGeneralaLabel.Text = elevObj.GetMediaAritmetica().ToString("0.000");

            // Update informations for profesor form
            profesorForm.UpdateData();

            // Afisare mesaj succes
            MessageBox.Show("Elevul " + elevObj.GetName() + " a primit nota " + nota);
            notaTextBox.Text = "nota";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendPrivateMessage();
        }

        private void SendPrivateMessage()
        {
            string message = "Mesaj privat de la profesorul " + profesorForm.profesorObj.GetName() + ": " + privateMsgTextBox.Text + "\n";

            File.AppendAllText(MainProgram.GetInboxPath(elevObj.GetUsername(), true), message);

            MessageBox.Show("Mesajul privat a fost trimis cu succes!");
        }
    }
}
