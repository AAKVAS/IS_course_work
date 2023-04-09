using Microsoft.EntityFrameworkCore;
using System;


namespace WpfApp1.Models.DTO
{
    [PrimaryKey(nameof(OrderId), nameof(StatusChangedAt), nameof(WorkerId))]
    public class WorkersInOrdersDTO : ICopied<WorkersInOrdersDTO>
    {
        public int OrderId { get; set; }
        public DateTime StatusChangedAt { get; set; }
        public int WorkerId { get; set; }
        public string WorkerLastname { get; set; } = null!;
        public string WorkerFirstname { get; set; } = null!;
        public string WorkerPatronymic { get; set; } = null!;
        public string WorkerPost { get; set; } = null!;

        public WorkersInOrdersDTO Clone()
        {
            WorkersInOrdersDTO workersInOrdersDTO = new();
            workersInOrdersDTO.OrderId = OrderId;
            workersInOrdersDTO.StatusChangedAt = StatusChangedAt;
            workersInOrdersDTO.WorkerId = WorkerId;
            return workersInOrdersDTO;
        }

        public void Copy(WorkersInOrdersDTO workersInOrdersDTO)
        {
            OrderId = workersInOrdersDTO.OrderId;
            StatusChangedAt = workersInOrdersDTO.StatusChangedAt;
            WorkerId = workersInOrdersDTO.WorkerId;
        }
    }
}
