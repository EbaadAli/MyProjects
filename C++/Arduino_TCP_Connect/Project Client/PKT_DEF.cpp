#include <windows.networking.sockets.h>
#pragma comment(lib, "Ws2_32.lib")

#include "PKT_DEF.h"
#include <iostream>
#include <bitset>
#include <string>
#include <sstream>
using namespace std;

Packet::Packet() {
	PacketID = 0;
	CmdListSize = 1;
	DriveCmdList = new unsigned char[CmdListSize];
	DriveCmdList[0] = 0;
	Parity = 0;
}

Packet::~Packet() {
	delete[] DriveCmdList;
	DriveCmdList = nullptr;
}

void Packet::SetInfo() {

	delete[] DriveCmdList;
	DriveCmdList = nullptr;

	int AuxInt;
	cout << "What is the PacketID (15 - DRIVE | 5 - STATUS | 0 - SLEEP) ? ";
	cin >> AuxInt;
	PacketID = (unsigned char)AuxInt;
	if (PacketID == 5 || PacketID == 0)
	{
		CmdListSize = 1;  
		DriveCmdList = new unsigned char[CmdListSize*2];
		int k = 0;
		for (int i = 0; i < CmdListSize; i++)
		{
			DriveCmdList[k] = 0;
			k++;
			DriveCmdList[k] = 1;
			k++;
		}
		int parity_value;
		parity_value = generateParity(); //call generate function to get parity
		Parity = (unsigned char)parity_value;
	}
	else
	{
		cout << "What is CmdListSize? :";
		cin >> AuxInt;
		CmdListSize = (unsigned char)AuxInt;
		DriveCmdList = new unsigned char[CmdListSize * 2];
		int k = 0;
		for (int i = 0; i < CmdListSize; i++){
			cout << "Direction: ";
			cin >> AuxInt;
			DriveCmdList[k] = (unsigned char)AuxInt;

			k++;
			cout << "Duration: ";
			cin >> AuxInt;
			DriveCmdList[k] = (unsigned char)AuxInt;
			k++;
		}
		int parity_value;
		parity_value = generateParity(); //call generate function to get parity
		Parity = (unsigned char)parity_value;
	}
}
//get parity value
int Packet::generateParity()
{
	int count = 0;
	int pktid_int;
	int cmdlistsize_int;
	int direction;
	int duration;
	pktid_int = (int)PacketID;
	while (pktid_int > 0)  
	{
		if (pktid_int % 2 != 0) //check if remainder is not zero
			count++; //count number of ones
		pktid_int /= 2; //divide by 2
	}
	
	cmdlistsize_int = (int)CmdListSize;
	while (cmdlistsize_int > 0)
	{
		if (cmdlistsize_int % 2 != 0)
			count++;
		cmdlistsize_int /= 2;
	}
	int k = 0;
	for (int i = 0; i < CmdListSize; i++)
	{
		direction = (int)DriveCmdList[k];
		k++;
		duration = (int)DriveCmdList[k];
		while (direction > 0)
		{
			if (direction % 2 != 0)
				count++;
			direction /= 2;
		}
		while (duration > 0)
		{
			if (duration % 2 != 0)
				count++;
			duration /= 2;
		}
		k++;
	}	
	return count;
}
void Packet::PrintInfo() {
	cout << "PacketID: " << (int)PacketID << endl;
	
	int k = (int)CmdListSize;
	for (int i = 0; i < (int)CmdListSize; i++)
	{
		cout << (int)DriveCmdList[i] << ", " << " ";
	}
	cout << endl;
	cout << "Parity: " << (int)Parity << endl;
}

int Packet::Listen(SOCKET &ServerSocket, char * IP, int Port) {
	//starts Winsock DLLs   
	WSADATA wsaData;
	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0)
		return 0;

	//create server socket
	ServerSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (ServerSocket == INVALID_SOCKET) {
		WSACleanup();
		return 0;
	}

	//binds socket to address
	struct sockaddr_in SvrAddr;
	SvrAddr.sin_family = AF_INET;
	SvrAddr.sin_addr.s_addr = inet_addr(IP);
	SvrAddr.sin_port = htons(Port);
	if (bind(ServerSocket, (struct sockaddr *)&SvrAddr, sizeof(SvrAddr)) == SOCKET_ERROR)
	{
		closesocket(ServerSocket);
		WSACleanup();
		return 0;
	}

	//listen on a socket
	if (listen(ServerSocket, 1) == SOCKET_ERROR) {
		closesocket(ServerSocket);
		WSACleanup();
		return 0;
	}

	cout << "Waiting for client connection" << endl;

	return 1;

}

int Packet::Accept(SOCKET &ServerSocket, SOCKET &ConnectionSocket) {
	if ((ConnectionSocket = accept(ServerSocket, NULL, NULL)) == SOCKET_ERROR) {
		closesocket(ServerSocket);
		WSACleanup();
		return 0;
	}

	cout << "Connection Established" << endl;
	return 1;
}

void Packet::ReceivePkt(SOCKET &ConnectionSocket){

	char RxBuffer[128] = {};
	int len =sizeof(RxBuffer);
	if (recv(ConnectionSocket, RxBuffer, len, 0) > 0) {
		PacketID = RxBuffer[0];
		CmdListSize = RxBuffer[1];
		DriveCmdList = new unsigned char[CmdListSize];
		for (int i = 0; i < CmdListSize; i++)
			DriveCmdList[i] = RxBuffer[2 + i];
		Parity = RxBuffer[2 + CmdListSize];
	}
}

void Packet::Send(SOCKET &ConnectionSocket){

	unsigned char TxBuffer[128] = {};
	TxBuffer[0] = PacketID;
	TxBuffer[1] = CmdListSize;
	for (int i = 0; i < CmdListSize * 2; i++)
		TxBuffer[2 + i] = DriveCmdList[i];
	TxBuffer[2 + CmdListSize * 2] = Parity;
	send(ConnectionSocket, (char *)TxBuffer, 3 + CmdListSize*2, 0);
}

void Packet::Receive(SOCKET &ConnectionSocket){

	char RxBuffer[128] = {};
	if (recv(ConnectionSocket, RxBuffer, sizeof(RxBuffer), 0) > 0) {
		cout << "Received Message: " << RxBuffer << endl;

	}

}

void Packet::CloseSocket(SOCKET &Socket){
	closesocket(Socket);
}


void Packet::WinsockExit(){
	WSACleanup();
}

int Packet::getPacketID()
{
	return PacketID;
}
