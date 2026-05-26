using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

#region Defining Snackbar States
public enum SnackbarDurumlari
{
    Bilgilendirme,
    Basarili,
    Uyari,
    Hata
}
#endregion

public class Snackbar
{
    #region Defining the Snackbar Structure
    private readonly Form GosterimYapilacakForm;
    private readonly Panel SnackbarPanel;
    private readonly Label MesajLabel;
    private readonly PictureBox MesajIkonuPictureBox;

    private readonly Timer AnimasyonIcinTimer;
    private readonly Timer GoruntuleyiciTimer;

    private float Opaklık = 0f;
    private bool AnimasyonaBasla = false;
    private bool AnimasyonuBitir = false;
    private int GosterimSuresi = 3000;

    private Color ArkaPlanRengi = Color.FromArgb(33, 150, 243);
    private int YEkseni;

    private readonly Dictionary<SnackbarDurumlari, Image> IkonlarOnbellegi = new Dictionary<SnackbarDurumlari, Image>();

    private readonly float DPIOlcegi;
    private readonly int TemelPanelYuksekligi = 60;
    private readonly int TemelPanelYanKenarlarBoslugu = 20;
    private readonly int TemelPanelAltKenarBoslugu = 20;
    private readonly int TemelPanelKoselerininYuvarlakligi = 8;
    #endregion

    #region Defining the Snackbar Layout
    public Snackbar(Form GosterimFormu)
    {
        GosterimYapilacakForm = GosterimFormu;

        using (Graphics KullaniciEkrani = Graphics.FromHwnd(IntPtr.Zero))
            DPIOlcegi = KullaniciEkrani.DpiX / 96f;

        int OlceklenmisPanelYuksekligi = (int)(TemelPanelYuksekligi * DPIOlcegi);
        int OlceklenmisPanelYanKenarlarBoslugu = (int)(TemelPanelYanKenarlarBoslugu * DPIOlcegi);

        SnackbarPanel = new Panel
        {
            Height = OlceklenmisPanelYuksekligi,
            Width = GosterimYapilacakForm.ClientSize.Width - 2 * OlceklenmisPanelYanKenarlarBoslugu,
            Left = OlceklenmisPanelYanKenarlarBoslugu,
            Top = GosterimYapilacakForm.ClientSize.Height,
            BackColor = ArkaPlanRengi,
            Visible = false
        };

        typeof(Panel).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, SnackbarPanel, new object[] { true });

        SnackbarPanel.Region = KoseleriYuvarla(SnackbarPanel.Width, SnackbarPanel.Height, (int)(TemelPanelKoselerininYuvarlakligi * DPIOlcegi));

        MesajIkonuPictureBox = new PictureBox
        {
            Size = new Size((int)(24 * DPIOlcegi), (int)(24 * DPIOlcegi)),
            SizeMode = PictureBoxSizeMode.Zoom,
            BackColor = Color.Transparent
        };

        MesajLabel = new Label
        {
            AutoSize = false,
            AutoEllipsis = true,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font("Segoe UI Semibold", 11 * DPIOlcegi),
            ForeColor = Color.White,
            BackColor = Color.Transparent
        };

        SnackbarPanel.Controls.Add(MesajIkonuPictureBox);
        SnackbarPanel.Controls.Add(MesajLabel);
        GosterimYapilacakForm.Controls.Add(SnackbarPanel);

        AnimasyonIcinTimer = new Timer { Interval = 15 };
        AnimasyonIcinTimer.Tick += AnimasyonIcinTimer_Tick;

        GoruntuleyiciTimer = new Timer();
        GoruntuleyiciTimer.Tick += (s, e) =>
        {
            GoruntuleyiciTimer.Stop();
            AnimasyonuBitir = true;
            AnimasyonIcinTimer.Start();
        };

        GosterimYapilacakForm.Resize += (s, e) => SnackBariDPIyaUyarla();

