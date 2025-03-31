using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPListener
{
    public partial class Form1 : Form
    {
        private UdpClient udpListener;
        private Thread listenerThread;
        private bool isListening = false;
        private int listeningPort = 9999; //Port to listen on



        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isListening)
            {
                isListening = true;
                listenerThread = new Thread(new ThreadStart(StartUDPListener));
                listenerThread.IsBackground = true;
                listenerThread.Start();
                richTextBox2.AppendText("UDP Listener started on port " + listeningPort + Environment.NewLine);
            }
        }

        private void StartUDPListener()
        {
            try
            {
                udpListener = new UdpClient(listeningPort);
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, listeningPort);

                while (isListening)
                {
                    byte[] receivedBytes = udpListener.Receive(ref remoteEP);
                    string receivedData = Encoding.UTF8.GetString(receivedBytes);

                    this.Invoke(new Action(() =>
                    {
                        richTextBox2.AppendText($"Received: {receivedData} from {remoteEP}" + Environment.NewLine);
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            isListening = false;
            udpListener?.Close();
        }
    }
}
