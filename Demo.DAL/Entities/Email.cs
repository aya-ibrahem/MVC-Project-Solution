using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
	public class Email
	{
		public int Id { get; set; }
        public String Subject { get; set; }
        public String To { get; set; }
        public String Body { get; set; }

    }
}
