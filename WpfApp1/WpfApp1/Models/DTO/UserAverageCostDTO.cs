﻿namespace WpfApp1.Models.DTO
{
    public class UserAverageCostDTO : ICopied<UserAverageCostDTO>
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public double AvgCost { get; set; }

        public object Clone()
        {
            throw new System.NotImplementedException();
        }

        public void Copy(UserAverageCostDTO t)
        {
            throw new System.NotImplementedException();
        }
    }
}
