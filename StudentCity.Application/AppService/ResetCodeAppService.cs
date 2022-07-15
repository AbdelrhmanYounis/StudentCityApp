using System.Collections.Generic;
using StudentCity.Application.Helpers;
using StudentCity.BL.Dtos;
using StudentCity.BL.Services.Permissions;
using StudentCity.DAL.Models.Permissions;

namespace StudentCity.Application.AppService
{
    public class ResetCodeAppService
    {
        private readonly ResetCodeService _service;

        public ResetCodeAppService(ResetCodeService service)
        {
            _service = service;
        }

      
        public List<ResetCodeModel> GetAll()
        {
            return _service.GetAll();
        }
        public ResetCodeModel Post(ResetCodeModel model)
        {
            // any gurd validation 
            Check.ThrowIfLessEqualZero(model.UserId, "UserId");
            Check.ThrowIfStringIsNullOrEmptyOrWhiteSpace(model.Code, "Code");
          
            return _service.Post(model);
        }
        public bool Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}
