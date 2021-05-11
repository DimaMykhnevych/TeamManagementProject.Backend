using System;

namespace TeamManagement.DataLayer.Domain.Interdaces
{
    public interface IIdentificated
    {
        public Guid Id { get; set; }
    }
}
