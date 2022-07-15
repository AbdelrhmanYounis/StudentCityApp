using System.Collections.Generic;
using System.Linq;
using StudentCity.DAL.Models.Permissions;
using StudentCity.DAL.Repos;

namespace StudentCity.BL.Services.Permissions
{
    public class ResetCodeService
    {
        private readonly ResetCodeRepository _repository;
        private readonly UserRepository _userRepository;

        public ResetCodeService(ResetCodeRepository repository, UserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public List<ResetCodeModel> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public ResetCodeModel GetById(int id)
        {
            return _repository.GetById(id);
        }

        public ResetCodeModel Post(ResetCodeModel model)
        {
            return _repository.Add(model);
        }

        public bool Delete(int id)
        {
            var IsExist = _repository.GetAll().SingleOrDefault(x => x.UserId==(id));
            if (IsExist != null)
                return (_repository.DeleteObj(IsExist.Id));
               

            return false;
        }

    }
}
