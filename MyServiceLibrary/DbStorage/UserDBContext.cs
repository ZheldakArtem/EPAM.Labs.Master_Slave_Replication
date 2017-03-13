using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.DbStorage
{
	public class UserDbContext:DbContext
	{
		public UserDbContext(string nameConnectedString)
			: base(nameConnectedString)
		{
				
		}

		public DbSet<User> Users { get; set; }
	}
}
