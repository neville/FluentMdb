# csharp-mongo-wrapper

MongoWrapper wrapper = new MongoWrapper();

wrapper.Connect();

wrapper.Find().Equals(<key, value>).Include(<key, value>).Exclude(<key, value>);
wrapper.Find().NotEquals(<key, value>);
wrapper.Find().GreaterThan(<key, value>);
wrapper.Find().GreaterThanEquals(<key, value>);
wrapper.Find().LessThan(<key, value>);
wrapper.Find().LessThanEquals(<key, value>);
wrapper.Find().Exists(<key, value>);
wrapper.Find().NotExists(<key, value>);

wrapper.Insert(List<KeyValuePair<string, value>>);

wrapper.Update().SetMultiple();
