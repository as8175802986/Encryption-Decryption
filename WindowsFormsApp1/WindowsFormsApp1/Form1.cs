using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();
                string password = @"myKey123";//this is your key 
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);
                string cryptFile = @"E:\Sagitec File practice\output\Encrypted.arun";
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();
                CryptoStream cs = new CryptoStream(fsCrypt, RMCrypto.CreateEncryptor(key, key), CryptoStreamMode.Write);
                FileStream fsIn = new FileStream(openFileDialog.FileName, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1) cs.WriteByte((byte)data);

                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
                MessageBox.Show("Encrypted Successfull");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string password = @"myKey123";//this is your key 
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(openFileDialog.FileName, FileMode.Open);
                RijndaelManaged RMCrypto = new RijndaelManaged();
                CryptoStream cs = new CryptoStream(fsCrypt, RMCrypto.CreateDecryptor(key, key), CryptoStreamMode.Read);

                String outputFile = @"E:\Sagitec File practice\output\Decrypted.arun";

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);
                int data;
                while ((data = cs.ReadByte())!= -1)fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();fsCrypt.Close();

                MessageBox.Show("File Decrypted Succesfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
