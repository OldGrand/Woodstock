using System;

namespace Woodstock.DAL.Entities
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public DateTime CompletionDateTime { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
