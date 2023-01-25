using System;

namespace Financial.Common
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
    }
}
