using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.Web.ApiResources
{
    public class ModelResource
    {
        public ModelResource()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MakeResource
    {
        public MakeResource()
        {
            Models = new Collection<ModelResource>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ModelResource> Models { get; set; }
    }

    public class FeatureResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SaveVehicleResource
    {
        public SaveVehicleResource()
        {
            Features = new Collection<int>();
            Person = new Person();
        }

        public int Id { get; set; }
        public int ModelId { get; set; }
        public Person Person { get; set; }
        public bool IsRegistered { get; set; }
        public ICollection<int> Features { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class VehicleResource
    {
        public VehicleResource()
        {
            Features = new Collection<FeatureResource>();
            Person = new Person();
        }

        public int Id { get; set; }
        public Person Person { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime LastUpdate { get; set; }


        public ModelResource Model { get; set; }
        public MakeResource Make { get; set; }
        public ICollection<FeatureResource> Features { get; set; }
    }
}
