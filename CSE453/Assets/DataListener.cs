using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class DataListener : MonoBehaviour {

    private Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private byte[] _receiveBuffer = new byte[1024];
    private ArrayList _messageBuffer = new ArrayList();
    private Socket _dataSocket;

    private void WaitForClient() {
        try {
           _serverSocket.Bind(new IPEndPoint(IPAddress.Any, 6000));
           _serverSocket.Listen(1);

           Debug.Log("Waiting for client connection...");
           _dataSocket = _serverSocket.Accept();
           Debug.Log("Connected to client.");
        }
        catch (SocketException e) {
            Debug.Log(e.ToString());
        }
    }

    private void UpdateReceivedData(byte[] buf, int len) {
        for (int i = 0; i < len; i++) {
            if (buf[i] == '\0') {
                // Message Complete. Message buffer contains a complete data point
                Debug.Log("Received a complete message.");
                Debug.Log(_messageBuffer.ToString());
                _messageBuffer.Clear();
            } else {
                _messageBuffer.Add(buf[i]);
            }
        }
    }

	// Use this for initialization
	void Start () {
        WaitForClient();
	}
	
	// Update is called once per frame
	void Update () {
        // Check if there is data in socket
        if (_dataSocket.Available > 0) {
            Debug.Log("Data available!");
            int toRead;
            if (_dataSocket.Available > _receiveBuffer.Length) {
                toRead = _receiveBuffer.Length;
            } else {
                toRead = _dataSocket.Available;
            }
            int bytesReceived = _dataSocket.Receive(_receiveBuffer, toRead, SocketFlags.None);
            string message = System.Text.Encoding.Default.GetString(_receiveBuffer, 0, bytesReceived);
            Debug.Log(message);
            //UpdateReceivedData(_receiveBuffer, bytesReceived);
        }
	}
}
