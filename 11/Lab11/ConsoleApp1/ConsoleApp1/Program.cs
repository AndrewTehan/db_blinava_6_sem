namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Connection.getInstance();
            /*
            Actions.InitTables();
            Actions.AddInitData();
            Actions.AddTrigger();*/
            /* 
            Actions.AddTour("Tour1", "Route1", 1, 1, 1);
            Actions.AddTour("Tour2", "Route2", 2, 2, 2);
            Actions.GetTours();*/

            /*
            Actions.UpdateTour(1, "Tour1Upd", "Route1Upd", 1, 1, 1);
            Actions.UpdateTour(3, "Tour2Upd", "Route2Upd", 2, 2, 2);
            Actions.GetTours();*/
           
            Actions.DeleteTour(2);
            Actions.DeleteTour(4);
            Actions.GetTours();

            Connection.Close();
        }
    }
}
