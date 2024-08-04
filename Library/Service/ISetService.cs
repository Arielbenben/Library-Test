using Library.Models;
using Library.ViewModels;

namespace Library.Service
{
	public interface ISetService
	{
		Task<List<SetModel>> GetAllSetsByShelfId(long id);
		Task<SetModel> CreateNewSet(SetVM setVM);
	}
}
