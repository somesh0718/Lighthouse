using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class CountryMapper
    {
        public static CountryModel ToModel(this Country country)
        {
            if (country == null)
                return null;

            CountryModel countryModel = new CountryModel
            {
                CountryCode = country.CountryCode,
                CountryName = country.CountryName,
                ISDCode = country.ISDCode,
                ISOCode = country.ISOCode,
                CurrencyName = country.CurrencyName,
                CurrencyCode = country.CurrencyCode,
                CountryIcon = country.CountryIcon,
                Description = country.Description,
                CreatedBy = country.CreatedBy,
                CreatedOn = country.CreatedOn,
                UpdatedBy = country.UpdatedBy,
                UpdatedOn = country.UpdatedOn,
                IsActive = country.IsActive
            };

            //country.States.ForEach((state) => countryModel.StateModels.Add(state.ToModel()));

            return countryModel;
        }
        public static Country FromModel(this CountryModel countryModel, Country country)
        {
            country.CountryCode = countryModel.CountryCode;
            country.CountryName = countryModel.CountryName;
            country.ISDCode = countryModel.ISDCode;
            country.ISOCode = countryModel.ISOCode;
            country.CurrencyName = countryModel.CurrencyName;
            country.CurrencyCode = countryModel.CurrencyCode;
            country.CountryIcon = countryModel.CountryIcon;
            country.Description = countryModel.Description;
            country.IsActive = countryModel.IsActive;
            country.RequestType = countryModel.RequestType;
            country.SetAuditValues(countryModel.RequestType);

            //// Handling multiple country states
            //foreach (var stateModel in countryModel.StateModels)
            //{
            //    State state = country.States.FirstOrDefault(f => f.StateCode == stateModel.StateCode);
            //    if (state == null || countryModel.RequestType == RequestType.New)
            //    {
            //        state = new State();
            //        state.CountryCode = country.CountryCode;
            //    }
            //    state = stateModel.FromModel(state);
            //    state.SetAuditValues(countryModel.RequestType);

            //    country.States.Add(state);
            //}

            return country;
        }
    }
}
