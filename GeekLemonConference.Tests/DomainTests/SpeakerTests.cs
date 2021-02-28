using FluentAssertions;
using GeekLemonConference.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static GeekLemonConference.Tests.Builders.SpeakerBuilder;

namespace GeekLemonConference.Tests.DomainTests
{
    public class SpeakerTests
    {
        [Fact]
        public void Speaker_Born1976_IsAt2021_45YearsOld()
        {
            var customer = GivenSpeaker()
                .BornOn(new DateTime(1976, 6, 26))
                .Build();

            var ageAt2019 = customer.AgeInYearsAt(new DateTime(2021, 1, 1));

            ageAt2019.Should().Be(45.Years());
        }

        [Fact]
        public void Speaker_Born1976_IsAt2022_46YearsOld()
        {
            var customer = GivenSpeaker()
                .BornOn(new DateTime(1976, 6, 26))
                .Build();

            var ageAt2019 = customer.AgeInYearsAt(new DateTime(2022, 1, 1));

            ageAt2019.Should().Be(46.Years());
        }

        [Fact]
        public void Speaker_Born1976_IsAt2023_47YearsOld()
        {
            var customer = GivenSpeaker()
                .BornOn(new DateTime(1976, 6, 26))
                .Build();

            var ageAt2019 = customer.AgeInYearsAt(new DateTime(2023, 1, 1));

            ageAt2019.Should().Be(47.Years());
        }

        [Fact]
        public void Speaker_CannotBeCreatedWithout_Name()
        {
            Action act = () => new Speaker
            (
                null,
                new DateTime(1974, 6, 26),
                new Address("Poland", "00-001", "Warsaw", "Lemonowa 81"),
                new SpeakerWebsites(),
                ""
                , new Contact("555-555-555", "c@gmail.com")
            );

            act
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Name cannot be null");
        }

        [Fact]
        public void Speaker_CannotBeCreatedWithout_Address()
        {
            Action act = () => new Speaker
            (
                new Name("Cezary", "W"),
                new DateTime(1974, 6, 26),
                null,
                new SpeakerWebsites(),
                 ""
                , new Contact("555-555-555", "c@gmail.com")
            );

            act
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Address cannot be null");
        }

        [Fact]
        public void Speaker_CannotBeCreatedWithout_Birthdate()
        {
            Action act = () => new Speaker
            (
                new Name("Cezary", "W"),
                default,
                new Address("Poland", "00-001", "Warsaw", "Lemonowa 81"),
                new SpeakerWebsites(),
                 ""
                , new Contact("555-555-555", "c@gmail.com")
            );

            act
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Birthdate cannot be empty");
        }

        [Fact]
        public void Speaker_CannotBeCreatedWithout_SpeakerWebsities()
        {
            Action act = () => new Speaker
            (
                new Name("Cezary", "W"),
                new DateTime(1974, 6, 26),
                new Address("Poland", "00-001", "Warsaw", "Lemonowa 81"),
                null,
                 ""
                , new Contact("555-555-555", "c@gmail.com")
            );

            act
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("SpeakerWebsites cannot be empty");
        }
    }
}
