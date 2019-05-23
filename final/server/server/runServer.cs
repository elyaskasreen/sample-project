using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace server
{
    class runServer
    {
        public MainWindow mainwindow;//the main window 

        //the array of threads //array.length represent the count of client that can be connected in the same time
        private connectThread[] CT = new connectThread[5];

        //the Class constructor
        public runServer(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
        }

        //the main function in the server that will recieve connections
        public void run()
        {
            //tcp listner declaration and (IP,port) assign 
            TcpListener listner;
            int clientscount = 0;
            try
            {

                IPAddress local = IPAddress.Parse("127.0.0.1");
                listner = new TcpListener(local, 50000);
                listner.Start(); //start listening
                for (int i = 0; i < CT.Length; i++)
                {
                    CT[i] = new connectThread();//constructing the connection thread
                }
                while (true)//non finishing loop to keep the server working all the time
                {
                    if (CT[clientscount].connect == false)//a loop to search the array of threads for offline connection
                    {
                        DisplayMessage("Waiting for connection");//showing the connection steps
                        CT[clientscount] = new connectThread(listner.AcceptSocket(), this);//the server ready to recieve connection
                        Thread thread = new Thread(CT[clientscount].startconnection) //assign thread to the connection
                        { IsBackground = true };// to close all threads when the main window thread close
                        thread.Start();//start the thread
                        DisplayMessage("client is connected");//showing the connection steps
                    }
                    clientscount++;
                    if (clientscount == 5) clientscount = 0;
                    Thread.Sleep(500);//to rest the server and not let it work 100% looping
                }
            }

            catch (Exception error)
            {
                DisplayMessage(error.ToString());
            }
        }

        //display message in the main window 
        public void DisplayMessage(string message)
        {
            mainwindow.DisplayMessage(message);
        }
    }
}
