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
    public partial class frmFilmler : Form
    {
        public frmFilmler()
        {
            InitializeComponent();
        }

        private void frmFilmler_Load(object sender, EventArgs e)
        {
            cFilm f = new cFilm();
            f.FilmleriGoster(lvFilmler);
        }
    }
}
