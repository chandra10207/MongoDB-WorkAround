using System;
namespace MongoCRUDCSharpConsole
{
    /*
		 * Single field index
		 * compound 
		 * 
		 */
    public class MongoIndex
	{

        /*
		 * Sample_analytics:
		 * db.customers.createIndex({birthdate:1})
		 * db.customers.createIndex({email:1},{unique:true})
		 */
        public void CreateIndex()
		{

		}

		/*
		 * db.customers.explain().find({acounts: 627788})
		 * 
		 
		 */
		public void MultiKeyIndex()
		{

        }

        /*
		 * Follow order: Equality, Sort, Range
		 * 
		 * 
		 
		 */
        public void CompoundIndex()
        {

        }

        /*
		 * Index have write affect
		 * hideIndex instead of delete
		 * db.collection.GetIndexes()
		 * hideIndex, dropIndex by Key
		 * dropIndexes()
		 * 
		 * 
		 * 
		 
		 */
        public void DeleteIndex()
        {

        }




        public MongoIndex()
		{
		}
	}
}

