namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Size = new Size(330, 250);
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //
            label1.Font = new Font("dotum", 14);
            label2.Font = new Font("dotum", 14);
            label3.Font = new Font("dotum", 14);
            label1.Location = new Point(20, 20);
            label2.Location = new Point(20, 50);
            label3.Location = new Point(20, 80);
            button1.Location = new Point(220, 20);
            button2.Location = new Point(220, 50);
            button3.Location = new Point(220, 80);
            button4.Location = new Point(20, 140);
            button5.Location = new Point(120, 140);
            button6.Location = new Point(220, 140);
            button7.Location = new Point(20, 170);
            button1.Text = "copy";
            button2.Text = "copy";
            button3.Text = "copy";
            button4.Text = "network";
            button5.Text = "ping";
            button6.Text = "internet ip";
            button7.Text = "speedtest";
            comboBox1.Location = new Point(30, 110);
            comboBox1.Size = new Size(250, 20);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            main();
        }

        void main()
        {
            if (Convert.ToInt32(Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Default).OpenSubKey("software\\microsoft\\windows nt\\currentversion").GetValue("currentbuild")) < 22000)
            {
                MessageBox.Show("windows 11 update 21H2" + Environment.NewLine + "윈도우 업데이트 필요");
            }
            foreach (System.Net.NetworkInformation.NetworkInterface networkinterface in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                comboBox1.Items.Add(networkinterface.Name);
            }
            comboBox1.SelectedIndex = 0;
        }

        void ip()
        {
            string ip = null;
            foreach (System.Net.NetworkInformation.NetworkInterface networkinterface in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (System.Net.NetworkInformation.UnicastIPAddressInformation unicastipaddressinformation in networkinterface.GetIPProperties().UnicastAddresses)
                {
                    if (comboBox1.SelectedItem.ToString() == networkinterface.Name)
                    {
                        if (unicastipaddressinformation.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ip = unicastipaddressinformation.Address.ToString();
                        }
                    }
                }
            }
            label1.Text = ip;
        }

        void dns()
        {
            List<string> list = new List<string>();
            foreach (System.Net.NetworkInformation.NetworkInterface networkinterface in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (System.Net.IPAddress ipaddress in networkinterface.GetIPProperties().DnsAddresses)
                {
                    list.Add(ipaddress.ToString());
                }
            }
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                label2.Text = list.ToArray()[0];
            }
            else
            {
                label2.Text = null;
            }
        }

        void mac()
        {
            string mac = null;
            System.Net.NetworkInformation.NetworkInterface[] networkinterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            byte[] bytes = networkinterfaces[comboBox1.SelectedIndex].GetPhysicalAddress().GetAddressBytes();
            for (int i = 0; i < bytes.Length; i++)
            {
                mac += bytes[i].ToString("X2");
                if (i != bytes.Length - 1)
                {
                    mac += "-";
                }
            }
            label3.Text = mac;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ip();
            mac();
            dns();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\google\\chrome\\application"))
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\google\\chrome\\application\\chrome";
                process.StartInfo.Arguments = "/new-window /incognito" + string.Empty.PadLeft(1) + "http://www.speedtest.net";
                process.Start();
            }
            else
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\microsoft\\edge\\application\\msedge";
                process.StartInfo.Arguments = "/new-window /inprivate" + string.Empty.PadLeft(1) + "http://www.speedtest.net";
                process.Start();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\google\\chrome\\application"))
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\google\\chrome\\application\\chrome";
                process.StartInfo.Arguments = "/new-window /incognito" + string.Empty.PadLeft(1) + "http://checkip.amazonaws.com";
                process.Start();
            }
            else
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\microsoft\\edge\\application\\msedge";
                process.StartInfo.Arguments = "/new-window /inprivate" + string.Empty.PadLeft(1) + "http://checkip.amazonaws.com";
                process.Start();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "cmd";
            process.StartInfo.Arguments = "/c" + string.Empty.PadLeft(1) + "ping -t" + string.Empty.PadLeft(1) + label2.Text;
            process.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "cmd";
            process.StartInfo.Arguments = "/c" + string.Empty.PadLeft(1) + "ncpa.cpl";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (label3.Text == string.Empty)
            { }
            else
            {
                Clipboard.SetText(label3.Text);
                MessageBox.Show(Clipboard.GetText());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label2.Text == string.Empty)
            { }
            else
            {
                Clipboard.SetText(label2.Text);
                MessageBox.Show(Clipboard.GetText());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label1.Text == string.Empty)
            { }
            else
            {
                Clipboard.SetText(label1.Text);
                MessageBox.Show(Clipboard.GetText());
            }
        }
    }
}