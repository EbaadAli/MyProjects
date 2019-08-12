#include <windows.networking.sockets.h> 
#include <stdio.h> 
#include <iostream>
#pragma comment(lib, "Ws2_32.lib")

using namespace std;

void main(int argc, char *argv[]) {

  struct sockaddr_in SvrAddr; 
  SOCKET ClientSocket; 
  WORD wVersionRequested; 
  WSADATA wsaData; 
  wVersionRequested = MAKEWORD(2, 3); 

  if (WSAStartup(wVersionRequested, &wsaData) != 0) 
    return;

  if ((ClientSocket = socket(AF_INET, SOCK_STREAM, 0)) == INVALID_SOCKET) 
    return;

  SvrAddr.sin_family = AF_INET; 
  SvrAddr.sin_port = htons(27000); 
  SvrAddr.sin_addr.s_addr = inet_addr("127.0.0.1");

  //Connect socket to specified server 
  if ((connect(ClientSocket, (struct sockaddr *)&SvrAddr, sizeof(SvrAddr))) 
                                == SOCKET_ERROR) { 
    closesocket(ClientSocket); 
    WSACleanup(); 
    return; 
  }

  char RxBuffer[128] = {};

  recv(ClientSocket, RxBuffer, sizeof(RxBuffer), 0);
  if (!strcmp(RxBuffer, "Full")) {
    cout << "Could not connect, server full" << endl;
    return;
  }
  else {
    while (1) {
      char TxBuffer[50] = {};
    
      cout << "Enter A String (x to terminate)" << endl;
      cin >> TxBuffer;
      send(ClientSocket, TxBuffer, strlen(TxBuffer), 0);
      recv(ClientSocket, RxBuffer, sizeof(RxBuffer), 0);
      cout << "Rx: " << RxBuffer << endl;
      if (TxBuffer[0] == 'x') 
        break;
    }
  }

  closesocket(ClientSocket); 
  WSACleanup();
}