using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


// 17-155-014 HAKKI EGEMEN GÜLPINAR 
// BİLGİSAYAR MÜHENDİSLİĞİ 
// COMPUTER GRAPHICS PROJE

//*****PROGRAMDA ISTENILEN TÜM FONKSIYONLAR ÇALIŞMAKTADIR.*****//

namespace graphicProje {
	public partial class goruntuIsleme : Form {
		private Point orjinal_baslangicNoktasi = Point.Empty, orjinal_bitisNoktasi = Point.Empty, hedef_baslangicNoktasi = Point.Empty, hedef_bitisNoktasi = Point.Empty;
		private bool orjinal_tasiniyor = false, hedef_tasiniyor = false;

        //// DDA CIZGI ALGORITMASI KOD BLOĞU
        
        public Bitmap bmp;
        public Bitmap pixel;
        public Graphics g;
        Thread t;
        delegate void PixelFunc(int x, int y, Color c);

        private void Init()
        {
            if (g != null) g.Dispose();
            pct_hedefGoruntu.Image = null;
            if (bmp != null) bmp.Dispose();
            bmp = new Bitmap(pct_hedefGoruntu.Width, pct_hedefGoruntu.Height);
            if (pixel != null) pixel.Dispose();
            pixel = new Bitmap(1, 1);
            pct_hedefGoruntu.Image = bmp;
            g = pct_hedefGoruntu.CreateGraphics();
        }

        private void SetPixel(int x, int y, Color c)
        {
            lock (g)
            {
                pixel.SetPixel(0, 0, Color.Red);
                g.DrawImageUnscaled(pixel, x, y);
                bmp.SetPixel(x, y, Color.Blue);
            }
        }

