using AccountingStaff.Data.Models;

namespace AccountingStaff.Web.ViewModels
{
    public interface IViewModel
    {
        int Id { get; set; }

        M_Base GetModel();
        void SetModel(M_Base model);
    }
}