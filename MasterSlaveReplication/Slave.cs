using MyServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MasterSlaveReplication
{
    public class Slave : MarshalByRefObject
    {
        private readonly int port;
        private readonly UserService userService;
        private readonly object lockObg = new object();
        public Slave(int port)
        {
            this.port = port;
            this.userService = new UserService();
        }

	    public User GetUserById(int id)
	    {
		    return this.userService.GetUserById(id);
	    }

	    public IList<User> SearchByName(User user)
	    {
		    return this.userService.SearchByName(user);
	    }

	    public IList<User> SearchByLastName(User user)
	    {
		    return this.userService.SearchByLastName(user);
	    }

	    public IList<User> SearchByLastAndFirstName(User user)
	    {
		    return this.userService.SearchByLastAndFirstName(user);
	    }

	    public IList<User> GetUsers()
	    {
		    return this.userService.GetUsers();
	    }

        public void ListenMaster()
        {
            TcpListener server = null;
            Message message = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(localAddr, this.port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data

                var bf = new BinaryFormatter();

                // Enter the listening loop.
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();

                    using (NetworkStream stream = client.GetStream())
                    {
                        message = (Message)bf.Deserialize(stream);
                        MakeAction(message);
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {

            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }
        }

	    private void Add(User user)
	    {
		    this.userService.Add(user);
	    }

	    private void Update(User user)
	    {
		   this.userService.UpdateUser(user);
	    }

	    private void Delete(User user)
	    {
		    this.userService.Delete(user);
	    }

        private void MakeAction(Message message)
        {
            switch (message.Operation)
            {
                case Operation.Add:
                    Add(message.User);
                    break;

                case Operation.Delete:
                    Delete(message.User);
                    break;

                case Operation.Update:
                    Update(message.User);
                    break;

                default:
                    break;
            }
        }
    }
}
