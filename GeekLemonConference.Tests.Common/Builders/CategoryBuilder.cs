using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Tests.Builders
{
    public class CategoryBuilder
    {
        private CategoryId categoryId =
            new CategoryId(999);

        private CategoryUniqueId categoryUniqueId =
    new CategoryUniqueId(Guid.NewGuid());

        private string name = "csharp";
        private string displayname = "C#";
        private string whatWeAreLookingFor = "BBBBB CCCCC AAAA";


        public static CategoryBuilder GivenCategory() => new CategoryBuilder();

        public CategoryBuilder WithId(int categorId)
        {
            categoryId = new CategoryId(categorId);
            return this;
        }

        public CategoryBuilder WithUniqueId(Guid guid)
        {
            categoryUniqueId = new CategoryUniqueId(guid);
            return this;
        }

        public CategoryBuilder WithName(string newname)
        {
            name = newname;
            return this;
        }

        public CategoryBuilder WithWhatWeAreLookingFor(string newWhatWeAreLookingFor)
        {
            whatWeAreLookingFor = newWhatWeAreLookingFor;
            return this;
        }

        public CategoryBuilder WithDisplayName(string newdisplayname)
        {
            displayname = newdisplayname;
            return this;
        }

        public Category Build()
        {
            var c = new Category(categoryId, name, displayname, whatWeAreLookingFor);
            c.InjectUniqueId(categoryUniqueId);
            return c;
        }



    }
}
