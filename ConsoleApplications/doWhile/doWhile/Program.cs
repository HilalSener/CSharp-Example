﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doWhile
{
    class Program
    {
        static void Main(string[] args)
        {
            //doWhile döngülerinin en önemli özelliği, önce işlemler bir defa çalıştırılır. Daha sonra şart kontrol edilir.Bu nedenle döngü en az bir defa çalışmış olur.

            //şifre olarak 9999 girilinceye kadar şifre istemeye devam eden uygulamayı yazınız. 
            string sifre;
            int sayac = 0;
            do
            {
                sayac++;
                Console.Write("Şifrenizi giriniz: ");
                sifre = Console.ReadLine();
            } while (sifre != "9999");
            Console.WriteLine("Hoşgeldiniz...");

            Console.ReadKey();
        }
    }
}
