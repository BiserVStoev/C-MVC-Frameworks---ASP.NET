namespace CarDealer.Models.ViewModels
{
    using CarDealer.Models.EntityModels;
    using System;

    public class AllLogVm
    {
        public string UserName { get; set; }

        public OperationLog Operation { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string ModfiedTable { get; set; }
    }
}
