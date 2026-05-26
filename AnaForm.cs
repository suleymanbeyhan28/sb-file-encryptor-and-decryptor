using SBFileEncryptorDecryptor.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SBCustomControls.SBControls;

namespace SBFileEncryptorDecryptor
{
    public partial class AnaForm : Form
    {
        #region Variable Declarations & Initializations
        private const string SifrelemeImzasi = "SB_EncryptedFile";

        private int IlerlemeDegeri = 0;
        private Image IlerlemeGorseli;

        private CancellationTokenSource IptalBiletiKaynagi;

        private readonly OpenFileDialog DosyaDiyalogu = new OpenFileDialog();
        private readonly SaveFileDialog KayitDiyalogu = new SaveFileDialog();
        private readonly Font IlerlemePaneliFontu = new Font("Segoe UI Semibold", 20);

        private readonly float OrijinalSifrelemeSatiriYuksekligi;
        private readonly float OrijinalCozmeSatiriYuksekligi;
        private readonly float OrijinalIptalSatiriYuksekligi;
        private readonly float OrijinalIlerlemeSatiriYuksekligi;
        private readonly float OrijinalHakkindaSatiriYuksekligi;

        float Olcek, AyarlanacakImajOrani;
        int ImageListImajlarIcinYeniBoyut, ImageListFormIcinYeniBoyut;

        bool IslemYapiliyormu = false;

        private readonly Snackbar Snackbarim;
        #endregion

