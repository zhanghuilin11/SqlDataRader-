using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 增删改查练习
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            string str = "server = .;database = test;uid = sa;pwd = 8sfxa2eengp-pc";
            List<RX> list = new List<RX>();
            using (SqlConnection cnn = new SqlConnection(str))
            {
                string sql = "select * from RX";
                using (SqlCommand cmd = new SqlCommand(sql,cnn))
                {
                    cnn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            
                            while (reader.Read())
                            {
                                RX rX = new RX();
                                rX.Bh = reader.IsDBNull(0) ? 0 :  reader.GetInt32(0);
                                rX.Name = reader.IsDBNull(1)? "Null": reader.GetString(1);
                                rX.Sex = reader.IsDBNull(2) ? "Null" : reader.GetString(2);
                                rX.Age = reader.IsDBNull(3) ? 0 :  reader.GetInt32(3);
                                rX.Jj = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                                list.Add(rX);
                            }
                        }
                        else
                        {
                            this.Text = "没有数据";
                        }

                    }


                }
            }
            dgv.DataSource = list;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (textName.Text != "" && int.Parse(textAge.Text.Trim()) > 0 && int.Parse(textAge.Text.Trim()) < 120 && int.Parse(textJj.Text.Trim()) > 0)
                {
                    //string name = textName.Text.Trim();
                    //string sex = comboBox1.SelectedItem.ToString();
                    //int age = int.Parse(textAge.Text.Trim());
                    //int jj = int.Parse(textJj.Text.Trim());
                    int a = 0;
                    Boolean insetr = false;
                    string str = "server = .;database = test;uid = sa;pwd = 8sfxa2eengp-pc";
                    using (SqlConnection cnn = new SqlConnection(str))
                    {
                        string name = textName.Text.Trim();
                        string sex = comboBox1.SelectedItem.ToString();
                        int age = int.Parse(textAge.Text.Trim());
                        int jj = int.Parse(textJj.Text.Trim());
                        string sql = string.Format("insert into RX(姓名,性别,年龄,奖金) output inserted.编号 values ('{0}','{1}',{2},{3})", name, sex, age, jj);
                        using (SqlCommand cmd = new SqlCommand(sql, cnn))
                        {
                            cnn.Open();
                            a = int.Parse(cmd.ExecuteScalar().ToString());
                            cnn.Close();
                            if (a >0)
                            {
                                insetr = true;
                            }
                        }
                    }
                    if (insetr)
                    {
                        this.Text = "新增成功";
                        LoadData();
                        MessageBox.Show(a.ToString());
                    }
                    else
                    {
                        MessageBox.Show("新增失败");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textName.Clear();
            textAge.Clear();
            textJj.Clear();
            
        }

        private void dgv_MouseClick(object sender, MouseEventArgs e)
        {
            


        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //textEAge.Clear();
            //textEName.Clear();
            //textEJj.Clear();
            //lbBh.Text = "";
            //lbSEX.Text = "";
            DataGridViewRow dataGridView = this.dgv.Rows[e.RowIndex];
            RX rX =dataGridView.DataBoundItem as RX;
            if (rX != null)
            {
                textEName.Text = rX.Name;
                textEAge.Text = rX.Age.ToString();
                textEJj.Text = rX.Jj.ToString();
                lbBh.Text = rX.Bh.ToString();
                lbSEX.Text = rX.Sex;
            }
            //rX.Bh = int.Parse(dataGridView.Cells[0].Value.ToString());
            //rX.Name = dataGridView.Cells[1].Value.ToString();
            //rX.Sex = dataGridView.Cells[2].Value.ToString();
            //rX.Age = int.Parse(dataGridView.Cells[3].Value.ToString());
            //rX.Jj = int.Parse(dataGridView.Cells[4].Value.ToString());

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = "server = .;database = test;uid = sa;pwd = 8sfxa2eengp-pc";
            Boolean update = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(str))
                {
                    string sql = string.Format("update RX set 姓名 = '{0}',年龄 = {1},奖金 = {2} where 编号 = {3}",textEName.Text.Trim(),textEAge.Text.Trim(),textEJj.Text.Trim(),lbBh.Text);
                    using (SqlCommand cmd = new SqlCommand(sql,cnn))
                    {
                        cnn.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            update = true;
                        }
                        cnn.Close();
                    }
                }
                LoadData();
                if (update)
                {
                    this.Text = "修改成功";

                }
                else
                {
                    this.Text = "修改失败";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str = "server = .;database = test;uid = sa ;pwd = 8sfxa2eengp-pc";
            Boolean delete = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(str))
                {
                    string sql = string.Format(" delete from RX where 编号 = {0}",lbBh.Text);
                    using (SqlCommand cmd = new SqlCommand(sql,cnn))
                    {
                        cnn.Open();
                        if (cmd.ExecuteNonQuery()>0)
                        {
                            delete = true;
                        }
                        cnn.Close();
                    }
                }
                LoadData();
                if (delete)
                {
                    this.Text = "删除成功";
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}
