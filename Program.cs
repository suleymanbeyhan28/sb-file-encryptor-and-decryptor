using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;

namespace SBFileEncryptorDecryptor
{
    internal static class Program
    {
        #region Defining a mutex, making the application DPI-aware, and checking DLL files
        private static readonly string SBFileEncryptorDecryptorMutex = "A5639qB9E953tq8tSB457r5C722BvD9EBb06079";

        private static readonly (string DLLAdi, string HashButunlukDegeri)[] GerekliDLLDosyalari = new[]
        {
            ("SBCustomControls.dll", "30B4877BDD0067CB78FA8B35280A7338029C87C00087BC76EA9C5A251726E8E2"),
        };

        [STAThread]
        static void Main()
        {
            using (Mutex SBFileEncryptorDecryptorMutexim = new Mutex(true, SBFileEncryptorDecryptorMutex, out bool IlkKezCalistirildi))
            {
                if (!IlkKezCalistirildi)
                {
                    return;
                }

                foreach ((string DosyaAdi, string BeklenenHashDegeri) in GerekliDLLDosyalari)
                {
                    string DosyaYolu = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DosyaAdi);

                    if (!File.Exists(DosyaYolu))
                    {
                        MessageBox.Show($"Required file is missing: {DosyaAdi}", "Missing DLL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(1);
                    }

                    string OrijinalHashDegeri = SHA256ButunluguDogrulama(DosyaYolu);
                    if (!OrijinalHashDegeri.Equals(BeklenenHashDegeri, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show($"File is corrupted or modified: {DosyaAdi}", "Hash Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(1);
                    }
                }

                try
                {
                    if (Environment.OSVersion.Version.Major >= 10)
                    {
                        SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
                    }

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new AnaForm());
                }

                finally
                {
                    SBFileEncryptorDecryptorMutexim.ReleaseMutex();
                }
            }
        }
        private static string SHA256ButunluguDogrulama(string DLLDosyaYolu)
        {
            using (SHA256 SHA256Degeri = SHA256.Create())
            using (FileStream Oku = File.OpenRead(DLLDosyaYolu))
            {
                var HashDegeri = SHA256Degeri.ComputeHash(Oku);
                return BitConverter.ToString(HashDegeri).Replace("-", "").ToUpperInvariant();
            }
        }

        [DllImport("user32.dll")]
        private static extern bool SetProcessDpiAwarenessContext(IntPtr DPIKapsami);
        private static readonly IntPtr DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = new IntPtr(-4);
        #endregion
    }
}