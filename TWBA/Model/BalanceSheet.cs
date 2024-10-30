using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWeakestBankOfAntarctica.Model
{
    public class BalanceSheet
    {
        public List<BalanceRecord> balanceRecords = new List<BalanceRecord>();
        public double Total { get; set; }

        public void AddRecord(BalanceRecord record)
        {
            balanceRecords.Add(record);
            Total = Total + record.Balance;
        }
    }
}
