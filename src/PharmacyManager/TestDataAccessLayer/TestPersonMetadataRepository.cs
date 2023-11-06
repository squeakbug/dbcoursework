using System;
using Xunit;
using System.Collections.Generic;

using DataAccessLayer;
using DataAccessLayer.RepositoriesImp;
using BusinessSpecification;
using BusinessSpecification.RepositoryInterfaces;
using BusinessSpecification.Entities;

namespace TestDataAccessLayer
{
    public class TestPersonMetadataRepository
    {
        [Fact]
        public void TestPersonMetadataCRUD()
        {
            var factory = new SqlRepositoryFactory
            (
                dataSource: "127.0.0.1,1433",
                initialCatalog: "pharmacy",
                user: "SA",
                passwd: "P@ssword"
            );

            SetupPersonMetadataRepository(factory);

            PersonMetadata tmpPersonMetadata = null;
            List<PersonMetadata> personMetadataSample = GetSamplePersonMetadata();
            using (var rep = factory.CreatePersonMetadataRep())
            {
                tmpPersonMetadata = rep.GetPersonMetadataById(1);
                Assert.NotNull(tmpPersonMetadata);
                Assert.Equal("PMFN1", tmpPersonMetadata.First_name);

                var personMetadatas = new List<PersonMetadata>(rep.GetPersonMetadatas());
                Assert.Equal(3, personMetadatas.Count);
                tmpPersonMetadata = rep.GetPersonMetadataById(1);
            }

            using (var rep = factory.CreatePersonMetadataRep())
            {
                tmpPersonMetadata.First_name = "PMFN4";
                rep.Update(tmpPersonMetadata);

                tmpPersonMetadata = rep.GetPersonMetadataById(1);
                Assert.Equal("PMFN4", tmpPersonMetadata.First_name);

                rep.Delete(tmpPersonMetadata.Id);
                var personMetadatas = new List<PersonMetadata>(rep.GetPersonMetadatas());
                Assert.Equal(2, personMetadatas.Count);
            }

            ReleasePersonMetadataRepository(factory);
        }

        public static void SetupPersonMetadataRepository(SqlRepositoryFactory factory)
        {
            List<PersonMetadata> personMetadataSample = GetSamplePersonMetadata();
            using (var rep = factory.CreatePersonMetadataRep())
            {
                rep.Create(personMetadataSample[0]);
                rep.Truncate();
            }

            using (var rep = factory.CreatePersonMetadataRep())
            {
                rep.Create(personMetadataSample[0]);
                rep.Create(personMetadataSample[1]);
                rep.Create(personMetadataSample[2]);
            }
        }

        public static void ReleasePersonMetadataRepository(SqlRepositoryFactory factory)
        {
            using (var rep = factory.CreatePersonMetadataRep())
                rep.Truncate();
        }

        public static List<PersonMetadata> GetSamplePersonMetadata()
        {
            return new List<PersonMetadata>
            {
                new PersonMetadata
                {
                    First_name = "PMFN1", Middle_name = "PMMN1",
                    Last_name = "PMLN1", Email = "PME1", Phone = "PMP1"
                },
                new PersonMetadata
                {
                    First_name = "PMFN2", Middle_name = "PMMN2",
                    Last_name = "PMLN2", Email = "PME2", Phone = "PMP2"
                },
                new PersonMetadata
                {
                    First_name = "PMFN3", Middle_name = "PMMN3",
                    Last_name = "PMLN3", Email = "PME3", Phone = "PMP3"
                },
            };
        }
    }
}
