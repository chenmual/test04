using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;



class ByteArray
{
	private int category;
	private byte[] buffer;
	private int position = 0;
	private int limit = 0;

	public ByteArray() : this(2048) {
	}

	public ByteArray(int category) {
		this.category = category;
		buffer = new byte[category];
		this.writeShort(0);
	}

	public void writeByte(byte data) {
		if (limit == category) {
			throw new System.Exception("错误超出长度");
		}
		position = limit;
		buffer[position] = data;
		position++;
		limit = position;
		refreshLength();
	}

	void refreshLength() {
		position = 0;
		buffer[position] = (byte)(limit >> 8);
		buffer[++position] = (byte)(limit & ((1 << 8) - 1));
		Debug.Log("limit=" + limit);
		//for (int i = 0; i < 20; i++) {
		//	Debug.Log("[" + i + "]" + buffer[i]);
		//}
	}

	public void writeShort(short data) {
		if (limit > category - 4) {
			throw new System.Exception("错误超出长度");
		}
		position = limit;
		int index = 0;
		while (index != 2) {
			buffer[position] = (byte)((data >> ((1 - index) << 3)) & ((1 << 8) - 1));
			data = (short)(data >> 8);
			position++;
			index++;
		}
		limit = position;
		refreshLength();
	}

	public void writeInt(int data) {
		if (limit > category - 4) {
			throw new System.Exception("错误超出长度");
		}
		position = limit;
		int index = 0;
		
		while (index != 4) {
			buffer[position] = (byte)((data >> ((3 - index) << 3)) & ((1 << 8) - 1));
			//data = data >> 8;
			position++;
			index++;
		}
		limit = position;
		refreshLength();	
	}

	public void writeUTF8(string data) {
		byte[] tmpBytes = Encoding.UTF8.GetBytes(data);
		int len = tmpBytes.Length;
		if (limit > category - len) {
			throw new System.Exception("错误超出长度");
		}
		writeInt(len);
		position = limit;
		int index = 0;
		while (index < len) {
			buffer[position] = tmpBytes[len - index - 1];
			index++;
			position++;
		}
		Debug.Log("strlen = " + len);
		limit = position;
		refreshLength();
	}

	public byte[] getBuffer() {
		return buffer;
	}
}