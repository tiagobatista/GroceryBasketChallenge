namespace Domain.Model
{
    public class Product
    {
        /// <summary>
        /// Represents a product that has a name that is unique, its price and its stock
        /// </summary>
        public Product()
        {
        }

        /// <summary>
        /// Product's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product's price
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Product's stock
        /// </summary>
        public int Stock { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Product))
            {
                return false;
            }

            var objCasted = (Product)obj;

            return this.Name.Equals(objCasted.Name, System.StringComparison.InvariantCultureIgnoreCase)
                && objCasted.Price == this.Price
                && objCasted.Stock == this.Stock;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
