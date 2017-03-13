using MyServiceLibrary;
using System.Collections.Generic;
using System;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace MasterSlaveReplication
{
    public enum Operation { Add, Delete, Update }

    [Serializable]
    public class Message
    {
        public User User { get; set; }
        public Operation Operation { get; set; }

        public Message(User user, Operation operation)
        {
            this.User = user;
            this.Operation = operation;
        }
    }

    public class Master
    {
        private readonly UserService userService;
        private readonly int[] ports;

        public Master(int[] ports)
        {
            this.ports = ports;
            this.userService = new UserService();
        }

        public int Add(User user)
        {
            var result = this.userService.Add(user);

            if (result != 0)
            {
                SendMessage(new Message(user, Operation.Add));
            }

            return result;
        }

        public bool Update(User user)
        {
            var result = this.userService.UpdateUser(user);

            if (result)
            {
                SendMessage(new Message(user, Operation.Update));
            }

            return result;
        }

        public bool Delete(User user)
        {
            var result = this.userService.Delete(user);

            if (result)
            {
                SendMessage(new Message(user, Operation.Delete));
            }

            return result;
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

        private void SendMessage(Message message)
        {
            var bf = new BinaryFormatter();
            // Create a TcpClient.
            // Note, for this client to work you need to have a TcpServer 
            // connected to the same address as specified by the server, port
            // combination.
            for (int i = 0; i < ports.Length; i++)
            {
                using (TcpClient client = new TcpClient("127.0.0.1", ports[i]))
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
