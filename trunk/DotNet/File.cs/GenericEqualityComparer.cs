namespace System
{
    public class GenericEqualityComparer<T> : IEqualityComparer<T>
    {
        private Func<T, T, Boolean> _comparer;
        private Func<T, int> _hashCodeEvaluator;
        public GenericEqualityComparer(Func<T, T, Boolean> comparer)
        {
            _comparer = comparer;
        }

        public GenericEqualityComparer(Func<T, T, Boolean> comparer, Func<T, int> hashCodeEvaluator)
        {
            _comparer = comparer;
            _hashCodeEvaluator = hashCodeEvaluator;
        }

        #region IEqualityComparer<T> Members

        public bool Equals(T x, T y)
        {
            return _comparer(x, y);
        }

        public int GetHashCode(T obj)
        {
            if(obj == null) {
                throw new ArgumentNullException("obj");
            }
            if(_hashCodeEvaluator == null) {
                return obj.GetHashCode();
            } 
            return _hashCodeEvaluator(obj);
        }

        #endregion
    }
}
/*
调用方式
    var comparer = new GenericEqualityComparer<ShopByProduct>((x, y) => { return x.ProductId == y.ProductId; });
    var current = SelectAll().Where(p => p.ShopByGroup == group).ToList();
    var toDelete = current.Except(products, comparer);
    var toAdd = products.Except(current, comparer);
or
    var comparer = new GenericEqualityComparer<ShopByProduct>(
           (x, y) => { return x.ProductId == y.ProductId; }, 
           (x)    => { return x.Product.GetHashCode()}
    );*/