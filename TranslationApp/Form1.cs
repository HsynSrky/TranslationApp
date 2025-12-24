using System;
using System.Drawing;
using System.Windows.Forms;
using GTranslate.Translators; // Kütüphaneyi buraya ekledik

namespace TranslationApp
{
    public partial class Form1 : Form
    {
        // Çeviri motorunu tanımlıyoruz
        private readonly GoogleTranslator _translator = new GoogleTranslator();

        public Form1()
        {
            InitializeComponent();
            SetupLanguages(); // ComboBox'ı doldurur
        }

        private void SetupLanguages()
        {
            // Hedef dilleri ekliyoruz
            cmbTargetLang.Items.Add("English");
            cmbTargetLang.Items.Add("Turkish");
            cmbTargetLang.Items.Add("German");
            cmbTargetLang.Items.Add("French");
            cmbTargetLang.SelectedIndex = 0; // İlk dili seçili getir
        }

        // Tasarımda butona çift tıkladığında bu isimle bir metod oluşmalı
        private async void btnTranslate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSource.Text))
            {
                MessageBox.Show("Lütfen çevrilecek bir metin girin!");
                return;
            }

            try
            {
                btnTranslate.Text = "Çevriliyor...";
                btnTranslate.Enabled = false;

                // Seçilen dile göre dil kodunu belirle
                string targetTag = "en";
                string selected = cmbTargetLang.SelectedItem.ToString();

                if (selected == "Turkish") targetTag = "tr";
                else if (selected == "German") targetTag = "de";
                else if (selected == "French") targetTag = "fr";

                // Çeviri işlemi (İnternet üzerinden asenkron yapılır)
                var result = await _translator.TranslateAsync(txtSource.Text, targetTag);

                txtTarget.Text = result.Translation;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Çeviri sırasında bir hata oluştu: " + ex.Message);
            }
            finally
            {
                btnTranslate.Text = "Çevir";
                btnTranslate.Enabled = true;
            }
        }
    }
}