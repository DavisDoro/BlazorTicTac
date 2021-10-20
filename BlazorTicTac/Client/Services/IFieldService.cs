using System;
using BlazorTicTac.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTicTac.Client.Services
{
    public interface IFieldService
    {
        IList<int> OccupiedFields { get; set; }
        void ConfirmFields(int fieldId);
        void ResetField();
        bool CheckField(int fieldId);
    }
}
