#include <windows.networking.sockets.h> 
#include <stdio.h>
#include <iostream> 
#include <thread>
#pragma comment(lib, "Ws2_32.lib")

int const MAX_SOCKETS = 5; 
SOCKET ConnectionSockets[MAX_SOCKETS], Aux_Socket;
int Active_Sockets[MAX_SOCKETS + 1] = { 0, 0, 0, 0, 0, 0 };

int find_available_socket(void) {
  for (int i = 0; i < MAX_SOCKETS; i++) {
    if (Active_Sockets[i] == 0) {
      return i;
    }
  }
  return MAX_SOCKETS;
}

void Run(int Index) {
  std::cout << "Thread Started for Index " << Index << std::endl;
  Active_Sockets[Index] = 1;
  while (1) {
    char RxBuffer[50] = { }; 
    int n = recv(ConnectionSockets[Index], RxBuffer, sizeof(RxBuffer), 0);
    std::cout << "From thread "<< Index << ": " << RxBuffer << std::endl;
    send(ConnectionSockets[Index], "Ok", sizeof("Ok"), 0);
    if (RxBuffer[0] == 'x') 
      break;
  }
  std::cout << "Closing Connection" << std::endl;
  closesocket(ConnectionSockets[Index]);
  Active_Sockets[Index] = 0;
}

void main(int argc, char *argv[]) {

  struct sockaddr_in SvrAddr; 
  SOCKET WelcomeSocket; 
  WORD wVersionRequested; 
  WSADATA wsaData;
  int Socket_Number;
  
  if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0)
    return;

  if ((WelcomeSocket = socket(AF_INET, SOCK_STREAM, 0)) == INVALID_SOCKET) 
    return;

  SvrAddr.sin_family = AF_INET; 
  SvrAddr.sin_addr.s_addr = INADDR_ANY; 
  SvrAddr.sin_port = htons(27000);

  if ((bind(WelcomeSocket, (struct sockaddr *)&SvrAddr, sizeof(SvrAddr))) == SOCKET_ERROR) { 
    closesocket(WelcomeSocket); 
    WSACleanup(); 
    return; 
  }
  if (listen(WelcomeSocket, 1) == SOCKET_ERROR) { 
    closesocket(WelcomeSocket); 
    WSACleanup();
    return; 
  }
  for (int x = 0; x < MAX_SOCKETS; x++) 
    ConnectionSockets[x] = SOCKET_ERROR;

  while (1) {
    //wait for an incoming connection from a client
    if ((Aux_Socket = accept(WelcomeSocket, NULL, NULL)) == SOCKET_ERROR) {
      return;
    }
    else {
      Socket_Number = find_available_socket();
      if (Socket_Number < MAX_SOCKETS) {
        ConnectionSockets[Socket_Number] = Aux_Socket;
        send(ConnectionSockets[Socket_Number], "Welcome", sizeof("Welcome"), 0);
        std::thread(Run, Socket_Number).detach();
      }
      else {
        send(ConnectionSockets[MAX_SOCKETS], "Full", sizeof("Full"), 0);
        std::cout << "Someone tried to connect when I was full" << std::endl;
      }
    }
  }
  WSACleanup();
}