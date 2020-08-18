namespace DBLab4
{
    public interface ShopService
    {
        void CreateShop(string shop_name, int ID);
        void CreateProduct(string name, int id, int price, int amount);
        void makeProduct(string name);

        void DisplayShop();

        Shop FindCheapestProduct(string name);

        void buyGoodsForSum(int sum, string shop_name);

        void getCertainPrice();

        Shop CheapestBatch();

        void countBatch(string name, int amount);


    }
}