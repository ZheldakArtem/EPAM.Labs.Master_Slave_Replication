using ServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using ServiceLibrary.Model;
using ServiceLibrary.Services;

namespace MasterSlaveReplication
{
    public class Slave : MarshalByRefObject
    {
        private readonly int _port;
        private readonly UserService _userService;
        private readonly object _lockObg = new object();
        public Slave(int port)
        {
            this._port = port;
			this._userService = new UserService(port.ToString());
        }

	    public User GetUserById(int id)
	    {
		    return this._userService.GetUserById(id);
	    }

	    public IList<User> SearchByName(User user)
	    {
		    return this._userService.SearchByName(user);
	    }

	    public IList<User> SearchByLastName(User user)
	    {
		    return this._userService.SearchByLastName(user);
	    }

	    public IList<User> SearchByLastAndFirstName(User user)
	    {
		    return this._userService.SearchByLastAndFirstName(user);
	    }

	    public IList<User> GetUsers()
	    {
		    return this._userService.GetUsers();
	    }

        public void ListenMaster()
        {
            TcpListener server = null;
	        try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(localAddr, this._port);

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
	                    var message = (Message)bf.Deserialize(stream);
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
	            if (server != null) server.Stop();
            }
        }

	    private void Add(User user)
	    {
		    this._userService.Add(user);
	    }

	    private void Update(User user)
	    {
		   this._userService.UpdateUser(user);
	    }

	    private void Delete(User user)
	    {
		    this._userService.Delete(user);
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
            }
        }
    }
}
