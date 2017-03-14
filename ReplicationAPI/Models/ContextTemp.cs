using ServiceLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ServiceLibrary.Model;

namespace ReplicationAPI.Models
{
	public class ContextTemp:DbContext
	{
		public ContextTemp(string cn):base(cn)
		{
		}

		public DbSet<User> Users { get; set; }
	}
}