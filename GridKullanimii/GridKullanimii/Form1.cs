using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GridKullanimii
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'urunlerDataSet.Urunler' table. You can move, or remove it, as needed.
            this.urunlerTableAdapter.Fill(this.urunlerDataSet.Urunler);
            VerileriGoster();

        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-ASFN1BE;Initial Catalog=Urunler;Integrated Security=True");
        private void VerileriGoster()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Urunler", baglanti);
            DataTable datatable = new DataTable();
            adapter.Fill(datatable);
            gridControl1.DataSource = datatable;
        }

  

        private void gridView1_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                txtID.Text = row[0].ToString();
                txtAD.Text = row[1].ToString();
                txtFiyat.Text = row[2].ToString();
            }
        }
        private void VerileriTemizle()
        {
            txtID.Text = "";
            txtAD.Text = "";
            txtFiyat.Text = "";
           
        }
        //Silme 

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text);

            if (id != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from Urunler  where UrunId=@id", baglanti);
                komut.Parameters.AddWithValue("@id", id);
                komut.ExecuteNonQuery();

                baglanti.Close();
                MessageBox.Show(txtAD.Text + " ürünü başarı ile silindi!");
                VerileriGoster();
                VerileriTemizle();

            }
            else
            {
                DialogResult dialog = MessageBox.Show("Lütfen silmek için ürün seçin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Ekleme
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Urunler WHERE UrunId=", baglanti);
           
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO  Urunler (UrunAd,UrunFiyat) VALUES (@ad, @fiyat)", baglanti);
            
            komut.Parameters.AddWithValue("@ad", txtAD.Text);
            komut.Parameters.AddWithValue("@fiyat", txtFiyat.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show(txtAD.Text + " ayarı başarılı bir şekilde eklendi!");
            baglanti.Close();
            VerileriGoster();
            VerileriTemizle();
        }
        //Güncelleme
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text);
            if (txtID.Text != "" && txtAD.Text != "" && txtFiyat.Text != "" )
            {

                SqlCommand komut = new SqlCommand("UPDATE Urunler set   UrunAd=@ad,UrunFiyat=@fiyat  where UrunId=@id", baglanti);
                baglanti.Open();
                komut.Parameters.AddWithValue("@id", txtID.Text);
                komut.Parameters.AddWithValue("@ad", txtAD.Text);
                komut.Parameters.AddWithValue("@fiyat", txtFiyat.Text);          
                komut.ExecuteNonQuery();
                MessageBox.Show("Başarılı bir şekilde güncellendi!");
                baglanti.Close();
                VerileriGoster();
                VerileriTemizle();
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek için bir sütun seçin!");
            }
        }
    }
}
