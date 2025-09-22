using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace TP_Sockets
{
    public partial class Form1 : Form
    {
        Socket udpSocket;
        IPEndPoint localEndPoint;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "Creer Socket et bind";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Récupération des valeurs depuis l’IHM
                string ip = textBox1.Text; // Exemple : "127.0.0.1"
                int port = int.Parse(textBox2.Text); // Exemple : "3031"

                // Création du point de terminaison local
                localEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

                // Création du socket UDP
                udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                // Liaison du socket à l’adresse locale
                udpSocket.Bind(localEndPoint);

                MessageBox.Show("Socket créé et lié avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la création du socket : " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (udpSocket != null)
                {
                    udpSocket.Close();      // Ferme le socket
                    udpSocket = null;       // Nettoie la référence
                    MessageBox.Show("Socket fermé avec succès.");
                }
                else
                {
                    MessageBox.Show("Aucun socket à fermer.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la fermeture du socket : " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Vérifie que le socket existe
                if (udpSocket == null)
                {
                    MessageBox.Show("Le socket n'est pas initialisé. Cliquez d'abord sur 'Créer Socket'.");
                    return;
                }

                // Récupération des données depuis l’IHM
                string ipDest = textBox3.Text;
                int portDest = int.Parse(textBox4.Text);
                string message = textBox5.Text;

                // Mise en forme du message
                byte[] msg = Encoding.ASCII.GetBytes(message);

                // Création du point de terminaison de destination
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(ipDest), portDest);

                // Envoi du message
                udpSocket.SendTo(msg, remoteEndPoint);

                MessageBox.Show("Message envoyé avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'envoi : " + ex.Message);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (udpSocket == null)
                {
                    MessageBox.Show("Le socket n'est pas initialisé. Cliquez d'abord sur 'Créer Socket'.");
                    return;
                }

                // Définir un timeout de réception (en millisecondes)
                udpSocket.ReceiveTimeout = 5000; // 5 secondes

                // Préparer le buffer de réception
                byte[] buffer = new byte[1024];
                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);

                // Tenter de recevoir un message
                int receivedBytes = udpSocket.ReceiveFrom(buffer, ref senderEndPoint);

                // Convertir les données reçues en texte
                string messageRecu = Encoding.ASCII.GetString(buffer, 0, receivedBytes);

                // Afficher le message dans la zone de réception
                textBox6.AppendText("Reçu : " + messageRecu + Environment.NewLine);
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.TimedOut)
                {
                    MessageBox.Show("Aucun message reçu dans le délai imparti.");
                }
                else
                {
                    MessageBox.Show("Erreur de réception : " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur générale : " + ex.Message);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
