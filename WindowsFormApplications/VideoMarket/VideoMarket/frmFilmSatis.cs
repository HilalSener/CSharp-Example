﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoMarket
{
    public partial class frmFilmSatis : Form
    {
        int orjAdet;
        double orjFiyat;

        public frmFilmSatis()
        {
            InitializeComponent();
        }
       
        private void frmFilmSatis_Load(object sender, EventArgs e)
        {
            txtTarih.Text = DateTime.Now.ToString();
            cFilmSatis fs = new cFilmSatis();
            fs.SatislariGetir(lvSatislar, txtToplamAdet, txtToplamTutar);
        }

        private void dtpTarih_ValueChanged(object sender, EventArgs e)
        {
            txtTarih.Text = dtpTarih.Value.ToShortDateString();
        }

        private void btnMusteriBul_Click(object sender, EventArgs e)
        {
            frmMusteriSorgulama frm = new frmMusteriSorgulama();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            txtMusteriNo.Text = cGenel.musteriNo.ToString();
            txtMusteri.Text = cGenel.musteri.ToString();
        }

        private void btnFilmBul_Click(object sender, EventArgs e)
        {
            frmFilmSorgulama frm = new frmFilmSorgulama();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            txtFilmNo.Text = cGenel.filmNo.ToString();
            txtFilm.Text = cGenel.film;
            txtBirimFiyat.Text = cGenel.fiyat.ToString();
            txtStok.Text = cGenel.miktar.ToString();
        }

        private void txtAdet_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAdet.Text))
            {
                txtAdet.Text = "1";

            }
            if (string.IsNullOrEmpty(txtBirimFiyat.Text))
            {
                txtBirimFiyat.Text = "0";

            }
            txtTutar.Text = (Convert.ToInt32(txtAdet.Text) * Convert.ToDouble(txtBirimFiyat.Text)).ToString();
        }

        private void txtBirimFiyat_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAdet.Text))
            {
                txtAdet.Text = "1";
                txtAdet.Select(0, 1);
            }
            if (string.IsNullOrEmpty(txtBirimFiyat.Text))
            {
                txtBirimFiyat.Text = "0";
                txtBirimFiyat.Select(0, 1);
            }
            txtTutar.Text = (Convert.ToInt32(txtAdet.Text) * Convert.ToDouble(txtBirimFiyat.Text)).ToString();
        }
        
        private void txtBirimFiyat_MouseDown(object sender, MouseEventArgs e)
        {
            txtBirimFiyat.Select(0, txtBirimFiyat.Text.Length);
        }

        private void txtAdet_MouseDown(object sender, MouseEventArgs e)
        {
            txtAdet.Select(0, txtAdet.Text.Length);
        }

        private void txtAdet_Enter(object sender, EventArgs e)
        {
            txtAdet.Select(0, txtAdet.Text.Length);
        }

        private void txtBirimFiyat_Enter(object sender, EventArgs e)
        {
            txtBirimFiyat.Select(0, txtBirimFiyat.Text.Length);
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            btnFilmBul.Enabled = true;
            btnMusteriBul.Enabled = true;
            btnKaydet.Enabled = true;
            Temizle();
        }

        public void Temizle()
        {
            //txtMusteriNo.Clear();
            //txtMusteri.Clear();
            txtFilmNo.Clear();
            txtFilm.Clear();
            txtStok.Clear();
            txtAdet.Text = "1";
            txtBirimFiyat.Text = "0";
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtMusteri.Text.Trim() != "" || txtFilm.Text.Trim() != "")
            {
                if (Convert.ToInt32(txtAdet.Text) <= Convert.ToInt32(txtStok.Text))
                {
                    cFilmSatis fs = new cFilmSatis();
                    fs.Tarih = Convert.ToDateTime(txtTarih.Text);
                    fs.FilmNo = Convert.ToInt32(txtFilmNo.Text);
                    fs.MusteriNo = Convert.ToInt32(txtMusteriNo.Text);
                    fs.Adet = Convert.ToInt32(txtAdet.Text);
                    fs.BirimFiyat = Convert.ToDouble(txtBirimFiyat.Text);
                    if (fs.SatisEkle(fs))
                    {
                        MessageBox.Show("Satış bilgileri eklendi!");
                        fs.SatislariGetir(lvSatislar, txtToplamAdet, txtToplamTutar);
                        //Satılan film stoktan düşülmeli...
                        cFilm f = new cFilm();
                        if (f.StokGuncelleBySatisEkle(fs.FilmNo, fs.Adet))
                            MessageBox.Show("Stok güncellendi!");
                        Temizle();
                        btnKaydet.Enabled = false;
                        btnFilmBul.Enabled = false;
                        btnMusteriBul.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("En fazla " + txtStok.Text + " adet satılabilir!", "Yetersiz stok miktarı!");
                }
            }
            else
                MessageBox.Show("Mutlaka müştri ve film seçilmelidir!", "Dikkat, Eksik bilgi!");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Silmek istiyor musunuz?", "Silinsin mi?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cFilmSatis fs = new cFilmSatis();
                if(fs.SatisSil(Convert.ToInt32(txtSatisNo.Text)))
                {
                    MessageBox.Show("Satış bilgileri iptal edildi!");
                    //İptal edilen satış miktarı stoğa iade edilmeli.
                    cFilm f = new cFilm();
                    if (f.StokGuncelleBySatisIptal(Convert.ToInt32(txtFilmNo.Text), Convert.ToInt32(txtAdet.Text)))
                    {
                        MessageBox.Show("Stok güncellendi.");
                    }
                    Temizle();
                    fs.SatislariGetir(lvSatislar, txtToplamAdet, txtToplamTutar);
                }
            }
        }

        private void lvSatislar_DoubleClick(object sender, EventArgs e)
        {
            txtSatisNo.Text = lvSatislar.SelectedItems[0].SubItems[0].Text;
            txtTarih.Text = lvSatislar.SelectedItems[0].SubItems[1].Text;
            txtFilm.Text = lvSatislar.SelectedItems[0].SubItems[2].Text;
            txtFilmNo.Text = lvSatislar.SelectedItems[0].SubItems[8].Text;
            txtMusteri.Text = lvSatislar.SelectedItems[0].SubItems[3].Text;
            txtMusteriNo.Text = lvSatislar.SelectedItems[0].SubItems[7].Text;
            txtBirimFiyat.Text = lvSatislar.SelectedItems[0].SubItems[4].Text;
            txtAdet.Text = lvSatislar.SelectedItems[0].SubItems[5].Text;
            txtTutar.Text = lvSatislar.SelectedItems[0].SubItems[6].Text;
            orjAdet = Convert.ToInt32(txtAdet.Text);
            orjFiyat = Convert.ToDouble(txtBirimFiyat.Text);
            btnDegistir.Enabled = true;
            btnSil.Enabled = true;
            btnKaydet.Enabled = false;
            txtAdet.Focus();
        }

        private void btnDegistir_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtAdet.Text) == orjAdet && Convert.ToDouble(txtBirimFiyat.Text) == orjFiyat)
            {
                MessageBox.Show("Herhangi bir satış bilgisi değişmemiştir!");
                txtAdet.Focus();
            }
            else
            {
                cFilmSatis fs = new cFilmSatis();
                bool Sonuc = fs.SatisGuncelle(Convert.ToInt32(txtSatisNo.Text), Convert.ToInt32(txtAdet.Text), Convert.ToDouble(txtBirimFiyat.Text));
                if (Sonuc) MessageBox.Show("Satış bilgileri güncellendi!");
                if (Convert.ToInt32(txtAdet.Text) != orjAdet)
                {
                    cFilm f = new cFilm();
                    if (f.StokGuncelleBySatisDegistir(Convert.ToInt32(txtFilmNo.Text), Convert.ToInt32(txtAdet.Text), orjAdet))
                    {
                        MessageBox.Show("Stok güncellendi!");
                    }
                }
                Temizle();
                fs.SatislariGetir(lvSatislar, txtToplamAdet, txtToplamTutar);
            }
        }
    }
}