        IkonlarOnbellegi[SnackbarDurumlari.Basarili] = SBFileEncryptorDecryptor.Properties.Resources.basarilidurumu;
        IkonlarOnbellegi[SnackbarDurumlari.Hata] = SBFileEncryptorDecryptor.Properties.Resources.hatadurumu;
        IkonlarOnbellegi[SnackbarDurumlari.Uyari] = SBFileEncryptorDecryptor.Properties.Resources.uyaridurumu;
        IkonlarOnbellegi[SnackbarDurumlari.Bilgilendirme] = SBFileEncryptorDecryptor.Properties.Resources.bilgidurumu;
    }
    #endregion

    #region Public API (Show the Snackbar)
    public void Goster(string Mesaj, SnackbarDurumlari Durum = SnackbarDurumlari.Bilgilendirme, int GoruntulemeSuresi = 3000)
    {
        GosterimSuresi = GoruntulemeSuresi;
        MesajLabel.Text = Mesaj;

        switch (Durum)
        {
            case SnackbarDurumlari.Basarili:
                ArkaPlanRengi = Color.FromArgb(46, 125, 50);
                MesajIkonuPictureBox.Image = IkonlarOnbellegi[SnackbarDurumlari.Basarili];
                SystemSounds.Hand.Play();
                break;

            case SnackbarDurumlari.Hata:
                ArkaPlanRengi = Color.FromArgb(160, 35, 35);
                MesajIkonuPictureBox.Image = IkonlarOnbellegi[SnackbarDurumlari.Hata];
                SystemSounds.Exclamation.Play();
                break;

            case SnackbarDurumlari.Uyari:
                ArkaPlanRengi = Color.FromArgb(190, 98, 0);
                MesajIkonuPictureBox.Image = IkonlarOnbellegi[SnackbarDurumlari.Uyari];
                SystemSounds.Exclamation.Play();
                break;

            case SnackbarDurumlari.Bilgilendirme:
            default:
                ArkaPlanRengi = Color.FromArgb(21, 101, 192);
                MesajIkonuPictureBox.Image = IkonlarOnbellegi[SnackbarDurumlari.Bilgilendirme];
                SystemSounds.Beep.Play();
                break;
        }

        SnackbarPanel.BackColor = ArkaPlanRengi;
        SnackbarPanel.Region = KoseleriYuvarla(SnackbarPanel.Width, SnackbarPanel.Height, (int)(TemelPanelKoselerininYuvarlakligi * DPIOlcegi));
        MesajLabel.ForeColor = Color.White;

        SnackBariDPIyaUyarla();

        Opaklık = 0f;
        SnackbarPanel.Visible = true;
        SnackbarPanel.BringToFront();

        AnimasyonaBasla = true;
        AnimasyonuBitir = false;

        AnimasyonIcinTimer.Start();
    }
    #endregion

    #region Make the Snackbar DPI-Aware
    private void SnackBariDPIyaUyarla()
    {
        int YenidenOlceklenmisPanelYanKenarlarBoslugu = (int)(TemelPanelYanKenarlarBoslugu * DPIOlcegi);
        int YenidenOlceklenmisPanelAltKenarBoslugu = (int)(TemelPanelAltKenarBoslugu * DPIOlcegi);

        SnackbarPanel.Width = GosterimYapilacakForm.ClientSize.Width - 2 * YenidenOlceklenmisPanelYanKenarlarBoslugu;
        SnackbarPanel.Left = YenidenOlceklenmisPanelYanKenarlarBoslugu;

        YEkseni = GosterimYapilacakForm.ClientSize.Height - SnackbarPanel.Height - YenidenOlceklenmisPanelAltKenarBoslugu;

        if (!SnackbarPanel.Visible)
            SnackbarPanel.Top = GosterimYapilacakForm.ClientSize.Height;

        MesajLabel.Location = new Point((int)(50 * DPIOlcegi), 0);
        MesajLabel.Size = new Size(SnackbarPanel.Width - (int)(70 * DPIOlcegi), SnackbarPanel.Height);
        MesajIkonuPictureBox.Location = new Point((int)(16 * DPIOlcegi), (SnackbarPanel.Height - MesajIkonuPictureBox.Height) / 2);
    }
    #endregion

    #region Round the Corners of the Snackbar
    private Region KoseleriYuvarla(int Genislik, int Uzunluk, int YariCap)
    {
        GraphicsPath Yol = new GraphicsPath();
        Yol.AddArc(0, 0, YariCap, YariCap, 180, 90);
        Yol.AddArc(Genislik - YariCap, 0, YariCap, YariCap, 270, 90);
        Yol.AddArc(Genislik - YariCap, Uzunluk - YariCap, YariCap, YariCap, 0, 90);
        Yol.AddArc(0, Uzunluk - YariCap, YariCap, YariCap, 90, 90);
        Yol.CloseFigure();
        return new Region(Yol);
    }
    #endregion

    #region Snackbar Animation
    private void AnimasyonIcinTimer_Tick(object sender, EventArgs e)
    {
        int EfektHizi = (int)(4 * DPIOlcegi);

        if (AnimasyonaBasla)
        {
            Opaklık += (1f - Opaklık) * 0.12f;

            if (SnackbarPanel.Top > YEkseni)
                SnackbarPanel.Top = Math.Max(SnackbarPanel.Top - EfektHizi, YEkseni);

            if (Opaklık >= 0.98f && SnackbarPanel.Top <= YEkseni)
            {
                AnimasyonaBasla = false;
                Opaklık = 1f;
                SnackbarPanel.Top = YEkseni;
                GoruntuleyiciTimer.Interval = GosterimSuresi;
                GoruntuleyiciTimer.Start();
                AnimasyonIcinTimer.Stop();
            }
        }

        else if (AnimasyonuBitir)
        {
            Opaklık -= Opaklık * 0.12f;

            if (SnackbarPanel.Top < GosterimYapilacakForm.ClientSize.Height)
                SnackbarPanel.Top = Math.Min(SnackbarPanel.Top + EfektHizi, GosterimYapilacakForm.ClientSize.Height);

            if (Opaklık <= 0.02f)
            {
                AnimasyonuBitir = false;
                Opaklık = 0f;
                SnackbarPanel.Visible = false;
                AnimasyonIcinTimer.Stop();
            }
        }

        int AlfaOrani = Math.Max(0, Math.Min(255, (int)(Opaklık * 255)));
        SnackbarPanel.BackColor = Color.FromArgb(AlfaOrani, ArkaPlanRengi);
        MesajLabel.ForeColor = Color.FromArgb(AlfaOrani, Color.White);
    }
    #endregion
}