using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace server
{
    class connectThread
    {
        public bool connect = false; //to know the connection status
        public runServer runserver; //the main connect class
        private BinaryWriter writer; //class to send throw network
        private BinaryReader reader;//class to recieve throw network
        public messenger m; //class responsible of (sending & recieving & encrypting protocol)
        string username = ""; //client user name
        string userID = "";//client user ID

        //constructor
        public connectThread()
        {

        }

        //constructor
        public connectThread(Socket socket, runServer runserver)
        {
            this.runserver = runserver;
            NetworkStream networkstream = new NetworkStream(socket);
            writer = new BinaryWriter(networkstream);
            reader = new BinaryReader(networkstream);
            this.m = new messenger(writer, reader);
            connect = true;
        }

        //the main function in this class that will talk with client and do his orders
        public void startconnection()
        {
            try
            {
                bool verified = false; //client verifing status
                orders orders = new orders(this); //class that have the orders
                string[] cells; //array of string will be send and recieve throw network

                while (!verified)//talking loop will keep going untill the client verified being true
                {
                    cells = m.receivepassword();//reciev password
                    runserver.DisplayMessage(cells[1] + " try to signin");//show connection steps
                    verified = orders.passwordcheck(cells, m, out userID, out username);//checking password
                    m.sendverified(verified);//sending the checking result to the client
                    runserver.DisplayMessage(cells[1] + " verified is " + verified.ToString());//show the result on server window
                    if (verified)//if password was match
                    {
                        m.send("1", userID, username);//send username and userID to the client
                        orders.sendPermissions(userID);//send the permissions to the client
                    }
                }
                while (verified)//while client signing in
                {
                    cells = m.receive();//recieving client orders
                    orders.chooseOrder(cells);//doing the orders
                }

            }
            catch { runserver.DisplayMessage("Error connection is lost"); connect = false; }
        }

    }
}
