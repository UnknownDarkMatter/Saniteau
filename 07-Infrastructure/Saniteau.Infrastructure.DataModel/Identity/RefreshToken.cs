using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saniteau.Infrastructure.DataModel.Identity
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public bool IsActive => (Revoked == null) & !IsExpired;
        public bool IsExpired => DateTime.Now > Expires;
        public DateTime Expires { get; set; } = DateTime.Now.AddDays(7);
        public DateTime? Revoked { get; set; }
        public string Token { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public int UserId { get; set; }
    }
}
