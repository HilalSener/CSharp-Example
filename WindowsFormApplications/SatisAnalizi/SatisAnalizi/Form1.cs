﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SatisAnalizi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string[] Firmalar = {"A Tekstil", "B Gıda", "C İnşaat"};

        private int[,] Satislar =
        {
            {7000, 8000, 900000, 500000}, {1200000, 1500000, 800000, 1100000},
            {3600000, 4200000, 2200000, 2800000}
        };
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Firmalar.Length; i++)
            {
                lvSatislar.Items.Add(Firmalar[i]);

                for (int j = 0; j < Satislar.GetLength(1); j++)
                {
                    lvSatislar.Items[i].SubItems.Add(Satislar[i, j].ToString());
                }
            }
        }

        private void lvSatislar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvSatislar.SelectedItems.Count > 0)
            {
                //txtFirma.Text = lvSatislar.SelectedItems[0].Text;
                txtFirma.Text = lvSatislar.SelectedItems[0].SubItems[0].Text;
                int Toplam = 0;
                for (int i = 1; i <= Satislar.GetLength(1); i++)
                {
                    Toplam += Convert.ToInt32(lvSatislar.SelectedItems[0].SubItems[i].Text);
                }
                txtOrtalama.Text = (Toplam / Satislar.GetLength(1)).ToString();
            }
        }
    }
}
