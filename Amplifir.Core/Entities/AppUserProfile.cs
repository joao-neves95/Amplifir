using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Entities
{
    public class AppUserProfile
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Bio { get; set; }

        public int FollowingCount { get; set; }

        public int FollowersCount { get; set; }

        public string Userlocation { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
