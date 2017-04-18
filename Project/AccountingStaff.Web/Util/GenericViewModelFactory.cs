using AccountingStaff.Data.Models;
using AccountingStaff.Data.Repository;
using AccountingStaff.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccountingStaff.Web.Util
{
    public class ViewModelFactory<ViewModelT> : ViewModelFactory
        where ViewModelT : IViewModel, new()
    {
        public IEnumerable<M_Base> GetListModels(List<ViewModel> viewModels)
        {
            return viewModels.Select(viewModel => viewModel.GetModel()).ToList();
        }
        public IEnumerable<ViewModelT> GetListViewModels(IEnumerable<M_Base> models)
        {
            return models.Select(model =>
            {
                ViewModelT viewModel = new ViewModelT();
                viewModel.SetModel(model);
                return viewModel;
            }).ToList();
        }

        

        
    }
}