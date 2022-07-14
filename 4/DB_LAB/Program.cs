using Ado_net;
using Ado_net.Models;

var uwu = new UnitOfWork();

//var numer = Console.ReadLine();
//await uwu.workRepo.GetWorkersPoPrikolu(int.Parse(numer));

//var workers = await uwu.workRepo.GetAll();


await uwu.workRepo.UpdatePolygons("Dvorets sporta", "27.548877596855164 53.910253945071, 10 10, 20 50, 30 40, 27.548877596855164 53.910253945071");
await uwu.workRepo.GetIntersect("Dvorets sporta", "Misnk arena");

//Print(workers);

//Insert();

//workers = await uwu.workRepo.GetAll();

//Print(workers);



//Update();

//workers = await uwu.workRepo.GetAll();

//Print(workers);


//Console.WriteLine("\n--------------------------------\n");

//Console.Write("Id to Get: ");
//var getId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

//var getWorker = await uwu.workRepo.Get(getId);

//Console.WriteLine($"{getWorker.FirstName}: {getWorker.LastName}");

//Console.WriteLine("\n--------------------------------\n");

//Console.Write("Id to Delete: ");
//var deleteId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

//await uwu.workRepo.Delete(deleteId);

//workers = await uwu.workRepo.GetAll();

//Print(workers);

void Print(IEnumerable<Worker>? poker)
{
    foreach (var worker in poker!)
    {
        Console.WriteLine($"{worker.WorkerId}\t|\t{worker.FirstName}\t|\t{worker.LastName}\t\t|\t{worker.Pos}\t|\t{worker.IsFired}\t|\t{worker.HireDate}");
    }
}

async void Insert()
{

    var user = new Worker();
    Console.Write("name: ");
    user.FirstName = Console.ReadLine();

    Console.Write("lastname: ");
    user.LastName = Console.ReadLine();


    user.HireDate = Convert.ToDateTime(Console.ReadLine());
    Console.Write($"hire date: {user.HireDate}\n");

    Console.Write("Phonk: ");
    user.Phonk = Console.ReadLine();

    Console.Write("Pos: ");
    user.Pos = Int32.Parse(Console.ReadLine());



    await uwu.workRepo.Insert(user);

}

async void Update()
{

    Console.Write("Id to Update: ");
    var updateId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

    var updateUser = new Worker();
    Console.Write("name: ");
    updateUser.FirstName = Console.ReadLine();

    Console.Write("lastname: ");
    updateUser.LastName = Console.ReadLine();


    updateUser.HireDate = Convert.ToDateTime(Console.ReadLine());
    Console.Write($"hire date: {updateUser.HireDate}\n");

    Console.Write("Phonk: ");
    updateUser.Phonk = Console.ReadLine();

    Console.Write("Pos: ");
    updateUser.Pos = Int32.Parse(Console.ReadLine());



    await uwu.workRepo.Update(updateId, updateUser);
}

async void GetIntersect()
{
    Console.Write("name1: ");
    var inter1 = Console.ReadLine();
    Console.Write("name2: ");
    var inter2 = Console.ReadLine();

    await uwu.workRepo.GetIntersect(inter1, inter2);
}

async void UpdatePolygon()
{

    Console.Write("name: ");
    var name = Console.ReadLine();

    var newPoligons = Console.ReadLine();
    await uwu.workRepo.UpdatePolygons(name, newPoligons);
}