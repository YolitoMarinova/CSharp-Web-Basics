namespace Andreys.Models
{
    using System;
    using System.Collections.Generic;

    using SIS.MvcFramework;

    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public IEnumerable<Product> Products { get; set; }
    }
}
