using System;
using System.Collections.Generic;

#nullable disable

namespace WebTokenManagmentSystem.Models
{
    public partial class ServiceMaster
    {
        public ServiceMaster()
        {
            CounterServiceRelations = new HashSet<CounterServiceRelation>();
            ServiceOptions = new HashSet<ServiceOption>();
        }

        public int Id { get; set; }
        public string ServiceName { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<CounterServiceRelation> CounterServiceRelations { get; set; }
        public virtual ICollection<ServiceOption> ServiceOptions { get; set; }
    }
}
