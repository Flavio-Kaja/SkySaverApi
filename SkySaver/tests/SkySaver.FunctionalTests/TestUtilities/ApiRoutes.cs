namespace SkySaver.FunctionalTests.TestUtilities;
public class ApiRoutes
{
    public const string Base = "api";
    public const string Health = Base + "/health";

    // new api route marker - do not delete

    public static class ScavengerHunts
    {
        public static string GetList => $"{Base}/scavengerHunts";
        public static string GetRecord(Guid id) => $"{Base}/scavengerHunts/{id}";
        public static string Delete(Guid id) => $"{Base}/scavengerHunts/{id}";
        public static string Put(Guid id) => $"{Base}/scavengerHunts/{id}";
        public static string Create => $"{Base}/scavengerHunts";
        public static string CreateBatch => $"{Base}/scavengerHunts/batch";
    }

    public static class UserPurchases
    {
        public static string GetList => $"{Base}/userPurchases";
        public static string GetRecord(Guid id) => $"{Base}/userPurchases/{id}";
        public static string Delete(Guid id) => $"{Base}/userPurchases/{id}";
        public static string Put(Guid id) => $"{Base}/userPurchases/{id}";
        public static string Create => $"{Base}/userPurchases";
        public static string CreateBatch => $"{Base}/userPurchases/batch";
    }

    public static class PurchasableGoods
    {
        public static string GetList => $"{Base}/purchasableGoods";
        public static string GetRecord(Guid id) => $"{Base}/purchasableGoods/{id}";
        public static string Delete(Guid id) => $"{Base}/purchasableGoods/{id}";
        public static string Put(Guid id) => $"{Base}/purchasableGoods/{id}";
        public static string Create => $"{Base}/purchasableGoods";
        public static string CreateBatch => $"{Base}/purchasableGoods/batch";
    }

    public static class Streaks
    {
        public static string GetList => $"{Base}/streaks";
        public static string GetRecord(Guid id) => $"{Base}/streaks/{id}";
        public static string Delete(Guid id) => $"{Base}/streaks/{id}";
        public static string Put(Guid id) => $"{Base}/streaks/{id}";
        public static string Create => $"{Base}/streaks";
        public static string CreateBatch => $"{Base}/streaks/batch";
    }

    public static class Flights
    {
        public static string GetList => $"{Base}/flights";
        public static string GetRecord(Guid id) => $"{Base}/flights/{id}";
        public static string Delete(Guid id) => $"{Base}/flights/{id}";
        public static string Put(Guid id) => $"{Base}/flights/{id}";
        public static string Create => $"{Base}/flights";
        public static string CreateBatch => $"{Base}/flights/batch";
    }

    public static class Users
    {
        public static string GetList => $"{Base}/users";
        public static string GetRecord(Guid id) => $"{Base}/users/{id}";
        public static string Delete(Guid id) => $"{Base}/users/{id}";
        public static string Put(Guid id) => $"{Base}/users/{id}";
        public static string Create => $"{Base}/users";
        public static string CreateBatch => $"{Base}/users/batch";
    }
}
