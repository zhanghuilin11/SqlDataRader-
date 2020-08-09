using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace SqlDataRader使用
{
    class Program
    {
        static void Main(string[] args)
        {
            ////1.连接字符串
            //string str = "server = .;database = test;uid=sa;pwd = 8sfxa2eengp-pc";
            ////2.创建连接对象
            //using (SqlConnection cnn = new SqlConnection(str))
            //{
            //    //3.sql语句
            //    string sql = "insert into RX(姓名,性别,年龄,奖金) values('小胖','男',23,10)";
            //    //4.创建sqlcommand 对象
            //    using (SqlCommand cmd = new SqlCommand(sql,cnn))
            //    {
            //        //5.打开cnn连接
            //        cnn.Open();
            //        //6.执行sql语句
            //        int a =  cmd.ExecuteNonQuery();
                        ////增删改用这个方法，返回受影响的行数
            //        //7.关闭连接
            //        cnn.Close();
            //        Console.WriteLine(a);
            //        Console.ReadKey();

            //    }

            //}
            string str = "server = .;database = FYJ;uid=sa;pwd = 8sfxa2eengp-pc";
            using (SqlConnection cnn = new SqlConnection(str))
            {
                string sql = "select top 20 * from drug";
                using (SqlCommand cmd = new SqlCommand(sql,cnn))
                {
                    cnn.Open();
                    //通过调用ExecuteReader()方法，将给定的sql语句在服务器段执行
                    //执行完毕后，服务器端已经查到了数据，但是将数据保存在服务器的内存中，没有返回给程序
                    //只返回给i程序一个reader对象，这个对象就是用来获取数据的对象
                    //使用reader的时候连接必须是打开的
                    //使用完reader后必须关闭和释放reader对象和关闭连接
                    //默认情况下rader要求独占一个连接对象cnn
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        //通过reader一条一条的获取数据
                        //在获取一条数据之前，先判断是否查询到了数据
                        if (sdr.HasRows)//如果有数据位true，否则为false
                        {
                            //如果有数据就一条一条的获取数据，只能一条一条的获取
                            //每次获取之前都要调用sdr.Read()，向后移动一条，如果移动到了一条数据上，就返回true，否则返回false
                            //reader不能后退，取一条销毁一条
                            while (sdr.Read())
                            {
                                //获取当前reader指向的数据
                                //sdr.FieldCount;返回查询到的列数
                                for (int i = 0; i < sdr.FieldCount; i++)
                                {
                                    Console.Write(sdr[i]+" | ");
                                }
                                Console.WriteLine();
  
                            }
                           
                        }
                        else
                        {
                            Console.WriteLine("没有数据");
                        }

                    }
                    cnn.Close();

                }
                Console.ReadKey();
            }


        }
    }
}
