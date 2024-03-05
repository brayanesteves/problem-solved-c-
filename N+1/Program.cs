using Microsft.EntityFrameworkCore;
using N1.Models;

string connection = "Server=HALCONBIT\\SQLEXPRESS; Database= StoreSample; Trusted_Connection = True; Trust Server Certificate=true";
var optionBuilder = new DbContextOptionBuilder<StoreContext>();
optionBuilder.UseSqlServer(connection);

using(var db = new StoreContext(optionBuilder.Options)) {
    // Middle Nº0.
    var brands = db.Brands.ToList();
    foreach(var brand in brands) {
        Console.WriteLine(brand.Name);
        var beers = db.Beers.Where(beers => beers.BrandID == brand.BrandID).ToList();
        foreach(var beer in beers) {
            Console.WriteLine("--" + beer.Name);
        }
    }

    // Middle Nº1.
    var brandBeers = from beer in db.Beers join brand in db.Brands on beer.BrandID equals brand.BrandID orderby brand.BrandID select new { BrandName = brand.Name, BeerName = beer.Name, };
    foreach(var brandBeer in brandBeers) {
        Console.WriteLine(brandBeer.BrandName + " - " + brandBeer.BeerName);
    }

    // Middle Nº2.
    var _brands  = db.Brands.ToList();
    var brandIds = _brands.Select(brand => brand.BrandID).ToList();
    var _beers   = db.Beers.Where(bool => brandIds.Contains(bool.BrandID)).ToList();
    foreach(var brand in _brands) {
        Console.WriteLine(brand.Name);
        foreach(var beer in _beers.Where(bool => bool.BrandID == brand.BrandID).ToList()) {
            Console.WriteLine("--" + beer.Name);
        }
    }

    // Middle Nº3.
    var result = db.Database.SqlQueryRaw<string>("""
        SELECT Brands.Name as Name,
        (SELECT Beers.Name
        FROM Beers
        Where Beers.BrandID = Brands.BrandID
        FOR JSON PATH)
        as Beers
        FROM Brands
        FOR JSON PATH
    """).ToList();
    var brandBeers = JsonSerializer.Deserialize<List<BrandSingle>>(result[0]);
    foreach(var brand in brandBeers) {
        Console.WriteLine(brand.Name);
        foreach(var beer in brand.Beers) {
            Console.WriteLine("--" + beer.Name);
        }
    }

}