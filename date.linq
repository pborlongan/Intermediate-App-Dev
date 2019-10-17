<Query Kind="Statements">
  <Connection>
    <ID>b1fa7da7-6733-475c-b548-05cd8c5b4ba0</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

DateTime whenever;
whenever = DateTime.Today;
whenever.Dump("DateTime.Today");
whenever = DateTime.Now;
whenever.Dump("DateTime.Now");

whenever.Month.Dump("Month.Value");
whenever.Day.Dump("Day.Value");
whenever.Year.Dump("Year.Value");

DateTime tomorrow = DateTime.Today.AddDays(1);
(tomorrow > whenever).Dump("Is tomorrow greater than now?");

var lastDayOfYear = new DateTime(2019, 12, 31);