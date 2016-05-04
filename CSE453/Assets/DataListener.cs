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

    string array_list_to_string(ArrayList arr) {
        string ret = "";
        for (int i = 0; i < arr.Count; i++) {
            ret += System.Convert.ToChar(arr[i]);
        }
        return ret;
    }

    // Collect data in _messageBuffer until '\n' is received because this 
    // signifies the end of a message
    private void UpdateReceivedData(byte[] buf, int len) {
        for (int i = 0; i < len; i++) {
            if (buf[i] == '\n') {
                // Message Complete. Message buffer contains a complete data point
                Debug.Log("Received a complete message.");
                string message = array_list_to_string(_messageBuffer);
                HandleMessage(message);
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
            // Read either the entire amount available, or the size of the receive buffer. Whichever is smaller.
            if (_dataSocket.Available > _receiveBuffer.Length) {
                toRead = _receiveBuffer.Length;
            } else {
                toRead = _dataSocket.Available;
            }
            int bytesReceived = _dataSocket.Receive(_receiveBuffer, toRead, SocketFlags.None);
            UpdateReceivedData(_receiveBuffer, bytesReceived);
        }
    }

    void HandleMessage(string message) {
        // message should be a string with the following format:
        // left_roll left_pitch left_yaw right_roll right_pitch right_yaw
        char[] delimiterChars = {' '};
        string[] measurements = message.Split(delimiterChars);
        if (measurements.Length != 6) {
            Debug.Log("Message in incorrect format.");
            return;
        }

        Vector3 left_orientation = new Vector3();
        Vector3 right_orientation = new Vector3();

        left_orientation.x = -1 * System.Convert.ToSingle(measurements[1]);
        left_orientation.y = -1 * System.Convert.ToSingle(measurements[2]);
        left_orientation.z = -1 * System.Convert.ToSingle(measurements[0]);

        right_orientation.x = -1 * System.Convert.ToSingle(measurements[4]);
        right_orientation.y = -1 * System.Convert.ToSingle(measurements[5]);
        right_orientation.z = -1 * System.Convert.ToSingle(measurements[3]);

        GameObject left_foot = GameObject.FindGameObjectWithTag("LeftFoot");
        left_foot.transform.eulerAngles = left_orientation;

        GameObject right_foot = GameObject.FindGameObjectWithTag("RightFoot");
        right_foot.transform.eulerAngles = right_orientation;
    }
}
