using CoffeeShop.Models;

// this interface was created using the extract interface feature
namespace CoffeeShopApi.Interfaces
{
    public interface ICoffeeRepository
    {
        void AddCoffee(string title, int beanVarietyId);
        void Delete(int id);
        List<Coffee> GetAllCoffee();
        Coffee GetCoffeeById(int id);
        void UpdateCoffee(int id, Coffee coffee);
    }
}