using CoffeeShop.Models;

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