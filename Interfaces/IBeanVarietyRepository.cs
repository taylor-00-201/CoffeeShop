using CoffeeShopApi.Models;

// this interface was created using the extract interface feature
namespace CoffeeShopApi.Interfaces
{
    public interface IBeanVarietyRepository
    {
        void Add(BeanVariety variety);
        void Delete(int id);
        BeanVariety Get(int id);
        List<BeanVariety> GetAll();
        void Update(BeanVariety variety);
    }
}