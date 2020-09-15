namespace FluentMdb
{
    interface IOperations
    {
        IFilter Find();
    }

    interface IFilter
    {
        IField Equals();
        IField NotEquals();
        IField GreaterThan();
        IField GreaterThanEquals();
        IField LessThan();
        IField LessThanEquals();
        IField Exists();
        IField NotExists();
    }

    interface IField
    {
        IFieldOperations Fields();
    }

    interface IFieldOperations
    {
        IResult All();
        IResult Include();
        IResult Exclude();
    }

    interface IResult
    {
        void Limit();
    }
}