        private void Render()
        {

            int xInitial = 0, yInitial = 0, xFinal = 380, yFinal = 315; //BURADAKI x,y değerlerini pictureBox'un sol üst koşesinden sağ alt köşesine
                                                                        //düz bir çizgi çizmesi için verildi,farklı değerler de girilebilir.
                                                                        //Kendi bilgisayarımda köşegenler arasında düz bir çizgi elde ettim.
            int dx = xFinal - xInitial, dy = yFinal - yInitial, steps, k, xf, yf;

            float xIncrement, yIncrement, x = xInitial, y = yInitial;

            if (Math.Abs(dx) > Math.Abs(dy)) steps = Math.Abs(dx);

            else steps = Math.Abs(dy);
            xIncrement = dx / (float)steps;
            yIncrement = dy / (float)steps;
            PixelFunc func = new PixelFunc(SetPixel);
            for (k = 0; k < steps; k++)
            {
                x += xIncrement;
                xf = (int)x;
                y += yIncrement;
                yf = (int)y;
                try
                {
                    pct_hedefGoruntu.Invoke(func, xf, yf, Color.Blue);
                }
                catch (InvalidOperationException)
                {
                    return;
                }
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {
            Init();
            if (t != null && t.IsAlive) t.Abort();
            t = new Thread(new ThreadStart(Render));
            t.Start();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            g.Dispose();
            bmp.Dispose();
            pixel.Dispose();
        } /////**** DDA ÇIZGI ALGORITMASI KOD BLOĞU BITIŞ NOKTASI.****/////


        public goruntuIsleme() {
			InitializeComponent();
			cmb_zoomMetot.SelectedIndex = 0;
		}

		private void goruntuAc( object sender, EventArgs e ) {
			OpenFileDialog dosyaAcici = new OpenFileDialog() {
				InitialDirectory = @"D:\Resimler",
				Filter = "PNG Files|*.png|Jpg Files|*.jpg|Jpeg Files|*.jpeg|Bmp Files|*.bmp"
            };

			if ( dosyaAcici.ShowDialog() != DialogResult.OK ) return;

			string dosyaAdi = dosyaAcici.FileName;

			pct_orjinalGoruntu.ImageLocation = dosyaAdi;
            buyut_flow = 0;

			
		}

		private void resimYuklendi( object sender, AsyncCompletedEventArgs e ) {
			lbl_genislik.Text = "Width: " + pct_orjinalGoruntu.Image.Width + "px";
			lbl_yukseklik.Text = "Heigh: " + pct_orjinalGoruntu.Image.Height + "px";
			lbl_renkDerinligi.Text = "Color Depth: " + pct_orjinalGoruntu.Image.PixelFormat;
		}

		private void btn_orjinalGoruntu_sigdir_Click( object sender, EventArgs e ) { pct_orjinalGoruntu.SizeMode = PictureBoxSizeMode.Zoom; }

		private void btn_orjinalGoruntu_orjinalBoyut_Click( object sender, EventArgs e ) { pct_orjinalGoruntu.SizeMode = PictureBoxSizeMode.Normal; }

		private void btn_hedefGoruntu_orjinalBoyut_Click( object sender, EventArgs e ) { pct_hedefGoruntu.SizeMode = PictureBoxSizeMode.Normal; }

		private void btn_hedefGoruntu_sigdir_Click( object sender, EventArgs e ) { pct_hedefGoruntu.SizeMode = PictureBoxSizeMode.Zoom; }

		private void orjinal_fareBasildi( object sender, MouseEventArgs e ) {
			if ( pct_orjinalGoruntu.Image == null || pct_orjinalGoruntu.SizeMode == PictureBoxSizeMode.Zoom ) return;
			if ( e.Button == MouseButtons.Left ) {
				Cursor.Current = Cursors.SizeAll;
				orjinal_tasiniyor = true;
				orjinal_baslangicNoktasi = new Point( e.Location.X - orjinal_bitisNoktasi.X, e.Location.Y - orjinal_bitisNoktasi.Y );
			}
		}

		private void orjinal_fareBirakildi( object sender, MouseEventArgs e ) {
			if ( pct_orjinalGoruntu.SizeMode == PictureBoxSizeMode.Zoom ) return;
			orjinal_tasiniyor = false;
			Cursor.Current = Cursors.Default;
		}

		private void orjinal_fareHareket( object sender, MouseEventArgs e ) {
			if ( pct_orjinalGoruntu.SizeMode == PictureBoxSizeMode.Zoom ) return;
			Control c = ( Control )sender;
			if ( orjinal_tasiniyor && c != null ) {
				orjinal_bitisNoktasi = new Point( e.Location.X - orjinal_baslangicNoktasi.X, e.Location.Y - orjinal_baslangicNoktasi.Y );
				pct_orjinalGoruntu.Invalidate();
			}
		}

		private void orjinal_goruntuCiziliyor( object sender, PaintEventArgs e ) {
			if ( pct_orjinalGoruntu.SizeMode == PictureBoxSizeMode.Zoom ) return;
			if ( pct_orjinalGoruntu.Image == null || !orjinal_tasiniyor ) return;

			e.Graphics.Clear( Color.White );
			e.Graphics.DrawImage( pct_orjinalGoruntu.Image, orjinal_bitisNoktasi );
		}

		private void hedef_fareBasildi( object sender, MouseEventArgs e ) {
			if ( pct_hedefGoruntu.Image == null || pct_hedefGoruntu.SizeMode == PictureBoxSizeMode.Zoom ) return;
			if ( e.Button == MouseButtons.Left ) {
				Cursor.Current = Cursors.SizeAll;
				hedef_tasiniyor = true;
				hedef_baslangicNoktasi = new Point( e.Location.X - hedef_bitisNoktasi.X, e.Location.Y - hedef_bitisNoktasi.Y );
			}
		}

		private void hedef_fareBirakildi( object sender, MouseEventArgs e ) {
			if ( pct_hedefGoruntu.SizeMode == PictureBoxSizeMode.Zoom ) return;
			hedef_tasiniyor = false;
			Cursor.Current = Cursors.Default;
		}

        private void goruntuIsleme_Load(object sender, EventArgs e)
        {

        }

        //// YAKINLAŞTIRMA (+) KOD BLOĞU
        int buyut_flow = 0;
        Bitmap copyGoruntu;

        private void btn_yakinlas_Click(object sender, EventArgs e)
        {

            if(cmb_zoomMetot.SelectedIndex == 0) ////  0 SEÇİLİ = ZERO ORDER ZOOM(YAKINLAŞTIRMA) ALGORİTMA KOD BLOĞU
            {
                copyGoruntu = (Bitmap)pct_orjinalGoruntu.Image;
                if (buyut_flow != 0)
                {
                    Bitmap goruntu = (Bitmap)pct_hedefGoruntu.Image;
                    pct_orjinalGoruntu.Image = goruntu;
                }

                if (pct_orjinalGoruntu.Image == null) return;

                if (pct_hedefGoruntu.Image != null)
                {
                    pct_hedefGoruntu.Image = null;
                    pct_hedefGoruntu.Refresh();
                }

                int genislik = pct_orjinalGoruntu.Image.Width;
                int yukseklik = pct_orjinalGoruntu.Image.Height;

                Bitmap orjinalGoruntu = (Bitmap)pct_orjinalGoruntu.Image;
                Bitmap hedefGoruntu = new Bitmap(genislik * 2, yukseklik * 2);

                Stopwatch olcer = new Stopwatch();
                olcer.Start();
                prg_islem.Value = 0;
                prg_islem.Maximum = genislik;

                int hedefSatir = 0;

                for (int sutun = 0; sutun < genislik; sutun++)
                {
                    for (int satir = 0; satir < yukseklik; satir++)
                    {
                        Color orjinalPiksel = orjinalGoruntu.GetPixel(sutun, satir);
                        hedefGoruntu.SetPixel(sutun, hedefSatir, orjinalPiksel);
                        hedefGoruntu.SetPixel(sutun, hedefSatir + 1, orjinalPiksel);
                        hedefSatir = hedefSatir + 2;
                    }
                    hedefSatir = 0;
                }

                int hedefSutun = 0;

                Bitmap hedefGoruntu2 = new Bitmap(genislik * 2, yukseklik * 2);

                for (int satir = 0; satir < yukseklik * 2; satir++)
                {
                    for (int sutun = 0; sutun < genislik; sutun++)
                    {
                        Color orjinalPiksel = hedefGoruntu.GetPixel(sutun, satir);
                        hedefGoruntu2.SetPixel(hedefSutun, satir, orjinalPiksel);
                        hedefGoruntu2.SetPixel(hedefSutun + 1, satir, orjinalPiksel);
                        hedefSutun = hedefSutun + 2;
                    }
                    hedefSutun = 0;
                } 

                olcer.Stop();
                lbl_gecenSure.Text = "Process Time: " + olcer.Elapsed.Seconds + " sn " + olcer.Elapsed.Milliseconds + " ms";
                pct_hedefGoruntu.Image = hedefGoruntu2;
                buyut_flow = buyut_flow + 1;
                pct_orjinalGoruntu.Image = copyGoruntu;
            }    /////**** ZERO-ORDER ZOOM(YAKINLAŞTIRMA) KOD BLOĞU BITIŞ NOKTASI.****/////


            if (cmb_zoomMetot.SelectedIndex == 1)  ////1 SEÇİLİ = FIRST ORDER ZOOM(YAKINLAŞTIRMA) ALGORİTMA KOD BLOĞU
            {
                copyGoruntu = (Bitmap)pct_orjinalGoruntu.Image;
                if (buyut_flow != 0)
                {
                    Bitmap goruntu = (Bitmap)pct_hedefGoruntu.Image;
                    pct_orjinalGoruntu.Image = goruntu;
                }

                if (pct_orjinalGoruntu.Image == null) return;

                if (pct_hedefGoruntu.Image != null)
                {
                    pct_hedefGoruntu.Image = null;
                    pct_hedefGoruntu.Refresh();
                }

                int genislik = pct_orjinalGoruntu.Image.Width;
                int yukseklik = pct_orjinalGoruntu.Image.Height;

                Bitmap orjinalGoruntu = (Bitmap)pct_orjinalGoruntu.Image;
                Bitmap hedefGoruntu = new Bitmap(2 * genislik - 1, 2 * yukseklik - 1);

                Stopwatch olcer = new Stopwatch();
                olcer.Start();
                prg_islem.Value = 0;
                prg_islem.Maximum = genislik;

                for (int sutun = 0; sutun < genislik; sutun++)
                {
                    for (int satir = 0; satir < yukseklik; satir++)
                    {
                        Color orjinalPiksel = orjinalGoruntu.GetPixel(sutun, satir);
                        hedefGoruntu.SetPixel(sutun * 2, satir * 2, orjinalPiksel);
                    }
                }


                for (int sutun = 0; sutun + 2 < (2 * genislik); sutun += 2)
                {
                    for (int satir = 0; satir + 2 < (2 * yukseklik); satir += 2)
                    {
                        Color orjinalPiksel = hedefGoruntu.GetPixel(sutun, satir);
                        Color orjinalPiksel2 = hedefGoruntu.GetPixel(sutun, satir + 2);
                        int ortRenkR = (int)(orjinalPiksel.R + orjinalPiksel2.R) / 2;
                        int ortRenkG = (int)(orjinalPiksel.G + orjinalPiksel2.G) / 2;
                        int ortRenkB = (int)(orjinalPiksel.B + orjinalPiksel2.B) / 2;
                        Color araPiksel = Color.FromArgb(ortRenkR, ortRenkG, ortRenkB);
                        hedefGoruntu.SetPixel(sutun, satir + 1, araPiksel);
                    }
                }

                for (int sutun = 0; sutun + 2 < (2 * genislik); sutun += 2)
                {
                    for (int satir = 0; satir + 2 < (2 * yukseklik); satir++)
                    {
                        Color orjinalPiksel = hedefGoruntu.GetPixel(sutun, satir);
                        Color orjinalPiksel2 = hedefGoruntu.GetPixel(sutun + 2, satir);
                        int ortRenkR = (int)(orjinalPiksel.R + orjinalPiksel2.R) / 2;
                        int ortRenkG = (int)(orjinalPiksel.G + orjinalPiksel2.G) / 2;
                        int ortRenkB = (int)(orjinalPiksel.B + orjinalPiksel2.B) / 2;
                        Color araPiksel = Color.FromArgb(ortRenkR, ortRenkG, ortRenkB);
                        hedefGoruntu.SetPixel(sutun + 1, satir, araPiksel);
                    }

                } 


                olcer.Stop();
                lbl_gecenSure.Text = "Geçen Süre: " + olcer.Elapsed.Seconds + " sn " + olcer.Elapsed.Milliseconds + " ms";
                pct_hedefGoruntu.Image = hedefGoruntu;
                buyut_flow = buyut_flow + 1;
                pct_orjinalGoruntu.Image = copyGoruntu;
            }   /////**** FIRST-ORDER ZOOM(YAKINLAŞTIRMA) KOD BLOĞU BITIŞ NOKTASI.****/////


            if (cmb_zoomMetot.SelectedIndex == 2)  ////2 SEÇİLİ = K-TIME (YAKINLAŞTIRMA) ALGORİTMA KOD BLOĞU
            {
                copyGoruntu = (Bitmap)pct_orjinalGoruntu.Image;
                if (buyut_flow != 0)
                {
                    Bitmap goruntu = (Bitmap)pct_hedefGoruntu.Image;
                    pct_orjinalGoruntu.Image = goruntu;
                }

                if (pct_orjinalGoruntu.Image == null) return;

                if (pct_hedefGoruntu.Image != null)
                {
                    pct_hedefGoruntu.Image = null;
                    pct_hedefGoruntu.Refresh();
                }

                int genislik = pct_orjinalGoruntu.Image.Width;
                int yukseklik = pct_orjinalGoruntu.Image.Height;

                Bitmap orjinalGoruntu = (Bitmap)pct_orjinalGoruntu.Image;
                Bitmap hedefGoruntu = new Bitmap((2 * (genislik - 1)) + 1, (2 * (yukseklik - 1)) + 1);

                Stopwatch olcer = new Stopwatch();
                olcer.Start();
                prg_islem.Value = 0;
                prg_islem.Maximum = genislik;

                for (int sutun = 0; sutun < genislik; sutun++)
                {
                    for (int satir = 0; satir < yukseklik; satir++)
                    {
                        Color orjinalPiksel = orjinalGoruntu.GetPixel(sutun, satir);
                        hedefGoruntu.SetPixel(sutun * 2, satir * 2, orjinalPiksel);
                    }
                }


                for (int sutun = 0; sutun + 2 < (2 * genislik); sutun += 2)
                {
                    for (int satir = 0; satir + 2 < (2 * yukseklik); satir += 2)
                    {
                        Color orjinalPiksel = hedefGoruntu.GetPixel(sutun, satir);
                        Color orjinalPiksel2 = hedefGoruntu.GetPixel(sutun, satir + 2);
                        int p1r = orjinalPiksel.R; int p1g = orjinalPiksel.G; int p1b = orjinalPiksel.B;
                        int p2r = orjinalPiksel2.R; int p2g = orjinalPiksel2.G; int p2b = orjinalPiksel2.B;
                        int nr = 0; int ng = 0; int nb = 0;

                        if(p1r > p2r) //for red
                        {
                            int fk = (p1r - p2r) / 2;
                            nr = p2r + fk;
                        }
                        else
                        {
                            int fk = (p2r - p1r) / 2;
                            nr = p1r + fk;
                        }

                        
                        
                        if (p1g > p2g) // for green
                        {
                            int fg = (p1g - p2g) / 2;
                            ng = p2g + fg;
                        }
                        else
                        {
                            int fg = (p2g - p1g) / 2;
                            ng = p1g + fg;
                        }

                        
                        
                        if (p1b > p2b) //for blue
                        {
                            int fb = (p1b - p2b) / 2;
                            nb = p2b + fb;
                        }
                        else
                        {
                            int fb = (p2b - p1b) / 2;
                            nb = p1b + fb;
                        }

                       

                        Color araPiksel = Color.FromArgb(nr, ng, nb);
                        hedefGoruntu.SetPixel(sutun, satir + 1, araPiksel);
                    }
                }

                for (int sutun = 0; sutun + 2 < (2 * genislik); sutun += 2)
                {
                    for (int satir = 0; satir + 2 < (2 * yukseklik); satir++)
                    {
                        Color orjinalPiksel = hedefGoruntu.GetPixel(sutun, satir);
                        Color orjinalPiksel2 = hedefGoruntu.GetPixel(sutun + 2, satir);

                        int p1r = orjinalPiksel.R; int p1g = orjinalPiksel.G; int p1b = orjinalPiksel.B;
                        int p2r = orjinalPiksel2.R; int p2g = orjinalPiksel2.G; int p2b = orjinalPiksel2.B;
                        int nr = 0; int ng = 0; int nb = 0;

                        if (p1r > p2r) //for red
                        {
                            int fk = (p1r - p2r) / 2;
                            nr = p2r + fk;
                        }
                        else
                        {
                            int fk = (p2r - p1r) / 2;
                            nr = p1r + fk;
                        }

                        

                        if (p1g > p2g) // for green
                        {
                            int fg = (p1g - p2g) / 2;
                            ng = p2g + fg;
                        }
                        else
                        {
                            int fg = (p2g - p1g) / 2;
                            ng = p1g + fg;
                        }

                        

                        if (p1b > p2b) //for blue
                        {
                            int fb = (p1b - p2b) / 2;
                            nb = p2b + fb;
                        }
                        else
                        {
                            int fb = (p2b - p1b) / 2;
                            nb = p1b + fb;
                        }

                        

                        Color araPiksel = Color.FromArgb(nr, ng, nb);
                        hedefGoruntu.SetPixel(sutun + 1, satir, araPiksel);
                    }

                }    


                olcer.Stop();
                lbl_gecenSure.Text = "Geçen Süre: " + olcer.Elapsed.Seconds + " sn " + olcer.Elapsed.Milliseconds + " ms";
                pct_hedefGoruntu.Image = hedefGoruntu;
                buyut_flow = buyut_flow + 1;
                pct_orjinalGoruntu.Image = copyGoruntu;
            }     /////**** K-TIMES ZOOM(YAKINLAŞTIRMA) KOD BLOĞU BITIŞ NOKTASI.****/////



            if (cmb_zoomMetot.SelectedIndex == 3) ////  3 SEÇİLİ = BILINEAR ZOOM(YAKINLAŞTIRMA) ALGORİTMA KOD BLOĞU
            {
                if (pct_orjinalGoruntu.Image == null) return;

                Bitmap orjinalGoruntu;
                int genislik = pct_orjinalGoruntu.Image.Width;
                int yukseklik = pct_orjinalGoruntu.Image.Height;
                Stopwatch olcer = new Stopwatch();
                olcer.Start();
                prg_islem.Value = 0;
                prg_islem.Maximum = genislik;

                copyGoruntu = (Bitmap)pct_orjinalGoruntu.Image;
                if (buyut_flow != 0)
                {
                    orjinalGoruntu = (Bitmap)pct_hedefGoruntu.Image;
                    pct_orjinalGoruntu.Image = orjinalGoruntu;
                }

                if (pct_hedefGoruntu.Image != null)
                {
                    pct_hedefGoruntu.Image = null;
                    pct_hedefGoruntu.Refresh();
                }

                orjinalGoruntu = (Bitmap)pct_orjinalGoruntu.Image;
                if(orjinalGoruntu is Bitmap oi)
                {
                    Image result = Scale(oi, 1.6f, 1.6f);
                    pct_hedefGoruntu.Image = result;
                    buyut_flow = buyut_flow + 1;
                    pct_orjinalGoruntu.Image = copyGoruntu;
                    olcer.Stop();
                    lbl_gecenSure.Text = "Geçen Süre: " + olcer.Elapsed.Seconds + " sn " + olcer.Elapsed.Milliseconds + " ms";
                    pct_hedefGoruntu.Image = result;
                    buyut_flow = buyut_flow + 1;
                    pct_orjinalGoruntu.Image = copyGoruntu;
                }

                
            }



        }

        

        private static Image Scale(Bitmap self, float scaleX, float scaleY)
        {
            int newWidth = (int)(self.Width * scaleX);
            int newHeight = (int)(self.Height * scaleY);
            Bitmap newImage = new Bitmap(newWidth, newHeight, self.PixelFormat);


            for (int x = 0; x < newWidth; x++)
            {
                for (int y = 0; y < newHeight; y++)
                {
                    float gx = ((float)x) / newWidth * (self.Width - 1);
                    float gy = ((float)y) / newHeight * (self.Height - 1);
                    int gxi = (int)gx;
                    int gyi = (int)gy;
                    Color c00 = self.GetPixel(gxi, gyi);
                    Color c10 = self.GetPixel(gxi + 1, gyi);
                    Color c01 = self.GetPixel(gxi, gyi + 1);
                    Color c11 = self.GetPixel(gxi + 1, gyi + 1);

                    int red = (int)Blerp(c00.R, c10.R, c01.R, c11.R, gx - gxi, gy - gyi);
                    int green = (int)Blerp(c00.G, c10.G, c01.G, c11.G, gx - gxi, gy - gyi);
                    int blue = (int)Blerp(c00.B, c10.B, c01.B, c11.B, gx - gxi, gy - gyi);
                    Color rgb = Color.FromArgb(red, green, blue);
                    newImage.SetPixel(x, y, rgb);
                }
            }

            return newImage;
        }


        private static float Lerp(float s, float e, float t)
        {
            return s + (e - s) * t;
        }

        private static float Blerp(float c00, float c10, float c01, float c11, float tx, float ty)
        {
            return Lerp(Lerp(c00, c10, tx), Lerp(c01, c11, tx), ty);
        }    /////**** BILINEAR ZOOM(YAKINLAŞTIRMA) KOD BLOĞU BITIŞ NOKTASI.****/////




        //// UZAKŞATIRMA (-) KOD BLOĞU
        Bitmap copykucult;

        private void btn_uzaklas_Click(object sender, EventArgs e)
        {
            if (cmb_zoomMetot.SelectedIndex == 0 || cmb_zoomMetot.SelectedIndex == 1) ////1 veya 2 SEÇİLİ = ZERO ORDER veya FIRST ORDER (UZAKLAŞTIRMA) ALGORİTMA KOD BLOĞU
            {
                copykucult = (Bitmap)pct_orjinalGoruntu.Image;
                if (buyut_flow != 0)
                {
                    Bitmap goruntu = (Bitmap)pct_hedefGoruntu.Image;
                    pct_orjinalGoruntu.Image = goruntu;
                }

                if (pct_orjinalGoruntu.Image == null) return;

                if (pct_hedefGoruntu.Image != null)
                {
                    pct_hedefGoruntu.Image = null;
                    pct_hedefGoruntu.Refresh();
                }

                int genislik = pct_orjinalGoruntu.Image.Width;
                int yukseklik = pct_orjinalGoruntu.Image.Height;

                Bitmap orjinalGoruntu = (Bitmap)pct_orjinalGoruntu.Image;
                Bitmap hedefGoruntu = new Bitmap(genislik / 2, yukseklik / 2);

                Stopwatch olcer = new Stopwatch();
                olcer.Start();
                prg_islem.Value = 0;
                prg_islem.Maximum = genislik;

                int hedefSatir = 0;
                int hedefSutun = 0;

                for (int sutun = 0; sutun < genislik/2; sutun++)
                {
                    for (int satir = 0; satir < yukseklik/2; satir++)
                    {
                        Color orj1 = orjinalGoruntu.GetPixel(hedefSutun, hedefSatir);
                        Color orj2 = orjinalGoruntu.GetPixel(hedefSutun, hedefSatir + 1);
                        Color orj3 = orjinalGoruntu.GetPixel(hedefSutun + 1, hedefSatir);
                        Color orj4 = orjinalGoruntu.GetPixel(hedefSutun + 1, hedefSatir + 1);
                        int ortRed = Math.Abs(orj1.R + orj2.R + orj3.R + orj4.R ) / 4;
                        int ortGreen = Math.Abs(orj1.G + orj2.G + orj3.G + orj4.G) / 4;
                        int ortBlue = Math.Abs(orj1.B + orj2.B + orj3.B + orj4.B) / 4;
                        Color yeniRenk = Color.FromArgb(ortRed, ortGreen, ortBlue);
                        hedefGoruntu.SetPixel(sutun, satir, yeniRenk);
                        hedefSatir += 2;
                    }
                    hedefSutun += 2;
                    hedefSatir = 0;
                }

                olcer.Stop();
                lbl_gecenSure.Text = "Geçen Süre: " + olcer.Elapsed.Seconds + " sn " + olcer.Elapsed.Milliseconds + " ms";
                pct_hedefGoruntu.Image = hedefGoruntu;
                buyut_flow = buyut_flow + 1;
                pct_orjinalGoruntu.Image = copykucult;
                /////**** ZERO veya FIRST ORDER ZOOM(UZAKLAŞTIRMA) KOD BLOĞU BITIŞ NOKTASI.****/////
            }

            if (cmb_zoomMetot.SelectedIndex == 2) ////2 SEÇİLİ = K-TIME (UZAKLAŞTIRMA) ALGORİTMA KOD BLOĞU
            {
                copykucult = (Bitmap)pct_orjinalGoruntu.Image;
                if (buyut_flow != 0)
                {
                    Bitmap goruntu = (Bitmap)pct_hedefGoruntu.Image;
                    pct_orjinalGoruntu.Image = goruntu;
                }

                if (pct_orjinalGoruntu.Image == null) return;

                if (pct_hedefGoruntu.Image != null)
                {
                    pct_hedefGoruntu.Image = null;
                    pct_hedefGoruntu.Refresh();
                }

                int genislik = pct_orjinalGoruntu.Image.Width;
                int yukseklik = pct_orjinalGoruntu.Image.Height;

                Bitmap orjinalGoruntu = (Bitmap)pct_orjinalGoruntu.Image;
                Bitmap hedefGoruntu = new Bitmap(genislik / 2, yukseklik / 2);

                Stopwatch olcer = new Stopwatch();
                olcer.Start();
                prg_islem.Value = 0;
                prg_islem.Maximum = genislik;

                int hedefSatir = 0;
                int hedefSutun = 0;
                int K = 2;

                for (int sutun = 0; sutun < genislik / 2; sutun++)
                {
                    for (int satir = 0; satir < yukseklik / 2; satir++)
                    {
                        Color orj1 = orjinalGoruntu.GetPixel(hedefSutun, hedefSatir);
                        Color orj2 = orjinalGoruntu.GetPixel(hedefSutun, hedefSatir + 1);
                        Color orj3 = orjinalGoruntu.GetPixel(hedefSutun + 1, hedefSatir);
                        Color orj4 = orjinalGoruntu.GetPixel(hedefSutun + 1, hedefSatir + 1);
                        int ortRed = (Math.Abs(orj1.R + orj2.R + orj3.R + orj4.R) / 4);
                        int ortGreen = (Math.Abs(orj1.G + orj2.G + orj3.G + orj4.G) / 4);
                        int ortBlue = (Math.Abs(orj1.B + orj2.B + orj3.B + orj4.B) / 4);

                        if (ortRed >= 2)
                        {
                            ortRed -= K;
                        }

                        if (ortGreen >= 2)
                        {
                            ortGreen -= K;
                        }

                        if (ortBlue >= 2)
                        {
                            ortBlue -= K;
                        }

                        Color yeniRenk = Color.FromArgb(ortRed, ortGreen, ortBlue);
                        hedefGoruntu.SetPixel(sutun, satir, yeniRenk);
                        hedefSatir += 2;
                    }
                    hedefSutun += 2;
                    hedefSatir = 0;
                } 

                olcer.Stop();
                lbl_gecenSure.Text = "Geçen Süre: " + olcer.Elapsed.Seconds + " sn " + olcer.Elapsed.Milliseconds + " ms";
                pct_hedefGoruntu.Image = hedefGoruntu;
                buyut_flow = buyut_flow + 1;
                pct_orjinalGoruntu.Image = copykucult;
            }  /////**** K-TIMES ZOOM(UZAKLAŞTIRMA) KOD BLOĞU BITIŞ NOKTASI.****/////


            if (cmb_zoomMetot.SelectedIndex == 3) ////  3 SEÇİLİ = BILINEAR ZOOM(UZAKLAŞTIRMA) ALGORİTMA KOD BLOĞU
            {
                if (pct_orjinalGoruntu.Image == null) return;

                Bitmap orjinalGoruntu;
                int genislik = pct_orjinalGoruntu.Image.Width;
                int yukseklik = pct_orjinalGoruntu.Image.Height;
                Stopwatch olcer = new Stopwatch();
                olcer.Start();
                prg_islem.Value = 0;
                prg_islem.Maximum = genislik;

                copyGoruntu = (Bitmap)pct_orjinalGoruntu.Image;
                if (buyut_flow != 0)
                {
                    orjinalGoruntu = (Bitmap)pct_hedefGoruntu.Image;
                    pct_orjinalGoruntu.Image = orjinalGoruntu;
                }

                if (pct_hedefGoruntu.Image != null)
                {
                    pct_hedefGoruntu.Image = null;
                    pct_hedefGoruntu.Refresh();
                }

                orjinalGoruntu = (Bitmap)pct_orjinalGoruntu.Image;
                if (orjinalGoruntu is Bitmap oi)
                {
                    Image result = Scale2(oi, 1.6f, 1.6f);
                    pct_hedefGoruntu.Image = result;
                    buyut_flow = buyut_flow + 1;
                    pct_orjinalGoruntu.Image = copyGoruntu;
                    olcer.Stop();
                    lbl_gecenSure.Text = "Geçen Süre: " + olcer.Elapsed.Seconds + " sn " + olcer.Elapsed.Milliseconds + " ms";
                    pct_hedefGoruntu.Image = result;
                    buyut_flow = buyut_flow + 1;
                    pct_orjinalGoruntu.Image = copyGoruntu;
                }
            }
        }

        private static Image Scale2(Bitmap self, float scaleX, float scaleY)
        {
            int newWidth = (int)(self.Width / scaleX);
            int newHeight = (int)(self.Height / scaleY);
            Bitmap newImage = new Bitmap(newWidth, newHeight, self.PixelFormat);

            for (int x = 0; x < newWidth; x++)
            {
                for (int y = 0; y < newHeight; y++)
                {
                    float gx = ((float)x) / newWidth * (self.Width - 1);
                    float gy = ((float)y) / newHeight * (self.Height - 1);
                    int gxi = (int)gx;
                    int gyi = (int)gy;
                    Color c00 = self.GetPixel(gxi, gyi);
                    Color c10 = self.GetPixel(gxi + 1, gyi);
                    Color c01 = self.GetPixel(gxi, gyi + 1);
                    Color c11 = self.GetPixel(gxi + 1, gyi + 1);

                    int red = (int)Blerp(c00.R, c10.R, c01.R, c11.R, gx - gxi, gy - gyi);
                    int green = (int)Blerp(c00.G, c10.G, c01.G, c11.G, gx - gxi, gy - gyi);
                    int blue = (int)Blerp(c00.B, c10.B, c01.B, c11.B, gx - gxi, gy - gyi);
                    Color rgb = Color.FromArgb(red, green, blue);
                    newImage.SetPixel(x, y, rgb);
                }
            }

            return newImage;

            /////**** BILINEAR(UZAKLAŞTIRMA) KOD BLOĞU BITIŞ NOKTASI.****/////
        }

        /////**** UZAKLAŞTIRMA(-) KOD BLOĞU BITIŞ NOKTASI.****/////


        private void cmb_zoomMetot_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //////////   SOLA DONDÜRME(ROTATION) KOD BLOĞU

        int dondur = 0;
        private void btnSol_Click(object sender, EventArgs e)
        {

            int genislik = 0;
            int yukseklik = 0;
            Bitmap orjinalGoruntu;
            Bitmap hedefGoruntu;

            // buradaki kontrol,şayet bir resim zoom yapılmışsa,o zoom yapılan resimi döndürme işlemini gerçekleştirmesi için yapılmıştır.
            if (dondur != 0)
            {

                orjinalGoruntu = new Bitmap(pct_hedefGoruntu.Image);
                genislik = orjinalGoruntu.Width;
                yukseklik = orjinalGoruntu.Height;
                hedefGoruntu = new Bitmap(yukseklik, genislik);

            }
            else
            {
                orjinalGoruntu = new Bitmap(pct_orjinalGoruntu.Image);
                genislik = pct_orjinalGoruntu.Image.Width;
                yukseklik = pct_orjinalGoruntu.Image.Height;
                hedefGoruntu = new Bitmap(yukseklik, genislik);
                dondur++;
            }

            //// 0 seçili = 90 derece sola döndürme işlemi

            if (cmb_Rotation.SelectedIndex == 0)
            {

                if (pct_orjinalGoruntu.Image == null) return;

                if (pct_hedefGoruntu.Image != null)
                {
                    pct_hedefGoruntu.Image = null;
                    pct_hedefGoruntu.Refresh();
                }
                for (int i = 0; i < genislik; i++)
                {
                    for (int j = 0; j < yukseklik; j++)
                    {
                        hedefGoruntu.SetPixel(j, hedefGoruntu.Height - 1 - i, orjinalGoruntu.GetPixel(i, j));
                    }
                }

                pct_hedefGoruntu.Image = hedefGoruntu;
            }

            //// 1 seçili = 180 derece sola döndürme işlemi
            if (cmb_Rotation.SelectedIndex == 1)
            {
                if (pct_orjinalGoruntu.Image == null) return;

                if (pct_hedefGoruntu.Image != null)
                {
                    pct_hedefGoruntu.Image = null;
                    pct_hedefGoruntu.Refresh();
                }


                for (int i = 0; i < genislik; i++)
                {
                    for (int j = 0; j < yukseklik; j++)
                    {
                        hedefGoruntu.SetPixel(j, hedefGoruntu.Height - 1 - i, orjinalGoruntu.GetPixel(i, j));
                    }
                }
                pct_hedefGoruntu.Image = hedefGoruntu;

                int genislik2 = pct_hedefGoruntu.Image.Width;
                int yukseklik2 = pct_hedefGoruntu.Image.Height;
                Bitmap orjinalGoruntu2 = new Bitmap(pct_hedefGoruntu.Image);
                Bitmap hedefGoruntu2 = new Bitmap(yukseklik2, genislik2);
                for (int i = 0; i < genislik2; i++)
                {
                    for (int j = 0; j < yukseklik2; j++)
                    {
                        hedefGoruntu2.SetPixel(j, hedefGoruntu2.Height - 1 - i, orjinalGoruntu2.GetPixel(i, j));
                    }
                }



                pct_hedefGoruntu.Image = hedefGoruntu2;
            }

            //// 2 seçili = 270 derece sola döndürme işlemi 
            
            if (cmb_Rotation.SelectedIndex == 2)
            {
                if (pct_orjinalGoruntu.Image == null) return;

                if (pct_hedefGoruntu.Image != null)
                {
                    pct_hedefGoruntu.Image = null;
                    pct_hedefGoruntu.Refresh();
                }


                for (int i = 0; i < genislik; i++)
                {
                    for (int j = 0; j < yukseklik; j++)
                    {
                        hedefGoruntu.SetPixel(hedefGoruntu.Width - 1 - j, hedefGoruntu.Height - 1 - i, orjinalGoruntu.GetPixel(i, j));
                    }
                }


                pct_hedefGoruntu.Image = hedefGoruntu;
            }
        } /////**** SOLA DONDURME KOD BLOĞU BITIŞ NOKTASI.****/////



        //////////   SAGA DONDÜRME(ROTATION) KOD BLOĞU

        private void btnSag_Click(object sender, EventArgs e)
        {
            int genislik = 0;
            int yukseklik = 0;
            Bitmap orjinalGoruntu;
            Bitmap hedefGoruntu;

           // buradaki kontrol,şayet bir resim zoom yapılmışsa,o zoom yapılan resimi döndürme işlemini gerçekleştirmesi için yapılmıştır.
            if (dondur != 0)
            {

                orjinalGoruntu = new Bitmap(pct_hedefGoruntu.Image);
                genislik = orjinalGoruntu.Width;
                yukseklik = orjinalGoruntu.Height;
                hedefGoruntu = new Bitmap(yukseklik, genislik);

            }
            else
            {
                orjinalGoruntu = new Bitmap(pct_orjinalGoruntu.Image);
                genislik = pct_orjinalGoruntu.Image.Width;
                yukseklik = pct_orjinalGoruntu.Image.Height;
                hedefGoruntu = new Bitmap(yukseklik, genislik);
                dondur++;
            }


            //// 0 seçili = 90 derece sağa döndürme işlemi

            {
                if (cmb_Rotation.SelectedIndex == 0)
                {
                    if (pct_orjinalGoruntu.Image == null) return;

                    if (pct_hedefGoruntu.Image != null)
                    {
                        pct_hedefGoruntu.Image = null;
                        pct_hedefGoruntu.Refresh();
                    }
                    for (int i = 0; i < genislik; i++)
                    {
                        for (int j = 0; j < yukseklik; j++)
                        {
                            hedefGoruntu.SetPixel(hedefGoruntu.Width - j - 1, i, orjinalGoruntu.GetPixel(i, j));
                        }
                    }

                    pct_hedefGoruntu.Image = hedefGoruntu;
                }

                //// 1 seçili = 180 derece sağa döndürme işlemi
                if (cmb_Rotation.SelectedIndex == 1)
                {
                    if (pct_orjinalGoruntu.Image == null) return;

                    if (pct_hedefGoruntu.Image != null)
                    {
                        pct_hedefGoruntu.Image = null;
                        pct_hedefGoruntu.Refresh();
                    }

                    for (int i = 0; i < genislik; i++)
                    {
                        for (int j = 0; j < yukseklik; j++)
                        {
                            hedefGoruntu.SetPixel(j, hedefGoruntu.Height - 1 - i, orjinalGoruntu.GetPixel(i, j));
                        }
                    }
                    pct_hedefGoruntu.Image = hedefGoruntu;

                    int genislik2 = pct_hedefGoruntu.Image.Width;
                    int yukseklik2 = pct_hedefGoruntu.Image.Height;
                    Bitmap orjinalGoruntu2 = new Bitmap(pct_hedefGoruntu.Image);
                    Bitmap hedefGoruntu2 = new Bitmap(yukseklik2, genislik2);
                    for (int i = 0; i < genislik2; i++)
                    {
                        for (int j = 0; j < yukseklik2; j++)
                        {
                            hedefGoruntu2.SetPixel(j, hedefGoruntu2.Height - 1 - i, orjinalGoruntu2.GetPixel(i, j));
                        }
                    }
                    pct_hedefGoruntu.Image = hedefGoruntu2;
                }

                //// 2 seçili = 270 derece sağa döndürme işlemi

                if (cmb_Rotation.SelectedIndex == 2)
                {
                    if (pct_orjinalGoruntu.Image == null) return;

                    if (pct_hedefGoruntu.Image != null)
                    {
                        pct_hedefGoruntu.Image = null;
                        pct_hedefGoruntu.Refresh();
                    }

                    for (int i = 0; i < genislik; i++)
                    {
                        for (int j = 0; j < yukseklik; j++)
                        {
                            hedefGoruntu.SetPixel(j, hedefGoruntu.Height - 1 - i, orjinalGoruntu.GetPixel(i, j));
                        }
                    }


                    pct_hedefGoruntu.Image = hedefGoruntu;
                }
            }
        }  /////**** SAĞA DONDURME KOD BLOĞU BITIŞ NOKTASI.****/////
         



        ////////   BRESHENHAM ÇİZGİ ALGORİTMASI KOD BLOĞU

        private void BresenhamCizgiAlgoritmasi(Point from, Point to)
        {

            Bitmap p = new Bitmap(pct_hedefGoruntu.Width, pct_hedefGoruntu.Height);
            double deltaX = to.X - from.X;
            double deltaY = to.Y - from.Y;
            double deltaErr = Math.Abs(deltaY / deltaX);
            double error = deltaErr - 0.5;
            int y = from.Y;

            for (int x = from.X; x <= to.X; x++)
            {
                p.SetPixel(x, y, Color.Green);
                error += deltaErr;
                if (error >= 0.5)
                {
                    y += 1;
                    error -= 1.0;
                }
            }

            pct_hedefGoruntu.Image = p;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Point from = new Point(0, 0);
            Point to = new Point(pct_hedefGoruntu.Width - 1, pct_hedefGoruntu.Height - 1);
            BresenhamCizgiAlgoritmasi(from, to);
        }  /////**** BRESENHAM ÇIZGI ALGORITMASI KOD BLOĞU BITIŞ NOKTASI.****/////

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lbl_genislik_Click(object sender, EventArgs e)
        {

        }

        private void hedef_fareHareket( object sender, MouseEventArgs e ) {
			if ( pct_hedefGoruntu.SizeMode == PictureBoxSizeMode.Zoom ) return;
			Control c = ( Control )sender;
			if ( hedef_tasiniyor && c != null ) {
				hedef_bitisNoktasi = new Point( e.Location.X - hedef_baslangicNoktasi.X, e.Location.Y - hedef_baslangicNoktasi.Y );
				pct_hedefGoruntu.Invalidate();
			}
		}

		private void hedef_goruntuCiziliyor( object sender, PaintEventArgs e ) {
			if ( pct_hedefGoruntu.SizeMode == PictureBoxSizeMode.Zoom ) return;
			if ( pct_hedefGoruntu.Image == null || !hedef_tasiniyor ) return;

			e.Graphics.Clear( Color.White );
			e.Graphics.DrawImage( pct_hedefGoruntu.Image, hedef_bitisNoktasi );
		}

		private void btn_cevirYavas_Click( object sender, EventArgs e ) {
			if ( pct_orjinalGoruntu.Image == null ) return;

			if ( pct_hedefGoruntu.Image != null ) {
				pct_hedefGoruntu.Image = null;
				pct_hedefGoruntu.Refresh();
			}

			int genislik = pct_orjinalGoruntu.Image.Width;
			int yukseklik = pct_orjinalGoruntu.Image.Height;

			Bitmap orjinalGoruntu = ( Bitmap )pct_orjinalGoruntu.Image;
			Bitmap hedefGoruntu = new Bitmap( genislik, yukseklik );

			Stopwatch olcer = new Stopwatch();
			olcer.Start();
			prg_islem.Value = 0;
			prg_islem.Maximum = genislik;

			for ( int sutun = 0; sutun < genislik; sutun++ ) {
				for ( int satir = 0; satir < yukseklik; satir++ ) {
					Color orjinalPiksel = orjinalGoruntu.GetPixel( sutun, satir );
					int griRenk = ( int )( orjinalPiksel.R * 0.3f + orjinalPiksel.G * 0.59f + orjinalPiksel.B * 0.11f );
					Color hedefPiksel = Color.FromArgb( griRenk, griRenk, griRenk );

					hedefGoruntu.SetPixel( sutun, satir, hedefPiksel );
				}

				prg_islem.Value++;
			}

			olcer.Stop();
			lbl_gecenSure.Text = "Geçen Süre: " + olcer.Elapsed.Seconds + " sn " + olcer.Elapsed.Milliseconds + " ms";
			pct_hedefGoruntu.Image = hedefGoruntu;
		}

		private void btn_griyeCevirHizli_Click( object sender, EventArgs e ) {
			if ( pct_orjinalGoruntu.Image == null ) return;

			if ( pct_hedefGoruntu.Image != null ) {
				pct_hedefGoruntu.Image = null;
				pct_hedefGoruntu.Refresh();
			}

			int genislik = pct_orjinalGoruntu.Image.Width;
			int yukseklik = pct_orjinalGoruntu.Image.Height;

			Bitmap orjinalGoruntu = ( Bitmap )pct_orjinalGoruntu.Image;
			BitmapData orjinalGoruntuVerisi = orjinalGoruntu.LockBits(
				new Rectangle( 0, 0, genislik, yukseklik ),
				ImageLockMode.ReadOnly,
				PixelFormat.Format24bppRgb
			);
			IntPtr ptr_orjinalGoruntu = orjinalGoruntuVerisi.Scan0;
			int orjinalGoruntuBoyutu = orjinalGoruntuVerisi.Stride * yukseklik;
			byte[] byte_orjinalGoruntu = new byte[orjinalGoruntuBoyutu];
			Marshal.Copy(
				ptr_orjinalGoruntu,
				byte_orjinalGoruntu,
				0,
				orjinalGoruntuBoyutu
			);

			Bitmap hedefGoruntu = new Bitmap( genislik,
				yukseklik,
				PixelFormat.Format8bppIndexed
			);
			BitmapData hedefGoruntuVerisi = hedefGoruntu.LockBits(
				new Rectangle( 0, 0, genislik, yukseklik ),
				ImageLockMode.WriteOnly,
				PixelFormat.Format8bppIndexed
			);
			IntPtr ptr_hedefGoruntu = hedefGoruntuVerisi.Scan0;
			int hedefGoruntuBoyutu = hedefGoruntuVerisi.Stride * yukseklik;
			byte[] byte_hedefGoruntu = new byte[hedefGoruntuBoyutu];
			ColorPalette griRenkPaleti = hedefGoruntu.Palette;
			for ( int i = 0; i < 255; i++ ) {
				griRenkPaleti.Entries[ i ] = Color.FromArgb( i, i, i );
			}

			hedefGoruntu.Palette = griRenkPaleti;

			Stopwatch olcer = new Stopwatch();
			olcer.Start();
			prg_islem.Value = 0;
			prg_islem.Maximum = genislik;

			for ( int i = 0, j = 0; i < hedefGoruntuBoyutu; i++, j += 3 ) {
				byte_hedefGoruntu[ i ] = ( byte )(
					byte_orjinalGoruntu[ j ] * 0.3f +
					byte_orjinalGoruntu[ j + 1 ] * 0.58f +
					byte_orjinalGoruntu[ j + 2 ] * 0.11f
				);

				prg_islem.Value = ( int )i / yukseklik;
			}

			Marshal.Copy(
				byte_hedefGoruntu,
				0,
				ptr_hedefGoruntu,
				hedefGoruntuBoyutu
			);

			hedefGoruntu.UnlockBits( hedefGoruntuVerisi );
			orjinalGoruntu.UnlockBits( orjinalGoruntuVerisi );

			olcer.Stop();
			lbl_gecenSure.Text = "Geçen Süre: " + olcer.Elapsed.Seconds + " sn " + olcer.Elapsed.Milliseconds + " ms";
			pct_hedefGoruntu.Image = hedefGoruntu;
		}
	}
}

// 17-155-014 HAKKI EGEMEN GÜLPINAR 
// BİLGİSAYAR MÜHENDİSLİĞİ 3.SINIF
// COMPUTER GRAPHICS PROJE