using Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Windows.Forms;

namespace TestDTC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            string url1 = this.txtBoxUrl1.Text;
            string url2 = this.txtBoxUrl2.Text;

            DoHttpInvoke(url1, url2);

            Log("Done! " + DateTime.Now.ToLongDateString());

        }

        private void DoHttpInvoke(string url1,string url2)
        {
            using (var scope = new TransactionScope())
            {
                // cross app domain call
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, url1))
                    {
                        // forward transaction token
                        request.AddTransactionPropagationToken();
                        var response = client.SendAsync(request).Result;
                        response.EnsureSuccessStatusCode();
                    }



                    using (var request = new HttpRequestMessage(HttpMethod.Get, url2))
                    {
                        // forward transaction token
                        request.AddTransactionPropagationToken();
                        var response = client.SendAsync(request).Result;
                        response.EnsureSuccessStatusCode();
                    }

                }

                // Raising an exception client and server operations are rolled back

                scope.Complete();
            }
            
            return;
        }

        private void Log(string s)
        {
            this.textBox1.AppendText(s + Environment.NewLine);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url1 = this.txtBoxUrl1.Text;
            string url2 = this.txtBoxUrl2.Text;

            DoHttpInvokeWithException(url1, url2);
        }

        private void DoHttpInvokeWithException(string url1, string url2)
        {
            using (var scope = new TransactionScope())
            {
                // cross app domain call
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, url1))
                    {
                        // forward transaction token
                        request.AddTransactionPropagationToken();
                        var response = client.SendAsync(request).Result;
                        response.EnsureSuccessStatusCode();
                    }



                    using (var request = new HttpRequestMessage(HttpMethod.Get, url2))
                    {
                        // forward transaction token
                        request.AddTransactionPropagationToken();
                        var response = client.SendAsync(request).Result;
                        response.EnsureSuccessStatusCode();
                    }

                }

                // Raising an exception client and server operations are rolled back
                throw new Exception();

                scope.Complete();
            }

            return;
        }
    }
}
