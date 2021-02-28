using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Tests.Builders
{
    public class JudgeBuilder
    {
        private string login = "guest";

        private int id = 1234;

        private Category category = CategoryBuilder.GivenCategory().Build();

        public static JudgeBuilder GivenJudge() => new JudgeBuilder();


        public JudgeBuilder WithLogin(string login)
        {
            this.login = login;
            return this;
        }

        public JudgeBuilder WithCategory(
            Action<CategoryBuilder> categoryBuilderAction)
        {
            var categoryBuilder = new CategoryBuilder();
            categoryBuilderAction(categoryBuilder);
            category = categoryBuilder.Build();
            return this;
        }

        public Judge Build()
        {
            return new Judge(id, new Login(login),
                new Password(login),
                new Name(login, login), category);
        }

        public JudgeBuilder WithId(int v)
        {
            id = v;
            return this; ;
        }
    }
}
