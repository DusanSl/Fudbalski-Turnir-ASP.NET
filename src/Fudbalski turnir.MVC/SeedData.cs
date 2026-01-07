using FudbalskiTurnir.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FudbalskiTurnir.DAL
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // 1. IDENTITY
            string[] roles = { "Admin", "User" };
            foreach (var r in roles) if (!await roleManager.RoleExistsAsync(r)) await roleManager.CreateAsync(new IdentityRole(r));

            if (await userManager.FindByEmailAsync("admin@football.com") == null)
            {
                var admin = new User { UserName = "admin@football.com", Email = "admin@football.com", EmailConfirmed = true };
                await userManager.CreateAsync(admin, "Admin123!");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
            if (await userManager.FindByEmailAsync("marko.markovic@gmail.com") == null)
            {
                var regularUser = new User
                {
                    UserName = "marko.markovic@gmail.com",
                    Email = "marko.markovic@gmail.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(regularUser, "User123!");
                await userManager.AddToRoleAsync(regularUser, "User");
            }

            if (context.Turnir.Any()) return;

            var s1 = new Sponzor { ImeSponzora = "Adidas", KontaktSponzora = "contact@adidas.com", VrednostSponzora = 5000000 };
            var s2 = new Sponzor { ImeSponzora = "Nike", KontaktSponzora = "info@nike.com", VrednostSponzora = 4500000 };
            var s3 = new Sponzor { ImeSponzora = "Emirates", KontaktSponzora = "fly@emirates.com", VrednostSponzora = 10000000 };
            context.Sponzor.AddRange(s1, s2, s3);
            await context.SaveChangesAsync();

            var ls = new List<Klub> {
                new Klub { ImeKluba = "Real Madrid", GodinaOsnivanja = 1902, RankingTima = 1, Stadion = "Bernabéu", BrojIgraca = 25, BrojOsvojenihTitula = 15 },
                new Klub { ImeKluba = "Milan", GodinaOsnivanja = 1899, RankingTima = 13, Stadion = "San Siro", BrojIgraca = 24, BrojOsvojenihTitula = 7 },
                new Klub { ImeKluba = "Bayern", GodinaOsnivanja = 1900, RankingTima = 3, Stadion = "Allianz", BrojIgraca = 26, BrojOsvojenihTitula = 6 },
                new Klub { ImeKluba = "Liverpool", GodinaOsnivanja = 1892, RankingTima = 4, Stadion = "Anfield", BrojIgraca = 25, BrojOsvojenihTitula = 6 },
                new Klub { ImeKluba = "Barcelona", GodinaOsnivanja = 1899, RankingTima = 5, Stadion = "Camp Nou", BrojIgraca = 24, BrojOsvojenihTitula = 5 },
                new Klub { ImeKluba = "Ajax", GodinaOsnivanja = 1900, RankingTima = 20, Stadion = "Johan Cruyff Arena", BrojIgraca = 23, BrojOsvojenihTitula = 4 },
                new Klub { ImeKluba = "Inter Milan", GodinaOsnivanja = 1908, RankingTima = 8, Stadion = "San Siro", BrojIgraca = 24, BrojOsvojenihTitula = 3 },
                new Klub { ImeKluba = "Man City", GodinaOsnivanja = 1880, RankingTima = 2, Stadion = "Etihad", BrojIgraca = 23, BrojOsvojenihTitula = 1 },
                new Klub { ImeKluba = "Chelsea", GodinaOsnivanja = 1905, RankingTima = 16, Stadion = "Stamford Bridge", BrojIgraca = 25, BrojOsvojenihTitula = 2 },
                new Klub { ImeKluba = "Juventus", GodinaOsnivanja = 1897, RankingTima = 9, Stadion = "Allianz Stadium", BrojIgraca = 24, BrojOsvojenihTitula = 2 },
                new Klub { ImeKluba = "Benfica", GodinaOsnivanja = 1904, RankingTima = 15, Stadion = "Da Luz", BrojIgraca = 24, BrojOsvojenihTitula = 2 },
                new Klub { ImeKluba = "Dortmund", GodinaOsnivanja = 1909, RankingTima = 10, Stadion = "Westfalen", BrojIgraca = 25, BrojOsvojenihTitula = 1 },
                new Klub { ImeKluba = "Arsenal", GodinaOsnivanja = 1886, RankingTima = 6, Stadion = "Emirates", BrojIgraca = 24, BrojOsvojenihTitula = 0 },
                new Klub { ImeKluba = "PSG", GodinaOsnivanja = 1970, RankingTima = 7, Stadion = "Park Princeva", BrojIgraca = 25, BrojOsvojenihTitula = 0 },
                new Klub { ImeKluba = "Atletico Madrid", GodinaOsnivanja = 1903, RankingTima = 11, Stadion = "Metropolitano", BrojIgraca = 25, BrojOsvojenihTitula = 0 },
                new Klub { ImeKluba = "Napoli", GodinaOsnivanja = 1926, RankingTima = 12, Stadion = "Diego Maradona", BrojIgraca = 25, BrojOsvojenihTitula = 0 }
            };

            var le = new List<Klub> {
                new Klub { ImeKluba = "Sevilla", GodinaOsnivanja = 1890, RankingTima = 14, Stadion = "Pizjuán", BrojIgraca = 25, BrojOsvojenihTitula = 7 },
                new Klub { ImeKluba = "Porto", GodinaOsnivanja = 1893, RankingTima = 15, Stadion = "Dragão", BrojIgraca = 25, BrojOsvojenihTitula = 2 },
                new Klub { ImeKluba = "AS Roma", GodinaOsnivanja = 1927, RankingTima = 12, Stadion = "Olimpico", BrojIgraca = 26, BrojOsvojenihTitula = 0 },
                new Klub { ImeKluba = "Lyon", GodinaOsnivanja = 1950, RankingTima = 22, Stadion = "Groupama", BrojIgraca = 24, BrojOsvojenihTitula = 0 }
            };

            context.Klub.AddRange(ls);
            context.Klub.AddRange(le);
            await context.SaveChangesAsync();

            var t1 = new Turnir { NazivTurnira = "Champions League", Lokacija = "EU", DatumPocetka = new DateTime(2025, 9, 15), TipTurnira = "Internacionalni", Sponzori = new List<Sponzor> { s1, s3 }, Klubovi = ls };
            var t2 = new Turnir { NazivTurnira = "Europa League", Lokacija = "EU", DatumPocetka = new DateTime(2025, 9, 20), TipTurnira = "Internacionalni", Sponzori = new List<Sponzor> { s2 }, Klubovi = le };
            context.Turnir.AddRange(t1, t2);
            await context.SaveChangesAsync();

            context.Menadzer.AddRange(
                new Menadzer { Ime = "Carlo", Prezime = "Ancelotti", Nacionalnost = "ITA", GodineIskustva = 30, DatumRodjenja = new DateTime(1959, 6, 10), KlubID = ls[0].KlubID },
                new Menadzer { Ime = "Pep", Prezime = "Guardiola", Nacionalnost = "ESP", GodineIskustva = 16, DatumRodjenja = new DateTime(1971, 1, 18), KlubID = ls[7].KlubID },
                new Menadzer { Ime = "Hansi", Prezime = "Flick", Nacionalnost = "GER", GodineIskustva = 20, DatumRodjenja = new DateTime(1965, 2, 24), KlubID = ls[4].KlubID }
            );

            context.Igrac.AddRange(
                new Igrac { Ime = "Kylian", Prezime = "Mbappé", Nacionalnost = "FRA", Pozicija = "Napadač", BrojDresa = 9, DatumRodjenja = new DateTime(1998, 12, 20), KlubID = ls[0].KlubID },
                new Igrac { Ime = "Lamine", Prezime = "Yamal", Nacionalnost = "ESP", Pozicija = "Krilo", BrojDresa = 19, DatumRodjenja = new DateTime(2007, 7, 13), KlubID = ls[4].KlubID },
                new Igrac { Ime = "Erling", Prezime = "Haaland", Nacionalnost = "NOR", Pozicija = "Napadač", BrojDresa = 9, DatumRodjenja = new DateTime(2000, 7, 21), KlubID = ls[7].KlubID }
            );

            context.Utakmica.AddRange(
                new Utakmica { TurnirID = t2.TurnirID, Datum = new DateTime(2025, 9, 20), Mesto = "Sevilja", PrviKlubNaziv = "Sevilla", DrugiKlubNaziv = "AS Roma", Kolo = "Grupna faza", Grupa = "C", PrviKlubGolovi = 1, DrugiKlubGolovi = 1 },
                new Utakmica { TurnirID = t2.TurnirID, Datum = new DateTime(2025, 9, 20), Mesto = "Porto", PrviKlubNaziv = "Porto", DrugiKlubNaziv = "Lyon", Kolo = "Grupna faza", Grupa = "C", PrviKlubGolovi = 2, DrugiKlubGolovi = 0 },
                new Utakmica { TurnirID = t2.TurnirID, Datum = new DateTime(2025, 10, 05), Mesto = "Rim", PrviKlubNaziv = "AS Roma", DrugiKlubNaziv = "Porto", Kolo = "Grupna faza", Grupa = "C", PrviKlubGolovi = 0, DrugiKlubGolovi = 0 },
                new Utakmica { TurnirID = t2.TurnirID, Datum = new DateTime(2025, 10, 05), Mesto = "Lion", PrviKlubNaziv = "Lyon", DrugiKlubNaziv = "Sevilla", Kolo = "Grupna faza", Grupa = "C", PrviKlubGolovi = 1, DrugiKlubGolovi = 2 },
                new Utakmica { TurnirID = t2.TurnirID, Datum = new DateTime(2025, 10, 20), Mesto = "Sevilja", PrviKlubNaziv = "Sevilla", DrugiKlubNaziv = "Porto", Kolo = "Grupna faza", Grupa = "C", PrviKlubGolovi = 3, DrugiKlubGolovi = 2 },
                new Utakmica { TurnirID = t2.TurnirID, Datum = new DateTime(2025, 10, 20), Mesto = "Rim", PrviKlubNaziv = "AS Roma", DrugiKlubNaziv = "Lyon", Kolo = "Grupna faza", Grupa = "C", PrviKlubGolovi = 2, DrugiKlubGolovi = 1 },

                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2025, 9, 15), Mesto = "Madrid", PrviKlubNaziv = "Real Madrid", DrugiKlubNaziv = "Inter Milan", Kolo = "Grupna faza", Grupa = "A", PrviKlubGolovi = 2, DrugiKlubGolovi = 1 },
                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2025, 9, 15), Mesto = "Lisabon", PrviKlubNaziv = "Benfica", DrugiKlubNaziv = "Napoli", Kolo = "Grupna faza", Grupa = "A", PrviKlubGolovi = 0, DrugiKlubGolovi = 0 },
                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2025, 10, 01), Mesto = "Milano", PrviKlubNaziv = "Inter Milan", DrugiKlubNaziv = "Benfica", Kolo = "Grupna faza", Grupa = "A", PrviKlubGolovi = 3, DrugiKlubGolovi = 1 },
                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2025, 10, 01), Mesto = "Napulj", PrviKlubNaziv = "Napoli", DrugiKlubNaziv = "Real Madrid", Kolo = "Grupna faza", Grupa = "A", PrviKlubGolovi = 1, DrugiKlubGolovi = 2 },

                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2025, 9, 16), Mesto = "Manchester", PrviKlubNaziv = "Man City", DrugiKlubNaziv = "Bayern", Kolo = "Grupna faza", Grupa = "B", PrviKlubGolovi = 1, DrugiKlubGolovi = 1 },
                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2025, 9, 16), Mesto = "Barcelona", PrviKlubNaziv = "Barcelona", DrugiKlubNaziv = "Dortmund", Kolo = "Grupna faza", Grupa = "B", PrviKlubGolovi = 4, DrugiKlubGolovi = 2 },
                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2025, 10, 02), Mesto = "Minhen", PrviKlubNaziv = "Bayern", DrugiKlubNaziv = "Barcelona", Kolo = "Grupna faza", Grupa = "B", PrviKlubGolovi = 2, DrugiKlubGolovi = 0 },
                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2025, 10, 02), Mesto = "Dortmund", PrviKlubNaziv = "Dortmund", DrugiKlubNaziv = "Man City", Kolo = "Grupna faza", Grupa = "B", PrviKlubGolovi = 1, DrugiKlubGolovi = 3 },

                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2026, 2, 10), Mesto = "Madrid", PrviKlubNaziv = "Real Madrid", DrugiKlubNaziv = "Napoli", Kolo = "Osmina finala", PrviKlubGolovi = 3, DrugiKlubGolovi = 1 },
                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2026, 2, 10), Mesto = "London", PrviKlubNaziv = "Arsenal", DrugiKlubNaziv = "Ajax", Kolo = "Osmina finala", PrviKlubGolovi = 2, DrugiKlubGolovi = 1, Produzeci = true },
                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2026, 2, 11), Mesto = "Manchester", PrviKlubNaziv = "Man City", DrugiKlubNaziv = "Benfica", Kolo = "Osmina finala", PrviKlubGolovi = 4, DrugiKlubGolovi = 0 },
                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2026, 2, 11), Mesto = "Munich", PrviKlubNaziv = "Bayern", DrugiKlubNaziv = "Dortmund", Kolo = "Osmina finala", PrviKlubGolovi = 2, DrugiKlubGolovi = 0 },
                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2026, 2, 12), Mesto = "Barcelona", PrviKlubNaziv = "Barcelona", DrugiKlubNaziv = "Juventus", Kolo = "Osmina finala", PrviKlubGolovi = 1, DrugiKlubGolovi = 0 },
                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2026, 2, 12), Mesto = "Liverpool", PrviKlubNaziv = "Liverpool", DrugiKlubNaziv = "Atletico Madrid", Kolo = "Osmina finala", PrviKlubGolovi = 2, DrugiKlubGolovi = 2, Produzeci = true, Penali = true, PrviKlubPenali = 4, DrugiKlubPenali = 3 },
                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2026, 2, 13), Mesto = "Paris", PrviKlubNaziv = "PSG", DrugiKlubNaziv = "Milan", Kolo = "Osmina finala", PrviKlubGolovi = 2, DrugiKlubGolovi = 1 },
                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2026, 2, 13), Mesto = "London", PrviKlubNaziv = "Chelsea", DrugiKlubNaziv = "Inter Milan", Kolo = "Osmina finala", PrviKlubGolovi = 0, DrugiKlubGolovi = 1 },

                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2026, 3, 15), Mesto = "Madrid", PrviKlubNaziv = "Real Madrid", DrugiKlubNaziv = "Arsenal", Kolo = "Četvrtfinale", PrviKlubGolovi = 3, DrugiKlubGolovi = 2 },
                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2026, 3, 15), Mesto = "Manchester", PrviKlubNaziv = "Man City", DrugiKlubNaziv = "Bayern", Kolo = "Četvrtfinale", PrviKlubGolovi = 1, DrugiKlubGolovi = 0, Produzeci = true },
                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2026, 3, 16), Mesto = "Barcelona", PrviKlubNaziv = "Barcelona", DrugiKlubNaziv = "Liverpool", Kolo = "Četvrtfinale", PrviKlubGolovi = 0, DrugiKlubGolovi = 2 },
                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2026, 3, 16), Mesto = "Paris", PrviKlubNaziv = "PSG", DrugiKlubNaziv = "Inter Milan", Kolo = "Četvrtfinale", PrviKlubGolovi = 1, DrugiKlubGolovi = 2, Produzeci = true },

                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2026, 4, 15), Mesto = "Madrid", PrviKlubNaziv = "Real Madrid", DrugiKlubNaziv = "Man City", Kolo = "Polufinale", PrviKlubGolovi = 2, DrugiKlubGolovi = 1 },
                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2026, 4, 16), Mesto = "Liverpool", PrviKlubNaziv = "Liverpool", DrugiKlubNaziv = "Inter Milan", Kolo = "Polufinale", PrviKlubGolovi = 3, DrugiKlubGolovi = 0 },

                new Utakmica { TurnirID = t1.TurnirID, Datum = new DateTime(2026, 5, 30), Mesto = "Munich", PrviKlubNaziv = "Real Madrid", DrugiKlubNaziv = "Liverpool", Kolo = "Finale", PrviKlubGolovi = 2, DrugiKlubGolovi = 1 }
            );

            await context.SaveChangesAsync();
        }
    }
}