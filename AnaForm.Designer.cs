namespace SBFileEncryptorDecryptor
{
    partial class AnaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();

                IlerlemePaneliFontu.Dispose(); // FIX: global font dispose
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnaForm));
            this.TLPGenel = new System.Windows.Forms.TableLayoutPanel();
            this.ImagelistAnaFormIcin = new System.Windows.Forms.ImageList(this.components);
            this.PanelIlerlemeCubugu = new System.Windows.Forms.Panel();
            this.SBButtonSifrele = new SBCustomControls.SBControls.SBButton();
            this.SBButtonCoz = new SBCustomControls.SBControls.SBButton();
            this.SBButtonIptal = new SBCustomControls.SBControls.SBButton();
            this.ImageListOzelFormIcin = new System.Windows.Forms.ImageList(this.components);
            this.SBButtonHakkinda = new SBCustomControls.SBControls.SBButton();
            this.TLPGenel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TLPGenel
            // 
            this.TLPGenel.BackColor = System.Drawing.Color.Gainsboro;
            this.TLPGenel.ColumnCount = 1;
            this.TLPGenel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLPGenel.Controls.Add(this.PanelIlerlemeCubugu, 0, 3);
            this.TLPGenel.Controls.Add(this.SBButtonSifrele, 0, 0);
            this.TLPGenel.Controls.Add(this.SBButtonCoz, 0, 1);
            this.TLPGenel.Controls.Add(this.SBButtonIptal, 0, 2);
            this.TLPGenel.Controls.Add(this.SBButtonHakkinda, 0, 4);
            this.TLPGenel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLPGenel.Location = new System.Drawing.Point(0, 0);
            this.TLPGenel.Name = "TLPGenel";
            this.TLPGenel.RowCount = 5;
            this.TLPGenel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TLPGenel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TLPGenel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TLPGenel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TLPGenel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TLPGenel.Size = new System.Drawing.Size(880, 334);
            this.TLPGenel.TabIndex = 0;
            // 
            // ImagelistAnaFormIcin
            // 
            this.ImagelistAnaFormIcin.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImagelistAnaFormIcin.ImageStream")));
            this.ImagelistAnaFormIcin.TransparentColor = System.Drawing.Color.Transparent;
            this.ImagelistAnaFormIcin.Images.SetKeyName(0, "islemiptal.png");
            this.ImagelistAnaFormIcin.Images.SetKeyName(1, "koru.png");
            this.ImagelistAnaFormIcin.Images.SetKeyName(2, "coz.png");
            this.ImagelistAnaFormIcin.Images.SetKeyName(3, "hakkinda.png");
            // 
            // PanelIlerlemeCubugu
            // 
            this.PanelIlerlemeCubugu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelIlerlemeCubugu.Location = new System.Drawing.Point(3, 201);
            this.PanelIlerlemeCubugu.Name = "PanelIlerlemeCubugu";
            this.PanelIlerlemeCubugu.Size = new System.Drawing.Size(874, 60);
            this.PanelIlerlemeCubugu.TabIndex = 4;
            this.PanelIlerlemeCubugu.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelIlerlemeCubugu_Paint);
            // 
            // SBButtonSifrele
            // 
            this.SBButtonSifrele.ArkaplanRengi = System.Drawing.Color.DarkGreen;
            this.SBButtonSifrele.BackColor = System.Drawing.Color.DarkGreen;
            this.SBButtonSifrele.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SBButtonSifrele.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SBButtonSifrele.FlatAppearance.BorderSize = 0;
            this.SBButtonSifrele.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SBButtonSifrele.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold);
            this.SBButtonSifrele.ForeColor = System.Drawing.Color.White;
            this.SBButtonSifrele.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SBButtonSifrele.ImageIndex = 1;
            this.SBButtonSifrele.ImageList = this.ImagelistAnaFormIcin;
            this.SBButtonSifrele.KenarBoyutu = 0;
            this.SBButtonSifrele.KenarlikRenk = System.Drawing.Color.DarkGreen;
            this.SBButtonSifrele.Location = new System.Drawing.Point(3, 3);
            this.SBButtonSifrele.Name = "SBButtonSifrele";
            this.SBButtonSifrele.Size = new System.Drawing.Size(874, 60);
            this.SBButtonSifrele.TabIndex = 6;
            this.SBButtonSifrele.Text = "Encrypt the File...";
            this.SBButtonSifrele.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SBButtonSifrele.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SBButtonSifrele.UseVisualStyleBackColor = false;
            this.SBButtonSifrele.YaziRengi = System.Drawing.Color.White;
            this.SBButtonSifrele.YuvarlakKoseler = 25;
            this.SBButtonSifrele.Click += new System.EventHandler(this.SBButtonSifrele_Click);
            // 
            // SBButtonCoz
            // 
            this.SBButtonCoz.ArkaplanRengi = System.Drawing.Color.SaddleBrown;
            this.SBButtonCoz.BackColor = System.Drawing.Color.SaddleBrown;
            this.SBButtonCoz.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SBButtonCoz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SBButtonCoz.FlatAppearance.BorderSize = 0;
            this.SBButtonCoz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SBButtonCoz.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold);
            this.SBButtonCoz.ForeColor = System.Drawing.Color.White;
            this.SBButtonCoz.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SBButtonCoz.ImageIndex = 2;
            this.SBButtonCoz.ImageList = this.ImagelistAnaFormIcin;
            this.SBButtonCoz.KenarBoyutu = 0;
            this.SBButtonCoz.KenarlikRenk = System.Drawing.Color.DarkGreen;
            this.SBButtonCoz.Location = new System.Drawing.Point(3, 69);
            this.SBButtonCoz.Name = "SBButtonCoz";
            this.SBButtonCoz.Size = new System.Drawing.Size(874, 60);
            this.SBButtonCoz.TabIndex = 7;
            this.SBButtonCoz.Text = "Decrypt the File...";
            this.SBButtonCoz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SBButtonCoz.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SBButtonCoz.UseVisualStyleBackColor = false;
            this.SBButtonCoz.YaziRengi = System.Drawing.Color.White;
            this.SBButtonCoz.YuvarlakKoseler = 25;
            this.SBButtonCoz.Click += new System.EventHandler(this.SBButtonCoz_Click);
            // 
            // SBButtonIptal
            // 
            this.SBButtonIptal.ArkaplanRengi = System.Drawing.Color.Brown;
            this.SBButtonIptal.BackColor = System.Drawing.Color.Brown;
            this.SBButtonIptal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SBButtonIptal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SBButtonIptal.FlatAppearance.BorderSize = 0;
            this.SBButtonIptal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SBButtonIptal.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold);
            this.SBButtonIptal.ForeColor = System.Drawing.Color.White;
            this.SBButtonIptal.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SBButtonIptal.ImageIndex = 0;
            this.SBButtonIptal.ImageList = this.ImagelistAnaFormIcin;
            this.SBButtonIptal.KenarBoyutu = 0;
            this.SBButtonIptal.KenarlikRenk = System.Drawing.Color.DarkGreen;
            this.SBButtonIptal.Location = new System.Drawing.Point(3, 135);
            this.SBButtonIptal.Name = "SBButtonIptal";
            this.SBButtonIptal.Size = new System.Drawing.Size(874, 60);
            this.SBButtonIptal.TabIndex = 8;
            this.SBButtonIptal.Text = "Cancel the Operation";
            this.SBButtonIptal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SBButtonIptal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SBButtonIptal.UseVisualStyleBackColor = false;
            this.SBButtonIptal.YaziRengi = System.Drawing.Color.White;
            this.SBButtonIptal.YuvarlakKoseler = 25;
            this.SBButtonIptal.Click += new System.EventHandler(this.SBButtonIptal_Click);
            // 
            // ImageListOzelFormIcin
            // 
            this.ImageListOzelFormIcin.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListOzelFormIcin.ImageStream")));
            this.ImageListOzelFormIcin.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageListOzelFormIcin.Images.SetKeyName(0, "iptalet.png");
            this.ImageListOzelFormIcin.Images.SetKeyName(1, "onayla.png");
            this.ImageListOzelFormIcin.Images.SetKeyName(2, "goster.png");
            this.ImageListOzelFormIcin.Images.SetKeyName(3, "gizle.png");
            // 
            // SBButtonHakkinda
            // 
            this.SBButtonHakkinda.ArkaplanRengi = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(171)))));
            this.SBButtonHakkinda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(171)))));
            this.SBButtonHakkinda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SBButtonHakkinda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SBButtonHakkinda.FlatAppearance.BorderSize = 0;
            this.SBButtonHakkinda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SBButtonHakkinda.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold);
            this.SBButtonHakkinda.ForeColor = System.Drawing.Color.White;
            this.SBButtonHakkinda.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SBButtonHakkinda.ImageIndex = 3;
            this.SBButtonHakkinda.ImageList = this.ImagelistAnaFormIcin;
            this.SBButtonHakkinda.KenarBoyutu = 0;
            this.SBButtonHakkinda.KenarlikRenk = System.Drawing.Color.DarkGreen;
            this.SBButtonHakkinda.Location = new System.Drawing.Point(3, 267);
            this.SBButtonHakkinda.Name = "SBButtonHakkinda";
            this.SBButtonHakkinda.Size = new System.Drawing.Size(874, 64);
            this.SBButtonHakkinda.TabIndex = 9;
            this.SBButtonHakkinda.Text = "About";
            this.SBButtonHakkinda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SBButtonHakkinda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SBButtonHakkinda.UseVisualStyleBackColor = false;
            this.SBButtonHakkinda.YaziRengi = System.Drawing.Color.White;
            this.SBButtonHakkinda.YuvarlakKoseler = 25;
            this.SBButtonHakkinda.Click += new System.EventHandler(this.SBButtonHakkinda_Click);
            // 
            // AnaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(880, 334);
            this.Controls.Add(this.TLPGenel);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AnaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SB File Encryptor & Decryptor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AnaForm_FormClosing);
            this.Load += new System.EventHandler(this.AnaForm_Load);
            this.TLPGenel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TLPGenel;
        private System.Windows.Forms.Panel PanelIlerlemeCubugu;
        private System.Windows.Forms.ImageList ImageListOzelFormIcin;
        private System.Windows.Forms.ImageList ImagelistAnaFormIcin;
        private SBCustomControls.SBControls.SBButton SBButtonSifrele;
        private SBCustomControls.SBControls.SBButton SBButtonCoz;
        private SBCustomControls.SBControls.SBButton SBButtonIptal;
        private SBCustomControls.SBControls.SBButton SBButtonHakkinda;
    }
}

