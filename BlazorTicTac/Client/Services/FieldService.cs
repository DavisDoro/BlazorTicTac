using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTicTac.Client.Services
{
    public class FieldService : IFieldService
    {
        public IList<int> OccupiedFields { get; set; } = new List<int>();

        public void ConfirmFields(int fieldId)
        {
            OccupiedFields.Add(fieldId);

        }

        public bool CheckField(int fieldId)
        {
            throw new NotImplementedException();
        }

        public void ResetField()
        {
            throw new NotImplementedException();
        }
    }
}
