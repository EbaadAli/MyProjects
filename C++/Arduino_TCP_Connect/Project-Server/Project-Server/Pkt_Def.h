#ifndef _PACKET_H
#define _PACKET_H

class Packet {
private:
	unsigned char PacketID;
	unsigned char CmdListSize;
	unsigned char *DriveCmdList;
	unsigned char Parity;
public:
	Packet();
	~Packet();
	void SetInfo();
	void PrintInfo();
	int Listen(SOCKET &, char *, int);
	int Accept(SOCKET &, SOCKET &);
	void ReceivePkt(SOCKET &);
	void Send(SOCKET &);
	void Receive(SOCKET &);
	int generateParity();
	int getPacketID();
	void CloseSocket(SOCKET &);
	void WinsockExit();
};

#endif
