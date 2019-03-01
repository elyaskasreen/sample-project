using System;
using System.Data;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace client
{
    public class runClient
    {
        public MainWindow mainwindow;
        public NetworkStream output;
        public BinaryWriter writer;
        public BinaryReader reader;
        public messenger m;
        public TcpClient client;
        public bool connect = false;
        public bool verified = false;

        //the Class constructor
        public runClient(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
        }

        //the main function in the client that will connect with server
        public void run()
        {
            do
            {
                try
                {
                    DisplayMessage("try to connect server ");//showing the connection steps

                    do//connection loop will keep trying till connect with server
                    {
                        try
                        {
                            client = new TcpClient();
                            connect = false;
                            client.Connect("127.0.0.1", 50000);//tcp client declaration and (IP,port) assign 
                            output = client.GetStream();//start listening
                            writer = new BinaryWriter(output);
                            reader = new BinaryReader(output);
                            connect = true;//getting out of the loop
                            DisplayMessage("connected to server");//showing the connection steps
                        }
                        catch { DisplayMessage("!"); }//showing the connection steps
                    } while (!connect);//connection loop will keep trying till connect with server

                    mainwindow.Dispatcher.BeginInvoke((ThreadStart)delegate()
                    {
                        mainwindow.btn_login.IsEnabled = true;//enable login button after connecting with server
                    });

                    //declaration of the client orders object which will doing server's orders
                    clientOrders CO = new clientOrders(writer, reader, this);
                    //declaration of the messenger object which will recieve server's messages
                    m = new messenger(writer, reader);

                    try
                    {
                        while (!verified)//logging loop will keep working till user enter a correct password
                        {
                            if (mainwindow.logedin)//if the client already have user name and password (verified befor in this Session but connect get offline) 
                            {
                                m.sendpassword("1", mainwindow.UserName, mainwindow.password);//send password
                                DisplayMessage("sending password");//showing the connection steps
                            }
                            verified = m.receiveVerified();//recieve password checking result
                            DisplayMessage("password verified is " + verified.ToString());//showing the connection steps
                            mainwindow.login.matchingresult(verified);//sending password checking result to login window to let the user know
                        }

                    }
                    catch { DisplayMessage("?"); }
                    while (verified)//talking loop will keep working as long as the connection is on
                    {
                        try
                        {
                            string[] cells = m.receive();//recieving messages from server
                            CO.chooseOrder(cells);//doing server orders
                        }
                        catch
                        {
                            verified = false;//connection lost
                            DisplayMessage("connection is lost");//showing the connection steps
                            mainwindow.Dispatcher.BeginInvoke((ThreadStart)delegate()
                            { mainwindow.logout(); });//logout and try connect again
                        }
                    }
                    DisplayMessage("Disconnecting");//showing the connection steps
                }
                catch (Exception error)
                {
                    DisplayMessage(error.ToString());//showing the connection steps
                }
            } while (true);
        }

        //displaying message in the main window
        public void DisplayMessage(string message)
        {
           mainwindow.DisplayMessage(message);
        }

        //send password to server
        public void sendpassword()
        {
            try
            {
                m.sendpassword("1", mainwindow.UserName, mainwindow.password);
            }
            catch { DisplayMessage("ERROR sending password"); }
        }

        //send messages to server
        public void send(params string[] cells)
        {
            try
            {
                m.send(cells);
            }
            catch { DisplayMessage("ERROR sending data"); }
        }
    }
}
