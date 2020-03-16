using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using System.IO;


public class Connector {

	private const string IP = "localhost";// "111.196.165.238";//"127.0.0.1";
	private const int PORT = 8899;
	private Socket client;
	private string msg, ip;

	private byte[] buff;

	private static Connector connector;

	private Connector() {
	}

	public static Connector getConnector() {
		if (connector == null) {
			connector = new Connector();
			connector.StartConnect();
		}
		return connector;
	}

	public void StartConnect() {
		try {
			client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			client.Connect(IP, PORT);
			Debug.Log("连接成功");
			client.BeginReceive(buff, 0, 2048, SocketFlags.None, ReceiveMsg, buff);
			//Thread thread = new Thread(ReceiveMsg);
			//thread.IsBackground = true;
			//thread.Start();
		} catch {
		}
	}

	public void ReceiveMsg(System.IAsyncResult ar) {
		Debug.Log("开始监听");
		int readCount = 0;
		try {
			byte[] buffer = new byte[1024 * 1024];
			readCount = client.EndReceive(ar);
			byte[] temp = new byte[readCount];
			System.Buffer.BlockCopy(buff, 0, temp, 0, readCount);

			ReadMsg(temp);
		} catch (System.Exception ex) {
			client.Close();
		}

		//int len = 0;
		//while (true) {
		//	len = client.Receive(buffer);
		//	if (buffer[0] == 1) {
		//		ip = Encoding.UTF8.GetString(buffer, 1, len - 1);
		//	} else {
		//		msg = Encoding.UTF8.GetString(buffer, 1, len - 1);
		//	}
		//}
		Debug.Log(msg);
		client.BeginReceive(buff, 0, 1024, SocketFlags.None, ReceiveMsg, buff);
	}

	void ReadMsg(byte[] temp) {

	}

	public void SendMessage(int type, int area, int command, string message) {
		//ByteArray
		ByteArray byteArray = new ByteArray();
		byteArray.writeInt(type);
		byteArray.writeInt(area);
		byteArray.writeInt(command);
		byteArray.writeUTF8(message);
		client.Send(byteArray.getBuffer());
	}
}
