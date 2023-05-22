namespace JsSaleService
{
    public abstract class Service
    {

        protected IFreeSql FreeSql { get; }
        public Service(IFreeSql freeSql)
        {
            FreeSql = freeSql;
        }
    }
}
