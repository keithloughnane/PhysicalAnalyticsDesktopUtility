using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace TrainingSessionUtility
{
    public partial class SyncConnection : Form
    {
        public SyncConnection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void SyncConnection_Load(object sender, EventArgs e)
        {
            TcpClient clientSocket = default(TcpClient);
            Console.WriteLine("Netclient started.");

        }
    }
}
