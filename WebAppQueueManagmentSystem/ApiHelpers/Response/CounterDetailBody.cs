namespace WebAppQueueManagmentSystem.ApiHelpers.Response
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="CounterDetailBody" />.
    /// </summary>
    public class CounterDetailBody
    {
        /// <summary>
        /// Gets or sets the CounterNumber.
        /// </summary>
        public int CounterNumber { get; set; }

        /// <summary>
        /// Gets or sets the CounterStatus.
        /// </summary>
        public List<Status> CounterStatus { get; set; }

        /// <summary>
        /// Gets or sets the CounterService.
        /// </summary>
        public List<ServiceOption> CounterService { get; set; }

        /// <summary>
        /// Gets or sets the CounterID.
        /// </summary>
        public int CounterID { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="Status" />.
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate.
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="ServiceOption" />.
    /// </summary>
    public class ServiceOption
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ServiceMasterId.
        /// </summary>
        public int? ServiceMasterId { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate.
        /// </summary>
        public DateTime? CreatedDate { get; set; }
    }
}
