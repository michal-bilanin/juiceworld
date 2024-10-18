using JuiceWorld.Constants;
using JuiceWorld.Entities;

namespace JuiceWorld.Data;

public static class ProductsSeedData
{
    public static List<Product> Products =
    [
        new()
        {
            Id = 1, Name = "Anadrol (Oxymetholone)", ManufacturerId = 1, Price = 4199,
            Description = "100 tablets, each 50mg", UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 2, Name = "Anastrozole", ManufacturerId = 1, Price = 2399, Description = "30 tablets, each 1mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 3, Name = "Anastrozole / Arimidex", ManufacturerId = 2, Price = 1899,
            Description = "30 tablets, each 1mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 4, Name = "Anavar (Oxandrolone)", ManufacturerId = 3, Price = 2299,
            Description = "100 tablets, each 20mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 5, Name = "Anavar (Oxandrolone)", ManufacturerId = 1, Price = 3299,
            Description = "100 tablets, each 10mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 6, Name = "Anavar (Oxandrolone)", ManufacturerId = 4, Price = 2399,
            Description = "100 tablets, each 10mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 7, Name = "Boldenone Undecylenate", ManufacturerId = 3, Price = 2099, Description = "250mg/ml - 10ml",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 8, Name = "Boldenone Undecylenate", ManufacturerId = 1, Price = 2399, Description = "250mg/ml - 10ml",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 9, Name = "Boldenone Undecylenate", ManufacturerId = 2, Price = 2099, Description = "300mg/ml - 10ml",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 10, Name = "BPC-157", ManufacturerId = 1, Price = 2599, Description = "10mg",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 11, Name = "Cabaser Cabergoline", ManufacturerId = 14, Price = 3299,
            Description = "20 tablets, each 1mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 12, Name = "Cialis Sex Med", ManufacturerId = 15, Price = 1899, Description = "30 tablets, each 20mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 13, Name = "CJC1295 DAC", ManufacturerId = 1, Price = 2899, Description = "5mg",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 14, Name = "Clenbuterol", ManufacturerId = 5, Price = 2399, Description = "100 tablets, each 40mcg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 15, Name = "Clenbuterol", ManufacturerId = 1, Price = 2199, Description = "100 tablets, each 40mcg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 16, Name = "Clomiphene Citrate", ManufacturerId = 6, Price = 1799,
            Description = "24 tablets, each 50mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 17, Name = "Clomipfene (Clomid)", ManufacturerId = 1, Price = 2199,
            Description = "60 tablets, each 50mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 18, Name = "Dianabol (Methandienone)", ManufacturerId = 5, Price = 2099,
            Description = "100 tablets, each 10mg", UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 19, Name = "Dianabol (Methandienone)", ManufacturerId = 3, Price = 2099,
            Description = "100 tablets, each 20mg", UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 20, Name = "Dianabol (Methandienone)", ManufacturerId = 1, Price = 1999,
            Description = "100 tablets, each 10mg", UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 21, Name = "DMAA Pre Workout Booster", ManufacturerId = 13, Price = 2599,
            Description = "270g (30 dávek)", UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 22, Name = "Drostanolone Enanthate", ManufacturerId = 3, Price = 2199, Description = "200mg/ml - 10ml",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 23, Name = "Drostanolone Propionate", ManufacturerId = 3, Price = 2099,
            Description = "100mg/ml - 10ml", UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 24, Name = "Exemestane", ManufacturerId = 1, Price = 2399, Description = "30 tablets, each 25mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 25, Name = "Exemestane Aromasin", ManufacturerId = 2, Price = 2099,
            Description = "30 tablets, each 25mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 26, Name = "Follistatin 334", ManufacturerId = 1, Price = 2899, Description = "1mg",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 27, Name = "GHRP-6", ManufacturerId = 1, Price = 2199, Description = "10mg",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 28, Name = "HCG 1vial", ManufacturerId = 16, Price = 1999, Description = "5000iu",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 29, Name = "HCG", ManufacturerId = 1, Price = 2199, Description = "5000iu",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 30, Name = "HGH", ManufacturerId = 17, Price = 10999, Description = "100iu",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 31, Name = "HGH Fragment 176-191", ManufacturerId = 1, Price = 2399, Description = "5mg",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 32, Name = "HGH (Somatropin)", ManufacturerId = 1, Price = 12999, Description = "100iu",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 33, Name = "IGF-1 LR3", ManufacturerId = 1, Price = 2899, Description = "1mg",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 34, Name = "Isotretinoin Roaccutane", ManufacturerId = 2, Price = 1999,
            Description = "30 tablets, each 20mg", UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 35, Name = "Mass 400 - Testo/Decamix (5:3)", ManufacturerId = 1, Price = 3299,
            Description = "400mg/ml - 10ml", UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 36, Name = "Masteron Drostanolone Propionate", ManufacturerId = 2, Price = 2099,
            Description = "100mg/ml - 10ml", UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 37, Name = "Masterone Enanthate (Drosta-E)", ManufacturerId = 1, Price = 3899,
            Description = "200mg/ml - 10ml", UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 38, Name = "MT2", ManufacturerId = 1, Price = 2399, Description = "10mg",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 39, Name = "Nandrolone Decanoate", ManufacturerId = 3, Price = 2099, Description = "250mg/ml - 10ml",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 40, Name = "Nandrolone Decanoate", ManufacturerId = 1, Price = 2599, Description = "250mg/ml - 10ml",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 41, Name = "Primobolan Enanthate", ManufacturerId = 3, Price = 2099, Description = "100mg/ml - 10ml",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 42, Name = "Primobolan Enanthate", ManufacturerId = 1, Price = 3899, Description = "100mg/ml - 10ml",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 43, Name = "Proviron", ManufacturerId = 1, Price = 2199, Description = "100 tablets, each 25mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 44, Name = "Proviron", ManufacturerId = 2, Price = 2099, Description = "100 tablets, each 25mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 45, Name = "Sildenafil", ManufacturerId = 16, Price = 2099, Description = "30 tablets, each 100mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 46, Name = "Sustanon 250", ManufacturerId = 1, Price = 2899, Description = "250mg/ml - 10ml",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 47, Name = "Tamoxifen", ManufacturerId = 1, Price = 2099, Description = "60 tablets, each 20mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 48, Name = "Tamoxifen (Nolvadex)", ManufacturerId = 2, Price = 1799,
            Description = "60 tablets, each 20mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 49, Name = "T3 Cytomel", ManufacturerId = 2, Price = 1999, Description = "100 tablets, each 25mcg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 50, Name = "Tadalafil", ManufacturerId = 1, Price = 1799, Description = "30 tablets, each 20mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 51, Name = "Tadalafil", ManufacturerId = 16, Price = 2099, Description = "30 tablets, each 20mg",
            UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 52, Name = "Testosterone Cypionate", ManufacturerId = 1, Price = 2399, Description = "250mg/ml - 10ml",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 53, Name = "Testosterone Enanthate", ManufacturerId = 1, Price = 2399, Description = "250mg/ml - 10ml",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 54, Name = "Testosterone Enanthate", ManufacturerId = 2, Price = 2599, Description = "250mg/ml - 10ml",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 55, Name = "Testosterone Mix Sustanon", ManufacturerId = 1, Price = 2899,
            Description = "250mg/ml - 10ml", UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 56, Name = "Trenbolone Acetate", ManufacturerId = 3, Price = 2399, Description = "100mg/ml - 10ml",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 57, Name = "Trenbolone Acetate", ManufacturerId = 1, Price = 2399, Description = "100mg/ml - 10ml",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 58, Name = "Trenbolone Acetate", ManufacturerId = 2, Price = 2299, Description = "100mg/ml - 10ml",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 59, Name = "Trenbolone Enanthate", ManufacturerId = 1, Price = 2599, Description = "200mg/ml - 10ml",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 60, Name = "Trenbolone Mix", ManufacturerId = 1, Price = 2599, Description = "200mg/ml - 10ml",
            UsageType = ProductUsageType.Injectable
        },
        new()
        {
            Id = 61, Name = "Turanabol (Chlorodehydromethyltestosterone)", ManufacturerId = 1, Price = 1999,
            Description = "100 tablets, each 10mg", UsageType = ProductUsageType.Oral
        },
        new()
        {
            Id = 62, Name = "Turanabol (Chlorodehydromethyltestosterone)", ManufacturerId = 3, Price = 1999,
            Description = "100 tablets, each 10mg", UsageType = ProductUsageType.Oral
        },
    ];
}
