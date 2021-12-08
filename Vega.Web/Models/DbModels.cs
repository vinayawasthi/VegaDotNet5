using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vega.Web.Models
{
    public class Model
    {
        public Model()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int MakeId { get; set; }

        public Make Make { get; set; }
    }

    public class Make
    {
        public Make()
        {
            Models = new Collection<Model>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; }
    }

    public class Feature
    {
        public Feature()
        {
            Vehicles = new Collection<VehicleFeature>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<VehicleFeature> Vehicles { get; set; }
    }

    public class Vehicle
    {
        public Vehicle()
        {
            Features = new Collection<VehicleFeature>();
        }

        public int Id { get; set; }
        public int ModelId { get; set; }
        public string PersonName { get; set; }
        public string PersonEmail { get; set; }
        public string PersonPhone { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime LastUpdate { get; set; }

        public Model Model { get; set; }
        public ICollection<VehicleFeature> Features { get; set; }
    }

    public class VehicleFeature
    {
        public int VehicleId { get; set; }
        public int FeatureId { get; set; }

        public Vehicle Vehicle { get; set; }
        public Feature Feature { get; set; }
    }
}