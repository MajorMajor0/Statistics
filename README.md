# Statistics
This project pulls multiple openly available internet data sources and APIs down into Sqlite. It has EF 6 datamodels to access the data once it is local.
All keys are in the Keys.cs file, which is git ignored
A Keys.txt file is provided, which is a template for the Keys.cs file, with instruction for getting the API keys you need.

# CDC
Right now has the Coronavirus data tracker and some methods to access it.
CDC runs on the SODA API
Some good instructions here: http://dev.socrata.com/
Sign up for account to get API key here: https://data.cdc.gov/signup
CDC data source here: https://data.cdc.gov/NCHS/Provisional-COVID-19-Deaths-by-Sex-and-Age/9bhg-hcku/data

# Census
Census API is not really used right now
Sign up for a census API key here (not necessary): https://api.census.gov/data/key_signup.html

The actual data used comes from here: https://www.safegraph.com/free-data/open-census-data
You can pull this data down in csv files and import to your sqlite database
To create a database with the right schema, run this code somewhere
  void DoIt()
  {
    Census.Context context = new();
    context.Database.EnsureCreated();
  }


# FBI
The beginnings of an effort to pull together th NIBRS survey data together. This has not gone very far.


# Data Sources
Table                           Source
US.CongressionalDistricts       Generated in code
US.ocid                         https://github.com/opencivicdata/ocd-division-ids
Census.census_bloc_cd116        https://www.census.gov/geographies/mapping-files/2019/dec/rdo/116-congressional-district-bef.html
Census.cbg_b01                  SafeGraph census data
Census.cbg_field_descriptions   SafeGraph census data
Census.cbg_fips_codes           SafeGraph census data
Census.cbg_geographic_data      SafeGraph census data
Cdc.Coronas                     CDC API