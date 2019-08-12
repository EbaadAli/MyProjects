#include <windows.networking.sockets.h>
#pragma comment(lib, "Ws2_32.lib")

#include<iostream>;
#include <string>;
#include "PKT_DEF.h";
using namespace std;

int main() {

	SOCKET ServerSocket, ConnectionSocket;
	char IP[128] = { "127.0.0.1" };
	int Port = 5000;

	Packet PacketObj;

	while (1)
	{
		if (PacketObj.Listen(ServerSocket, IP, Port) != 0) {

			while (1)
			{
				if (PacketObj.Accept(ServerSocket, ConnectionSocket) != 0) {


					PacketObj.SetInfo(); //set info
					PacketObj.Send(ConnectionSocket); //send packet to client
					if (PacketObj.getPacketID() == 5) //check packet id
					{
						PacketObj.ReceivePkt(ConnectionSocket); //receive packet from client
						PacketObj.PrintInfo();                  //print received packet from client
					}
					if (PacketObj.getPacketID() == 0) //check packet id 
					{
						cout << "GOODBYE ------ GOING TO SLEEP!!!" << endl;
						break; //break from while loop
					}

					PacketObj.CloseSocket(ConnectionSocket); //close client connection
				}
			}
			if (PacketObj.getPacketID() == 0)		//check if packet id is 0
			{
				PacketObj.CloseSocket(ServerSocket);  //close server connection
				PacketObj.WinsockExit();			  //exit winsock
				break;	//break from while loop and exit program
			}
		}
		else {
			cout << " Connection Error - Exiting..." << endl;
		}
	}
	return 0;

}
