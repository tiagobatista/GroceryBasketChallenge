namespace Domain.Model
{
    public class ProductDTO
    {
        /// <summary>
        /// Data transfer object that represents a product that has a name which is unique, its price and its stock
        /// </summary>
        public ProductDTO()
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

        public override bool Equals(object obj)
        {
            if(obj.GetType() != typeof(ProductDTO))
            {
                return false;
            }

            var objCasted = (ProductDTO)obj;

            return this.Name.Equals(objCasted.Name, System.StringComparison.InvariantCultureIgnoreCase)
                && objCasted.Price == this.Price;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
