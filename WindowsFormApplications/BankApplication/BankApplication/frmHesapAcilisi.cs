﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankApplication
{
    public partial class frmHesapAcilisi : Form
    {
        public frmHesapAcilisi()
        {
            InitializeComponent();
        }

        Random rnd = new Random();
        private void frmHesapAcilisi_Load(object sender, EventArgs e)
        {
            this.Top = 60;
            this.Left = 20;
            lblAcilisTarihi.Text = DateTime.Now.ToShortDateString(); //Kısa tarih
            cbHesapTurleri.SelectedIndex = 0;
            SonIDBul();
            HesapNoOlustur();
        }

        private void HesapNoOlustur()
        {
            lblHesapNo.Text = "ACC" + rnd.Next(1000, 1003); //Ratgele 1000 - 9999 arasında bir sayı üretir.
            StreamReader DosyaOku = new StreamReader("HesapKartlari.txt");
            string okunan = DosyaOku.ReadLine();
            if (okunan != null)
            {
                string[] Degerler = okunan.Split(';');
                foreach (var deger in Degerler)
                {
                    if(deger == lblHesapNo.Text)
                    {
                        lblHesapNo.Text = "ACC" + rnd.Next(1000, 1003);
                        DosyaOku.Close();
                        //HesapNoOlustur(); //Kendini çağıran (recursive methods) metotlar
                    }
                    okunan = DosyaOku.ReadLine();
                }
            }
        }

        private void SonIDBul()
        {
            StreamWriter DosyaOluştur = new StreamWriter("HesapKartlari.txt", true);

            DosyaOluştur.Close();

            string[] Degerler = new string[8];
            StreamReader DosyaOku = new StreamReader("HesapKartlari.txt");
            string okunan = DosyaOku.ReadLine();
            if (okunan == null)
                lblHesapId.Text = "1";
            else
                while (okunan != null)
                {
                    //string[] Degerler = okunan.Split(';');
                    //lblHesapId.Text = (Convert.ToInt32(Degerler[0]) + 1).ToString();
                    Degerler = okunan.Split(';');
                    okunan = DosyaOku.ReadLine();
                }
            lblHesapId.Text = (Convert.ToInt32(Degerler[0]) + 1).ToString();
            DosyaOku.Close();
        }

        private void btnHesapAc_Click(object sender, EventArgs e)
        {
            //StreamWriter kullanılacak.
        }
    }
}
