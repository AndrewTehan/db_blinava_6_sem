using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucking
{
    public enum Model
    {
       Driver = 1, City, Client, Goods, Transport
    }

    public enum ModelCrud
    {
        Read = 1, Create, Update, Delete
    }

    class ConsoleStep
    {
        public void Interaction(Db db)
        {
            string model = "", crudCommad = "";

            model = ChooseModel();
            crudCommad = CrudCommand();
            Perfom(model, crudCommad, db);

            Interaction(db);
        }

        public string ChooseModel()
        {
            Console.WriteLine($"{(int)Model.Driver} - Driver\n" +
                              $"{(int)Model.City} - City\n" +
                              $"{(int)Model.Client} - Client\n" +
                              $"{(int)Model.Goods} - Goods\n" +
                              $"{(int)Model.Transport} - Transport\n");

            return ReadCommand();
        }

        public void Perfom(string model, string crudCommand, Db db)
        {
            Model enumModel = (Model)int.Parse(model);
            ModelCrud enumCrud = (ModelCrud)int.Parse(crudCommand);

           if (enumModel == Model.Driver)
            {
                Driver obj = new Driver(db);
                DriverCrud(enumCrud, obj);
           }
            else if (enumModel == Model.City)
            {
                City obj = new City(db);
                CityCrud(enumCrud, obj);
            }
            else if (enumModel == Model.Goods)
            {
                Goods obj = new Goods(db);
                GoodsCrud(enumCrud, obj);
            }
            else if (enumModel == Model.Transport)
            {
                Transport obj = new Transport(db);
                TransportCrud(enumCrud, obj);
            }
        }

        public void DriverCrud(ModelCrud modelCrud, Driver obj)
        {
            if (modelCrud == ModelCrud.Read)
            {
                obj.GetAll();
            }
            else if (modelCrud == ModelCrud.Create)
            {
                string[] inputParams = InputParamas();

                obj.Insert(inputParams[0], inputParams[1], int.Parse(inputParams[2]), inputParams[3], int.Parse(inputParams[4]));
            }
            else if (modelCrud == ModelCrud.Update)
            {
                string[] inputParams = InputParamas();

                obj.Update(int.Parse(inputParams[0]), inputParams[1]);
            }
            else if (modelCrud == ModelCrud.Delete)
            {
                string[] inputParams = InputParamas();

                obj.Delete(int.Parse(inputParams[0]));
            }
        }

        public void GoodsCrud(ModelCrud modelCrud, Goods obj)
        {
            if (modelCrud == ModelCrud.Read)
            {
                obj.GetAll();
            }
            else if (modelCrud == ModelCrud.Create)
            {
                string[] inputParams = InputParamas();

                obj.Insert(inputParams[0], int.Parse(inputParams[1]));
            }
            else if (modelCrud == ModelCrud.Update)
            {
                string[] inputParams = InputParamas();

                obj.Update(int.Parse(inputParams[0]), inputParams[1]);
            }
            else if (modelCrud == ModelCrud.Delete)
            {
                string[] inputParams = InputParamas();

                obj.Delete(int.Parse(inputParams[0]));
            }
        }

        public void CityCrud(ModelCrud modelCrud, City obj)
        {
            if (modelCrud == ModelCrud.Read)
            {
                obj.GetAll();
            }
            else if (modelCrud == ModelCrud.Create)
            {
                string[] inputParams = InputParamas();

                obj.Insert(inputParams[0]);
            }
            else if (modelCrud == ModelCrud.Update)
            {
                string[] inputParams = InputParamas();

                obj.Update(int.Parse(inputParams[0]), inputParams[1]);
            }
            else if (modelCrud == ModelCrud.Delete)
            {
                string[] inputParams = InputParamas();

                obj.Delete(int.Parse(inputParams[0]));
            }
        }

        public void ParkCrud(ModelCrud modelCrud, Client obj)
        {
            if (modelCrud == ModelCrud.Read)
            {
                obj.GetAll();
            }
            else if (modelCrud == ModelCrud.Create)
            {
                string[] inputParams = InputParamas();

                obj.Insert(inputParams[0]);
            }
            else if (modelCrud == ModelCrud.Update)
            {
                string[] inputParams = InputParamas();

                obj.Update(int.Parse(inputParams[0]), inputParams[1]);
            }
            else if (modelCrud == ModelCrud.Delete)
            {
                string[] inputParams = InputParamas();

                obj.Delete(int.Parse(inputParams[0]));
            }
        }

        public void TransportCrud(ModelCrud modelCrud, Transport obj)
        {
            if (modelCrud == ModelCrud.Read)
            {
                obj.GetAll();
            }
            else if (modelCrud == ModelCrud.Create)
            {
                string[] inputParams = InputParamas();

                obj.Insert(inputParams[0], int.Parse(inputParams[1]), int.Parse(inputParams[2]));
            }
            else if (modelCrud == ModelCrud.Update)
            {
                string[] inputParams = InputParamas();

                obj.Update(int.Parse(inputParams[0]), int.Parse(inputParams[1]));
            }
            else if (modelCrud == ModelCrud.Delete)
            {
                string[] inputParams = InputParamas();

                obj.Delete(int.Parse(inputParams[0]));
            }
        }

        public string CrudCommand()
        {
            Console.WriteLine($"{(int)ModelCrud.Read} - Read\n" +
                              $"{(int)ModelCrud.Create} - Create\n" +
                              $"{(int)ModelCrud.Update} - Update\n" +
                              $"{(int)ModelCrud.Delete} - Delete\n");

            return ReadCommand();
        }

        private string ReadCommand()
        {
            Console.Write("Enter command: ");
            string command = Console.ReadLine();
            return command;
        }

        private string[] InputParamas()
        {
            Console.WriteLine("EnterParams through dots:\n\t");
            string inputParamsString = Console.ReadLine();

            char[] separators = { '.' };
            string[] inputParams = inputParamsString.Split(separators);

            return inputParams;
        }
    }
}