        #region Constructor & Initialization
        public AnaForm()
        {
            InitializeComponent();

            Snackbarim = new Snackbar(this);

            PanelIlerlemeCubugu.GetType().GetProperty("DoubleBuffered",System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(PanelIlerlemeCubugu, true, null);

            OrijinalSifrelemeSatiriYuksekligi = TLPGenel.RowStyles[0].Height;
            OrijinalCozmeSatiriYuksekligi = TLPGenel.RowStyles[1].Height;
            OrijinalIptalSatiriYuksekligi = TLPGenel.RowStyles[2].Height;
            OrijinalIlerlemeSatiriYuksekligi = TLPGenel.RowStyles[3].Height;
            OrijinalHakkindaSatiriYuksekligi = TLPGenel.RowStyles[4].Height;

            TLPGenel.RowStyles[2].Height = 0;
            TLPGenel.RowStyles[3].Height = 0;

            PanelIlerlemeCubugu.Visible = false;
            SBButtonIptal.Visible = false;

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        #endregion

        #region Check for Sufficient Disk Space
        private bool YeterliDiskAlaniVarMi(string SecilenSurucu, long GerekenAlan)
        {
            try
            {
                DriveInfo Surucu = new DriveInfo(SecilenSurucu);
                return Surucu.AvailableFreeSpace > GerekenAlan;
            }

            catch
            {
                return false;
            }
        }
        #endregion

        #region Resizing the ImageList
        public static void ImageListiYenidenBoyutlandir(ImageList Imaj, int YeniBoyut)
        {
            var Gorseller = new Dictionary<string, Image>();
            foreach (string Anahtar in Imaj.Images.Keys)
                Gorseller.Add(Anahtar, (Image)Imaj.Images[Anahtar].Clone());

            Imaj.ImageSize = new Size(YeniBoyut, YeniBoyut);
            Imaj.Images.Clear();

            foreach (var AnahtarDegeri in Gorseller)
                Imaj.Images.Add(AnahtarDegeri.Key, AnahtarDegeri.Value);
        }
        #endregion

        #region Show the ProgressBar Panel with Animation
        private async Task IlerlemeCubuguPaneliniAnimasyonluGoster()
        {
            PanelIlerlemeCubugu.Visible = true;
            SBButtonIptal.Visible = true;
            TLPGenel.RowStyles[2].Height = OrijinalIptalSatiriYuksekligi;

            int Sure = 275;
            var SW = System.Diagnostics.Stopwatch.StartNew();
            TLPGenel.SuspendLayout();

            while (SW.ElapsedMilliseconds < Sure)
            {
                double Ilerleme = SW.ElapsedMilliseconds / (double)Sure;
                double YumusatmaOrani = 1 - Math.Pow(1 - Ilerleme, 3);
                TLPGenel.RowStyles[3].Height = (int)(OrijinalIlerlemeSatiriYuksekligi * YumusatmaOrani);
                TLPGenel.ResumeLayout(false);
                TLPGenel.PerformLayout();
                TLPGenel.SuspendLayout();
                await Task.Delay(8);
            }

            TLPGenel.RowStyles[3].Height = OrijinalIlerlemeSatiriYuksekligi;
            TLPGenel.ResumeLayout(true);
        }
        #endregion

        #region Hide the ProgressBar Panel with Animation
        private async Task IlerlemeCubuguPaneliniAnimasyonluGizle()
        {
            TLPGenel.RowStyles[2].Height = 0;
            SBButtonIptal.Visible = false;

            int Sure = 275;
            var SW = Stopwatch.StartNew();
            TLPGenel.SuspendLayout();

            while (SW.ElapsedMilliseconds < Sure)
            {
                double Ilerleme = SW.ElapsedMilliseconds / (double)Sure;
                double YumusatmaOrani = Math.Pow(Ilerleme, 3);
                TLPGenel.RowStyles[3].Height = (int)(OrijinalIlerlemeSatiriYuksekligi * (1 - YumusatmaOrani));
                TLPGenel.ResumeLayout(false);
                TLPGenel.PerformLayout();
                TLPGenel.SuspendLayout();
                await Task.Delay(8);
            }

            TLPGenel.RowStyles[3].Height = 0;
            TLPGenel.ResumeLayout(true);
            PanelIlerlemeCubugu.Visible = false;
        }
        #endregion

        #region Form adapted for file password setting & password entry operations
        private DialogResult ParolaEkraniGoster(ref string ParolaGirisi, string FormBasligi, string AciklamaMetni,Icon FormIkonu)
        {
            float DPIOlcegiNPBF;
            using (Graphics EkranNPBF = Graphics.FromHwnd(IntPtr.Zero))
                DPIOlcegiNPBF = EkranNPBF.DpiX / 96f;

            Size NPBTemelFormBoyutu = new Size(360, 123);
            int NPBConfirmButonuTemelGenisligi = 100;
            int NPBCancelButonuTemelGenisligi = 90;
            int NPBButonlarinTemelYuksekligi = 32;
            int NPBBirakilanTemelKenarBoslugu = 10;
            int NPBTemelTextBoxYuksekligi = 28;
            int NPBButonlarArasiBirakilanTemelBosluk = 2;

            Size NPBOlceklenmisFormBoyutu = new Size((int)(NPBTemelFormBoyutu.Width * DPIOlcegiNPBF), (int)(NPBTemelFormBoyutu.Height * DPIOlcegiNPBF));
            int NPBConfirmButonuOlceklenmisGenisligi = (int)(NPBConfirmButonuTemelGenisligi * DPIOlcegiNPBF);
            int NPBCancelButonuOlceklenmisGenisligi = (int)(NPBCancelButonuTemelGenisligi * DPIOlcegiNPBF);
            int NPBButonlarinOlceklenmisYuksekligi = (int)(NPBButonlarinTemelYuksekligi * DPIOlcegiNPBF);
            int NPBBirakilanOlceklenmisKenarBoslugu = (int)(NPBBirakilanTemelKenarBoslugu * DPIOlcegiNPBF);
            int NPBOlceklenmisTextBoxYuksekligi = (int)(NPBTemelTextBoxYuksekligi * DPIOlcegiNPBF);
            int NPBButonlarArasiBirakilanOlceklenmisBosluk = (int)(NPBButonlarArasiBirakilanTemelBosluk * DPIOlcegiNPBF);

            Form FormNPB = new Form
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                ClientSize = NPBOlceklenmisFormBoyutu,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false,
                Icon = FormIkonu,
                ShowInTaskbar = false,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Text = FormBasligi,
                BackColor = SystemColors.Control,
                AutoScaleMode = AutoScaleMode.Dpi
            };

            Label LabelAciklamaNPB = new Label
            {
                Name = "LabelANPB",
                Text = AciklamaMetni,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(NPBBirakilanOlceklenmisKenarBoslugu, NPBBirakilanOlceklenmisKenarBoslugu)
            };
            FormNPB.Controls.Add(LabelAciklamaNPB);  

            TextBox TextBoxDosyaParolasiBelirleNPB = new TextBox
            {
                Name = "TextBoxNPB",
                Size = new Size(NPBOlceklenmisFormBoyutu.Width - 2 * NPBBirakilanOlceklenmisKenarBoslugu - NPBOlceklenmisTextBoxYuksekligi - 4, NPBOlceklenmisTextBoxYuksekligi),
                Location = new Point(NPBBirakilanOlceklenmisKenarBoslugu, LabelAciklamaNPB.Bottom + 8),
                Font = new Font("Segoe UI Semibold", 14F),
                BorderStyle = BorderStyle.FixedSingle,
                MaxLength = 25,
                UseSystemPasswordChar = true,
                BackColor = Color.FromArgb(237, 237, 237),
                Text = ParolaGirisi
            };
            FormNPB.Controls.Add(TextBoxDosyaParolasiBelirleNPB);

            SBButton SBButtonParolayiGizleGosterNPB = new SBButton
            {
                Name = "SBButtonPGGNPB",
                BackColor = Color.Transparent,
                ArkaplanRengi = Color.Transparent,
                KenarlikRenk = Color.Transparent,
                Size = new Size(NPBOlceklenmisTextBoxYuksekligi, NPBOlceklenmisTextBoxYuksekligi),
                Location = new Point(TextBoxDosyaParolasiBelirleNPB.Right + 2, TextBoxDosyaParolasiBelirleNPB.Top),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                YuvarlakKoseler = 8,
                KenarBoyutu = 0,
                TabStop = false,
                Image = ImageListOzelFormIcin.Images[2],
                TextImageRelation = TextImageRelation.ImageBeforeText,
                ImageAlign = ContentAlignment.MiddleCenter,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            FormNPB.Controls.Add(SBButtonParolayiGizleGosterNPB);

            bool BelirlenecekParolaGosterimi = false;
            SBButtonParolayiGizleGosterNPB.Click += (s, e) =>
            {
                BelirlenecekParolaGosterimi = !BelirlenecekParolaGosterimi;
                TextBoxDosyaParolasiBelirleNPB.UseSystemPasswordChar = !BelirlenecekParolaGosterimi;
                SBButtonParolayiGizleGosterNPB.Image = BelirlenecekParolaGosterimi ? ImageListOzelFormIcin.Images[3] : ImageListOzelFormIcin.Images[2];
                TextBoxDosyaParolasiBelirleNPB.Focus();
                TextBoxDosyaParolasiBelirleNPB.SelectionStart = TextBoxDosyaParolasiBelirleNPB.Text.Length;
            };

            SBButton SBButtonOnaylaNPB = new SBButton
            {
                DialogResult = DialogResult.OK,
                Name = "SBButtonONPB",
                BackColor = Color.Transparent,
                ArkaplanRengi = Color.FromArgb(235, 245, 235),
                KenarlikRenk = Color.Transparent,
                Size = new Size(NPBConfirmButonuOlceklenmisGenisligi, NPBButonlarinOlceklenmisYuksekligi),
                Text = "Confirm",
                Font = new Font("Segoe UI", 10F),
                Cursor = Cursors.Hand,
                YuvarlakKoseler = 8,
                KenarBoyutu = 0,
                ForeColor = Color.FromArgb(64, 64, 64),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(NPBOlceklenmisFormBoyutu.Width - NPBConfirmButonuOlceklenmisGenisligi - NPBCancelButonuOlceklenmisGenisligi - NPBButonlarArasiBirakilanOlceklenmisBosluk - NPBBirakilanOlceklenmisKenarBoslugu, NPBOlceklenmisFormBoyutu.Height - NPBButonlarinOlceklenmisYuksekligi - NPBBirakilanOlceklenmisKenarBoslugu),
                Image = ImageListOzelFormIcin.Images[1],
                TextImageRelation = TextImageRelation.ImageBeforeText,
                ImageAlign = ContentAlignment.MiddleCenter,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            SBButtonOnaylaNPB.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 235, 220);
            FormNPB.Controls.Add(SBButtonOnaylaNPB);

            SBButton SBButtonIptalNPB = new SBButton
            {
                DialogResult = DialogResult.Cancel,
                Name = "SBButtonINPB",
                BackColor = Color.Transparent,
                ArkaplanRengi = Color.FromArgb(242, 235, 234),
                KenarlikRenk = Color.Transparent,
                Size = new Size(NPBCancelButonuOlceklenmisGenisligi, NPBButonlarinOlceklenmisYuksekligi),
                Text = "Cancel",
                Font = new Font("Segoe UI", 10F),
                Cursor = Cursors.Hand,
                YuvarlakKoseler = 8,
                KenarBoyutu = 0,
                ForeColor = Color.FromArgb(64, 64, 64),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(NPBOlceklenmisFormBoyutu.Width - NPBCancelButonuOlceklenmisGenisligi - NPBBirakilanOlceklenmisKenarBoslugu, NPBOlceklenmisFormBoyutu.Height - NPBButonlarinOlceklenmisYuksekligi - NPBBirakilanOlceklenmisKenarBoslugu),
                Image = ImageListOzelFormIcin.Images[0],
                TextImageRelation = TextImageRelation.ImageBeforeText,
                ImageAlign = ContentAlignment.MiddleCenter,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            SBButtonIptalNPB.FlatAppearance.MouseOverBackColor = Color.MistyRose;
            FormNPB.Controls.Add(SBButtonIptalNPB);
            FormNPB.CancelButton = SBButtonIptalNPB;

            Label LabelKarakterSayaciNPB = new Label
            {
                Name = "LabelKSNPB",
                AutoSize = true,
                ForeColor = Color.FromArgb(100, 100, 100),
                Font = new Font("Segoe UI Semibold", 10f),
                Text = $"{TextBoxDosyaParolasiBelirleNPB.Text.Length} / 25",
                BackColor = Color.Transparent,
                Padding = new Padding(3, 0, 3, 0)
            };
            FormNPB.Controls.Add(LabelKarakterSayaciNPB);
            LabelKarakterSayaciNPB.Location = new Point(NPBBirakilanOlceklenmisKenarBoslugu, NPBOlceklenmisFormBoyutu.Height - NPBButonlarinOlceklenmisYuksekligi - NPBBirakilanOlceklenmisKenarBoslugu + (NPBButonlarinOlceklenmisYuksekligi - LabelKarakterSayaciNPB.Height) / 2);

            TextBoxDosyaParolasiBelirleNPB.TextChanged += (s, e) =>
            {
                LabelKarakterSayaciNPB.Text = $"{TextBoxDosyaParolasiBelirleNPB.Text.Length} / 25";

                if(TextBoxDosyaParolasiBelirleNPB.Text.Length==25)
                {
                    LabelKarakterSayaciNPB.ForeColor= Color.DarkRed;
                }

                else 
                {
                    LabelKarakterSayaciNPB.ForeColor = Color.FromArgb(100, 100, 100);
                }
            };

            TextBoxDosyaParolasiBelirleNPB.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    FormNPB.DialogResult = DialogResult.OK;
                    FormNPB.Close();
                }
            };

            DialogResult NPBCevap = FormNPB.ShowDialog();
            ParolaGirisi = TextBoxDosyaParolasiBelirleNPB.Text;
            return NPBCevap;
        }
        #endregion

        #region Exit Check
        private bool CikisiKontrolEt()
        {
            if (IslemYapiliyormu == true)
            {
                Snackbarim.Goster("There’s an operation in progress. Please cancel it to close the application.",SnackbarDurumlari.Uyari, 5000);
                return true;
            }
           
            else
            {
                return false;
            }
        }
        #endregion

        #region Main Process Executor
        private async Task AnaIslemYuruteci(bool YapilanIslemSifrelememi, CancellationToken IptalBileti)
        {
            IlerlemeGorseli = Resources.icon; 
            ImageAnimator.Animate(IlerlemeGorseli, (s, ev) =>
            {
                PanelIlerlemeCubugu.Invalidate();
            });

            try
            {
                DosyaDiyalogu.Title = YapilanIslemSifrelememi ? "Select the file to encrypt" : "Select the file to decrypt";
                if (DosyaDiyalogu.ShowDialog() != DialogResult.OK) return;

                string KaynakDosyaYolu = DosyaDiyalogu.FileName;

                if (!YapilanIslemSifrelememi)
                {
                    if (!File.Exists(KaynakDosyaYolu) || new FileInfo(KaynakDosyaYolu).Length < 16)
                    {
                       Snackbarim.Goster("Invalid File!\nThis file has not been encrypted by this application.",SnackbarDurumlari.Hata,5000);
                       return;
                    }

                    byte[] SifrelemeImzasiBytelari = new byte[16];
                    using (FileStream FS = new FileStream(KaynakDosyaYolu, FileMode.Open, FileAccess.Read))
                    {
                        FS.Read(SifrelemeImzasiBytelari, 0, 16);
                    }

                    if (Encoding.UTF8.GetString(SifrelemeImzasiBytelari) != SifrelemeImzasi)
                    {
                        Snackbarim.Goster("Invalid File!\nThis file has not been encrypted by this application.", SnackbarDurumlari.Hata, 5000);
                        return;
                    }
                }

                string Parola = "";
                string Baslik = YapilanIslemSifrelememi ? "Encrypt File" : "Decrypt File";
                string Aciklama = YapilanIslemSifrelememi ? "Enter a password to encrypt your file:" : "Enter the password to decrypt the file:";
                Icon Ikon = YapilanIslemSifrelememi ? Resources.parolabelirleekrani : Resources.scoz;

                DialogResult ParolaEkrani = ParolaEkraniGoster(ref Parola, Baslik, Aciklama,Ikon);
                if (ParolaEkrani != DialogResult.OK) return; 

                if (string.IsNullOrEmpty(Parola))
                {
                    Snackbarim.Goster(YapilanIslemSifrelememi ? "Password not set!" : "Password not entered!", SnackbarDurumlari.Uyari, 4000);
                    return;
                }
                if (Parola.Contains(" "))
                {
                    Snackbarim.Goster("Password cannot contain spaces!", SnackbarDurumlari.Uyari, 4000);
                    return;
                }

                KayitDiyalogu.Title = YapilanIslemSifrelememi ? "Save the encrypted file" : "Save the decrypted file";
                KayitDiyalogu.InitialDirectory = Path.GetDirectoryName(KaynakDosyaYolu);
                KayitDiyalogu.Filter = "All Files|*.*";
                KayitDiyalogu.FileName = YapilanIslemSifrelememi
                    ? Path.GetFileNameWithoutExtension(KaynakDosyaYolu) + "_encrypted" + Path.GetExtension(KaynakDosyaYolu)
                    : Path.GetFileNameWithoutExtension(KaynakDosyaYolu).Replace("encrypted", "decrypted") + Path.GetExtension(KaynakDosyaYolu);

             
                if (KayitDiyalogu.ShowDialog() != DialogResult.OK) return;
                string CiktiDosyasiKayitYolu = KayitDiyalogu.FileName;

                long TahminiDosyaBoyutu = new FileInfo(KaynakDosyaYolu).Length + Math.Max(1024 * 1024, new FileInfo(KaynakDosyaYolu).Length / 100); 
                string HedefSurucu = Path.GetPathRoot(CiktiDosyasiKayitYolu);

                if (!YeterliDiskAlaniVarMi(HedefSurucu, TahminiDosyaBoyutu))
                {
                    double TahminiBoyutGB = Math.Round(TahminiDosyaBoyutu / (1024.0 * 1024.0 * 1024.0), 2);
                    Snackbarim.Goster($"Not enough disk space to complete this operation!\nRequired space: {TahminiBoyutGB} GB",SnackbarDurumlari.Uyari, 7000);
                    return;
                }

                if (string.Equals(KaynakDosyaYolu, CiktiDosyasiKayitYolu, StringComparison.OrdinalIgnoreCase))
                {
                    Snackbarim.Goster("Input and output files cannot be the same!", SnackbarDurumlari.Hata,5000);
                    return;
                }

                try 
                { 
                    using (FileStream TestEt = File.Open(KaynakDosyaYolu, FileMode.Open, FileAccess.Read, FileShare.None)) 
                    {
                    } 
                }

                catch
                {
                    Snackbarim.Goster("File Locked. \n File is used by another application!",SnackbarDurumlari.Bilgilendirme, 5000);
                    return;
                }

                SBButtonSifrele.Visible = false;
                SBButtonCoz.Visible = false;
                SBButtonHakkinda.Visible = false;
                SBButtonIptal.YuvarlakKoseler = 25;

                TLPGenel.RowStyles[0].Height = 0;
                TLPGenel.RowStyles[1].Height = 0;
                TLPGenel.RowStyles[4].Height = 0;

                await IlerlemeCubuguPaneliniAnimasyonluGoster();

                try
                {
                    await DosyaIsleme(KaynakDosyaYolu, CiktiDosyasiKayitYolu, Parola, YapilanIslemSifrelememi, IptalBileti);
                    ImageAnimator.StopAnimate(IlerlemeGorseli, null);
                    Snackbarim.Goster(YapilanIslemSifrelememi ? "File encrypted successfully:\n" + CiktiDosyasiKayitYolu: "File decrypted successfully:\n" + CiktiDosyasiKayitYolu,SnackbarDurumlari.Basarili, 7000);                                       
                }

                catch (OperationCanceledException)
                {
                    if (File.Exists(CiktiDosyasiKayitYolu))
                    {
                        File.Delete(CiktiDosyasiKayitYolu);
                        Snackbarim.Goster("Operation canceled.", SnackbarDurumlari.Bilgilendirme, 3000);
                    }
                }

                catch (CryptographicException)
                {
                    if (IptalBileti.IsCancellationRequested)
                    {
                        if (File.Exists(CiktiDosyasiKayitYolu))
                        {
                            File.Delete(CiktiDosyasiKayitYolu);
                            Snackbarim.Goster("Operation canceled.", SnackbarDurumlari.Bilgilendirme, 3000);
                        }      
                    }

                    else
                    {
                        if (File.Exists(CiktiDosyasiKayitYolu))
                        {
                            File.Delete(CiktiDosyasiKayitYolu);
                            ImageAnimator.StopAnimate(IlerlemeGorseli, null);
                            Snackbarim.Goster("Incorrect password or corrupted file!", SnackbarDurumlari.Hata, 4000);
                        }      
                    }
                }

                finally
                {
                    IlerlemeDegeri = 0;
                    PanelIlerlemeCubugu.Tag = "";
                    PanelIlerlemeCubugu.Invalidate();

                    TLPGenel.RowStyles[0].Height = OrijinalSifrelemeSatiriYuksekligi;
                    TLPGenel.RowStyles[1].Height = OrijinalCozmeSatiriYuksekligi;
                    TLPGenel.RowStyles[4].Height = OrijinalHakkindaSatiriYuksekligi;

                    SBButtonSifrele.YuvarlakKoseler = 25;
                    SBButtonCoz.YuvarlakKoseler = 25;
                    SBButtonHakkinda.YuvarlakKoseler = 25;

                    await IlerlemeCubuguPaneliniAnimasyonluGizle();

                    SBButtonSifrele.Visible = true;
                    SBButtonCoz.Visible = true;
                    SBButtonHakkinda.Visible = true;

                    IslemYapiliyormu = false;
                }
            }

            catch (Exception ex)
            {
                if (ex is IOException)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IslemYapiliyormu = false;
                }

                else if (ex is UnauthorizedAccessException)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IslemYapiliyormu = false;
                }

                else if (!(ex is OperationCanceledException))
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IslemYapiliyormu = false;
                    Application.Exit();
                }
            }
        }
        #endregion

        #region File Processing
        private async Task DosyaIsleme(string KaynakDosyasininYolu, string CiktiDosyasininYolu, string SifrelemeParolasi, bool Sifrelememi, CancellationToken IptalEtmeBileti)
        {
            const int TamponBoyutu = 1048576;

            using (FileStream KaynakDosyasiOkumaAkisi = new FileStream(KaynakDosyasininYolu, FileMode.Open, FileAccess.Read, FileShare.Read, TamponBoyutu, true))
            using (FileStream CiktiDosyasiYazmaAkisi = new FileStream(CiktiDosyasininYolu, FileMode.Create, FileAccess.Write, FileShare.None, TamponBoyutu, true))
            {
                byte[] Salt = new byte[32];

                if (Sifrelememi)
                {
                    Salt = RastgeleBytelarUret(32);
                    await CiktiDosyasiYazmaAkisi.WriteAsync(Encoding.UTF8.GetBytes(SifrelemeImzasi), 0, 16, IptalEtmeBileti);
                    await CiktiDosyasiYazmaAkisi.WriteAsync(Salt, 0, Salt.Length, IptalEtmeBileti);
                }

                else
                {
                    byte[] SifreIemeImzasiBytelari = new byte[16];
                    int OkunanBytelar = await KaynakDosyasiOkumaAkisi.ReadAsync(SifreIemeImzasiBytelari, 0, 16, IptalEtmeBileti);

                    if (OkunanBytelar < 16 || Encoding.UTF8.GetString(SifreIemeImzasiBytelari) != SifrelemeImzasi)
                    {
                        throw new InvalidDataException("File not encrypted by this application!");
                    }

                    await KaynakDosyasiOkumaAkisi.ReadAsync(Salt, 0, Salt.Length, IptalEtmeBileti);
                }

                using (var SifrelemeAnahtari = new Rfc2898DeriveBytes(SifrelemeParolasi, Salt, 100000, HashAlgorithmName.SHA256))
                {
                    byte[] AESAnahtari = SifrelemeAnahtari.GetBytes(32);
                    byte[] AESIV = SifrelemeAnahtari.GetBytes(16);

                    using (Aes AESSifreleme = Aes.Create())
                    {
                        AESSifreleme.KeySize = 256;
                        AESSifreleme.BlockSize = 128;
                        AESSifreleme.Mode = CipherMode.CBC;
                        AESSifreleme.Padding = PaddingMode.PKCS7;
                        AESSifreleme.Key = AESAnahtari;
                        AESSifreleme.IV = AESIV;

                        if (Sifrelememi)
                        {
                            using (CryptoStream SifrelemeAkisi = new CryptoStream(CiktiDosyasiYazmaAkisi, AESSifreleme.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                await IlerlemeYuzdeliKopyalamaIslemi(KaynakDosyasiOkumaAkisi, SifrelemeAkisi, KaynakDosyasiOkumaAkisi.Length, IptalEtmeBileti, "Encrypting");
                                SifrelemeAkisi.FlushFinalBlock();
                            }
                        }

                        else
                        {
                            using (CryptoStream CozumlemeAkisi = new CryptoStream(KaynakDosyasiOkumaAkisi, AESSifreleme.CreateDecryptor(), CryptoStreamMode.Read))
                            {
                                await IlerlemeYuzdeliKopyalamaIslemi(CozumlemeAkisi, CiktiDosyasiYazmaAkisi, KaynakDosyasiOkumaAkisi.Length - KaynakDosyasiOkumaAkisi.Position, IptalEtmeBileti, "Decrypting");
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Copy Stream With Progress
        private async Task IlerlemeYuzdeliKopyalamaIslemi(Stream KaynakDosya, Stream CiktiDosya, long ToplamBytelar, CancellationToken Bilet, string YapilanIslem)
        {
            byte[] Tampon = new byte[1024 * 1024];
            long ToplamdaOkunanDeger = 0;
            int AnlikOkunanDeger;
            IslemYapiliyormu = true;

            while ((AnlikOkunanDeger = await KaynakDosya.ReadAsync(Tampon, 0, Tampon.Length, Bilet)) > 0)
            {
                Bilet.ThrowIfCancellationRequested();
                await CiktiDosya.WriteAsync(Tampon, 0, AnlikOkunanDeger, Bilet);
                ToplamdaOkunanDeger += AnlikOkunanDeger;
                IlerlemeDegeri = (int)(ToplamdaOkunanDeger * 100 / ToplamBytelar);
                if (IlerlemeDegeri > 100) IlerlemeDegeri = 100;
                PanelIlerlemeCubugu.Tag = $"{YapilanIslem}: %{IlerlemeDegeri}";
                PanelIlerlemeCubugu.Invalidate();
            }

            ImageAnimator.StopAnimate(IlerlemeGorseli, null);
            IlerlemeGorseli = null;
            IlerlemeDegeri = 100;
            PanelIlerlemeCubugu.Tag = $"{YapilanIslem}: %{IlerlemeDegeri}";
            PanelIlerlemeCubugu.Invalidate();
        }
        #endregion

        #region Generate Random Bytes
        private byte[] RastgeleBytelarUret(int Uzunluk)
        {
            byte[] Bytelar = new byte[Uzunluk];
            using (RNGCryptoServiceProvider RNG = new RNGCryptoServiceProvider())
                RNG.GetBytes(Bytelar);
            return Bytelar;
        }
        #endregion

        #region Set ProgressBar Panel Color
        private Color IlerlemeCubuguPanelininRenginiBelirle(int Ilerleme)
        {
            int Kirmizi = 255 * (100 - Ilerleme) / 100;
            int Yesil = 255 * Ilerleme / 100;
            return Color.FromArgb(Kirmizi, Yesil, 0);
        }
        #endregion

        #region MainForm_FormClosing
        private void AnaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = CikisiKontrolEt();
        }
        #endregion

        #region A smart form custom-designed for the About page
        private static class HakkindaFormuIcinRenkler
        {
            public static readonly Color BaslikKoyuTuruncu = Color.FromArgb(223, 100, 5);
            public static readonly Color BaslikMor = Color.Purple;
            public static readonly Color BaslikSiyah = Color.Black;
            public static readonly Color BaslikMavi = Color.FromArgb(30, 144, 255);
            public static readonly Color HoverEfekti = Color.FromArgb(255, 225, 225, 225);
        }

        private static Panel OgelerPaneliniOlustur(Image Gorsel, string Baslik, string Metin, Color BaslikRengi, float DPIOlcegi)
        {
            Panel PanelKapsayici = new Panel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0),
                Margin = new Padding(0, 0, 0, (int)(12 * DPIOlcegi)),
                BackColor = SystemColors.Control,
            };

            TableLayoutPanel TableLayoutPanelPOgeler = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2,
                RowCount = 2,
                Margin = new Padding(0),
                Dock = DockStyle.Fill,
            };

            TableLayoutPanelPOgeler.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, (int)(32 * DPIOlcegi)));
            TableLayoutPanelPOgeler.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            TableLayoutPanelPOgeler.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            TableLayoutPanelPOgeler.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            PictureBox PictureBoxGorsel = new PictureBox
            {
                Image = Gorsel,
                SizeMode = PictureBoxSizeMode.Zoom,
                Margin = new Padding(3),
                Dock = DockStyle.Fill
            };

            Label LabelBaslik = new Label
            {
                Text = Baslik,
                Font = new Font("Segoe UI", 12F * DPIOlcegi, FontStyle.Bold),
                AutoSize = true,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 0, 3, 1),
                ForeColor = BaslikRengi,
            };

            Label LabelMetin = new Label
            {
                Text = Metin,
                Font = new Font("Segoe UI", 11F * DPIOlcegi, FontStyle.Regular),
                AutoSize = true,
                Dock = DockStyle.Fill,
                MaximumSize = new Size((int)(600 * DPIOlcegi), 0),
                Margin = new Padding(3, (int)(2 * DPIOlcegi), 3, 0),
                TextAlign = ContentAlignment.TopLeft,
            };

            TableLayoutPanelPOgeler.Controls.Add(PictureBoxGorsel, 0, 0);
            TableLayoutPanelPOgeler.Controls.Add(LabelBaslik, 1, 0);
            TableLayoutPanelPOgeler.Controls.Add(LabelMetin, 1, 1);

            PanelKapsayici.Controls.Add(TableLayoutPanelPOgeler);

            void HoverEfektiUygula(Control Kontrol)
            {
                Kontrol.MouseEnter += (s, e) => PanelKapsayici.BackColor = HakkindaFormuIcinRenkler.HoverEfekti;
                Kontrol.MouseLeave += (s, e) => PanelKapsayici.BackColor = SystemColors.Control;

                foreach (Control AltKontroller in Kontrol.Controls)
                    HoverEfektiUygula(AltKontroller);
            }

            HoverEfektiUygula(PanelKapsayici);

            return PanelKapsayici;
        }

        private DialogResult HakkindaFormunuGoster()
        {
            float DPIOlcek;
            using (Graphics GrafikEkran = Graphics.FromHwnd(IntPtr.Zero))
                DPIOlcek = GrafikEkran.DpiX / 96f;

            Form FormHakkinda = new Form
            {
                Text = "About",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false,
                AutoScaleMode = AutoScaleMode.Dpi,
                BackColor = SystemColors.Control,
                ShowInTaskbar = false,
                Width = (int)(740 * DPIOlcek),
                Height = (int)(480 * DPIOlcek),
                Icon = Resources.uygulamamin_ikonu,
                Padding = new Padding((int)(12 * DPIOlcek)),
            };

            Panel PanelGenelKapsayici = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(0),
                BackColor = SystemColors.Control,
            };

            FlowLayoutPanel FlowLayoutPanelGenelKapsayici = new FlowLayoutPanel
            {
                BackColor = SystemColors.Control,
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding((int)(10 * DPIOlcek)),
            };

            Color[] BaslikRenkleri = new Color[]
           {
               HakkindaFormuIcinRenkler.BaslikKoyuTuruncu,
               HakkindaFormuIcinRenkler.BaslikSiyah,
               HakkindaFormuIcinRenkler.BaslikMavi,
               HakkindaFormuIcinRenkler.BaslikMor,
           };

            (Image BaslikGorseli, string BolumBasligi, string BolumMetni)[] HakkindaFormuOgeleri = new (Image, string, string)[]
            {
               (Resources.featuresapp,"Features of the Application","• Protect all your files with a password of your choice\r\n• Secure Office documents, archive files, media files, and more\r\n• Encrypt photos, videos, PDFs, ZIP/RAR archives, and other file types\r\n• Add strong password protection in just a few clicks\r\n• Fast, simple, and user-friendly file security solution\r\n• Prevent unwanted access to your private and confidential files\r\n• Reliable encryption technology for maximum data security"),
               (Resources.politika, "Data Policy","This application operates entirely offline. It does not collect, transmit, or store any personal data on external servers. All encrypted file data remains on your local device and is never shared with third parties."),
               (Resources.copyrightlicense, "License","SB File Encryptor && Decryptor v1.0.0 \r\nCopyright © 2026 Süleyman BEYHAN. All rights reserved."),
               (Resources.websitelink, "Web Page","For more information about the application, click here to visit the web page."),
            };

            for (int i = 0; i < HakkindaFormuOgeleri.Length; i++)
            {
                (Image BolumGorseli, string BolumBasligi, string BolumMetni) = HakkindaFormuOgeleri[i];
                Color BaslikRengi = i < BaslikRenkleri.Length ? BaslikRenkleri[i] : Color.Black;
                Panel BolumPanel = OgelerPaneliniOlustur(BolumGorseli, BolumBasligi, BolumMetni, BaslikRengi, DPIOlcek);
                FlowLayoutPanelGenelKapsayici.Controls.Add(BolumPanel);
            }

            PanelGenelKapsayici.Controls.Add(FlowLayoutPanelGenelKapsayici);
            FormHakkinda.Controls.Add(PanelGenelKapsayici);

            FormHakkinda.KeyPreview = true;
            FormHakkinda.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    FormHakkinda.DialogResult = DialogResult.Cancel;
                    FormHakkinda.Close();
                }
            };

            FlowLayoutPanelGenelKapsayici.Controls[3].Cursor = Cursors.Hand;

            void AddClickEvent(Control Parent, EventHandler Handler)
            {
                Parent.Click += Handler;

                foreach (Control Cntrl in Parent.Controls)
                {
                    AddClickEvent(Cntrl, Handler);
                }
            }

            AddClickEvent(FlowLayoutPanelGenelKapsayici.Controls[3], (s, e) =>
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "https://suleymanbprojects.blogspot.com/2026/04/sb-file-encryptor-decryptor.html",
                        UseShellExecute = true
                    });
                }

                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while opening the link:\n" + ex.Message,
                                    "ERROR",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            });

            return FormHakkinda.ShowDialog();
        }
        #endregion

        #region EncryptButton_Click
        private async void SBButtonSifrele_Click(object sender, EventArgs e)
        {
            IptalBiletiKaynagi?.Dispose();
            IptalBiletiKaynagi = new CancellationTokenSource();
            await AnaIslemYuruteci(true, IptalBiletiKaynagi.Token);
        }
        #endregion

        #region DecryptButton_Click
        private async void SBButtonCoz_Click(object sender, EventArgs e)
        {
            IptalBiletiKaynagi?.Dispose();
            IptalBiletiKaynagi = new CancellationTokenSource();
            await AnaIslemYuruteci(false, IptalBiletiKaynagi.Token);
        }
        #endregion

        #region CancelButton_Click
        private void SBButtonIptal_Click(object sender, EventArgs e)
        {
            IptalBiletiKaynagi?.Cancel();
        }
        #endregion

        #region AboutButton_Click
        private void SBButtonHakkinda_Click(object sender, EventArgs e)
        {
            HakkindaFormunuGoster();
        }
        #endregion

        #region ProgressBarPanel_Paint
        private void PanelIlerlemeCubugu_Paint(object sender, PaintEventArgs e)
        {
            Graphics Grafik = e.Graphics;
            Grafik.Clear(Color.LightGray);

            int IlerlemeGenisligi = (int)(PanelIlerlemeCubugu.Width * (IlerlemeDegeri / 100.0));
            using (Brush Firca = new SolidBrush(IlerlemeCubuguPanelininRenginiBelirle(IlerlemeDegeri)))
                Grafik.FillRectangle(Firca, 0, 0, IlerlemeGenisligi, PanelIlerlemeCubugu.Height);

            string Metin = PanelIlerlemeCubugu.Tag?.ToString() ?? "";
            SizeF MetinBoyutu = Grafik.MeasureString(Metin, IlerlemePaneliFontu);

            int IkonBoyutu = IlerlemeGorseli != null ? (int)(MetinBoyutu.Height * 0.8f) : 0;
            int Aralık = IlerlemeGorseli != null ? 4 : 0;

            int ToplamGenislik = IkonBoyutu + Aralık + (int)MetinBoyutu.Width;

            int XBaslangici = (PanelIlerlemeCubugu.Width - ToplamGenislik) / 2;
            int YBaslangici = (PanelIlerlemeCubugu.Height - (int)MetinBoyutu.Height) / 2;

            if (IlerlemeGorseli != null)
            {
                ImageAnimator.UpdateFrames(IlerlemeGorseli);

                int YIkonu = YBaslangici + ((int)MetinBoyutu.Height - IkonBoyutu) / 2;
                Grafik.DrawImage(IlerlemeGorseli, XBaslangici, YIkonu, IkonBoyutu, IkonBoyutu);
            }

            Grafik.DrawString(Metin, IlerlemePaneliFontu, Brushes.Black, XBaslangici + IkonBoyutu + Aralık, YBaslangici);
        }
        #endregion

        #region MainForm_Load
        private void AnaForm_Load(object sender, EventArgs e)
        {
            Graphics KullaniciEkranOlcegi = CreateGraphics();

            try
            {
                Olcek = KullaniciEkranOlcegi.DpiX;
                AyarlanacakImajOrani = Olcek / 96;

                ImageListImajlarIcinYeniBoyut = (int)Math.Round(ImagelistAnaFormIcin.ImageSize.Height * AyarlanacakImajOrani);
                ImageListiYenidenBoyutlandir(ImagelistAnaFormIcin, ImageListImajlarIcinYeniBoyut);

                ImageListFormIcinYeniBoyut = (int)Math.Round(ImageListOzelFormIcin.ImageSize.Height * AyarlanacakImajOrani);
                ImageListiYenidenBoyutlandir(ImageListOzelFormIcin, ImageListFormIcinYeniBoyut);
            }

            catch
            {
                ImageListiYenidenBoyutlandir(ImagelistAnaFormIcin, 256);
                ImageListiYenidenBoyutlandir(ImageListOzelFormIcin, 256);
            }

            finally
            {
                KullaniciEkranOlcegi.Dispose();
            }
        }
        #endregion
    }
}