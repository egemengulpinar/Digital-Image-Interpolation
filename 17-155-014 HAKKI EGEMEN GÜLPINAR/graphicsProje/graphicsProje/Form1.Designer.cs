namespace graphicProje {
	partial class goruntuIsleme {
		/// <summary>
		///Gerekli tasarımcı değişkeni.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///Kullanılan tüm kaynakları temizleyin.
		/// </summary>
		///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && ( components != null )) {
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer üretilen kod

		/// <summary>
		/// Tasarımcı desteği için gerekli metot - bu metodun 
		///içeriğini kod düzenleyici ile değiştirmeyin.
		/// </summary>
		private void InitializeComponent() {
            this.pct_orjinalGoruntu = new System.Windows.Forms.PictureBox();
            this.pct_hedefGoruntu = new System.Windows.Forms.PictureBox();
            this.btn_griyeCevirYavas = new System.Windows.Forms.Button();
            this.btn_griyeCevirHizli = new System.Windows.Forms.Button();
            this.lbl_genislik = new System.Windows.Forms.Label();
            this.lbl_yukseklik = new System.Windows.Forms.Label();
            this.lbl_renkDerinligi = new System.Windows.Forms.Label();
            this.prg_islem = new System.Windows.Forms.ProgressBar();
            this.lbl_gecenSure = new System.Windows.Forms.Label();
            this.cmb_zoomMetot = new System.Windows.Forms.ComboBox();
            this.btn_yakinlas = new System.Windows.Forms.Button();
            this.btn_uzaklas = new System.Windows.Forms.Button();
            this.btn_orjinalGoruntu_orjinalBoyut = new System.Windows.Forms.Button();
            this.btn_orjinalGoruntu_sigdir = new System.Windows.Forms.Button();
            this.btn_hedefGoruntu_sigdir = new System.Windows.Forms.Button();
            this.btn_hedefGoruntu_orjinalBoyut = new System.Windows.Forms.Button();
            this.btnSol = new System.Windows.Forms.Button();
            this.btnSag = new System.Windows.Forms.Button();
            this.cmb_Rotation = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pct_orjinalGoruntu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pct_hedefGoruntu)).BeginInit();
            this.SuspendLayout();
            // 
            // pct_orjinalGoruntu
            // 
            this.pct_orjinalGoruntu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pct_orjinalGoruntu.Location = new System.Drawing.Point(30, 54);
            this.pct_orjinalGoruntu.Name = "pct_orjinalGoruntu";
            this.pct_orjinalGoruntu.Size = new System.Drawing.Size(384, 319);
            this.pct_orjinalGoruntu.TabIndex = 0;
            this.pct_orjinalGoruntu.TabStop = false;
            this.pct_orjinalGoruntu.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.resimYuklendi);
            this.pct_orjinalGoruntu.Paint += new System.Windows.Forms.PaintEventHandler(this.orjinal_goruntuCiziliyor);
            this.pct_orjinalGoruntu.DoubleClick += new System.EventHandler(this.goruntuAc);
            this.pct_orjinalGoruntu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.orjinal_fareBasildi);
            this.pct_orjinalGoruntu.MouseMove += new System.Windows.Forms.MouseEventHandler(this.orjinal_fareHareket);
            this.pct_orjinalGoruntu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.orjinal_fareBirakildi);
            // 
            // pct_hedefGoruntu
            // 
            this.pct_hedefGoruntu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pct_hedefGoruntu.Location = new System.Drawing.Point(621, 54);
            this.pct_hedefGoruntu.Name = "pct_hedefGoruntu";
            this.pct_hedefGoruntu.Size = new System.Drawing.Size(384, 319);
            this.pct_hedefGoruntu.TabIndex = 1;
            this.pct_hedefGoruntu.TabStop = false;
            this.pct_hedefGoruntu.Paint += new System.Windows.Forms.PaintEventHandler(this.hedef_goruntuCiziliyor);
            this.pct_hedefGoruntu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.hedef_fareBasildi);
            this.pct_hedefGoruntu.MouseMove += new System.Windows.Forms.MouseEventHandler(this.hedef_fareHareket);
            this.pct_hedefGoruntu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.hedef_fareBirakildi);
            // 
            // btn_griyeCevirYavas
            // 
            this.btn_griyeCevirYavas.Location = new System.Drawing.Point(477, 79);
            this.btn_griyeCevirYavas.Name = "btn_griyeCevirYavas";
            this.btn_griyeCevirYavas.Size = new System.Drawing.Size(75, 43);
            this.btn_griyeCevirYavas.TabIndex = 2;
            this.btn_griyeCevirYavas.Text = "Scale to Gray(Slow)";
            this.btn_griyeCevirYavas.UseVisualStyleBackColor = true;
            this.btn_griyeCevirYavas.Click += new System.EventHandler(this.btn_cevirYavas_Click);
            // 
            // btn_griyeCevirHizli
            // 
            this.btn_griyeCevirHizli.Location = new System.Drawing.Point(480, 141);
            this.btn_griyeCevirHizli.Name = "btn_griyeCevirHizli";
            this.btn_griyeCevirHizli.Size = new System.Drawing.Size(75, 43);
            this.btn_griyeCevirHizli.TabIndex = 3;
            this.btn_griyeCevirHizli.Text = "Scale to Gray(Fast)";
            this.btn_griyeCevirHizli.UseVisualStyleBackColor = true;
            this.btn_griyeCevirHizli.Click += new System.EventHandler(this.btn_griyeCevirHizli_Click);
            // 
            // lbl_genislik
            // 
            this.lbl_genislik.AutoSize = true;
            this.lbl_genislik.Location = new System.Drawing.Point(36, 382);
            this.lbl_genislik.Name = "lbl_genislik";
            this.lbl_genislik.Size = new System.Drawing.Size(35, 13);
            this.lbl_genislik.TabIndex = 4;
            this.lbl_genislik.Text = "Width";
            this.lbl_genislik.Click += new System.EventHandler(this.lbl_genislik_Click);
            // 
            // lbl_yukseklik
            // 
            this.lbl_yukseklik.AutoSize = true;
            this.lbl_yukseklik.Location = new System.Drawing.Point(36, 406);
            this.lbl_yukseklik.Name = "lbl_yukseklik";
            this.lbl_yukseklik.Size = new System.Drawing.Size(38, 13);
            this.lbl_yukseklik.TabIndex = 5;
            this.lbl_yukseklik.Text = "Height";
            // 
            // lbl_renkDerinligi
            // 
            this.lbl_renkDerinligi.AutoSize = true;
            this.lbl_renkDerinligi.Location = new System.Drawing.Point(36, 433);
            this.lbl_renkDerinligi.Name = "lbl_renkDerinligi";
            this.lbl_renkDerinligi.Size = new System.Drawing.Size(63, 13);
            this.lbl_renkDerinligi.TabIndex = 6;
            this.lbl_renkDerinligi.Text = "Color Depth";
            // 
            // prg_islem
            // 
            this.prg_islem.Location = new System.Drawing.Point(431, 396);
            this.prg_islem.Name = "prg_islem";
            this.prg_islem.Size = new System.Drawing.Size(171, 23);
            this.prg_islem.TabIndex = 7;
            // 
            // lbl_gecenSure
            // 
            this.lbl_gecenSure.AutoSize = true;
            this.lbl_gecenSure.Location = new System.Drawing.Point(428, 433);
            this.lbl_gecenSure.Name = "lbl_gecenSure";
            this.lbl_gecenSure.Size = new System.Drawing.Size(71, 13);
            this.lbl_gecenSure.TabIndex = 8;
            this.lbl_gecenSure.Text = "Process Time";
            // 
            // cmb_zoomMetot
            // 
            this.cmb_zoomMetot.FormattingEnabled = true;
            this.cmb_zoomMetot.Items.AddRange(new object[] {
            "0-Order Hold",
            " 1-Order Hold",
            "K-Times  Zoom",
            "Bilinear"});
            this.cmb_zoomMetot.Location = new System.Drawing.Point(452, 211);
            this.cmb_zoomMetot.Name = "cmb_zoomMetot";
            this.cmb_zoomMetot.Size = new System.Drawing.Size(121, 21);
            this.cmb_zoomMetot.TabIndex = 9;
            this.cmb_zoomMetot.SelectedIndexChanged += new System.EventHandler(this.cmb_zoomMetot_SelectedIndexChanged);
            // 
            // btn_yakinlas
            // 
            this.btn_yakinlas.Location = new System.Drawing.Point(518, 239);
            this.btn_yakinlas.Name = "btn_yakinlas";
            this.btn_yakinlas.Size = new System.Drawing.Size(38, 23);
            this.btn_yakinlas.TabIndex = 10;
            this.btn_yakinlas.Text = "+";
            this.btn_yakinlas.UseVisualStyleBackColor = true;
            this.btn_yakinlas.Click += new System.EventHandler(this.btn_yakinlas_Click);
            // 
            // btn_uzaklas
            // 
            this.btn_uzaklas.Location = new System.Drawing.Point(470, 239);
            this.btn_uzaklas.Name = "btn_uzaklas";
            this.btn_uzaklas.Size = new System.Drawing.Size(38, 23);
            this.btn_uzaklas.TabIndex = 11;
            this.btn_uzaklas.Text = "-";
            this.btn_uzaklas.UseVisualStyleBackColor = true;
            this.btn_uzaklas.Click += new System.EventHandler(this.btn_uzaklas_Click);
            // 
            // btn_orjinalGoruntu_orjinalBoyut
            // 
            this.btn_orjinalGoruntu_orjinalBoyut.Location = new System.Drawing.Point(101, 11);
            this.btn_orjinalGoruntu_orjinalBoyut.Name = "btn_orjinalGoruntu_orjinalBoyut";
            this.btn_orjinalGoruntu_orjinalBoyut.Size = new System.Drawing.Size(75, 35);
            this.btn_orjinalGoruntu_orjinalBoyut.TabIndex = 12;
            this.btn_orjinalGoruntu_orjinalBoyut.Text = "Original Size";
            this.btn_orjinalGoruntu_orjinalBoyut.UseVisualStyleBackColor = true;
            this.btn_orjinalGoruntu_orjinalBoyut.Click += new System.EventHandler(this.btn_orjinalGoruntu_orjinalBoyut_Click);
            // 
            // btn_orjinalGoruntu_sigdir
            // 
            this.btn_orjinalGoruntu_sigdir.Location = new System.Drawing.Point(219, 11);
            this.btn_orjinalGoruntu_sigdir.Name = "btn_orjinalGoruntu_sigdir";
            this.btn_orjinalGoruntu_sigdir.Size = new System.Drawing.Size(75, 35);
            this.btn_orjinalGoruntu_sigdir.TabIndex = 13;
            this.btn_orjinalGoruntu_sigdir.Text = "Fit Image";
            this.btn_orjinalGoruntu_sigdir.UseVisualStyleBackColor = true;
            this.btn_orjinalGoruntu_sigdir.Click += new System.EventHandler(this.btn_orjinalGoruntu_sigdir_Click);
            // 
            // btn_hedefGoruntu_sigdir
            // 
            this.btn_hedefGoruntu_sigdir.Location = new System.Drawing.Point(837, 11);
            this.btn_hedefGoruntu_sigdir.Name = "btn_hedefGoruntu_sigdir";
            this.btn_hedefGoruntu_sigdir.Size = new System.Drawing.Size(75, 35);
            this.btn_hedefGoruntu_sigdir.TabIndex = 15;
            this.btn_hedefGoruntu_sigdir.Text = "Fit Image";
            this.btn_hedefGoruntu_sigdir.UseVisualStyleBackColor = true;
            this.btn_hedefGoruntu_sigdir.Click += new System.EventHandler(this.btn_hedefGoruntu_sigdir_Click);
            // 
            // btn_hedefGoruntu_orjinalBoyut
            // 
            this.btn_hedefGoruntu_orjinalBoyut.Location = new System.Drawing.Point(719, 11);
            this.btn_hedefGoruntu_orjinalBoyut.Name = "btn_hedefGoruntu_orjinalBoyut";
            this.btn_hedefGoruntu_orjinalBoyut.Size = new System.Drawing.Size(75, 35);
            this.btn_hedefGoruntu_orjinalBoyut.TabIndex = 14;
            this.btn_hedefGoruntu_orjinalBoyut.Text = "Original Size";
            this.btn_hedefGoruntu_orjinalBoyut.UseVisualStyleBackColor = true;
            this.btn_hedefGoruntu_orjinalBoyut.Click += new System.EventHandler(this.btn_hedefGoruntu_orjinalBoyut_Click);
            // 
            // btnSol
            // 
            this.btnSol.Location = new System.Drawing.Point(470, 318);
            this.btnSol.Name = "btnSol";
            this.btnSol.Size = new System.Drawing.Size(38, 23);
            this.btnSol.TabIndex = 18;
            this.btnSol.Text = "Left";
            this.btnSol.UseVisualStyleBackColor = true;
            this.btnSol.Click += new System.EventHandler(this.btnSol_Click);
            // 
            // btnSag
            // 
            this.btnSag.Location = new System.Drawing.Point(518, 318);
            this.btnSag.Name = "btnSag";
            this.btnSag.Size = new System.Drawing.Size(38, 23);
            this.btnSag.TabIndex = 17;
            this.btnSag.Text = "Right";
            this.btnSag.UseVisualStyleBackColor = true;
            this.btnSag.Click += new System.EventHandler(this.btnSag_Click);
            // 
            // cmb_Rotation
            // 
            this.cmb_Rotation.FormattingEnabled = true;
            this.cmb_Rotation.Items.AddRange(new object[] {
            "90°",
            "180°",
            "270°"});
            this.cmb_Rotation.Location = new System.Drawing.Point(452, 291);
            this.cmb_Rotation.Name = "cmb_Rotation";
            this.cmb_Rotation.Size = new System.Drawing.Size(121, 21);
            this.cmb_Rotation.TabIndex = 16;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(701, 379);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 69);
            this.button1.TabIndex = 19;
            this.button1.Text = "Bresenham Line Algorithm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(837, 379);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 69);
            this.button2.TabIndex = 20;
            this.button2.Text = "DDA Line Algorithm";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // goruntuIsleme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 473);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSol);
            this.Controls.Add(this.btnSag);
            this.Controls.Add(this.cmb_Rotation);
            this.Controls.Add(this.btn_hedefGoruntu_sigdir);
            this.Controls.Add(this.btn_hedefGoruntu_orjinalBoyut);
            this.Controls.Add(this.btn_orjinalGoruntu_sigdir);
            this.Controls.Add(this.btn_orjinalGoruntu_orjinalBoyut);
            this.Controls.Add(this.btn_uzaklas);
            this.Controls.Add(this.btn_yakinlas);
            this.Controls.Add(this.cmb_zoomMetot);
            this.Controls.Add(this.lbl_gecenSure);
            this.Controls.Add(this.prg_islem);
            this.Controls.Add(this.lbl_renkDerinligi);
            this.Controls.Add(this.lbl_yukseklik);
            this.Controls.Add(this.lbl_genislik);
            this.Controls.Add(this.btn_griyeCevirHizli);
            this.Controls.Add(this.btn_griyeCevirYavas);
            this.Controls.Add(this.pct_hedefGoruntu);
            this.Controls.Add(this.pct_orjinalGoruntu);
            this.MaximizeBox = false;
            this.Name = "goruntuIsleme";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Interpolation";
            this.Load += new System.EventHandler(this.goruntuIsleme_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pct_orjinalGoruntu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pct_hedefGoruntu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pct_orjinalGoruntu;
		private System.Windows.Forms.PictureBox pct_hedefGoruntu;
		private System.Windows.Forms.Button btn_griyeCevirYavas;
		private System.Windows.Forms.Button btn_griyeCevirHizli;
		private System.Windows.Forms.Label lbl_genislik;
		private System.Windows.Forms.Label lbl_yukseklik;
		private System.Windows.Forms.Label lbl_renkDerinligi;
		private System.Windows.Forms.ProgressBar prg_islem;
		private System.Windows.Forms.Label lbl_gecenSure;
		private System.Windows.Forms.ComboBox cmb_zoomMetot;
		private System.Windows.Forms.Button btn_yakinlas;
		private System.Windows.Forms.Button btn_uzaklas;
		private System.Windows.Forms.Button btn_orjinalGoruntu_orjinalBoyut;
		private System.Windows.Forms.Button btn_orjinalGoruntu_sigdir;
		private System.Windows.Forms.Button btn_hedefGoruntu_sigdir;
		private System.Windows.Forms.Button btn_hedefGoruntu_orjinalBoyut;
        private System.Windows.Forms.Button btnSol;
        private System.Windows.Forms.Button btnSag;
        private System.Windows.Forms.ComboBox cmb_Rotation;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

