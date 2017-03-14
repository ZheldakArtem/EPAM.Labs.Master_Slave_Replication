using ServiceLibrary;
using System.Collections.Generic;
using System;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ServiceLibrary.Model;
using ServiceLibrary.Services;

namespace MasterSlaveReplication
{
    public enum Operation { Add, Delete, Update }

    [Serializable]
    public class Message
    {
        public User User { get; private set; }
        public Operation Operation { get; private set; }

        public Message(User user, Operation operation)
        {
            this.User = user;
            this.Operation = operation;
        }
    }

    public class Master
    {
        private readonly UserService _userService;
        private readonly int[] _ports;

        public Master(int[] ports)
        {
            this._ports = ports;
            this._userService = new UserService();
        }

        public int Add(User user)
        {
            var result = this._userService.Add(user);

            if (result != 0)
            {
                SendMessage(new Message(user, Operation.Add));
            }

            return result;
        }

        public bool Update(User user)
        {
            var result = this._userService.UpdateUser(user);

            if (result)
            {
                SendMessage(new Message(user, Operation.Update));
            }

            return result;
        }

        public bool Delete(User user)
        {
            var result = this._userService.Delete(user);

            if (result)
            {
                SendMessage(new Message(user, Operation.Delete));
            }

            return result;
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

        private void SendMessage(Message message)
        {
	        var bf = new BinaryFormatter();
            // Create a TcpClient.
            // Note, for this client to work you need to have a TcpServer 
            // connected to the same address as specified by the server, port
            // combination.
	        foreach (int p in _ports)
	        {
		        using (TcpClient client = new TcpClient("127.0.0.1", p))
		        {
			        using (NetworkStream stream = client.GetStream())
			        {
				        bf.Serialize(stream, message);
			        }
		        }
	        }
        }
    }
}
